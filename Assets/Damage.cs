using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{

    public Stats stats;

    public int damage;


    // Start is called before the first frame update
    void Start()
    {
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(takeDamage());
    }

    IEnumerator takeDamage()
    {
        stats.TakeDamage(damage);

        yield return new WaitForSeconds(4);

        yield break;
    }
}
