namespace Ex03.GarageLogic
{
    using System;
    using System.Collections.Generic;

    public class VehicleCreator
    {
        public static Vehicle CreateNewVehicle(GeneralVehicleDetails i_GeneralDetails, SpecificVehicleDetails i_SpecificDetails)
        {
            eVehicleType vehicleType = i_GeneralDetails.TypeOfVehicle;
            Vehicle newVehicle = null;
            Engine newEngine = CreateNewEngine(i_GeneralDetails.TypeOfVehicle, i_GeneralDetails.CurrentEnergyLeftInEngine);
            List<Wheel> newWheels = CreateWheelsList(i_GeneralDetails.TypeOfVehicle,
                i_GeneralDetails.WheelsManufacturer,
                i_GeneralDetails.CurrentAirPressureInWheels);

            switch (vehicleType)
            {
                case eVehicleType.FuelCar:
                case eVehicleType.ElectricCar:
                    newVehicle = new Car(
                        i_GeneralDetails.ModelName,
                        i_GeneralDetails.LicenseNumber,
                        i_GeneralDetails.OwnersName,
                        i_GeneralDetails.OwnersPhoneNumber,
                        i_SpecificDetails.CarColor,
                        i_SpecificDetails.HowManyDoors);
                    break;

                case eVehicleType.FuelMotorCycle:
                case eVehicleType.ElectricMotorCycle:
                    newVehicle = new MotorCycle(
                        i_GeneralDetails.ModelName,
                        i_GeneralDetails.LicenseNumber,
                        i_GeneralDetails.OwnersName,
                        i_GeneralDetails.OwnersPhoneNumber,
                        i_SpecificDetails.MotorCycleLicenseType,
                        i_SpecificDetails.EngineCapacity);
                    break;

                case eVehicleType.Truck:
                    newVehicle = new Truck(
                        i_GeneralDetails.ModelName,
                        i_GeneralDetails.LicenseNumber,
                        i_GeneralDetails.OwnersName,
                        i_GeneralDetails.OwnersPhoneNumber,
                        i_SpecificDetails.IsCoolingPossible,
                        i_SpecificDetails.CargoVolume);
                    break;
            }

            newVehicle.VehicleEngine = newEngine;
            newVehicle.m_Wheels = newWheels;
            newVehicle.PrecentageEnergyLeft = newEngine.GetRemainingPrecentageLeft();

            return newVehicle;
        }

        public static Engine CreateNewEngine(eVehicleType i_TypeOfVehicle, float i_CurrentEnergyLeftInEngine)
        {
            Engine newEngine = null;
            eFuelType fuelType = 0;
            float maximunEnergyAmount = 0;

            switch (i_TypeOfVehicle)
            {
                case eVehicleType.FuelCar:
                    fuelType = eFuelType.Octan98;
                    maximunEnergyAmount = Car.sr_FuelTankCapacity;
                    break;

                case eVehicleType.ElectricCar:
                    maximunEnergyAmount = Car.sr_MaximumBatteryTime;
                    break;


                case eVehicleType.FuelMotorCycle:
                    fuelType = eFuelType.Octan95;
                    maximunEnergyAmount = MotorCycle.sr_FuelTankCapacity;
                    break;

                case eVehicleType.ElectricMotorCycle:
                    maximunEnergyAmount = MotorCycle.sr_MaximumBatteryTime;
                    break;

                case eVehicleType.Truck:
                    fuelType = eFuelType.Soler;
                    maximunEnergyAmount = Truck.sr_FuelTankCapacity;
                    break;
                default:
                    break;
            }

            switch (i_TypeOfVehicle)
            {
                case eVehicleType.FuelCar:
                case eVehicleType.FuelMotorCycle:
                case eVehicleType.Truck:
                    newEngine = new FuelTank(fuelType, i_CurrentEnergyLeftInEngine, maximunEnergyAmount);
                    break;
                case eVehicleType.ElectricCar:
                case eVehicleType.ElectricMotorCycle:
                    newEngine = new Battery(i_CurrentEnergyLeftInEngine, maximunEnergyAmount);
                    break;
            }

            return newEngine;
        }

        public static List<Wheel> CreateWheelsList(eVehicleType i_TypeOfVehicle, string i_WheelsManufacturer, float i_CurrentAirPressureInWheels)
        {
            List<Wheel> newWheels = new List<Wheel>();
            int numberOfWheels;
            float maximunAirPressure;

            switch (i_TypeOfVehicle)
            {
                case eVehicleType.FuelCar:
                case eVehicleType.ElectricCar:
                    numberOfWheels = Car.sr_Wheels;
                    maximunAirPressure = Car.sr_MaximumAirPressure;
                    break;

                case eVehicleType.FuelMotorCycle:
                case eVehicleType.ElectricMotorCycle:
                    numberOfWheels = MotorCycle.sr_Wheels;
                    maximunAirPressure = MotorCycle.sr_MaximumAirPressure;
                    break;

                case eVehicleType.Truck:
                    numberOfWheels = Truck.sr_Wheels;
                    maximunAirPressure = Truck.sr_MaximumAirPressure;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(i_TypeOfVehicle), i_TypeOfVehicle, "Please choose an option within the valid range!");
            }

            for (int i = 0; i < numberOfWheels; i++)
            {
                Wheel wheel = new Wheel
                {
                    Manufacturer = i_WheelsManufacturer,
                    CurrentAirPressure = i_CurrentAirPressureInWheels,
                    MaximalAirPressure = maximunAirPressure
                };

                newWheels.Add(wheel);
            }

            return newWheels;
        }
    }

    public class GeneralVehicleDetails
    {
        public string ModelName
        {
            get; set;
        }

        public string LicenseNumber
        {
            get; set;
        }

        public float CurrentEnergyLeftInEngine
        {
            get; set;
        }

        public float CurrentAirPressureInWheels
        {
            get; set;
        }

        public string WheelsManufacturer
        {
            get; set;
        }

        public string OwnersName
        {
            get; set;
        }

        public string OwnersPhoneNumber
        {
            get; set;
        }

        public eVehicleType TypeOfVehicle
        {
            get; set;
        }
    }

    public class SpecificVehicleDetails
    {
        public eMotorCycleLicenseType MotorCycleLicenseType
        {
            get; set;
        }

        public int EngineCapacity
        {
            get; set;
        }

        public eCarColor CarColor
        {
            get; set;
        }

        public int HowManyDoors
        {
            get; set;
        }

        public bool IsCoolingPossible
        {
            get; set;
        }

        public float CargoVolume
        {
            get; set;
        }
    }
}