using System;
using MyBase;
using R3;
using UnityEngine;
namespace MyComponents
{
    public class PenetrableComponent : ComponentBase, IPenetrable
    {

        private readonly ReactiveProperty<int> _penetrationLevel = new(PlayerStateManager.Instance.allPenetrationLevel);
        public PenetrableComponent(string componentName, string type, GameObject selfObj) : base(componentName, type, selfObj)
        {
            if(selfObj.GetComponent<Bullet>() != null) {
                PenetrationLevel += PlayerStateManager.Instance.bulletPenetrationLevel;
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
            MonoBehaviour.Destroy(SelfObj);
        }

        public override void TriggerExec(GameObject enemyObj)
        {
            PenetrationLevel -= enemyObj.GetComponent<EnemyBase>().Config.blocks;
            if(PenetrationLevel <= 0) {
                HandleDestruction();
            }
        }
    }

}
