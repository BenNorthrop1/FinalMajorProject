using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponScript : MonoBehaviour
{
    public string WeaponName;

    public CamShake cShake;

    public bool Aim;

    public int damage;
    public float 
    timeBetweenShooting,
    spread,
    range,
    reloadTime,
    timeBetweenShots,
    camShakeMagnitude,
    camShakeDuration,
    adsSpeed;
    public float impactForce = 30f;
    public int magazineSize, bulletsPerTap;
    public bool buttonHold;

    int bulletsLeft , bulletsShot;
    
    bool shooting , readyToShoot, reloading;

    public Camera MainCam;
    //public Camera WeaponCam;
    public RaycastHit rayHit;
    public LayerMask Enemy,Player;

    private Vector3 originalPos;
    public Vector3 aimPos;

    public ParticleSystem muzzleflash;
    public GameObject impactEffect;
    public TextMeshProUGUI AmmoUi;


    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
        originalPos = transform.localPosition;
    }

    private void Update()
    {
        M_Input();
        AimDownSight();

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
    }

    private void AimDownSight()
    {
        if(Input.GetButton("Fire2") && !reloading || Aim)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, aimPos, Time.deltaTime * adsSpeed);
            
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPos, Time.deltaTime * adsSpeed);
        }
    }


    private void Shoot()
    {
        muzzleflash.Play();
        readyToShoot = false;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);
        
        Vector3 direction = MainCam.transform.forward + new Vector3(x, y , 0);

        if(Physics.Raycast(MainCam.transform.position, direction,  out rayHit, range, ~Player))
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

        cShake.Shake(camShakeDuration,camShakeMagnitude);

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
        MainCam.fieldOfView = fov;
        //WeaponCam.fieldOfView = fov;
    }


}
