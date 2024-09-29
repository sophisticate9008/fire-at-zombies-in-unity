using MyBase;
using UnityEngine;

namespace ArmsChild
{
    public class Laser : ArmChildBase
    {
        public override void Move()
        {
            GameObject indeedEnemy;
            if(TargetEnemyByArm != null) {
                indeedEnemy = TargetEnemyByArm;
            }else {
                indeedEnemy = TargetEnemy;
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