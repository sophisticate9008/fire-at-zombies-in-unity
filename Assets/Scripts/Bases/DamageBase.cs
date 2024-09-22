public class DamageBase : IDamage
{
    private string name;
    private string type;
    private float harm;
    private string master;
    public string Type { get => type; set => type = value; }
    public float Harm { get => harm; set => harm = value; }
    public string Name { get => name; set => name = value; }
    public string Master{get => master; set => master = value;}
    public DamageBase(string name, string type, float harm, string master) {
        Name = name;
        Type = type;
        Harm = harm;
        Master = master;
    }
}