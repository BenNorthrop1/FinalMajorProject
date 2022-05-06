using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningItem : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        float z = Mathf.PingPong(t: Time.deltaTime, length:1f);
        Vector3 axis = new Vector3(x:1, y:1, z);
        transform.Rotate(axis, angle:1f);




    }
}
