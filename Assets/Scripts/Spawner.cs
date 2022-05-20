using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    public  EnemyAi enemy;
    private List<EnemyAi> enemies;
    
    [Range (0,100)]
    public int numberOfEnemies = 25;
    private float range = 70.0f;
    
    void Start()
    {
        enemies = new List<EnemyAi>(); // init as type
        for (int index = 0; index < numberOfEnemies; index++)
        {
            EnemyAi spawned = Instantiate(enemy, RandomNavmeshLocation(range), Quaternion.identity) as EnemyAi;
            enemies.Add(spawned);
        }
    }
    
    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }
}