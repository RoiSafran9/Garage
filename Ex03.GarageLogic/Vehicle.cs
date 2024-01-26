using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private string m_ModelName;
        private string m_LicenseNumber;
        private float m_PrecentageEnergyLeft;
        public List<Wheel> m_Wheels;
        private string m_OwnersName;
        private string m_OwnersPhoneNumber;

        public Engine m_VehicleEngine = null;
        private eVehicleCondition m_VehicleCondition;

        public string ModelName
        {
            get => m_ModelName;
            set => m_ModelName = value;
        }

        public string LicenseNumber
        {
            get => m_LicenseNumber;
            set => m_LicenseNumber = value;
        }

        public float PrecentageEnergyLeft
        {
            get => m_PrecentageEnergyLeft;
            set => m_PrecentageEnergyLeft = value;
        }

        public eVehicleCondition Condition
        {
            get => m_VehicleCondition;
            set => m_VehicleCondition = value;
        }

        public string OwnersName
        {
            get => m_OwnersName;
            set => m_OwnersName = value;
        }

        public string OwnersPhoneNumber
        {
            get => m_OwnersPhoneNumber;
            set => m_OwnersPhoneNumber = value;
        }

        public Engine VehicleEngine
        {
            get => (Engine)m_VehicleEngine;
            set => m_VehicleEngine = value;
        }

        // constructor - Electric vehicles
        protected Vehicle(string i_modelName, string i_LicenseNumber, string i_OwnersName, string i_OwnersPhoneNumber)
        {
            m_ModelName = i_modelName;
            m_LicenseNumber = i_LicenseNumber;
#pragma warning disable SA1101 // Prefix local calls with this
            m_OwnersName = i_OwnersName;
#pragma warning restore SA1101 // Prefix local calls with this
            m_OwnersPhoneNumber = i_OwnersPhoneNumber;
        }

        public override string ToString()
        {
            StringBuilder generalDetails = new StringBuilder();
            StringBuilder wheelsDetails =  new StringBuilder();

            string licenseNumber = string.Format("License number:{0}", m_LicenseNumber);
            string modelName = string.Format("Model name:{0}", m_ModelName); 
            string ownerName = string.Format("Owner's name:{0}", m_OwnersName);
            string condition = string.Format("Vehicle Condition:{0}", m_VehicleCondition);
            string engineDetails = string.Empty;

            int wheelCounter = 1;
            foreach (Wheel wheel in m_Wheels)
            {
                string singleWhellDetails = string.Format("Wheel #{0}: Manufacturer:{1}, {2} PSI.", wheelCounter, wheel.Manufacturer, wheel.CurrentAirPressure);
                wheelsDetails.AppendLine(singleWhellDetails);
                wheelCounter++;
            }

            switch (m_VehicleEngine)
            {
                case FuelTank currentFuelTank:
                    engineDetails = string.Format("Fuel type:{0}, Precentage remaining: {1}", currentFuelTank.TypeOfFuel, currentFuelTank.GetRemainingPrecentageLeft());
                    break;
                case Battery currentBattery:
                    engineDetails = string.Format("Battery precentage remaining: {0}", currentBattery.GetRemainingPrecentageLeft());
                    break;
            }

            generalDetails.AppendLine(licenseNumber);
            generalDetails.AppendLine(modelName);
            generalDetails.AppendLine(ownerName);
            generalDetails.AppendLine(condition);
            generalDetails.AppendLine(wheelsDetails.ToString());
            generalDetails.AppendLine(engineDetails);

            return generalDetails.ToString();
        }
    }
}
