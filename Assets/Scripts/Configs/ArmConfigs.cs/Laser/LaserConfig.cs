using MyBase;

namespace ArmConfigs
{
    public class LaserConfig : ArmConfigBase
    {

        public string FindType { get ; set ; } = "scope";

        public override void Init()
        {
            base.Init();
            Tlc = 0.5f;
            Name = "Laser";
            Description = "激光";
            RangeFire = 10;
            Speed = 10f;
            Cd = 4f;
            AttackCd = 0.2f;
            AttackCount = 1;
            Duration = 3f;
            TriggerType = "stay";
            DamageType = "energy";
            DamagePos = "all";
            CritRate = 0.5f;
            ScopeRadius = 3f;
        }
    }
}