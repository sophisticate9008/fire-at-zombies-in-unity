

using System.Collections.Generic;

[System.Serializable]
public class EnemyConfig
{
    public int Life;
    public float Speed;
    public int Damage;
    public int ImmunityCount;
    public int Blocks;
    public float RangeFire;
    public float AtkSpeed;
    public float Weight;
    public float DerateAd;
    public float DerateIce;
    public float DerateFire;
    public float DerateElec;
    public float DerateWind;
    public float DerateEnergy;
    public bool CanAction;
    public List<string> ControlImmunityList;
}
