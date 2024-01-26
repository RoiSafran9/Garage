namespace Ex03.GarageLogic
{
    using System;

    public class Battery : Engine
    {
        public float m_CurrentHoursLeftInBattery { get; set; }

        public float m_MaximalHoursOfBattery { get; set; }

        public Battery(float i_HoursLeft, float i_MaximalTime) : base(i_HoursLeft, i_MaximalTime)
        {
            m_CurrentHoursLeftInBattery = i_HoursLeft;
            m_MaximalHoursOfBattery = i_MaximalTime;
        }

        public void Charge(float i_AmountOfHoursToCharge)
        {
            if (i_AmountOfHoursToCharge + m_CurrentHoursLeftInBattery > m_MaximalHoursOfBattery)
            {
                throw new ValueOutOfRangeException(new Exception() ,i_AmountOfHoursToCharge, m_MaximalHoursOfBattery - m_CurrentHoursLeftInBattery, 0);
            }
            else
            {
                m_MaximalHoursOfBattery += i_AmountOfHoursToCharge;
            }
        }

        public override float GetRemainingPrecentageLeft()
        {
            return 100 * (m_CurrentHoursLeftInBattery / m_MaximalHoursOfBattery);

        }
    }
}
