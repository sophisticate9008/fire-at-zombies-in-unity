using System;

[Serializable]
public class ArmConfigBase
{
    public string name;
    public string description;
    public int level;
    //倍率
    public float tlc;
    
    public float speed;
    public int rangeFire;
    public float cd;
}