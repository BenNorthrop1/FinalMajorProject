using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class PickupController : MonoBehaviour {
 
    GameObject itemPickedUp;
    Transform itemT;
 
    Transform player;
    Transform cam;
 
    Rigidbody itemrb;
 
    public static PickupController instance;
 
    [Header("Settings")]
    public float distanceInFront = 1f;
    public float minDropDistance = 2f;
    public float maxThrowSpeed = 10f;
    public float followSpeed = 20f;
    public float pickupDelay = 1f;
 
    float pickupTime;
 
    [Header("Smoothing")]
    public float rotationSmooth = 10f;
 
    float currentDistance;
 
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cam = Camera.main.transform;
 
        currentDistance = distanceInFront;
 
        instance = this;
    }
 
    void Update() {
        if (itemPickedUp != null) {
            itemT.rotation = Quaternion.RotateTowards(itemT.rotation, Quaternion.LookRotation(-cam.forward), rotationSmooth * Time.deltaTime);
 
            if (Vector3.Distance(itemT.position, player.position) > minDropDistance) {
                Letgo();
            }
        }
 
        currentDistance = Mathf.Clamp(currentDistance, 0.5f, distanceInFront);
    }
 
    private void FixedUpdate() {
        if (itemrb && itemT) {
            Vector3 targetPos = cam.position + cam.forward * currentDistance;
            itemrb.velocity = ( targetPos - itemT.position ) * followSpeed / itemrb.mass;
        }
    }
 
    public bool currentlyHoldingItem() {
        return itemPickedUp != null;
    }
 
    public void Pickup(GameObject go) {
        if (Time.time - pickupTime < pickupDelay) return;
        itemPickedUp = go;
        itemT = go.transform;
 
        itemrb = go.GetComponent<Rigidbody>();
 
        itemrb.angularVelocity = Vector3.zero;
        itemrb.useGravity = false;
    }
 
    public void Letgo() {
        pickupTime = Time.time;
        itemPickedUp = null;
        itemT = null;
 
        if (itemrb) {
            itemrb.useGravity = true;
 
            if (itemrb.velocity.magnitude > maxThrowSpeed) {
                itemrb.velocity = itemrb.velocity.normalized * maxThrowSpeed;
            }
        }
    }
 
}