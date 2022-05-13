using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAi : MonoBehaviour
{

NavMeshAgent nm;
public Transform target;

void Awake()
{
    nm = GetComponent<NavMeshAgent>();

}

void Update()
{

}


IEnumerator Think()
{
    while(true)
    {
        nm.SetDestination(target.position);

        yield return new WaitForSeconds(1f);

    }
}
//Making basic zombie AI in Unity3d- Part 2

















}
