namespace Ex03.GarageLogic
{
    public class MotorCycle : Vehicle
    {
        private readonly eMotorCycleLicenseType r_LicenseType;
        private readonly int r_EngineCapacity;
        public static readonly int sr_Wheels = 2;
        public static readonly float sr_MaximumAirPressure = 30;
        public static readonly eFuelType sr_FuelType = eFuelType.Octan98;
        public static readonly float sr_FuelTankCapacity = 6.2f;
        public static readonly float sr_MaximumBatteryTime = 2.4f;

        public MotorCycle(
            string i_ModelName,
            string i_LicenseNumber,
            string i_OwnersName,
            string i_OwnersPhoneNumber,
            eMotorCycleLicenseType i_LicenseType,
            int i_EngineCapacity)
            : base(i_ModelName, i_LicenseNumber, i_OwnersName, i_OwnersPhoneNumber)
        {
            r_LicenseType = i_LicenseType;
            r_EngineCapacity = i_EngineCapacity;
        }

        public override string ToString()
        {
            string MotorCycleDetails = string.Format("{0}\nLicense Type: {1}\nEngine Capacity: {2}", base.ToString(), r_LicenseType.ToString(), r_EngineCapacity);
            return MotorCycleDetails;
        }
    }
}
