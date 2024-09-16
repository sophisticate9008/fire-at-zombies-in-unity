
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

        public void CopyComponents<T>(T prefab, T newInstance) where T : MonoBehaviour
        {
            IArmChild prefabArmChild = prefab.GetComponent<IArmChild>();
            IArmChild newInstanceArmChild = newInstance.GetComponent<IArmChild>();

            if (prefabArmChild != null && newInstanceArmChild != null)
            {
                // 清空 newInstance 的组件列表，防止冲突
                newInstanceArmChild.InstalledComponents.Clear();

                // 遍历 prefab 的 InstallComponents 列表，并为 newInstance 创建新的组件实例
                foreach (var component in prefabArmChild.InstalledComponents)
                {
                    // 假设有一个方式来复制组件，可以通过工厂或者直接实例化
                    var newComponent = ComponentFactory.Creat(component.ComponentName,  newInstance.gameObject, component.EnemyObj);
                    newInstanceArmChild.InstalledComponents.Add(newComponent);
                }
            }
        }
    }
}