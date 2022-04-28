using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{

    public float amount = 0.002f;
    public float maxAmount = 0.006f;
    public float smoothAmount = 1f;

    
    public float rotationAmount = 2f;
    public float maxRotationAmount = 4f;
    public float smoothRotation = 6f;

    private float input_X;
    private float input_Y;

    public bool rotationX;
    public bool rotationY;
    public bool rotationZ;

    private Vector3 initialPos;
    private Quaternion initialRot;

   
    void Start()
    {
        initialPos = transform.localPosition;
        initialRot = transform.localRotation;
    }

   
    void Update()
    {
        CalculateSway();
        Sway();
        Tilt();

        
    }

    private void CalculateSway()
    {
        input_X = -Input.GetAxis("Mouse X");
        input_Y = -Input.GetAxis("Mouse Y");
    }


    private void Sway()
    {
        float moveX = Mathf.Clamp(input_X * amount, -maxAmount,maxAmount);
        float moveY = Mathf.Clamp(input_Y * amount, -maxAmount,maxAmount);

        Vector3 finalPosition = new Vector3(moveX, moveY, 0);

        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initialPos, Time.deltaTime * smoothAmount);
    }

    private void Tilt()
    {
        float tiltX = Mathf.Clamp(input_X * rotationAmount, -maxRotationAmount, maxRotationAmount);
        float tiltY = Mathf.Clamp(input_Y * rotationAmount, -maxRotationAmount, maxRotationAmount);

        Quaternion finalRotation = Quaternion.Euler(rotationX ? tiltY : 0f, rotationY ? tiltX : 0f, rotationZ ? tiltX : 0f);

        transform.localRotation = Quaternion.Slerp(transform.localRotation, finalRotation * initialRot, Time.deltaTime * smoothRotation);

        
    }

}

   




