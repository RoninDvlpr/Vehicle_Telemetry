using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using VehiclePhysics;
using TMPro;
using static VehiclePhysics.Gearbox;

public class TelemetryHUD : MonoBehaviour
{
    [SerializeField] VehicleBase vehicle;
    [SerializeField] VPVehicleController vehicleController;
    [SerializeField] TextMeshProUGUI speedCounter, rpmCounter, engineState, gearState, transmissionMode;

    
    void Update()
    {
        UpdateSpeedCounter();
        UpdateEngineRPMCounter();
        UpdateEngineStatus();
        UpdateGearState();
        UpdateTransmissionMode();
    }

    void UpdateSpeedCounter()
    {
        speedCounter.text = $"{PlayerVehicleTelemetry.MetricSpeed:n2} km/h";
    }

    void UpdateEngineRPMCounter()
    {
        rpmCounter.text = $"{PlayerVehicleTelemetry.EngineRPMData:n0} RPM";
    }

    void UpdateEngineStatus()
    {
        if (PlayerVehicleTelemetry.EngineIsActive)
        {
            if (PlayerVehicleTelemetry.EngineIsStalled)
                engineState.text = "Stalled";
            else
                engineState.text = "Off";
        }
        else
            engineState.text = "On";
    }

    void UpdateGearState()
    {
        gearState.text = $"Gear: {PlayerVehicleTelemetry.CurrentGearIndex}";
    }

    void UpdateTransmissionMode()
    {
        transmissionMode.text = PlayerVehicleTelemetry.CurrentTransmissionMode.ToString();
    }

}
