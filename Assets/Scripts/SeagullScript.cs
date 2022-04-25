using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeagullScript : MonoBehaviour
{

    Rigidbody rb;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine( ExampleCoroutine() );
    }

    IEnumerator ExampleCoroutine()
{
        rb.AddForce(transform.forward * speed, ForceMode.Acceleration);
        
        yield return new WaitForSeconds(5); //yield on a new YieldInstruction that waits for 5 seconds.

        transform.Rotate(0, 90  * Time.deltaTime, 0);

        yield return new WaitForSeconds(1);

        rb.AddForce(transform.forward * speed, ForceMode.Acceleration);

        yield return new WaitForSeconds(5); //yield on a new YieldInstruction that waits for 5 seconds.

        transform.Rotate(0, -90 * Time.deltaTime, 0);

        yield return new WaitForSeconds(1);

        rb.AddForce(transform.forward * speed, ForceMode.Acceleration);

        // end of coroutine
}

}
