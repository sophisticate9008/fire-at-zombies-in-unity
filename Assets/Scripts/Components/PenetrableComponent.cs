using System;
using ArmChild;
using MyBase;
using R3;
using UnityEngine;
namespace MyComponents
{
    public class PenetrableComponent : ComponentBase, IPenetrable
    {

        private readonly ReactiveProperty<int> _penetrationLevel = new(PlayerStateManager.Instance.globalConfig.allPenetrationLevel);
        public PenetrableComponent(string componentName, string type, GameObject selfObj) : base(componentName, type, selfObj)
        {

        }
        public override void Init()
        {
            base.Init();
            PenetrationLevel = PlayerStateManager.Instance.globalConfig.allPenetrationLevel;
            if (SelfObj.GetComponent<Bullet>() != null)
            {
                PenetrationLevel += PlayerStateManager.Instance.bulletConfig.BulletPenetrationLevel;
            }
        }
        public int PenetrationLevel
        {
            get => _penetrationLevel.Value;
            set
            {
                _penetrationLevel.Value = value;
            }
        }
        public void HandleDestruction()
        {
            SelfObj.GetComponent<ArmChildBase>().ReturnToPool();
        }

        public override void TriggerExec(GameObject enemyObj)
        {
            PenetrationLevel -= enemyObj.GetComponent<EnemyBase>().Config.blocks;
            if (PenetrationLevel <= 0)
            {
                HandleDestruction();
            }
        }
    }

}
