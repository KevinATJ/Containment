using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupNote : MonoBehaviour, IInteractable
{
    public NoteData data;
    public bool destroyOnPickup = true;

    public void Interact()
    {
        if (data == null) return;

        if (NoteUIManager.Instance != null)
            NoteUIManager.Instance.Show(data);

        /*if (destroyOnPickup)
            Destroy(gameObject);*/
    }
}


