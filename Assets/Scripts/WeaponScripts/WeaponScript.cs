using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public LayerMask Enemy,Player;

    [Header("Hipfire Recoil")]
    public float recoilX;
    public float recoilY;
    public float recoilZ;

    [Header("ADS Recoil")]
    public float aimRecoilX;
    public float aimRecoilY;
    public float aimRecoilZ;

    [Header("Settings")]
    public float snappiness;
    public float returnSpeed;


    bool shooting , readyToShoot, reloading ;
    
    public bool aiming;
    



    private Recoil recoilScript;

    //public Camera WeaponCam;
    public RaycastHit rayHit;


    private Camera mainCam;


    private void Awake()
    {   
        mainCam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        recoilScript = GetComponentInParent<Recoil>();
        bulletsLeft = magazineSize;
        readyToShoot = true;
        reloading = false;
    }

    private void Update()
    {
        M_Input();


        AmmoUi.SetText(bulletsLeft + " / " + magazineSize);
    }

    private void M_Input()
    {
        if(buttonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if( Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();
        if( Input.GetKeyDown(KeyCode.Mouse0) && bulletsLeft == 0 && !reloading) Reload();

        if(readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
        }

        if(Input.GetKeyDown(KeyCode.Mouse1) && !reloading)
        {
            Aim();
            aiming = true;
        }
        else
        {
            aiming = false;
        }


    }


    private void Aim()
    {
        //put aiming here
        Debug.Log("Aiming");
    }





    private void Shoot()
    {
        muzzleflash.Play();
        recoilScript.RecoilFire();
        readyToShoot = false;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);
        
        Vector3 direction = mainCam.transform.forward + new Vector3(x, y , 0);

        if(Physics.Raycast(mainCam.transform.position, direction,  out rayHit, range, ~Player))
        {

            //This makes it so if the object has the Targert Script it takes damage
            Target target = rayHit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            //This makes it so if the object has the crate script it breaks it 
            BreakableObject b_Object = rayHit.transform.GetComponent<BreakableObject>();
            if(b_Object != null)
            {
                b_Object.Break();
            }

            CoinScript coin = rayHit.transform.GetComponent<CoinScript>();
            if(coin != null)
            {
                coin.Collect();
            }

            if (rayHit.rigidbody != null)
            {
            rayHit.rigidbody.AddForce(-rayHit.normal * impactForce);
            }

            GameObject impactGO = Instantiate(impactEffect, rayHit.point, Quaternion.LookRotation(rayHit.normal));
            impactGO.transform.parent = rayHit.transform;
            Destroy(impactGO, 2f);
        }



        bulletsLeft--;
        bulletsShot--;

        if(!IsInvoking("ResetShot") && !readyToShoot)
        {
            Invoke("ResetShot", timeBetweenShooting);
        }

        if(bulletsShot > 0 && bulletsLeft > 0)
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
