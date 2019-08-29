using System;

namespace GameLive.Core
{
    public class Global
    {
        private static Random _random;

        public static Random Random
        {
            get
            {
                if (_random == null)
                {
                    _random = new Random();
                }

                return _random;
            }
        }
    }
}
