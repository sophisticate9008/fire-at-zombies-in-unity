using System.Collections.Generic;
using UnityEngine;

public class GlobalConfig : IConfig
{
    // 私有字段
    [SerializeField] private float critRate;
    [SerializeField] private float critDamage;
    [SerializeField] private int attack;
    [SerializeField] private int life;
    [SerializeField] private int allPenetrationLevel;
    [SerializeField] private float fireAddition;
    [SerializeField] private float iceAddition;
    [SerializeField] private float elecAddition;
    [SerializeField] private float energyAddition;
    [SerializeField] private float windAddition;
    [SerializeField] private float allAddition;
    [SerializeField] private float boomAddition;
    [SerializeField] private float adAddition;

    // 构造函数
    public GlobalConfig()
    {
        Init();
    }

    // 初始化方法
    protected virtual void Init()
    {
        // 可以在这里添加初始化逻辑
    }

    // 公共属性
    public virtual float CritRate
    {
        get => critRate;
        set => critRate = value;
    }

    public virtual float CritDamage
    {
        get => critDamage;
        set => critDamage = value;
    }

    public virtual int Attack
    {
        get => attack;
        set => attack = value;
    }

    public virtual int Life
    {
        get => life;
        set => life = value;
    }

    public virtual int AllPenetrationLevel
    {
        get => allPenetrationLevel;
        set => allPenetrationLevel = value;
    }

    public virtual float FireAddition
    {
        get => fireAddition;
        set => fireAddition = value;
    }

    public virtual float IceAddition
    {
        get => iceAddition;
        set => iceAddition = value;
    }

    public virtual float ElecAddition
    {
        get => elecAddition;
        set => elecAddition = value;
    }

    public virtual float EnergyAddition
    {
        get => energyAddition;
        set => energyAddition = value;
    }

    public virtual float WindAddition
    {
        get => windAddition;
        set => windAddition = value;
    }

    public virtual float AllAddition
    {
        get => allAddition;
        set => allAddition = value;
    }

    public virtual float BoomAddition
    {
        get => boomAddition;
        set => boomAddition = value;
    }

    public virtual float AdAddition
    {
        get => adAddition;
        set => adAddition = value;
    }
    public GameObject Prefab { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    // 获取伤害加成的字典
    public virtual Dictionary<string, float> GetDamageAddition()
    {
        return new Dictionary<string, float>
        {
            { "fire", FireAddition },
            { "ice", IceAddition },
            { "elec", ElecAddition },
            { "energy", EnergyAddition },
            { "wind", WindAddition },
            { "all", AllAddition },
            { "boom", BoomAddition },
            { "ad", AdAddition }
        };
    }

    public void SaveConfig()
    {
        throw new System.NotImplementedException();
    }
}
