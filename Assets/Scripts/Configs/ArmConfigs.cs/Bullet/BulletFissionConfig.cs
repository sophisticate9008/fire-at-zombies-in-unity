namespace ArmConfigs
{
    public class BulletFissionConfig : ArmConfigBase
    {
        public BulletConfig BulletConfig { get; set; }

        // 使用 override 重写属性，保持多态性
        public override float Tlc
        {
            get => BulletConfig.Tlc * 0.25f;  // 使用 BulletConfig 的 tlc 属性
        }
        public virtual int FissionLevel { get; set; } = 2;
        // 构造函数
        public BulletFissionConfig()
        {
            Init();
            Name = "bulletFission";
            Description = "次级子弹,造成本体的25%伤害";
            CritRate = 0.1f;
        }
    }
}