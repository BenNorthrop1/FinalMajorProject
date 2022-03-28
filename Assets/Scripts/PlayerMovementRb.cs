using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementRb : MonoBehaviour
{

    public Rigidbody rb;

    public float mouseX, turnForce;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X");
        
        
    }

    void FixedUpdate()
    {
        Rotate();
    }

    void Move()
    {






    }

    void Rotate()
    {
        if (mouseX != 0)
            turnForce = mouseX;
        
        print(mouseX);
        rb.AddRelativeTorque(new Vector3(0, turnForce,0));
        if (mouseX == 0)
            rb.AddRelativeTorque(0, -turnForce, 0);
        
    }


}
