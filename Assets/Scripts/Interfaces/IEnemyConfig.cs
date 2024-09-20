using System.Collections.Generic;
using UnityEngine;

public interface IEnemyConfig: IEditable
{
    //易伤
    float EasyHurt { get; set; }
    //攻击类型 long short
    string AttackType { get; set; }
    //行动方式 空中地面 sky land
    string ActionType { get; set; }
    //类型，normal, elite, boss
    string CharacterType { get; set; }
    //能否行动
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
    List<string> ControlImmunityList { get; set; }

}
