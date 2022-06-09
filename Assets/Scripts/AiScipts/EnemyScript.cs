using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{

private NavMeshAgent navMeshAgent;

private Transform player;

public GameObject Coin;


public int maxHealth;
private int currentHealth;



public float sightRange;
public float EscapeRange;
public float attackRange;

public enum AiState 
{ 
Idle, 
Chase, 
Attack
}

public AiState aiState = AiState.Idle;

private Animator animator;



private void Start() 
{
    navMeshAgent = GetComponent<NavMeshAgent>();
    player = GameObject.FindGameObjectWithTag("Player").transform;
    animator = GetComponentInChildren<Animator>();



    currentHealth = maxHealth;

    StartCoroutine(Think());
}







private void Update()
{




}


public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Invoke(nameof(Die), 0.5f);
        }

    }

void Die()
{
    StopCoroutine(Think());

    animator.SetBool("Dead", true);

    Destroy(gameObject, 10f);

    int[] amount = new int[] {1, 2, 3, 4, 5};

    int randomValue = Random.Range(0, amount .Length -1);
    int coinAmount = amount[randomValue];

    for(int i = 0; i < coinAmount; i++)
    {
        Instantiate(Coin, transform.position , transform.rotation);
    }
}



IEnumerator Think()
{
while(true)
{
switch (aiState)
    {
    case AiState.Idle:

        float distance = Vector3.Distance(player.position, transform.position);

        if(distance < sightRange)
        {
            aiState = AiState.Chase;
            animator.SetBool("Chasing", true);
        }

        navMeshAgent.SetDestination(transform.position);


    break;
            
    case AiState.Chase:

        distance = Vector3.Distance(player.position, transform.position);

        if(distance > EscapeRange)
        {
            aiState = AiState.Idle;
            animator.SetBool("Chasing", false);
        }

        if(distance < attackRange)
        {
            aiState = AiState.Attack;
            animator.SetBool("Attacking", true);

        }

        navMeshAgent.SetDestination(player.position);

    break;

    case AiState.Attack:

        distance = Vector3.Distance(player.position, transform.position);

        navMeshAgent.SetDestination(transform.position);





        if(distance > attackRange)
        {
            aiState = AiState.Chase;
            animator.SetBool("Attacking", false);
        }

    break;
    
    default:
        break;
            
    }


    yield return new WaitForSeconds(.01f);


    }
}  



}
