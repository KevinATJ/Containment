using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBattery : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        if (HandCam.Instance != null)
        {
            HandCam.Instance.PickupBattery();
        }
        Destroy(gameObject);
    }
}
