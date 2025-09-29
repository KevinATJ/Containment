using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [Header("Door Settings")]
    public bool IsOpen = false;
    [SerializeField] private float RotationAmount = 90f;
    [SerializeField] private float Speed = 1f;

    [Header("Requirement")]
    public bool requiresObject = false;
    public ObjectData requiredObject;

    private Vector3 startRotation;
    private Coroutine animationCoroutine;

    private void Awake()
    {
        startRotation = transform.rotation.eulerAngles;
    }

    public void Interact()
    {
        if (requiresObject)
        {
            if (HandSystem.Instance.CurrentObjectData == null ||
                HandSystem.Instance.CurrentObjectData != requiredObject)
            {
                Debug.Log($"Puerta cerrada. Requiere: {requiredObject.displayName}");
                return;
            }
        }

        GameObject player = GameObject.FindWithTag("Player");
        if (player == null) return;

        if (animationCoroutine != null)
            StopCoroutine(animationCoroutine);

        if (!IsOpen)
        {
            Vector3 toPlayer = player.transform.position - transform.position;
            toPlayer.y = 0;

            float dot = Vector3.Dot(transform.forward, toPlayer.normalized);

            animationCoroutine = StartCoroutine(OpenDoor(dot));
        }
        else
        {
            animationCoroutine = StartCoroutine(CloseDoor());
        }
    }

    private IEnumerator OpenDoor(float dot)
    {
        Quaternion startRot = transform.rotation;
        Quaternion endRot;

        if (dot >= 0f)
            endRot = Quaternion.Euler(0, startRotation.y + RotationAmount, 0);
        else
            endRot = Quaternion.Euler(0, startRotation.y - RotationAmount, 0);

        IsOpen = true;

        float time = 0f;
        while (time < 1f)
        {
            transform.rotation = Quaternion.Slerp(startRot, endRot, time);
            time += Time.deltaTime * Speed;
            yield return null;
        }
    }

    private IEnumerator CloseDoor()
    {
        Quaternion startRot = transform.rotation;
        Quaternion endRot = Quaternion.Euler(startRotation);

        IsOpen = false;

        float time = 0f;
        while (time < 1f)
        {
            transform.rotation = Quaternion.Slerp(startRot, endRot, time);
            time += Time.deltaTime * Speed;
            yield return null;
        }
    }
}
