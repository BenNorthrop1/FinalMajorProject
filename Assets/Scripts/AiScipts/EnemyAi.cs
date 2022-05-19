
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

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    private bool isDead;

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
        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();

    }

        float Distance = Vector3.Distance(transform.position , player.position);

        if( Distance <= agent.stoppingDistance)
        {
            anim.SetFloat("Speed", 0, 0.3f, Time.deltaTime);
            AttackPlayer();
        }

    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        agent.SetDestination(walkPoint);
        anim.SetFloat("Speed", 0.5f, 0.3f, Time.deltaTime);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
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
            if (Physics.Raycast(raycastObject.transform.position, fwd, out objectHit, 20))
            {
                
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

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);

    }
    private void DestroyEnemy()
    {   
        anim.SetBool("IsDead", true);
        Destroy(gameObject, 10f);

        int[] amount = new int[] {1, 2, 3, 4, 5};

        int randomValue = Random.Range(0, amount .Length -1);
        int coinAmount = amount[randomValue];

        for(int i = 0; i < coinAmount; i++)
        {
            Instantiate(Coin, transform.position , transform.rotation);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
