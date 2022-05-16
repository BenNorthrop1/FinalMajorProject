using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAi : MonoBehaviour
{

NavMeshAgent nm;

private Transform target;

public float maxHealth;
private float currentHealth;

public float distanceThreshold = 10f;
public float attackThreshold = 1.5f;

public enum AiState{ idle, chasing , attack, die};

public AiState aiState = AiState.idle;

private Animator animator;



void Start()
{
    target = GameObject.FindGameObjectWithTag("Player").transform;
    animator = GetComponent<Animator>();
    nm = GetComponent<NavMeshAgent>();
    StartCoroutine(Think());
    currentHealth = maxHealth;
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

            if(currentHealth ==  0)
            {
                animator.SetBool("Death", true);
                aiState = AiState.die;
            }

            break;

            case AiState.chasing:

                dist = Vector3.Distance(target.position, transform.position);

                if(dist > distanceThreshold)
                {
                    aiState = AiState.idle;
                    animator.SetBool("Chasing", false);
                }
                if(dist < attackThreshold)
                {
                    aiState = AiState.attack;
                    animator.SetBool("Attacking", true);
                }

                nm.SetDestination(target.position);

                if(currentHealth ==  0)
                {
                    animator.SetBool("Death", true);
                    aiState = AiState.die;
                }
            
                break;

            case AiState.attack:

                dist = Vector3.Distance(target.position, transform.position);

                nm.SetDestination(transform.position);

                if(dist > attackThreshold)
                {
                    aiState = AiState.chasing;
                    animator.SetBool("Attacking", false);
                }
                if(currentHealth ==  0)
                {
                    animator.SetBool("Death", true);
                    aiState = AiState.die;
                }

                break;

                case AiState.die:

                if(currentHealth ==  0)
                { 
                    Die();
                }





                break;



            default:
                break;
        }
        yield return new WaitForSeconds(0.2f);

    }
}

    public void TakeDamage (float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die ()
    {
        Destroy(gameObject, 10f);
    }














}
