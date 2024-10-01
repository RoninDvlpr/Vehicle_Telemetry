using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class TelemetryLogger : MonoBehaviour
{
    [SerializeField] bool logToConole = true, writeIntoFile = true;
    string TelemetryFilePath => Application.dataPath + "/telemetry.csv";


    private void Start()
    {
        CreateTelemetryFile();
    }

    void Update()
    {
        LogTelemetryToConsole();
        WriteTelemetryIntoFile();
    }

    void LogTelemetryToConsole()
    {
        if (!logToConole)
            return;

        Debug.Log($"Velocity: {PlayerVehicleTelemetry.MetricSpeed}, " +
            $"RPM: {PlayerVehicleTelemetry.EngineRPMData}, " +
            $"Engine Is Running: {PlayerVehicleTelemetry.EngineIsActive}, " +
            $"Active Gear: {PlayerVehicleTelemetry.CurrentGearIndex}, " +
            $"Transmission Mode: {PlayerVehicleTelemetry.CurrentTransmissionMode}");
    }

    void CreateTelemetryFile()
    {
        if (!writeIntoFile)
            return;

        if (File.Exists(TelemetryFilePath))
            File.Delete(TelemetryFilePath);

        using (StreamWriter sw = File.AppendText(TelemetryFilePath))
        {
            sw.WriteLine("Velocity,RPM,Enigine Status,Gear,Transmission Mode");
        }
    }

    void WriteTelemetryIntoFile()
    {
        if (!writeIntoFile)
            return;

        string telemetryDataString = $"{PlayerVehicleTelemetry.MetricSpeed}," +
            $"{PlayerVehicleTelemetry.EngineRPMData}," +
            $"{PlayerVehicleTelemetry.EngineIsActive}," +
            $"{PlayerVehicleTelemetry.CurrentGearIndex}," +
            $"{PlayerVehicleTelemetry.CurrentTransmissionMode}";

        using (StreamWriter sw = File.CreateText(TelemetryFilePath))
        {
            sw.WriteLine(telemetryDataString);
        }
    }

    void LogTransmissionModeVariables()
    {
        Debug.Log($"Gearbox type setting: {PlayerVehicleTelemetry.GearboxTypeSetting}, Gearbox mode: {PlayerVehicleTelemetry.GearboxMode}");
    }
}
