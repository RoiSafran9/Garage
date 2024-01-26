namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        public float m_CurrentEnergyAmount { get; set; }

        public float m_MaximunEnergyAmount { get; set; }

        protected Engine(float i_CurrentEnergyAmount, float i_MaximunEnergyAmount)
        {
            m_CurrentEnergyAmount = i_CurrentEnergyAmount;
            m_MaximunEnergyAmount = i_MaximunEnergyAmount;
        }

        public virtual float GetRemainingPrecentageLeft()
        {
            return 100 * (m_CurrentEnergyAmount / m_MaximunEnergyAmount);

        }
    }
}
