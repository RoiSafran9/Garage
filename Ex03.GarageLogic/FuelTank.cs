namespace Ex03.GarageLogic
{
    using System;

    public class FuelTank : Engine
    {
        private eFuelType m_FuelType;
        public float m_CurrentFuleQuantity;
        public float m_MaximumAmountOfFuelPossible;

        public eFuelType TypeOfFuel
        {
            get => m_FuelType;
            set => m_FuelType = value;

        }

        public float CurrentFuleQuantity
        {
            get => m_CurrentFuleQuantity;
            set => m_CurrentFuleQuantity = value;
        }

        public float MaximumAmountOfFuelPossible
        {
            get => m_MaximumAmountOfFuelPossible;
            set => m_MaximumAmountOfFuelPossible = value;
        }

        public FuelTank(eFuelType i_FuelType, float i_CurrentFuleQuantity, float i_MaximumAmountOfFuelPossible) : base(i_CurrentFuleQuantity, i_MaximumAmountOfFuelPossible)
        {
            m_FuelType = i_FuelType;
            m_CurrentFuleQuantity = i_CurrentFuleQuantity;
            m_MaximumAmountOfFuelPossible = i_MaximumAmountOfFuelPossible;
        }

        public void Refuel(eFuelType i_FuelType, float i_AmountOfFuelToAdd)
        {
            if (i_FuelType != m_FuelType)
            {
                throw new ArgumentException("The type of fuel you wish to add is not supported by the current vehicle.");
            }

            if (i_AmountOfFuelToAdd + m_CurrentFuleQuantity > m_MaximumAmountOfFuelPossible)
            {
                throw new ValueOutOfRangeException(new Exception() ,i_AmountOfFuelToAdd, m_MaximumAmountOfFuelPossible - m_CurrentFuleQuantity, 0);
            }
            else
            {
                m_CurrentFuleQuantity += i_AmountOfFuelToAdd;
            }
        }

        public override float GetRemainingPrecentageLeft()
        {
            return 100 * (m_CurrentFuleQuantity / m_MaximumAmountOfFuelPossible);
        }
    }
}
