using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTurbine : MonoBehaviour
{
    void FixedUpdate() => transform.Rotate(new Vector3(1,0,0));
}
