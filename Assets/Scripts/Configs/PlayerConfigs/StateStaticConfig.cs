using System.Collections.Generic;

[System.Serializable]
public class StateStaticConfig
{
    //攻击力
    public int attack;
    //生命值
    public int life;
    //全穿透
    public int allPenetrationLevel;
    //火焰伤害加成
    public float fireAddition;
    //冰冻伤害加成
    public float iceAddition;
    //电击伤害加成
    public float elecAddition;
    //能量伤害加成
    public float energyAddition;
    //风伤害
    public float windAddition;
    //全伤害
    public float allAddition;
    //爆炸伤害
    public float boomAddition;
    //物理伤害
    public float adAddition;

    public Dictionary<string, float> GetDamageAddition()
    {
        var damageAddition = new Dictionary<string, float>
        {
            { "fire", fireAddition },
            { "ice", iceAddition },
            { "elec", elecAddition },
            { "energy", energyAddition },
            { "wind", windAddition },
            { "all", allAddition },
            { "boom", boomAddition },
            { "ad", adAddition }
        };
        return damageAddition;
    }
}