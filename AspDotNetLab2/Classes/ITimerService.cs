namespace AspDotNetLab2.Classes
{
    public interface ITimerService
    {
        public string GetDate();
    }

    public class TimerService : ITimerService
    {
        private DateTime _date;

        public TimerService() 
        {
            _date = DateTime.Now;
        }
        public string GetDate() 
        {
            return _date.ToLongTimeString();
        }
    }
}
