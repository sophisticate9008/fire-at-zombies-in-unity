
using Factorys;
using UnityEngine;
namespace MyBase
{


    public class ArmBase : MonoBehaviour, IArms
    {
        private int rangeFire = 10;
        private float speed = 10;
        private GameObject targetEnemy;
        public int RangeFire
        {
            get { return rangeFire; }
            set { rangeFire = value; }
        }
        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        public GameObject TargetEnemy
        {
            get { return targetEnemy; }
            set { targetEnemy = value; }
        }
        public void FindTargetNearest()
        {
            EnemyBase[] enemies = FindObjectsOfType<EnemyBase>();
            float shortestDistance = Mathf.Infinity;
            GameObject nearestEnemy = null;

            foreach (EnemyBase enemy in enemies)
            {
                float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance && distanceToEnemy <= RangeFire)
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


        
    }
}