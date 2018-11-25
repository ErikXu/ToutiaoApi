using System;

namespace ToutiaoApi
{
    public class ToutiaoException : Exception
    {
        public ToutiaoException(string message) : base(message)
        {

        }
    }
}