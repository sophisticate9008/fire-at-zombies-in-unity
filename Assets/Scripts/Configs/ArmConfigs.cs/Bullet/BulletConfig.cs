

namespace ArmConfigs
{
    public class BulletConfig : ArmConfigBase
    {
        // 新增属性
        public int BulletPenetrationLevel { get; set; } = 0;
        public int ReboundCount { get; set; } = 1;
        public BulletFissionConfig BulletFissionConfig { get; private set; }
        public int BulletFissionCount { get; set; } = 2;
        public int MultipleLevel { get; set; } = 2;
        public int RepeatLevel { get; set; } = 2;

        // 构造函数
        public BulletConfig() : base()
        {
            // 延迟初始化 BulletFissionConfig，并传递当前 BulletConfig 实例
            BulletFissionConfig = new BulletFissionConfig
            {
                BulletConfig = this
            };
        }

        // 重写父类的 Init 方法
        public override void Init() 
        {
            // 初始化 BulletConfig 的属性
            Name = "bullet";
            Description = "bullet";
            Level = 1;
            RangeFire = 7;
            Speed = 7f;
            Tlc = 1f;
            Cd = 2f;
            CritRate = 0.1f;
            ComponentStrs.Add("穿透");
        }
    }
}
