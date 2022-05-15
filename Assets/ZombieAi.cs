using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAi : MonoBehaviour
{

NavMeshAgent nm;

private Transform target;

public float distanceThreshold = 10f;

public enum AiState{ idle, chasing};

public AiState aiState = AiState.idle;

private Animator animator;



void Start()
{
    target = GameObject.FindGameObjectWithTag("Player").transform;
    animator = GetComponent<Animator>();
    nm = GetComponent<NavMeshAgent>();
    StartCoroutine(Think());
}

void Update()
{

}


IEnumerator Think()
{
    while(true)
    {
        switch (aiState)
        {
            case AiState.idle:
            
            float dist = Vector3.Distance(target.position, transform.position);

            if(dist < distanceThreshold)
            {
                aiState = AiState.chasing;
                animator.SetBool("Chasing", true);
            }

            nm.SetDestination(transform.position);

                break;
            case AiState.chasing:

                dist = Vector3.Distance(target.position, transform.position);

                if(dist > distanceThreshold)
                {
                    aiState = AiState.idle;
                    animator.SetBool("Chasing", false);
                }

                nm.SetDestination(target.position);

                break;
            default:
                break;
        }
        yield return new WaitForSeconds(0.2f);

    }
}
//Making basic zombie AI in Unity3d- Part 2

















}
