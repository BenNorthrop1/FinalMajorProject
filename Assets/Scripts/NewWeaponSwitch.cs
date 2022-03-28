using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewWeaponSwitch : MonoBehaviour
{
    public GameObject weapon;

    public GameObject handPos;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        weapon.transform.position = handPos.transform.position;
    }
}
