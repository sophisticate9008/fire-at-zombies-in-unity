
using Factorys;
using UnityEngine;
namespace MyBase
{


    public class ArmBase : MonoBehaviour, IArms
    {
        

        public GameObject TargetEnemy{get;set;}
        

        public ArmConfigBase TheConfig{get; set;}

        public void FindTargetNearest()
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
        protected virtual void Start() {
            TheConfig = PlayerStateManager.Instance.GetArmConfigByClassName(GetType().Name);
        }

        
    }
}