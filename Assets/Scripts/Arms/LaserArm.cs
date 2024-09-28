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
        
        ArmChildBase obj = Instantiate(Config.Prefab, transform.position, quaternion.identity).GetComponent<ArmChildBase>();
        obj.Direction = baseDirection;
        obj.Init();
    }
}