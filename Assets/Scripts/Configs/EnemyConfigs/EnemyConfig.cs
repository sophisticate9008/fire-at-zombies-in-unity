using System.Collections.Generic;

[System.Serializable]
public class EnemyConfig
{
    // Fields for internal storage
    public int life;
    public float speed;
    public int damage;
    //免疫次数
    public int immunityCount;
    //阻挡数
    public int blocks;
    //攻击范围
    public float rangeFire;
    //攻击速度
    public float atkSpeed;
    //体重
    public float weight;
    //全减伤
    public float derateAll;
    //物理减伤
    public float derateAd;
    //冰减伤
    public float derateIce;
    //火减伤
    public float derateFire;
    //电减伤
    public float derateElec;
    //风减伤
    public float derateWind;
    //能量减伤
    public float derateEnergy;
    //控制免疫类型
    public List<string> controlImmunityList;
    //易伤
    public float easyHurt;
    //攻击类型
    public string attackType;
    //行动类型 sky/land
    public string actionType;
    //角色类型 normal elite boss
    public string characterType;
    //攻击次数
    public int attackCount;
    // Properties for external access, mapped to fields
    public Dictionary<string, float> GetDamageReduction()
    {
        var damageReduction = new Dictionary<string, float>
        {
            { "all", derateAll },
            { "ad", derateAd },
            { "ice", derateIce },
            { "fire", derateFire },
            { "electric", derateElec },
            { "wind", derateWind },
            { "energy", derateEnergy }
        };

        return damageReduction;
    }
}
