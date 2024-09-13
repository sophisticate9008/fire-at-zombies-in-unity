using UnityEngine;

public interface IEnemy
{
    int Life { get; set; }
    float Speed { get; set; }
    // 伤害
    int Damage { get; set; }
    // 免疫次数
    int ImmunityCount { get; set; }
    // 占用穿透次数
    int Blocks { get; set; }
    // 射程
    float RangeFire { get; set; }
    // 攻击速度
    float AtkSpeed { get; set; }
    // 重量, 击退牵引用
    float Weight { get; set; }
    // 物理伤害减免/增加
    float DerateAd { get; set; }
    // 冰系伤害减免/增加
    float DerateIce { get; set; }
    // 火系伤害减免/增加
    float DerateFire { get; set; }
    // 电系伤害减免/增加
    float DerateElec { get; set; }
    // 风系伤害减免/增加
    float DerateWind { get; set; }
    // 能量伤害减免/增加
    float DerateEnergy { get; set; }

}
