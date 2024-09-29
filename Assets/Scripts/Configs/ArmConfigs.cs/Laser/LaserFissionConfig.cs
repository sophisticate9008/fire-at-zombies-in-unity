using MyBase;

namespace ArmConfigs
{
    public class LaserFissionConfig : ArmConfigBase
    {
        public LaserConfig LaserConfig => ConfigManager.Instance.GetConfigByClassName("Laser") as LaserConfig;
        public override float Tlc { get => LaserConfig.Tlc * 0.1f; }
        public override float Speed => LaserConfig.Speed;
        public override float CritRate => LaserConfig.CritRate;
        public  int FissionLevel = 4;
        public override void Init()
        {
            base.Init();
            Name = "次级激光";
            ScopeRadius = 3f;
            TriggerType = "stay";
            DamageType = "energy";
            DamagePos = "all";
            Duration = 0.2f;
            AttackCd = 0.15f;
        }
    }
}