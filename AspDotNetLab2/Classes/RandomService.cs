namespace AspDotNetLab2.Classes
{
    public interface IRandom
    {
        public int GetRandom();
    }

    public class RandomNumber : IRandom
    {
        private static Random _random = new Random();
        private int _randomNum;

        public RandomNumber()
        {
            _randomNum = _random.Next(1, 100);            
        }

        public int GetRandom()
        {
            return _randomNum;
        }
    }

    public class RandomService
    {
        public IRandom Service;

        public RandomService(IRandom randomService)
        {
            Service = randomService;
        }
    }
}
