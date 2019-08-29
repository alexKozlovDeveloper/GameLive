using System.Runtime.Serialization;

namespace Arena.Core.ShipInfrastructure
{
    [DataContract]
    public class StarShip
    {
        [DataMember]
        public int HitPoints { get; set; }

        [DataMember]
        public string StarShipIcon { get; set; }

        public Cooldown Cooldown { get; set; }
    }
}
