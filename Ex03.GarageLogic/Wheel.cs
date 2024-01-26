namespace Ex03.GarageLogic
{
    using System;

    public class Wheel
    {
        private string m_ManufacturerName;
        private float m_CurrentAirPressure;
        private float m_MaximalAirPressure;

        public string Manufacturer
        {
            get => m_ManufacturerName;
            set => m_ManufacturerName = value;
        }

        public float CurrentAirPressure
        {
            get => m_CurrentAirPressure;
            set => m_CurrentAirPressure = value;
        }

        public float MaximalAirPressure
        {
            get => m_MaximalAirPressure;
            set => m_MaximalAirPressure = value;
        }

        public void Inflate(float i_AmountOfAirToInflate)
        {
            if (i_AmountOfAirToInflate + m_CurrentAirPressure > m_MaximalAirPressure) 
            {
                throw new ValueOutOfRangeException(new Exception(), i_AmountOfAirToInflate, m_MaximalAirPressure - m_CurrentAirPressure, 0);
            }
            else
            {
                m_CurrentAirPressure += i_AmountOfAirToInflate;
            }
        }

        public void InflateToMax()
        {
            m_CurrentAirPressure = m_MaximalAirPressure;
        }
    }
}
