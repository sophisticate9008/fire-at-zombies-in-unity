using MyBase;
using UnityEngine;

namespace TheBuffs
{
    public class DebuffFreeze : BuffBase
    {

        public DebuffFreeze(string buffName, float duration, GameObject obj) : base(buffName, duration, obj)
        {
            enemyBase = obj.GetComponent<EnemyBase>();
        }
        public override void Effect()
        {
            
            EffectControl();

            base.Effect();
        }

        public override void Remove()
        {
            
            RemoveControl();

            base.Remove();
            
        }
    }
}