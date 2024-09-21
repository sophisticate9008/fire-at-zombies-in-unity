using MyBase;
using UnityEngine;

namespace TheBuffs
{
    public class DebuffPalsy : BuffBase
    {

        public DebuffPalsy(string buffName, float duration, GameObject obj) : base(buffName, duration, obj)
        {
            enemyBase = obj.GetComponent<EnemyBase>();
        }
        public new void Effect()
        {
            EffectControl();
        }

        public new void Remove()
        {
            RemoveControl();
        }
    }
}