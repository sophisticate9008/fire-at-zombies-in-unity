
using System.Collections;
using Factorys;
using UnityEngine;
namespace MyBase
{


    public class ArmBase : MonoBehaviour, IArms
    {

        private float lastFireTime = -10000f;
        public GameObject TargetEnemy { get; set; }


        public ArmConfigBase Config  => ConfigManager.Instance.GetConfigByClassName(GetType().Name.Replace("Arm", "")) as ArmConfigBase;

        public void FindTargetNearestOrElite()
        {
            EnemyBase[] enemies = FindObjectsOfType<EnemyBase>();
            float shortestDistance = Mathf.Infinity;
            GameObject nearestEnemy = null;

            foreach (EnemyBase enemy in enemies)
            {
                float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance && distanceToEnemy <= Config.RangeFire)
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
        }
        public virtual void Update()
        {
            AttackLogic();
        }
        public virtual void AttackLogic() {
            if(TargetEnemy == null) {
                FindTargetNearestOrElite();
            }
            
            if (TargetEnemy != null && Time.time - lastFireTime > Config.Cd)
            {
                lastFireTime = Time.time + 100000;//设为较大值，避免再次进入
                StartCoroutine(AttackSequence()); // 发射
                
                
            }
        }
        
        public virtual IEnumerator AttackSequence() {

            for(int i = 0; i < Config.AttackCount; i++) {
                if(i == 0) {
                    FisrtFindTarget();
                }else {
                    OtherFindTarget();
                }
                lastFireTime = Time.time;
                Attack();
                
                yield return new WaitForSeconds(Config.AttackCd);
                
            }
            TargetEnemy = null;
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