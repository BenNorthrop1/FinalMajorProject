using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainDeath : MonoBehaviour
{

    public int damage;
    public Stats stats;

    private void OnTriggerStay(Collider other)
    {
        
        stats.TakeDamage(damage--);



    }
}
