namespace MyBase
{

    public class DamageBase
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public float Harm { get; set; }
        public string Master { get; set; }

        public DamageBase(string name, string type, float harm, string master)
        {
            Name = name;
            Type = type;
            Harm = harm;
            Master = master;
        }
    }
}