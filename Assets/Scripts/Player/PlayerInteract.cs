using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [Header("Raycast")]
    public Camera playerCamera;
    public float interactDistance = 3f;

    [Header("UI")]
    public TMPro.TextMeshProUGUI interactText;

    void Update()
    {
        RaycastHit hit;
        bool hasHit = Physics.Raycast(playerCamera.transform.position,
                                      playerCamera.transform.forward,
                                      out hit,
                                      interactDistance);

        if (hasHit)
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                interactText.text = "E - Interactuar";

                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact();
                }
                return;
            }
        }

        interactText.text = "";
    }
}

