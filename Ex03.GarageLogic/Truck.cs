namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private readonly bool r_IsContainCooling;
        private readonly float r_CargoVolume;
        public static readonly int sr_Wheels = 12;
        public static readonly float sr_MaximumAirPressure = 27f;
        public static readonly eFuelType sr_FuelType = eFuelType.Soler;
        public static readonly float sr_FuelTankCapacity = 130f;

        public Truck(
            string i_ModelName,
            string i_LicenseNumber,
            string i_OwnersName,
            string i_OwnersPhoneNumber,
            bool i_IsContainCooling ,
            float i_CargoVolume)
            : base(i_ModelName, i_LicenseNumber, i_OwnersName, i_OwnersPhoneNumber)
        {
            r_IsContainCooling = i_IsContainCooling;
            r_CargoVolume = i_CargoVolume;
        }

        public override string ToString()
        {
            string TruckCycleDetails = string.Format("{0}\nIs cooling possible: {1}\n Cargo volume: {2}", base.ToString(), r_IsContainCooling.ToString(), r_CargoVolume);
            return TruckCycleDetails;
        }
    }
}
