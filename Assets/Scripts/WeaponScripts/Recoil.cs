using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{   

    private WeaponScript weaponScript;

    private bool isAiming;
    
    private Vector3 currentRotation;
    private Vector3 targetRotation;



    void Start()
    {
        weaponScript = GetComponentInChildren<WeaponScript>();
    }


    void Update()
    {
        isAiming = weaponScript.aiming;

        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, weaponScript.returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, weaponScript.snappiness * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(currentRotation);
    }

    public void RecoilFire()
    {
        if(isAiming)
        {
            targetRotation += new Vector3(weaponScript.aimRecoilX, Random.Range(-weaponScript.aimRecoilY, weaponScript.aimRecoilY), Random.Range(-weaponScript.aimRecoilZ , weaponScript.aimRecoilZ));
        }
        else
        {
            targetRotation += new Vector3(weaponScript.recoilX, Random.Range(-weaponScript.recoilY, weaponScript.recoilY), Random.Range(-weaponScript.recoilZ , weaponScript.recoilZ));
        }
    }
}
