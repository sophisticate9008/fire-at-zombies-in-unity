
using System.Collections;
using Factorys;
using UnityEngine;
namespace MyBase
{


    public class ArmBase : MonoBehaviour, IArms
    {

        private float lastFireTime = -10000f;
        public GameObject TargetEnemy { get; set; }


        public ArmConfigBase TheConfig { get; set; }

        public void FindTargetNearestOrElite()
        {
            EnemyBase[] enemies = FindObjectsOfType<EnemyBase>();
            float shortestDistance = Mathf.Infinity;
            GameObject nearestEnemy = null;

            foreach (EnemyBase enemy in enemies)
            {
                float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance && distanceToEnemy <= TheConfig.RangeFire)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy.gameObject;
                }
            }

            if (nearestEnemy != null)
            {
                TargetEnemy = nearestEnemy;
            }
            else
            {
                TargetEnemy = null;
            }
        }
        protected virtual void Start()
        {
            TheConfig = PlayerStateManager.Instance.GetArmConfigByClassName(GetType().Name.Replace("Arm", ""));
        }
        public virtual void Update()
        {
            AttackLogic();
        }
        public virtual void AttackLogic() {
            FindTargetNearestOrElite();
            if (TargetEnemy != null && Time.time - lastFireTime > TheConfig.Cd)
            {
                StartCoroutine(AttackSequence()); // 发射子弹
                lastFireTime = Time.time + 100000;
                //设为较大值，避免再次进入
            }
        }
        
        public virtual IEnumerator AttackSequence() {

            for(int i = 0; i < TheConfig.AttackCount; i++) {
                if(i == 0) {
                    FisrtFindTarget();
                }else {
                    OtherFindTarget();
                }
                Attack();
                lastFireTime = Time.time;
                yield return new WaitForSeconds(TheConfig.AttackCd);
                
            }
        }
        public virtual void Attack() {

        }
        public void FindRandomTarget()
        {
            throw new System.NotImplementedException();
        }

        public virtual void FisrtFindTarget()
        {
            throw new System.NotImplementedException();
        }

        public virtual void OtherFindTarget()
        {
            throw new System.NotImplementedException();
        }
    }
}