using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{

    Rigidbody rb;

    public string GetDescription()
    {
        return "Talk To Npc";
    }

    public void Interact()
    {
        rb.AddForce(new Vector3(0, 10, 0), ForceMode.Impulse);
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
