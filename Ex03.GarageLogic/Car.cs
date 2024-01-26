namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private readonly eCarColor r_CarColor;
        private readonly int r_HowManyDoors;
        public static readonly int sr_Wheels = 5;
        public static readonly float sr_MaximumAirPressure = 32;
        public static readonly eFuelType sr_FuelType = eFuelType.Octan95;
        public static readonly float sr_FuelTankCapacity = 44;
        public static readonly float sr_MaximumBatteryTime = 5.2f;

        public Car(
            string i_ModelName,
            string i_LicenseNumber,
            string i_OwnersName,
            string i_OwnersPhoneNumber,
            eCarColor i_CarColor,
            int i_HowManyDoors)
            : base (i_ModelName, i_LicenseNumber, i_OwnersName, i_OwnersPhoneNumber)
        {
            r_CarColor = i_CarColor;
            r_HowManyDoors = i_HowManyDoors;
        }

        public override string ToString()
        {

            string carDetails = string.Format("{0}\nCar color: {1}\nAmount of doors: {2}", base.ToString(), r_CarColor.ToString(), r_HowManyDoors);
            return carDetails;
        }
    }
}
