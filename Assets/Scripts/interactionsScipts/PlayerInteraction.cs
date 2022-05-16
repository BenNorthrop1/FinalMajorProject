using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
 
public class PlayerInteraction : MonoBehaviour {
    
    public Camera mainCam;
    public float interactionDistance = 2f;

    public bool isPickedUp;
 
    public GameObject interactionUI;
    public TextMeshProUGUI interactionText;
    
    private void Awake()
    {

    }
 
    private void Update() {
        InteractionRay();
    }
 
    void InteractionRay() {
        Ray ray = mainCam.ViewportPointToRay(Vector3.one/2f);
        RaycastHit hit;
 
        bool hitSomething = false;
 
        if (Physics.Raycast(ray, out hit, interactionDistance)) {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
 
            if (interactable != null) {
                hitSomething = true;
                interactionText.text = interactable.GetDescription();
 
                if (Input.GetKeyDown(KeyCode.E)) {
                    interactable.Interact();
                }
            }

                if(Input.GetKeyDown(KeyCode.F)&& hit.collider.CompareTag("Pickupable"))
                {
                    PickupController.instance.Pickup(hit.collider.gameObject);
                    isPickedUp = true;
                }
                else
                {
                    isPickedUp = false;
                }

        }
 
        interactionUI.SetActive(hitSomething);
    }
}
 
 

