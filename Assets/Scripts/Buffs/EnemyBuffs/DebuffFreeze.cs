using MyBase;
using UnityEngine;

namespace TheBuffs
{
    public class DebuffFreeze : BuffBase
    {

        public DebuffFreeze(string buffName, float duration, GameObject obj) : base(buffName, duration, obj)
        {
            EnemyBase = obj.GetComponent<EnemyBase>();
        }
        public override void Effect()
        {
            EffectControl();
        }

        public override void Remove()
        {
            RemoveControl();
        }
    }
}