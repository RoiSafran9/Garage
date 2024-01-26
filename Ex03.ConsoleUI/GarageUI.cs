namespace Ex03.ConsoleUI
{
    using System;
    using System.Collections.Generic;
    using Ex03.GarageLogic;

    internal class GarageUI
    {
        private Garage m_Garage;
        private bool m_IsRunning = true;

        public void StartGarageUI(Garage i_Garage)
        {
            m_Garage = i_Garage;
            while (m_IsRunning)
            {
                ShowOpeningScreen();
                ShowMenu();
                GetUserinput();
            }
        }

        public void ShowOpeningScreen()
        {
            string openingMEssage = "Welcome to the Garage!";
            Console.WriteLine(openingMEssage);
        }

        public void ShowMenu()
        {
            Console.WriteLine("Please choose what you wish to do in the garage:");
            Console.WriteLine(@"1 - Add new vehicle.
2 - Show all vehicle license numbers by condition.
3 - Change condition of a specific vehicle.
4 - Inflate vehicle's wheels.
5 - Fuel a vehicle.
6 - Charge an electric vehicle.
7 - Present details of a specific vehicle.
8 - Exit.");
            Console.WriteLine("Please enter your desired action: ");
        }

        public void GetUserinput()
        {
            string userInput = Console.ReadLine();
            while (!UsersOptionValidationCheck(userInput))
            {
                Console.WriteLine("Invalid input. Between 1 to 8 only. Try again.");
                userInput = Console.ReadLine();
            }
            if (int.TryParse(userInput, out int optinPicked))
            {
                switch (optinPicked)
                {
                    case 1:
                        Console.Clear();
                        insertNewVehicle();
                        break;
                    case 2:
                        Console.Clear();
                        showVehichlesInGarageByLicense(m_Garage.m_VehiclesInTheGarage);
                        break;
                    case 3:
                        Console.Clear();
                        changeVehicleStatus();
                        break;
                    case 4:
                        Console.Clear();
                        inflateTiresToMax();
                        break;
                    case 5:
                        Console.Clear();
                        refuelVehicle();
                        break;
                    case 6:
                        Console.Clear();
                        rechargeVehicle();
                        break;
                    case 7:
                        Console.Clear();
                        showSpecificVehicleDetails();
                        break;
                    case 8:
                        Console.WriteLine("Exiting...");
                        m_IsRunning = false;
                        break;
                    default: break;
                }
            }
            else
            {
                throw new Exception("----- Unknown error occured -----");
            }
        }

        public void insertNewVehicle()
        {
            GeneralVehicleDetails generalVehicleDetails = new GeneralVehicleDetails();
            SpecificVehicleDetails specificVehicleDetails = new SpecificVehicleDetails();

            bool isFinishedProvidingDetails = false;

            while (!isFinishedProvidingDetails)
            {
                try
                {
                    string newVehicleMeesage = string.Format(
                        @"Thanks for choosing our garage.
In order to Insert your vehicle we'll need you to provide details about the vehicle.
But first, please tell us your name: ");
                    Console.WriteLine(newVehicleMeesage);

                    generalVehicleDetails.OwnersName = getUserName();
                    generalVehicleDetails.OwnersPhoneNumber = getUserPhoneNumber();

                    string afterProvidingNameAndPhoneMessage = string.Format(
                        @"Hi {0}! Let's continue with the process.", generalVehicleDetails.OwnersName);
                    Console.WriteLine(afterProvidingNameAndPhoneMessage);

                    generalVehicleDetails.LicenseNumber = getOwnerVehicleLicenseNumber();

                    if (IsLicenseNumberAlreadyExist(generalVehicleDetails.LicenseNumber))
                    {
                        Console.Clear();
                        break;
                    }

                    generalVehicleDetails.TypeOfVehicle = getVehicleType();
                    eVehicleType TypeOfVehicle = generalVehicleDetails.TypeOfVehicle;

                    generalVehicleDetails.ModelName = getUserVehicleModelName();

                    generalVehicleDetails.WheelsManufacturer = getWheelsManufacturer();

                    switch (TypeOfVehicle)
                    {
                        case eVehicleType.FuelCar:
                        case eVehicleType.ElectricCar:
                            generalVehicleDetails.CurrentAirPressureInWheels =
                                getCurrentAirPressure(Car.sr_MaximumAirPressure);
                            specificVehicleDetails.CarColor = getCarColor();
                            specificVehicleDetails.HowManyDoors = getHowManyDoors();
                            break;
                        case eVehicleType.FuelMotorCycle:
                        case eVehicleType.ElectricMotorCycle:
                            generalVehicleDetails.CurrentAirPressureInWheels =
                                getCurrentAirPressure(MotorCycle.sr_MaximumAirPressure);
                            specificVehicleDetails.MotorCycleLicenseType = getMotorcycleLicenseType();
                            specificVehicleDetails.EngineCapacity = getMotorcycleEngineCapacity();
                            break;
                        case eVehicleType.Truck:
                            generalVehicleDetails.CurrentAirPressureInWheels =
                                getCurrentAirPressure(Truck.sr_MaximumAirPressure);
                            specificVehicleDetails.IsCoolingPossible =
                                checkIfTruckCoolingPossible();
                            specificVehicleDetails.CargoVolume = getCargoVolume();
                            break;
                    }

                    switch (TypeOfVehicle)
                    {
                        case eVehicleType.FuelCar:
                            generalVehicleDetails.CurrentEnergyLeftInEngine = getCurrentFuelLevel(Car.sr_FuelTankCapacity);
                            break;
                        case eVehicleType.FuelMotorCycle:
                            generalVehicleDetails.CurrentEnergyLeftInEngine = getCurrentFuelLevel(MotorCycle.sr_FuelTankCapacity);
                            break;
                        case eVehicleType.Truck:
                            generalVehicleDetails.CurrentEnergyLeftInEngine = getCurrentFuelLevel(Truck.sr_FuelTankCapacity);
                            break;

                        case eVehicleType.ElectricCar:
                            generalVehicleDetails.CurrentEnergyLeftInEngine = getCurrentBatteryHoursLeft(Car.sr_MaximumBatteryTime);
                            break;
                        case eVehicleType.ElectricMotorCycle:
                            generalVehicleDetails.CurrentEnergyLeftInEngine = getCurrentBatteryHoursLeft(MotorCycle.sr_MaximumBatteryTime);
                            break;
                    }

                    isFinishedProvidingDetails = true;
                    m_Garage.AddNewVehicleToTheGarage(generalVehicleDetails, specificVehicleDetails);
                    Console.WriteLine("Your vehicle was added to the garage!");
                    Console.WriteLine("Press any key to go back to the main menu...");
                    Console.ReadLine();
                    Console.Clear();
                }
                catch (FormatException i_FormatException)
                {
                    Console.WriteLine(i_FormatException.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        public bool UsersOptionValidationCheck(string userInput)
        {
            bool returnAnswer = false;
            if (int.TryParse(userInput, out int optinPicked))
            {
                if (optinPicked >= 1 && optinPicked <= 8)
                {
                    returnAnswer = true;
                }
            }
            else
            {
                return returnAnswer;
            }

            return returnAnswer;
        }

        private string getUserName()
        {
            Console.Write("Please enter your name: ");
            return Console.ReadLine();
        }

        private string getUserPhoneNumber()
        {
            bool isInputValid = false;
            string ownerPhoneNumber = string.Empty;

            do
            {
                try
                {
                    Console.Write("Please enter your phone number: ");
                    ownerPhoneNumber = getOwnerPhoneNumberInput();
                    isInputValid = true;
                }
                catch (NullReferenceException i_NullReferenceException)
                {
                    Console.WriteLine("----- NullReferenceException error occurred -----");
                    Console.WriteLine(i_NullReferenceException.Message);
                    Console.WriteLine("");
                }
                catch (FormatException i_FormatException)
                {
                    Console.WriteLine("----- FormatException error occurred -----");
                    Console.WriteLine(i_FormatException.Message);
                    Console.WriteLine("");
                }
                catch (Exception i_Exception)
                {
                    Console.WriteLine("----- Unknown Exception error occurred -----");
                    Console.WriteLine(i_Exception.Message);
                    Console.WriteLine("");
                }
            }
            while (isInputValid == false);

            return ownerPhoneNumber;
        }

        private string getOwnerPhoneNumberInput()
        {
            string ownerPhoneNumber = Console.ReadLine();

            if (string.IsNullOrEmpty(ownerPhoneNumber))
            {
                throw new NullReferenceException("Phone number can't be empty!");
            }

            if (!isStringIsOnlyIntegers(ownerPhoneNumber))
            {
                throw new FormatException("Phone number can't contain non-digit characters. Try again");
            }

            return ownerPhoneNumber;
        }

        private bool isStringIsOnlyIntegers(string i_StringToCheck)
        {
            bool v_IsOnlyIntegers = true;

            foreach (char ch in i_StringToCheck)
            {
                if (!char.IsDigit(ch))
                {
                    v_IsOnlyIntegers = false;
                    break;
                }
            }

            return v_IsOnlyIntegers;
        }

        private string getOwnerVehicleLicenseNumber()
        {
            bool isInputValid = false;
            string ownerLicenseNumber = string.Empty;

            do
            {
                try
                {
                    Console.Write("Please enter vehicle license number: ");
                    ownerLicenseNumber = getVehicleLicenseNumberFromUser();

                    if (IsLicenseNumberAlreadyExist(ownerLicenseNumber))
                    {
                        Console.WriteLine("This license number already exists in the garage. Vehicle moved back to service.");
                        m_Garage.ChangeConditionOfVehicle(ownerLicenseNumber, eVehicleCondition.Repaired);
                        BackToMainMenu();
                    }

                    isInputValid = true;
                }
                catch (NullReferenceException i_NullReferenceException)
                {
                    Console.WriteLine("----- NullReferenceException error occurred -----");
                    Console.WriteLine(i_NullReferenceException.Message);
                    Console.WriteLine("");
                }
                catch (FormatException i_FormatException)
                {
                    Console.WriteLine("----- FormatException error occurred -----");
                    Console.WriteLine(i_FormatException.Message);
                    Console.WriteLine("");
                }
                catch (Exception i_Exception)
                {
                    Console.WriteLine("----- Unknown Exception error occurred -----");
                    Console.WriteLine(i_Exception.Message);
                    Console.WriteLine("");
                }
            }
            while (!isInputValid);

            return ownerLicenseNumber;
        }

        private string getVehicleLicenseNumberFromUser()
        {
            string VehicleLicenseNumber = Console.ReadLine();

            if (string.IsNullOrEmpty(VehicleLicenseNumber))
            {
                throw new NullReferenceException("License number can't be empty!");
            }

            return VehicleLicenseNumber;
        }

        private bool IsLicenseNumberAlreadyExist(string i_UserInputLicenseNumber)
        {
            return m_Garage.IsVehicleExistInGarage(i_UserInputLicenseNumber);
        }

        private void BackToMainMenu()
        {
            Console.Write("Press any key to return to the main menu...");
            Console.ReadLine();
            Console.Clear();
            ShowMenu();
        }

        private eVehicleType getVehicleType()
        {
            bool isInputValid = false;
            eVehicleType vehicleType = (eVehicleType)1;
            string getVehicleTypeMessage = string.Format(@"Please choose the type of the vehicle from the following options:
                1 - Fuel Car
                2 - Electric Car
                3 - Fuel Motorcycle
                4 - Electric Motorcycle
                5 - Truck");
            Console.WriteLine(getVehicleTypeMessage);

            while (isInputValid == false)
            {
                try
                {
                    int vehicleTypeInputOption = getUserInputForVehicleType();
                    isInputValid = true;

                    switch (vehicleTypeInputOption)
                    {
                        case 1:
                            vehicleType = (eVehicleType)1; // FuelCar
                            break;
                        case 2:
                            vehicleType = (eVehicleType)2; // ElectricCar
                            break;
                        case 3:
                            vehicleType = (eVehicleType)3; // FuelMotorCycle
                            break;
                        case 4:
                            vehicleType = (eVehicleType)4; // ElectricMotorCycle
                            break;
                        case 5:
                            vehicleType = (eVehicleType)5; // Truck
                            break;
                    }
                }
                catch (FormatException i_FormatException)
                {
                    Console.WriteLine("----- FormatException error occurred -----");
                    Console.WriteLine(i_FormatException.Message);
                    Console.Write("Please enter a valid number: ");
                }
                catch (NullReferenceException i_NullReferenceException)
                {
                    Console.WriteLine("----- NullReferenceException error occurred -----");
                    Console.WriteLine(i_NullReferenceException.Message);
                    Console.Write("Please enter a valid number: ");
                }
                catch (ValueOutOfRangeException i_valueOutOfRangeException)
                {
                    Console.WriteLine("----- ValueOutOfRangeException error occurred -----");
                    Console.WriteLine(i_valueOutOfRangeException.Message);
                    Console.Write("Please enter a valid number: ");
                }
                catch (Exception i_Exception)
                {
                    Console.WriteLine("----- UnknownException error occurred -----");
                    Console.WriteLine(i_Exception.Message);
                }
            }

            return vehicleType;
        }

        private int getUserInputForVehicleType()
        {
            string userInput = Console.ReadLine();

            if (string.IsNullOrEmpty(userInput))
            {
                throw new NullReferenceException();
            }

            if (!int.TryParse(userInput, out int chosenOption))
            {
                throw new FormatException("Input must be an integer. Try again");
            }

            if (!(chosenOption >= 1 && chosenOption <= 5))
            {
                throw new ValueOutOfRangeException(new Exception(), chosenOption, 1, 5);
            }

            return chosenOption;
        }

        private string getUserVehicleModelName()
        {
            Console.Write("Please enter the name of the vehicle model: ");
            return Console.ReadLine();
        }

        private string getWheelsManufacturer()
        {
            Console.Write("Please enter the name of the vehicle's wheels manufacturer: ");
            return Console.ReadLine();
        }

        private float getCurrentAirPressure(float i_VehicleMaximalAirPressure)
        {
            bool isInputValid = false;
            float currentAirPressure = 0;

            do
            {
                try
                {
                    Console.Write("Please enter the current air pressure of the wheels: ");
                    currentAirPressure = getCurrentAirPressureInput(i_VehicleMaximalAirPressure);
                    isInputValid = true;
                }
                catch (NullReferenceException i_NullReferenceException)
                {
                    Console.WriteLine("----- NullReferenceException error occurred -----");
                    Console.WriteLine(i_NullReferenceException.Message);
                    Console.WriteLine("");
                }
                catch (FormatException i_FormatException)
                {
                    Console.WriteLine("----- FormatException error occurred -----");
                    Console.WriteLine(i_FormatException.Message);
                    Console.WriteLine("");
                }
                catch (ValueOutOfRangeException i_ValueOutOfRangeException)
                {
                    Console.WriteLine("----- ValueOutOfRangeException error occurred -----");
                    Console.WriteLine(i_ValueOutOfRangeException.Message);
                    Console.WriteLine("");
                }
                catch (Exception i_Exception)
                {
                    Console.WriteLine("----- Unknown Exception error occurred -----");
                    Console.WriteLine(i_Exception.Message);
                    Console.WriteLine("");
                }
            }
            while (!isInputValid);

            return currentAirPressure;
        }

        private float getCurrentAirPressureInput(float i_VehicleMaximalAirPressure)
        {
            string currentAirPressureInput = Console.ReadLine();

            if (string.IsNullOrEmpty(currentAirPressureInput))
            {
                throw new NullReferenceException("Air pressure number can't be empty!");
            }

            if (!float.TryParse(currentAirPressureInput, out float currentAirPressure))
            {
                throw new FormatException("Air pressure must be a real number!");
            }

            if (!(currentAirPressure >= 1 && currentAirPressure <= i_VehicleMaximalAirPressure))
            {
                throw new ValueOutOfRangeException(new Exception(), currentAirPressure, 1, i_VehicleMaximalAirPressure);
            }

            return currentAirPressure;
        }

        private eCarColor getCarColor()
        {
            bool isInputValid = false;
            int selectedOption = 0;
            string getColorMessage = string.Format(
                @"Please select you car color:
                1 - White
                2 - Gray
                3 - Black
                4 - Blue");
            Console.WriteLine(getColorMessage);
            do
            {
                try
                {
                    selectedOption = userSelectedOptionToInteger(1, 4);
                    isInputValid = true;
                }
                catch (FormatException i_FormatException)
                {
                    Console.WriteLine("----- FormatException error occurred -----");
                    Console.WriteLine(i_FormatException.Message);
                    Console.Write("Please enter a valid number: ");
                }
                catch (ValueOutOfRangeException i_valueOutOfRangeException)
                {
                    Console.WriteLine("----- ValueOutOfRange error occurred -----");
                    Console.WriteLine(i_valueOutOfRangeException.Message);
                    Console.Write("Please enter a valid number within the range: ");
                }
                catch (Exception i_Exception)
                {
                    Console.WriteLine("----- Unknown error occurred -----");
                    Console.WriteLine(i_Exception.Message);
                }
            }
            while (!isInputValid);

            return (eCarColor)selectedOption;
        }

        private int userSelectedOptionToInteger(int i_MinValue, int i_MaxValue)
        {
            string userInput = Console.ReadLine();

            if (!int.TryParse(userInput, out int optionInteger))
            {
                throw new FormatException("Wrong format input. Input format is not an integer.");
            }

            if (!isUserInputOptionIsValid(optionInteger, i_MinValue, i_MaxValue))
            {
                throw new ValueOutOfRangeException(new Exception(), optionInteger, i_MinValue, i_MaxValue);
            }

            return optionInteger;
        }

        private bool isUserInputOptionIsValid(int i_UserInputOption, int i_MinValue, int i_MaxValue)
        {
            return i_UserInputOption >= i_MinValue && i_UserInputOption <= i_MaxValue;
        }

        private int getHowManyDoors()
        {
            string getAmountOfDoorsMessage = "Please select how many doors your car has: ";
            Console.WriteLine(getAmountOfDoorsMessage);
            string stringDoorsInput = Console.ReadLine();
            while (!HowManyDoorsValidationChecck(stringDoorsInput))
            {
                Console.WriteLine("Invalid input. Between 2 to 5 only. Try again.");
                stringDoorsInput = Console.ReadLine();
            }
            if (int.TryParse(stringDoorsInput, out int doorsInCar))
            {
                return doorsInCar;
            }
            else
            {
                Console.WriteLine("Invalid input");

                return -1;
            }
        }

        public bool HowManyDoorsValidationChecck(string i_UserInput)
        {
            if (int.TryParse(i_UserInput, out int amountOfDoors))
            {
                if (amountOfDoors >= 2 && amountOfDoors <= 5)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private eMotorCycleLicenseType getMotorcycleLicenseType()
        {
            bool isInputValid = false;
            int selectedOption = 0;
            string getLicenseTypeMsg = string.Format(
                @"Please select you motorcycle license type:
                1 - A
                2 - AA
                3 - B1
                4 - BB");
            Console.WriteLine(getLicenseTypeMsg);

            do
            {
                try
                {
                    selectedOption = userSelectedOptionToInteger(1, 4);
                    isInputValid = true;
                }
                catch (FormatException i_FormatException)
                {
                    Console.WriteLine("----- FormatException error occurred -----");
                    Console.WriteLine(i_FormatException.Message);
                    Console.Write("Please enter a valid number: ");
                }
                catch (ValueOutOfRangeException i_valueOutOfRangeException)
                {
                    Console.WriteLine("----- ValueOutOfRange error occurred -----");
                    Console.WriteLine(i_valueOutOfRangeException.Message);
                    Console.Write("Please enter a valid number within the range: ");
                }
                catch (Exception i_Exception)
                {
                    Console.WriteLine("----- Unknown error occurred -----");
                    Console.WriteLine(i_Exception.Message);
                }
            }
            while (!isInputValid);

            return (eMotorCycleLicenseType)selectedOption;
        }

        private int getMotorcycleEngineCapacity()
        {
            bool isInputValid = false;
            int motorCycleEngineCapacity = 0;

            do
            {
                try
                {
                    Console.Write("Please enter your motorcycle engine capacity: ");
                    motorCycleEngineCapacity = getMotorcycleEngineCapacityInput();
                    isInputValid = true;
                }
                catch (NullReferenceException i_NullReferenceException)
                {
                    Console.WriteLine("----- NullReferenceException error occurred -----");
                    Console.WriteLine(i_NullReferenceException.Message);
                    Console.WriteLine("");
                }
                catch (FormatException i_FormatException)
                {
                    Console.WriteLine("----- FormatException error occurred -----");
                    Console.WriteLine(i_FormatException.Message);
                    Console.Write("Please enter a valid number: ");
                }
                catch (ValueOutOfRangeException i_valueOutOfRangeException)
                {
                    Console.WriteLine("----- ValueOutOfRange error occurred -----");
                    Console.WriteLine(i_valueOutOfRangeException.Message);
                    Console.Write("Please enter a valid number within the range: ");
                }
                catch (Exception i_Exception)
                {
                    Console.WriteLine("----- Unknown error occurred -----");
                    Console.WriteLine(i_Exception.Message);
                }
            }
            while (!isInputValid);

            return motorCycleEngineCapacity;
        }

        private int getMotorcycleEngineCapacityInput()
        {
            string motorCycleEngineCapacityInput = Console.ReadLine();

            if (string.IsNullOrEmpty(motorCycleEngineCapacityInput))
            {
                throw new NullReferenceException("Motorcycle engine capacity can't be empty!");
            }

            if (!int.TryParse(motorCycleEngineCapacityInput, out int motorCycleEngineCapacity))
            {
                throw new FormatException("Wrong format input. Input format needs to be only numbers.");
            }

            return motorCycleEngineCapacity;
        }

        private bool checkIfTruckCoolingPossible()
        {
            bool isInputValid = false;
            bool isTruckCoolingPossible = false;

            do
            {
                try
                {
                    Console.WriteLine("Does the truck can transport refrigerated content? Y/N");
                    isTruckCoolingPossible = checkIfTruckCoolingPossibleInput();
                    isInputValid = true;
                }
                catch (NullReferenceException i_NullReferenceException)
                {
                    Console.WriteLine("----- NullReferenceException error occurred -----");
                    Console.WriteLine(i_NullReferenceException.Message);
                    Console.WriteLine("");
                }
                catch (FormatException i_FormatException)
                {
                    Console.WriteLine("----- FormatException error occurred -----");
                    Console.WriteLine(i_FormatException.Message);
                    Console.WriteLine("");
                }
            }
            while (!isInputValid);

            return isTruckCoolingPossible;
        }

        private bool checkIfTruckCoolingPossibleInput()
        {
            string isTruckCoolingPossibleInput = Console.ReadLine();
            bool isTruckCoolingPossible = true;

            if (string.IsNullOrEmpty(isTruckCoolingPossibleInput))
            {
                throw new NullReferenceException("Input can't be empty!");
            }

            if (isTruckCoolingPossibleInput != "Y" && isTruckCoolingPossibleInput != "y"
                                                            && isTruckCoolingPossibleInput != "N"
                                                            && isTruckCoolingPossibleInput != "n")
            {
                throw new FormatException("Input must be Y/N!");
            }

            if (isTruckCoolingPossibleInput == "N" || isTruckCoolingPossibleInput == "n")
            {
                isTruckCoolingPossible = false;
            }

            return isTruckCoolingPossible;
        }

        private float getCargoVolume()
        {
            bool isInputValid = false;
            float maximalCargoWeight = 0;

            do
            {
                try
                {
                    Console.Write("Please enter your truck cargo volume: ");
                    maximalCargoWeight = getCargoVolumeInput();
                    isInputValid = true;
                }
                catch (NullReferenceException i_NullReferenceException)
                {
                    Console.WriteLine("----- NullReferenceException error occurred -----");
                    Console.WriteLine(i_NullReferenceException.Message);
                    Console.WriteLine("");
                }
                catch (FormatException i_FormatException)
                {
                    Console.WriteLine("----- FormatException error occurred -----");
                    Console.WriteLine(i_FormatException.Message);
                    Console.Write("Please enter a valid number: ");
                }
                catch (Exception i_Exception)
                {
                    Console.WriteLine("----- Unknown error occurred -----");
                    Console.WriteLine(i_Exception.Message);
                }
            }
            while (!isInputValid);

            return maximalCargoWeight;
        }

        private int getCargoVolumeInput()
        {
            string CargoVolumeInput = Console.ReadLine();

            if (string.IsNullOrEmpty(CargoVolumeInput))
            {
                throw new NullReferenceException("Maximal cargo weight can't be empty!");
            }

            if (!int.TryParse(CargoVolumeInput, out int CargoVolumeInt))
            {
                throw new FormatException("Wrong format input. Input format needs to be only numbers.");
            }

            return CargoVolumeInt;
        }

        private float getCurrentFuelLevel(float i_VehicleMaximalFuelTankCapacity)
        {
            bool isInputValid = false;
            float currentFuelLevel = 0;

            do
            {
                try
                {
                    Console.Write("Please enter the current fuel level: ");
                    currentFuelLevel = getCurrentFuelLevelInput(i_VehicleMaximalFuelTankCapacity);
                    isInputValid = true;
                }
                catch (NullReferenceException i_NullReferenceException)
                {
                    Console.WriteLine("----- NullReferenceException error occurred -----");
                    Console.WriteLine(i_NullReferenceException.Message);
                    Console.WriteLine("");
                }
                catch (FormatException i_FormatException)
                {
                    Console.WriteLine("----- FormatException error occurred -----");
                    Console.WriteLine(i_FormatException.Message);
                    Console.WriteLine("");
                }
                catch (ValueOutOfRangeException i_ValueOutOfRangeException)
                {
                    Console.WriteLine("----- ValueOutOfRangeException error occurred -----");
                    Console.WriteLine(i_ValueOutOfRangeException.Message);
                    Console.WriteLine("");
                }
                catch (Exception i_Exception)
                {
                    Console.WriteLine("----- Unknown Exception error occurred -----");
                    Console.WriteLine(i_Exception.Message);
                    Console.WriteLine("");
                }
            }
            while (!isInputValid);

            return currentFuelLevel;
        }

        private float getCurrentFuelLevelInput(float i_VehicleMaximalFuelTankCapacity)
        {
            string currentFuelLevelInput = Console.ReadLine();

            if (string.IsNullOrEmpty(currentFuelLevelInput))
            {
                throw new NullReferenceException("Fuel level input can't be empty!");
            }

            if (!float.TryParse(currentFuelLevelInput, out float currentFuelLevel))
            {
                throw new FormatException("Fuel level must be a real number!");
            }

            if (!(currentFuelLevel >= 0 && currentFuelLevel <= i_VehicleMaximalFuelTankCapacity))
            {
                throw new ValueOutOfRangeException(new Exception(), currentFuelLevel, 0, i_VehicleMaximalFuelTankCapacity);
            }

            return currentFuelLevel;
        }

        private float getCurrentBatteryHoursLeft(float i_VehicleMaximalBatteryHours)
        {
            bool isInputValid = false;
            float currentBatteryHoursLeft = 0;

            do
            {
                try
                {
                    Console.Write("Please enter the current battery hours left: ");
                    currentBatteryHoursLeft = getCurrentBatteryHoursLeftInput(i_VehicleMaximalBatteryHours);
                    isInputValid = true;
                }
                catch (NullReferenceException i_NullReferenceException)
                {
                    Console.WriteLine("----- NullReferenceException error occurred -----");
                    Console.WriteLine(i_NullReferenceException.Message);
                    Console.WriteLine("");
                }
                catch (FormatException i_FormatException)
                {
                    Console.WriteLine("----- FormatException error occurred -----");
                    Console.WriteLine(i_FormatException.Message);
                    Console.WriteLine("");
                }
                catch (ValueOutOfRangeException i_ValueOutOfRangeException)
                {
                    Console.WriteLine("----- ValueOutOfRangeException error occurred -----");
                    Console.WriteLine(i_ValueOutOfRangeException.Message);
                    Console.WriteLine("");
                }
                catch (Exception i_Exception)
                {
                    Console.WriteLine("----- Unknown Exception error occurred -----");
                    Console.WriteLine(i_Exception.Message);
                    Console.WriteLine("");
                }
            }
            while (!isInputValid);

            return currentBatteryHoursLeft;
        }

        private float getCurrentBatteryHoursLeftInput(float i_VehicleMaximalBatteryHours)
        {
            string currentBatteryHoursLeftInput = Console.ReadLine();

            if (string.IsNullOrEmpty(currentBatteryHoursLeftInput))
            {
                throw new NullReferenceException("Fuel level input can't be empty!");
            }

            if (!float.TryParse(currentBatteryHoursLeftInput, out float currentBatteryHoursLeft))
            {
                throw new FormatException("Fuel level must be a real number!");
            }

            if (!(currentBatteryHoursLeft >= 0 && currentBatteryHoursLeft <= i_VehicleMaximalBatteryHours))
            {
                throw new ValueOutOfRangeException(new Exception(), currentBatteryHoursLeft, 0, i_VehicleMaximalBatteryHours);
            }

            return currentBatteryHoursLeft;
        }

        private void showVehichlesInGarageByLicense(List<Vehicle> i_Garage)
        {
            bool isFinished = false;
            bool isInputValid = false;

            while (!isFinished)
            {
                Console.WriteLine("===== Show current vehicles in garage =====");
                string optionsMessage = string.Format(
                    @"Choose the option to filter by:
0 - All     1 - In Service     2 - Repaired     3 - Paid");
                Console.WriteLine(optionsMessage);
                while (!isInputValid)
                {
                    try
                    {
                        int option = userSelectedOptionToInteger(0, 3);
                        isInputValid = true;
                        printVehicleInGarageByCondition(i_Garage, option);
                    }
                    catch (FormatException i_FormatException)
                    {
                        Console.WriteLine("----- FormatException error occurred -----");
                        Console.WriteLine(i_FormatException.Message);
                        Console.Write("Please enter a valid number: ");
                    }
                    catch (ValueOutOfRangeException i_valueOutOfRangeException)
                    {
                        Console.WriteLine("----- ValueOutOfRange error occurred -----");
                        Console.WriteLine(i_valueOutOfRangeException.Message);
                        Console.Write("Please enter a valid number within the range: ");
                    }
                    catch (Exception i_Exception)
                    {
                        Console.WriteLine("----- Unknown error occurred -----");
                        Console.WriteLine(i_Exception.Message);
                    }
                }

                Console.WriteLine("Press any key to go back to main menu...");
                Console.ReadLine();
                Console.Clear();
                isFinished = true;
            }
        }

        private void printVehicleInGarageByCondition(List<Vehicle> i_Garage, int i_UserSelectedOption)
        {
            if (m_Garage.IsGarageEmpty())
            {
                Console.WriteLine("Garage is empty!");
            }
            else
            {
                if (i_UserSelectedOption == 0)
                {
                    Console.WriteLine("Vehicle License Number  |   Condition");

                    foreach (Vehicle vehicle in i_Garage)
                    {
                        string format = string.Format(
                            @"      {0}               |          {1}",
                            vehicle.LicenseNumber,
                            vehicle.Condition);
                        Console.WriteLine(format);
                    }
                }
                else
                {
                    if (m_Garage.isNoVehiclesWithGivenConditionInTheGarage((eVehicleCondition)i_UserSelectedOption))
                    {
                        string noVehiclesMessag = string.Format(
                            @"No vehicles with this Condition: {0} in the garage",
                            (eVehicleCondition)i_UserSelectedOption);
                        Console.WriteLine(noVehiclesMessag);
                    }
                    else
                    {
                        List<Vehicle> vehiclesByCondition =
                            m_Garage.GetVehiclesFromGarageByCondition((eVehicleCondition)i_UserSelectedOption);

                        Console.WriteLine("Vehicle License Number  |   Condition");

                        foreach (Vehicle vehicle in vehiclesByCondition)
                        {
                            string format = string.Format(
                                @"      {0}           |  {1}",
                                vehicle.LicenseNumber,
                                vehicle.Condition);
                            Console.WriteLine(format);
                        }
                    }
                }
            }
        }

        private void changeVehicleStatus()
        {
            bool isFinished = false;
            bool isVehicleLicenseInputValid = false;

            while (!isFinished)
            {
                Console.WriteLine("===== Change a vehicle's Condition status =====");
                Console.Write("Please enter the desired vehicle license number: ");

                while (!isVehicleLicenseInputValid)
                {
                    try
                    {
                        string licenseNumber = getVehicleLicenseNumberFromUser();
                        printCurrentVehicleConditionStatus(licenseNumber);
                        changeChosenVehicleConditionStatus(licenseNumber);
                        isVehicleLicenseInputValid = true;
                    }
                    catch (FormatException i_FormatException)
                    {
                        Console.WriteLine("----- FormatException error occurred -----");
                        Console.WriteLine(i_FormatException.Message);
                        break;
                    }
                    catch (ArgumentException i_ArgumentException)
                    {
                        Console.WriteLine("----- ArgumentException error occurred -----");
                        Console.WriteLine(i_ArgumentException.Message);
                        break;
                    }
                    catch (Exception i_Exception)
                    {
                        Console.WriteLine("----- Unknown error occurred -----");
                        Console.WriteLine(i_Exception.Message);
                        break;
                    }
                }

                isFinished = true;
                Console.WriteLine("Press any key to go back to main menu...");
                Console.ReadLine();
                Console.Clear();
            }
        }

        private void changeChosenVehicleConditionStatus(string i_LicenseNumber)
        {
            bool isFinished = false;
            bool isOptionInputValid = false;

            while (!isFinished)
            {
                Console.WriteLine(@"Please choose the new condition status for the vehicle:
UnderRepair = 1     Repaired = 2    Paid = 3,");

                while (!isOptionInputValid)
                {
                    try
                    {
                        int option = userSelectedOptionToInteger(1, 3);
                        isOptionInputValid = true;
                        m_Garage.ChangeConditionOfVehicle(i_LicenseNumber, (eVehicleCondition)option);
                        string afterChangeMsg = string.Format(
                            @"Vehicle number {0} condition status has changed to {1}.", i_LicenseNumber, (eVehicleCondition)option);
                        Console.WriteLine(afterChangeMsg);
                    }
                    catch (FormatException i_FormatException)
                    {
                        Console.WriteLine("----- FormatException error occurred -----");
                        Console.WriteLine(i_FormatException.Message);
                        Console.Write("Please enter a valid number: ");
                    }
                    catch (ArgumentException i_ArgumentException)
                    {
                        Console.WriteLine("----- ArgumentException error occurred -----");
                        Console.WriteLine(i_ArgumentException.Message);
                    }
                    catch (ValueOutOfRangeException i_valueOutOfRangeException)
                    {
                        Console.WriteLine("----- ValueOutOfRange error occurred -----");
                        Console.WriteLine(i_valueOutOfRangeException.Message);
                        Console.Write("Please enter a valid number within the range: ");
                    }
                    catch (Exception i_Exception)
                    {
                        Console.WriteLine("----- Unknown error occurred -----");
                        Console.WriteLine(i_Exception.Message);
                    }
                }

                isFinished = true;
            }
        }

        private void printCurrentVehicleConditionStatus(string i_LicenseNumber)
        {
            string statusMessage = string.Format(
                @"The current status of the vehicle with the license number of {0} is: {1}.", i_LicenseNumber,
                m_Garage.GetVehicleByLicenseNumber(i_LicenseNumber).Condition);

            Console.WriteLine(statusMessage);
        }

        private void inflateTiresToMax()
        {
            bool isFinished = false;
            bool isInputValid = false;

            while (!isFinished)
            {
                Console.WriteLine("===== Inflate Vehicle wheels To Max =====");

                while (!isInputValid)
                {
                    try
                    {
                        Console.Write("Enter the desired vehicle to inflate wheels to max: ");
                        string licenseNumberInput = getVehicleLicenseNumberFromUser();
                        m_Garage.InflateWheelsToMax(licenseNumberInput);
                        Console.WriteLine("All wheels inflated to max air pressure!");
                        isInputValid = true;
                    }
                    catch (FormatException i_FormatException)
                    {
                        Console.WriteLine("----- FormatException error occurred -----");
                        Console.WriteLine(i_FormatException.Message);
                        break;
                    }
                    catch (ArgumentException i_ArgumentException)
                    {
                        Console.WriteLine("----- ArgumentException error occurred -----");
                        Console.WriteLine(i_ArgumentException.Message);
                        break;
                    }
                    catch (NullReferenceException i_NullReferenceException)
                    {
                        Console.WriteLine("----- NullReferenceException error occurred -----");
                        Console.WriteLine(i_NullReferenceException.Message);
                        break;
                    }
                    catch (Exception i_Exception)
                    {
                        Console.WriteLine("----- Unknown error occurred -----");
                        Console.WriteLine(i_Exception.Message);
                        break;
                    }
                }

                isFinished = true;
                Console.WriteLine("Press any key to go back to main menu...");
                Console.ReadLine();
                Console.Clear();
            }
        }

        private void refuelVehicle()
        {
            bool isFinished = false;
            bool isInputValid = false;
            const bool v_IsFuelEngine = true;

            while (!isFinished)
            {
                Console.WriteLine("===== Refuel Vehicle =====");

                while (!isInputValid)
                {
                    try
                    {
                        Console.WriteLine("Enter the desired vehicle license number to refuel: ");
                        string licenseNumberInput = getVehicleLicenseNumberFromUser();
                        eFuelType fuelTypeToRefuel = getUserInputForFuelType();
                        float amountOfFuelToAdd = getUserInputForAmountOfEnergyToAdd(v_IsFuelEngine);
                        m_Garage.FuelVehicle(licenseNumberInput, fuelTypeToRefuel, amountOfFuelToAdd);
                        Console.WriteLine("Vehicle with license {0} have been refueled!", licenseNumberInput);
                        isInputValid = true;
                    }
                    catch (FormatException i_FormatException)
                    {
                        Console.WriteLine("----- FormatException error occurred -----");
                        Console.WriteLine(i_FormatException.Message);
                        break;
                    }
                    catch (ArgumentException i_ArgumentException)
                    {
                        Console.WriteLine("----- ArgumentException error occurred -----");
                        Console.WriteLine(i_ArgumentException.Message);
                        break;
                    }
                    catch (ValueOutOfRangeException i_valueOutOfRangeException)
                    {
                        Console.WriteLine("----- ValueOutOfRange error occurred -----");
                        Console.WriteLine(i_valueOutOfRangeException.Message);
                        break;
                    }
                    catch (NullReferenceException i_NullReferenceException)
                    {
                        Console.WriteLine("----- NullReferenceException error occurred -----");
                        Console.WriteLine(i_NullReferenceException.Message);
                        break;
                    }
                    catch (Exception i_Exception)
                    {
                        Console.WriteLine("----- Unknown error occurred -----");
                        Console.WriteLine(i_Exception.Message);
                        break;
                    }
                }

                isFinished = true;
                Console.WriteLine("Press any key to go back to main menu...");
                Console.ReadLine();
                Console.Clear();
            }
        }

        private eFuelType getUserInputForFuelType()
        {
            bool isInputValid = false;
            bool isFinished = false;
            int option = 0;

            while (!isFinished)
            {
                Console.WriteLine(@"Choose the fuel type for the vehicle:
                Soler = 1,
                Octan95 = 2,
                Octan96 = 3,
                Octan98 = 4");

                while (!isInputValid)
                {
                    try
                    {
                        option = userSelectedOptionToInteger(1, 4);
                        isInputValid = true;
                    }
                    catch (FormatException i_FormatException)
                    {
                        Console.WriteLine("----- FormatException error occurred -----");
                        Console.WriteLine(i_FormatException.Message);
                        Console.Write("Please enter a valid number: ");
                    }
                    catch (ArgumentException i_ArgumentException)
                    {
                        Console.WriteLine("----- ArgumentException error occurred -----");
                        Console.WriteLine(i_ArgumentException.Message);
                    }
                    catch (ValueOutOfRangeException i_valueOutOfRangeException)
                    {
                        Console.WriteLine("----- ValueOutOfRange error occurred -----");
                        Console.WriteLine(i_valueOutOfRangeException.Message);
                        Console.Write("Please enter a valid number within the range: ");
                    }
                    catch (Exception i_Exception)
                    {
                        Console.WriteLine("----- Unknown error occurred -----");
                        Console.WriteLine(i_Exception.Message);
                    }
                }

                isFinished = true;
            }

            return (eFuelType)option;
        }

        private float getUserInputForAmountOfEnergyToAdd(bool i_IsFuelEngine)
        {
            bool isInputValid = false;
            bool isFinished = false;
            float amountOfEnergyToAdd = 0;

            while (!isFinished)
            {
                Console.WriteLine(i_IsFuelEngine ? "Choose the amount of fuel to add:" : "Choose the amount of battery hours to add:");

                while (!isInputValid)
                {
                    try
                    {
                        amountOfEnergyToAdd = getAmountOfEnergyToAddInput(i_IsFuelEngine);
                        isInputValid = true;
                    }
                    catch (FormatException i_FormatException)
                    {
                        Console.WriteLine("----- FormatException error occurred -----");
                        Console.WriteLine(i_FormatException.Message);
                        Console.Write("Please enter a valid number: ");
                    }
                    catch (NullReferenceException i_NullReferenceException)
                    {
                        Console.WriteLine("----- NullReferenceException error occurred -----");
                        Console.WriteLine(i_NullReferenceException.Message);
                        Console.Write("Please enter a valid number: ");
                    }
                }

                isFinished = true;
            }

            return amountOfEnergyToAdd;
        }

        private float getAmountOfEnergyToAddInput(bool i_IsFuelEngine)
        {
            string userInput = Console.ReadLine();

            if (string.IsNullOrEmpty(userInput))
            {
                string nulRefMsg =
                    i_IsFuelEngine ? "Amount of fuel can't be empty!" : "Amount of hours can't be empty!";
                throw new NullReferenceException(nulRefMsg);
            }

            if (!float.TryParse(userInput, out float amountOfEnergy))
            {
                string formatExMsg =
                    i_IsFuelEngine ? "Amount of fuel must be a real number!" : "Amount of hours must be a real number!";
                throw new FormatException(formatExMsg);
            }

            return amountOfEnergy;
        }

        private void rechargeVehicle()
        {
            bool isFinished = false;
            bool isInputValid = false;
            const bool v_IsFuelEngine = true;

            while (!isFinished)
            {
                Console.WriteLine("===== Recharge Vehicle =====");

                while (!isInputValid)
                {
                    try
                    {
                        Console.WriteLine("Enter the desired vehicle license number to recharge: ");
                        string licenseNumberInput = getVehicleLicenseNumberFromUser();
                        float amountOfBatteryHoursToAdd = getUserInputForAmountOfEnergyToAdd(!v_IsFuelEngine);
                        m_Garage.RechargeVehicle(licenseNumberInput, amountOfBatteryHoursToAdd);
                        Console.WriteLine("Vehicle with license {0} have been recharged!", licenseNumberInput);
                        isInputValid = true;
                    }
                    catch (FormatException i_FormatException)
                    {
                        Console.WriteLine("----- FormatException error occurred -----");
                        Console.WriteLine(i_FormatException.Message);
                        break;
                    }
                    catch (ArgumentException i_ArgumentException)
                    {
                        Console.WriteLine("----- ArgumentException error occurred -----");
                        Console.WriteLine(i_ArgumentException.Message);
                        break;
                    }
                    catch (ValueOutOfRangeException i_valueOutOfRangeException)
                    {
                        Console.WriteLine("----- ValueOutOfRange error occurred -----");
                        Console.WriteLine(i_valueOutOfRangeException.Message);
                        break;
                    }
                    catch (NullReferenceException i_NullReferenceException)
                    {
                        Console.WriteLine("----- NullReferenceException error occurred -----");
                        Console.WriteLine(i_NullReferenceException.Message);
                        break;
                    }
                    catch (Exception i_Exception)
                    {
                        Console.WriteLine("----- Unknown error occurred -----");
                        Console.WriteLine(i_Exception.Message);
                        break;
                    }
                }

                isFinished = true;
                Console.WriteLine("Press any key to go back to main menu...");
                Console.ReadLine();
                Console.Clear();
            }
        }

        private void showSpecificVehicleDetails()
        {
            bool isFinished = false;
            bool isInputValid = false;

            while (!isFinished)
            {
                Console.WriteLine("===== Show Vehicle Details =====");

                while (!isInputValid)
                {
                    try
                    {
                        Console.Write("Enter the desired vehicle to show details: ");
                        string licenseNumberInput = getVehicleLicenseNumberFromUser();
                        Console.WriteLine(m_Garage.ShowDetails(licenseNumberInput));
                        isInputValid = true;
                    }
                    catch (FormatException i_FormatException)
                    {
                        Console.WriteLine("----- FormatException error occurred -----");
                        Console.WriteLine(i_FormatException.Message);
                        break;
                    }
                    catch (ArgumentException i_ArgumentException)
                    {
                        Console.WriteLine("----- ArgumentException error occurred -----");
                        Console.WriteLine(i_ArgumentException.Message);
                        break;
                    }
                    catch (NullReferenceException i_NullReferenceException)
                    {
                        Console.WriteLine("----- NullReferenceException error occurred -----");
                        Console.WriteLine(i_NullReferenceException.Message);
                        break;
                    }
                    catch (Exception i_Exception)
                    {
                        Console.WriteLine("----- Unknown error occurred -----");
                        Console.WriteLine(i_Exception.Message);
                        break;
                    }
                }

                isFinished = true;
                Console.WriteLine("Press any key to go back to main menu...");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}