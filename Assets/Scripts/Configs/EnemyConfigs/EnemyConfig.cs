using System.Collections.Generic;

[System.Serializable]
public class EnemyConfig : IEnemyConfig
{
    // Fields for internal storage
    public int life;
    public float speed;
    public int damage;
    public int immunityCount;
    public int blocks;
    public float rangeFire;
    public float atkSpeed;
    public float weight;
    public float derateAd;
    public float derateIce;
    public float derateFire;
    public float derateElec;
    public float derateWind;
    public float derateEnergy;
    public bool canAction;
    public List<string> controlImmunityList;
    private float easyHurt;
    private string attackType;
    private string actionType;
    private string characterType;

    // Properties for external access, mapped to fields
    public float EasyHurt
    {
        get => easyHurt;
        set => easyHurt = value;
    }

    public string AttackType
    {
        get => attackType;
        set => attackType = value;
    }

    public string ActionType
    {
        get => actionType;
        set => actionType = value;
    }

    public string CharacterType
    {
        get => characterType;
        set => characterType = value;
    }

    public int Life
    {
        get => life;
        set => life = value;
    }

    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    public int Damage
    {
        get => damage;
        set => damage = value;
    }

    public int ImmunityCount
    {
        get => immunityCount;
        set => immunityCount = value;
    }

    public int Blocks
    {
        get => blocks;
        set => blocks = value;
    }

    public float RangeFire
    {
        get => rangeFire;
        set => rangeFire = value;
    }

    public float AtkSpeed
    {
        get => atkSpeed;
        set => atkSpeed = value;
    }

    public float Weight
    {
        get => weight;
        set => weight = value;
    }

    public float DerateAd
    {
        get => derateAd;
        set => derateAd = value;
    }

    public float DerateIce
    {
        get => derateIce;
        set => derateIce = value;
    }

    public float DerateFire
    {
        get => derateFire;
        set => derateFire = value;
    }

    public float DerateElec
    {
        get => derateElec;
        set => derateElec = value;
    }

    public float DerateWind
    {
        get => derateWind;
        set => derateWind = value;
    }

    public float DerateEnergy
    {
        get => derateEnergy;
        set => derateEnergy = value;
    }

    public bool CanAction
    {
        get => canAction;
        set => canAction = value;
    }

    public List<string> ControlImmunityList
    {
        get => controlImmunityList;
        set => controlImmunityList = value;
    }
}
