using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour, IInteractable
{
    public ObjectData data;
    public bool destroyOnPickup = false;

    public void Interact()
    {
        if (data == null) return;

        HandSystem.Instance.Pickup(this);

    }
}

