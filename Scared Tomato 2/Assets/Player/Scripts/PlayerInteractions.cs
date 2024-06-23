using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInteractions : MonoBehaviour {
    
    private PlayerControls playerControls;
    private InputAction interact;

    [SerializeField] Transform textLocation;

    [SerializeField] GameObject pickUpText;
    [SerializeField] GameObject noPickUpText;

    [SerializeField] float interactionRange = 2f;
    [SerializeField] LayerMask interactableLayer;

    Camera mainCamera;

    void Awake() {
        playerControls = new PlayerControls();
    }

    void Start() {
        mainCamera = Camera.main; // Get the reference to the main camera
    }

    void OnEnable() {
        interact = playerControls.Movement.Interact;

        interact.Enable();

        interact.performed += Interact;
    }

    void OnDisable() {
        interact.Disable();
    }

    void Interact(InputAction.CallbackContext context) {
        GameObject canvas = GameObject.Find("Canvas");
        
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)); // Cast a ray from the center of the screen
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactionRange, interactableLayer)) {
            // Debug.Log(hit.collider.name);
            GameObject text = Instantiate(pickUpText, textLocation.position, Quaternion.identity);
            text.transform.SetParent(canvas.transform);

            Destroy(hit.collider.gameObject);
        } else {
            GameObject text = Instantiate(noPickUpText, textLocation.position, Quaternion.identity);
            text.transform.SetParent(canvas.transform);
        }
    }

}
