
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent;

    public int damage;

    public GameObject Coin;

    private Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    private Animator anim;

    public float health;

    public GameObject raycastObject;



    private static bool isDead;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;


    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Character").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

    if(!isDead)
    {


        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
        if(!playerInAttackRange && !playerInSightRange) Idle();
        
    }

        float Distance = Vector3.Distance(transform.position , player.position);

        if( Distance <= agent.stoppingDistance)
        {
            AttackPlayer();
        }

    }


    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        anim.SetFloat("Speed", 1f, 0.3f, Time.deltaTime);

        Vector3 newPlayer = player.position;

		newPlayer.y = transform.position.y;
		transform.LookAt(newPlayer);	
    }

    private void AttackPlayer()
    {
	    Vector3 newPlayer = player.position;

		newPlayer.y = transform.position.y;
		transform.LookAt(newPlayer);

        
        if (!alreadyAttacked)
        {
            ///Attack code here
            RaycastHit objectHit;
            Vector3 fwd = raycastObject.transform.TransformDirection(Vector3.forward);
            if (Physics.Raycast(raycastObject.transform.position, fwd, out objectHit, 10))
            {

                anim.SetTrigger("Attack");
                
                Stats stats = objectHit.transform.GetComponent<Stats>();

                if(stats != null)
                {
                    stats.TakeDamage(damage);
                }
            }

            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Invoke(nameof(DestroyEnemy), 0.5f);
            isDead = true;
        }

    }
    private void DestroyEnemy()
    {   
        anim.SetBool("IsDead", true);
        agent.SetDestination(transform.position);
        Destroy(gameObject, 10f);

        int[] amount = new int[] {1, 2, 3, 4, 5};

        int randomValue = Random.Range(0, amount .Length -1);
        int coinAmount = amount[randomValue];

        for(int i = 0; i < coinAmount; i++)
        {
            Instantiate(Coin, transform.position , transform.rotation);
        }
    }

    public void Idle()
    {
        anim.SetFloat("Speed", 0, 0.3f, Time.deltaTime);
        agent.SetDestination(transform.position);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
