using System.Collections.Generic;
using ArmConfigs;
using MyBase;
using UnityEngine;

namespace ArmsChild
{
    public class Laser : ArmChildBase
    {
        private GameObject expectEnemy;
        public override void Move()
        {
            GameObject indeedEnemy = AllKindFindTarget();

            if (indeedEnemy == null)
            {
                transform.localScale = new Vector3(0, 1, 1);
                return;
            }else {
                expectEnemy = indeedEnemy;
            }
            float distance = Vector3.Distance(transform.position, indeedEnemy.transform.position);
            Vector3 direction = indeedEnemy.transform.position - transform.position;
            Direction = direction;
            Vector3 scale = transform.localScale;
            scale.x = distance / 20;
            scale.y = 0.3f;
            transform.localScale = scale;

        }
        public virtual GameObject AllKindFindTarget()
        {
            GameObject indeedEnemy;
            if (TargetEnemyByArm != null)
            {
                indeedEnemy = TargetEnemyByArm;
                if(!TargetEnemyByArm.activeSelf) {
                    TargetEnemyByArm = null;
                    indeedEnemy = null;
                }
            }
            else
            {
                if (TargetEnemy == null)
                {
                    
                    FindTargetInScope();
                }
                indeedEnemy = TargetEnemy;
                if(indeedEnemy == null) {
                    FindTargetRandom(null);
                }
                indeedEnemy = TargetEnemy;
            }
            return indeedEnemy;
        }
        public override void TriggerByTypeCallBack(string type)
        {
            if(type == Config.TriggerType) {
                LaserFissionConfig laserFissionConfig = ConfigManager.Instance.GetConfigByClassName("LaserFission") as LaserFissionConfig;
                List<GameObject> enemys = FindTargetInScope(laserFissionConfig.FissionLevel, expectEnemy);
                if(enemys == null) {
                    return;
                }
                foreach(var temp in enemys) { 
                    ArmChildBase armChildBase = ObjectPoolManager.Instance.GetFromPool("LaserFissionPool", laserFissionConfig.Prefab).GetComponent<ArmChildBase>();
                    armChildBase.gameObject.transform.position = expectEnemy.transform.position;
                    (armChildBase as LaserFission).TargetEnemyByArm = temp;
                    armChildBase.Init();
                }
            }
        }
    }
    
}