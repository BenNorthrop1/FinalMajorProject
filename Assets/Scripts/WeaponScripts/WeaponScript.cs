using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [Header("Info")]
    public string WeaponName;

    [Header("WeaponShootingValues")]
    public int damage;

    public float timeBetweenShooting;

    public float spread;

    public float range;

    public float impactForce = 30f;

    public bool buttonHold;

    [Header("WeaponReloadingValues")]
    public float reloadTime;

    int bulletsLeft;

    public float bulletsShot;

    public float timeBetweenShots;

    public int magazineSize;

    public float bulletsPerTap;

    [Header("WeaponAppearences")]
    public ParticleSystem muzzleflash;

    public GameObject impactEffect;

    [Header("WeaponReferences")]
    public TextMeshProUGUI AmmoUi;

    public LayerMask

            Enemy,
            Player;

    [Header("Hipfire Recoil")]
    public float recoilX;

    public float recoilY;

    public float recoilZ;

    [Header("Settings")]
    public float snappiness;

    public float returnSpeed;

    bool

            shooting,
            readyToShoot,
            reloading;

    private WeaponPickUp weaponPickUp;

    private Recoil recoilScript;

    //public Camera WeaponCam;
    public RaycastHit rayHit;

    private Camera mainCam;

    private void Awake()
    {
        mainCam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        bulletsLeft = magazineSize;
        readyToShoot = true;
        reloading = false;
    }

    void Start()
    {
        weaponPickUp = GetComponent<WeaponPickUp>();
    }

    private void Update()
    {
        if (UiManager.isPaused == false)
        {
            M_Input();
        }

        //You might need to set this to WeaponPickUp.primaryWeapon
        if (weaponPickUp.equipped == true)
        {
            AmmoUi.SetText(bulletsLeft + "/");
        }
        else
        {
            AmmoUi.SetText(" ");
        }
    }

    private void M_Input()
    {
        //Wrapped this in a second check to stop the gun doing anything when it isn't your primary weapon.
        //Simple bool check asking the weapon pick up if it is the primary weapon first
        if (weaponPickUp.primaryWeapon)
        {
            if (buttonHold)
                shooting = Input.GetKey(KeyCode.Mouse0);
            else
                shooting = Input.GetKeyDown(KeyCode.Mouse0);

            if (
                Input.GetKeyDown(KeyCode.R) &&
                bulletsLeft < magazineSize &&
                !reloading
            ) Reload();
            if (
                Input.GetKeyDown(KeyCode.Mouse0) &&
                bulletsLeft == 0 &&
                !reloading
            ) Reload();

            if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
            {
                bulletsShot = bulletsPerTap;
                Shoot();
            }
        }
    }

    internal void SetActive(bool v)
    {
        throw new System.NotImplementedException();
    }

    private void Shoot()
    {
        muzzleflash.Play();

        recoilScript = GetComponentInParent<Recoil>();
        recoilScript.RecoilFire();

        readyToShoot = false;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 direction = mainCam.transform.forward + new Vector3(x, y, 0);

        if (
            Physics
                .Raycast(mainCam.transform.position,
                direction,
                out rayHit,
                range,
                ~Player)
        )
        {
            //This makes it so if the object has the Targert Script it takes damage
            EnemyAi enemy = rayHit.transform.GetComponent<EnemyAi>();
            if (enemy != null)
            {
                enemy.TakeDamage (damage);
            }

            //This makes it so if the object has the crate script it breaks it
            BreakableObject b_Object =
                rayHit.transform.GetComponent<BreakableObject>();
            if (b_Object != null)
            {
                b_Object.Break();
            }

            CoinScript coin = rayHit.transform.GetComponent<CoinScript>();
            if (coin != null)
            {
                coin.Collect();
            }

            if (rayHit.rigidbody != null)
            {
                rayHit.rigidbody.AddForce(-rayHit.normal * impactForce);
            }

            GameObject impactGO =
                Instantiate(impactEffect,
                rayHit.point,
                Quaternion.LookRotation(rayHit.normal));
            impactGO.transform.parent = rayHit.transform;
            Destroy(impactGO, 2f);
        }

        bulletsLeft--;
        bulletsShot--;

        if (!IsInvoking("ResetShot") && !readyToShoot)
        {
            Invoke("ResetShot", timeBetweenShooting);
        }

        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }

    private void SetFieldOfView(float fov)
    {
        mainCam.fieldOfView = fov;
        //WeaponCam.fieldOfView = fov;
    }
}