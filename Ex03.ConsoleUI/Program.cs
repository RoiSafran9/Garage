namespace Ex03.ConsoleUI
{
    using Ex03.GarageLogic;

    internal class Program
    {
        public static void Main()
        {
            Run();
        }

        public static void Run()
        {
            GarageUI newGarageU = new GarageUI();
            Garage newGarage = new Garage();
            newGarageU.StartGarageUI(newGarage);
        }
    }
}
