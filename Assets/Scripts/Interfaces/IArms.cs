using UnityEngine;

public interface IArms {

    
    public GameObject TargetEnemy{get;set;}
    public void FindTargetNearest();
}