using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        public List<Vehicle> m_VehiclesInTheGarage;

        public Garage()
        {
            m_VehiclesInTheGarage = new List<Vehicle>();
        }

        public void AddNewVehicleToTheGarage(GeneralVehicleDetails i_GeneralDetails, SpecificVehicleDetails i_SpecificDetails)
        {
            Vehicle newVehicle = VehicleCreator.CreateNewVehicle(i_GeneralDetails, i_SpecificDetails);
            m_VehiclesInTheGarage.Add(newVehicle);
        }

        public bool IsVehicleExistInGarage(string i_LicenseNumber)
        {
            if (m_VehiclesInTheGarage.Exists(i_Vehicle => i_Vehicle.LicenseNumber == i_LicenseNumber))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ChangeConditionOfVehicle(string i_LicenseNumber, eVehicleCondition m_VehicleCondition)
        {
            Vehicle vehicleWanted = GetVehicleByLicenseNumber(i_LicenseNumber);
            vehicleWanted.Condition = m_VehicleCondition;
        }

        public Vehicle GetVehicleByLicenseNumber(string i_LicenseNumber)
        {
            if (!IsVehicleExistInGarage(i_LicenseNumber))
            {
                throw new ArgumentException("The vehicle you are looking for is not in the garage.");
            }

            return m_VehiclesInTheGarage.Find(i_Vehicle => i_Vehicle.LicenseNumber == i_LicenseNumber);
        }

        public bool IsGarageEmpty()
        {
            return m_VehiclesInTheGarage.Count == 0;
        }

        public void InflateWheelsToMax(string i_LicenseNumber)
        {
            Vehicle vehicleWanted = GetVehicleByLicenseNumber(i_LicenseNumber);
            foreach (Wheel wheel in vehicleWanted.m_Wheels)
            {
                wheel.InflateToMax();
            }
        }

        public void FuelVehicle(string i_LicenseNumber, eFuelType i_FuelType, float i_AmountToFuel)
        {
            Vehicle vehicleWanted = GetVehicleByLicenseNumber(i_LicenseNumber);
            if (vehicleWanted.m_VehicleEngine is FuelTank fuelTankWantedVehicle)
            {
                fuelTankWantedVehicle.Refuel(i_FuelType, i_AmountToFuel);
                vehicleWanted.PrecentageEnergyLeft = fuelTankWantedVehicle.GetRemainingPrecentageLeft();
            }
            else
            {
                throw new ArgumentException("The refuel you wish to make is impossible.");
            }
        }

        public void RechargeVehicle(string i_LicenseNumber, float i_MinutesToCharge)
        {
            float hoursToCharge = i_MinutesToCharge / 60;
            Vehicle vehicleWanted = GetVehicleByLicenseNumber(i_LicenseNumber);
            if (vehicleWanted.m_VehicleEngine is Battery batteryTankWantedVehicle)
            {
                batteryTankWantedVehicle.Charge(hoursToCharge);
                vehicleWanted.PrecentageEnergyLeft = batteryTankWantedVehicle.GetRemainingPrecentageLeft();
            }
            else
            {
                throw new ArgumentException("The Charge you wish to make is impossible.");
            }
        }

        public string ShowDetails(string i_LicenseNumber)
        {
            Vehicle vehicleWanted = GetVehicleByLicenseNumber(i_LicenseNumber);
            return vehicleWanted.ToString();
        }

        public List<Vehicle> GetVehiclesFromGarageByCondition(eVehicleCondition i_Condition)
        {
            return m_VehiclesInTheGarage.Where(i_Vehicle => i_Vehicle.Condition == i_Condition).ToList();
        }

        public bool isNoVehiclesWithGivenConditionInTheGarage(eVehicleCondition i_Condition)
        {
            return GetVehiclesFromGarageByCondition(i_Condition).Count == 0;
        }
    }
}
