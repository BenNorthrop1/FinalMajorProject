using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponScript : MonoBehaviour
{
    public string WeaponName;

    public int damage;
    public float 
    timeBetweenShooting,
    spread,
    range,
    reloadTime,
    timeBetweenShots;
    public float impactForce = 30f;
    public int magazineSize, bulletsPerTap;
    public bool buttonHold;
    int bulletsLeft , bulletsShot;
    
    bool shooting , readyToShoot, reloading;

    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask Enemy;

    public ParticleSystem muzzleflash;
    public GameObject impactEffect;
    public TextMeshProUGUI AmmoUi;


    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;

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
    }


    private void Shoot()
    {
        muzzleflash.Play();
        readyToShoot = false;

        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out rayHit, range))
        {
            Debug.Log(rayHit.collider.name);

            Target target = rayHit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            BreakableObject b_Object = rayHit.transform.GetComponent<BreakableObject>();
            if(b_Object != null)
            {
                b_Object.Break();
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
        Invoke("ResetShot", timeBetweenShooting);

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




}
