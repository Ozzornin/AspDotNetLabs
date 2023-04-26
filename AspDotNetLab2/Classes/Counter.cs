using System.Security.Cryptography;

namespace AspDotNetLab2.Classes
{
    public class Counter
    {
        private int _count = 0;

        public void Increment()
        {
            _count++;
        }
        public int GetCount()=> _count;
      
    }
}
