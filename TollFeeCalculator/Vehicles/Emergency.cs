namespace TollFeeCalculator
{
    public class Emergency : IVehicle
    {
        public bool isTollFreeVehicle()
        {
            return true;
        }
    }
}