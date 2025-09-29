using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSystem : MonoBehaviour
{
    public static HandSystem Instance;

    public Transform handPoint;
    public PickupObject CurrentObject { get; private set; }
    public ObjectData CurrentObjectData => CurrentObject ? CurrentObject.data : null;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        if (CurrentObject != null && Input.GetKeyDown(KeyCode.G))
        {
            Drop();
        }
    }

    public void Pickup(PickupObject pickup)
    {
        if (CurrentObject != null) return;

        CurrentObject = pickup;

        Rigidbody rb = pickup.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        pickup.transform.SetParent(handPoint);
        pickup.transform.localPosition = Vector3.zero;
        pickup.transform.localRotation = Quaternion.identity;

        Debug.Log($"Has recogido: {pickup.data.displayName}");
    }

    public void Drop()
    {
        if (CurrentObject == null) return;

        CurrentObject.transform.SetParent(null);

        Rigidbody rb = CurrentObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.AddForce(handPoint.forward * 2f, ForceMode.Impulse);
        }

        Debug.Log($"Has soltado: {CurrentObject.data.displayName}");

        CurrentObject = null;
    }
}

