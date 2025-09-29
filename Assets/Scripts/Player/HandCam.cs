using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HandCam : MonoBehaviour
{
    public static HandCam Instance;

    [Header("UI")]
    public GameObject recPanel;
    public GameObject crosshair;
    public TextMeshProUGUI batteryText;

    [Header("Battery")]
    public float maxBattery = 100f;
    public float drainRate = 5f;
    public int reserveMax = 3;

    private float currentBattery;
    private bool isActive = false;
    private int reserveCount = 0;

    void Awake() => Instance = this;

    void Start()
    {
        crosshair.SetActive(true);
        recPanel.SetActive(false);
        currentBattery = maxBattery;
        UpdateBatteryUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleCamera();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            UseReserveBattery();
        }

        if (isActive && currentBattery > 0f)
        {
            currentBattery -= drainRate * Time.deltaTime;
            if (currentBattery <= 0f)
            {
                currentBattery = 0f;
                Debug.Log("Bater�a agotada. Presiona R para usar una reserva.");
            }
            UpdateBatteryUI();
        }
    }

    void ToggleCamera()
    {
        isActive = !isActive;
        recPanel.SetActive(isActive);
        crosshair.SetActive(!isActive);
        Debug.Log("HandCam " + (isActive ? "activada" : "desactivada"));
    }

    void UpdateBatteryUI()
    {
        if (batteryText != null)
        {
            batteryText.text = $"Bater�a: {Mathf.RoundToInt(currentBattery)}% (x{reserveCount})";
        }
    }

    public void PickupBattery()
    {
        if (reserveCount < reserveMax)
        {
            reserveCount++;
            Debug.Log($"Bater�a recogida. Reservas: {reserveCount}");
        }
        else
        {
            Debug.Log("No puedes llevar m�s bater�as de reserva.");
        }
        UpdateBatteryUI();
    }

    public void UseReserveBattery()
    {
        if (reserveCount > 0)
        {
            reserveCount--;
            currentBattery = maxBattery;
            Debug.Log("Se us� una bater�a de reserva.");
            UpdateBatteryUI();
        }
        else
        {
            Debug.Log("No tienes bater�as de reserva.");
        }
    }
}




