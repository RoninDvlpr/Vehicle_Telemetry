using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehiclePhysics;
using static PlayerVehicleTelemetry;


public class PlayerVehicleTelemetry : MonoBehaviour
{
    public enum TransmissionMode
    {
        Automatic,
        SemiAutomatic,
        Manual
    }

    /// <summary>
    /// Vehicle speed in mm/s
    /// </summary>
    public static int RawSpeed => vehicleController.data.Get(Channel.Vehicle, VehicleData.Speed);

    /// <summary>
    /// Vehicle speed in km/h
    /// </summary>
    public static float MetricSpeed => RawSpeed * 3.6f / 1000f;

    /// <summary>
    /// Enigine RPM as returned by VPVehicleController
    /// </summary>
    public static int RawEngineRPMData => vehicleController.data.Get(Channel.Vehicle, VehicleData.EngineRpm);

    /// <summary>
    /// Enigine RPM
    /// </summary>
    public static int EngineRPMData => RawEngineRPMData / 1000;

    public static bool EngineIsActive => vehicleController.data.Get(Channel.Vehicle, VehicleData.EngineWorking) != 0;

    public static bool EngineIsStalled => vehicleController.data.Get(Channel.Vehicle, VehicleData.EngineStalled) == 1;

    public static int CurrentGearIndex => vehicleController.data.Get(Channel.Vehicle, VehicleData.GearboxGear);

    /// <summary>
    /// Gearbox mode as specified in the VPVehicleController.
    /// </summary>
    public static int GearboxMode => vehicleController.data.Get(Channel.Vehicle, VehicleData.GearboxMode);

    /// <summary>
    /// Gearbox mode as specified in the Gearbox.Settings.
    /// </summary>
    public static Gearbox.Type GearboxTypeSetting => vehicleController.gearbox.type;

    /// <summary>
    /// Gearbox autoshifting setting.
    /// It's overwritten by the autoShiftOverride property of the Gearbox. 
    /// The Gearbox objects itself is incapsulated in the VPVehicleController.
    /// </summary>
    public static bool GearboxAutoShiftSetting => vehicleController.gearbox.autoShift;

    /// <summary>
    /// This VPVehicleController's setting seems to override the autoShift setting of it's own Gearbox.
    /// </summary>
    /// <value>
    /// 0 - no override aplied; 1 - force enable autoshift; 2 - force disabled autoshift.
    /// </value>
    public static int AutoshiftOverride => vehicleController.data.Get(Channel.Settings, SettingsData.AutoShiftOverride);

    /// <summary>
    /// Effective autoshift setting after applying AutoshiftOverride.
    /// </summary>
    public static bool AutoshiftEnabled
    {
        get
        {
            if (AutoshiftOverride == 0)
                return GearboxAutoShiftSetting;
            else
                return AutoshiftOverride == 1;
        }
    }

    /// <summary>
    /// Effective transmission mode considering transmission type and autoshift setting. 
    public static TransmissionMode CurrentTransmissionMode
    {
        get
        {
            if (GearboxMode == 1)
                return TransmissionMode.Automatic;
            else if (AutoshiftEnabled)
                return TransmissionMode.SemiAutomatic;
            else
                return TransmissionMode.Manual;
        }
    }




    #region instance management

    static VPVehicleController vehicleController;

    void Awake()
    {
        SetVehicleControllerInstance();
    }

    void SetVehicleControllerInstance()
    {
        if (vehicleController == null)
            vehicleController = GetComponent<VPVehicleController>();
        else
            Debug.LogError("Another instance of the PlayerVehicleTelemetry already exist!");
    }

    private void OnDestroy()
    {
        vehicleController = null;
    }

    #endregion
}
