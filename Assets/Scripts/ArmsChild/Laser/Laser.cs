using MyBase;
using UnityEngine;

namespace ArmsChild
{
    public class Laser : ArmChildBase
    {
        public override void Move()
        {
            GameObject indeedEnemy = null;
            if (TargetEnemyByArm != null)
            {
                indeedEnemy = TargetEnemyByArm;
                if (!indeedEnemy.activeSelf)
                {
                    TargetEnemyByArm = null;
                    FindTargetInScope();
                    indeedEnemy = TargetEnemy;
                }
            }else {
                if(TargetEnemy == null) {
                    FindTargetInScope();
                }
                indeedEnemy = TargetEnemy;
            }
            if(indeedEnemy == null) {
                transform.localScale = new Vector3(0, 1,1);
                return;
            }


            float distance = Vector3.Distance(transform.position, indeedEnemy.transform.position);
            Vector3 direction = indeedEnemy.transform.position - transform.position;
            Direction = direction;
            Vector3 scale = transform.localScale;
            scale.x = distance / 20;
            scale.y = 0.3f;
            transform.localScale = scale;

        }
    }
}