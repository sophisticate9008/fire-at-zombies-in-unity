using MyBase;
using UnityEngine;

public interface IArms {

    public ArmConfigBase Config {get;}
    public GameObject TargetEnemy{get;set;}
    public void FindTargetNearestOrElite();
    public void FindRandomTarget();
    public void FisrtFindTarget();
    public void OtherFindTarget();


}