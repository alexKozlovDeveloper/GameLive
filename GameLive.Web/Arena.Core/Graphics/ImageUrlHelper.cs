using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arena.Core.Graphics
{
    public static class ImageUrlHelper
    {
        public const string BaseUrl = "../../Content/Arena/Images/";

        public static class StarShips
        {
            public static string Arasari = BaseUrl + "Arasari-Star-Ship.png";
            public static string Butterfly = BaseUrl + "Butterfly-Star-Ship.png";
            public static string Flamingo = BaseUrl + "Flamingo-Star-Ship.png";
            public static string Piranha = BaseUrl + "Piranha-Star-Ship.png";
            public static string Skat = BaseUrl + "Skat-Star-Ship.png";
        }

        public static class MapEntity
        {
            public static string Bullet = BaseUrl + "Bullet2.png";
            public static string Explosion = BaseUrl + "explosion.gif";
        }
    }
}
