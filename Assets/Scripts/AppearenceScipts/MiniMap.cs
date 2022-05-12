using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public Transform Player;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.position + offset;

        Vector3 rot = new Vector3(90, Player.eulerAngles.y, 0);

        transform.rotation = Quaternion.Euler(rot);
    }
}
