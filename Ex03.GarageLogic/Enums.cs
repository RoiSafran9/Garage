namespace Ex03.GarageLogic
{
    public enum eFuelType
    {
        Soler = 1,
        Octan95 = 2,
        Octan96 = 3,
        Octan98 = 4
    }

    public enum eVehicleCondition
    {
        None = 0,
        UnderRepair = 1, // default
        Repaired = 2,
        Paid = 3,
    }

    public enum eMotorCycleLicenseType
    {
        A = 1, // default
        A1 = 2,
        A2 = 3,
        AB = 4
    }

    public enum eCarColor
    {
        Black = 1,
        White = 2,
        Red = 3,
        Blue = 4
    }

    public enum eVehicleType
    {
        FuelCar = 1,
        ElectricCar = 2,
        FuelMotorCycle = 3,
        ElectricMotorCycle = 4,
        Truck = 5
    }
}
