using System;
using MyBase;
using R3;
using UnityEngine;
namespace MyComponents
{
    public class PenetrableComponent : ComponentBase, IPenetrable
    {
        private readonly ReactiveProperty<int> _penetrationLevel = new(3);
        public PenetrableComponent(string componentName, string type, GameObject selfObj, GameObject enemyObj) : base(componentName, type, selfObj, enemyObj)
        {
            _penetrationLevel.Subscribe(level =>
            {
                if (level <= 0)
                {
                    HandleDestruction();
                }
            });
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

        public override void TriggerExec(GameObject selfObj, GameObject enemyObj)
        {
            Debug.Log("PenetrableComponent TriggerExec");
            PenetrationLevel -= enemyObj.GetComponent<EnemyBase>().Blocks;
        }
    }

}
