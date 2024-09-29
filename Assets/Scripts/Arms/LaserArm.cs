using MyBase;
using Unity.Mathematics;
using UnityEngine;

public class LaserArm : ArmBase
{

    public override void FisrtFindTarget()
    {
        FindTargetNearestOrElite();
    }
    public override void OtherFindTarget()
    {
        FindTargetNearestOrElite();
    }
    public override void Attack()
    {
        Vector3 baseDirection = (TargetEnemy.transform.position - transform.position).normalized;
        ArmChildBase obj = ObjectPoolManager.Instance.GetFromPool(GetType().Name.Replace("Arm", "") + "Pool", Config.Prefab).GetComponent<ArmChildBase>();
        obj.transform.position = transform.position;
        obj.Direction = baseDirection;
        obj.TargetEnemyByArm = TargetEnemy;
        obj.Init();
    }
}