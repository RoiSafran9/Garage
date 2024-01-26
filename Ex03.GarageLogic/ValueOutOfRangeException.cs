namespace Ex03.GarageLogic
{
    using System;

    public class ValueOutOfRangeException : Exception
    {
        public float MaxValue { get; set; }
        public float MinValue { get; set; }

        public ValueOutOfRangeException(Exception i_InException, float i_UsersInput, float i_MinValue, float i_MaxValue)
            : base(string.Format("The amount you entered {0}, is impossible. The range is between {1} to {2}", i_UsersInput, i_MinValue, i_MaxValue))
        {
            MaxValue = i_MaxValue;
            MinValue = i_MinValue;
        }   
    }
}

