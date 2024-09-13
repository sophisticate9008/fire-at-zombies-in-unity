using UnityEngine;

public interface IArms {
    public int RangeFire {get;set;}
    public float Speed{get;set;}
    
    public Transform TargetEnemy{get;set;}
}