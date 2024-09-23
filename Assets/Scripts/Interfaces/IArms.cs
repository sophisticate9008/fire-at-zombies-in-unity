using UnityEngine;

public interface IArms {

    public ArmConfigBase TheConfig{get;set;}
    public GameObject TargetEnemy{get;set;}
    public void FindTargetNearest();
}