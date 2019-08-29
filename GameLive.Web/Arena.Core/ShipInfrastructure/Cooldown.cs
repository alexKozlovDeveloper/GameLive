namespace Arena.Core.ShipInfrastructure
{
    public class Cooldown
    {
        public int Value { get; set; }

        public bool IsReady => (Value <= 0);

        public Cooldown()
        {
            Value = 0;
        }

        public virtual void NextTick()
        {
            if (Value > 0)
            {
                Value--;
            }   
        }
    }
}
