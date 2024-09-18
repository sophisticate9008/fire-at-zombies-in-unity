using System.Collections.Generic;
using UnityEngine;


public interface IArmChild
{
    public List<IComponent> InstalledComponents { get; set; }
    public float Speed { get; set; }
    public Vector2 Direction { get; set; }
    public void CopyComponents<T>(T prefab) where T : MonoBehaviour
    {

    }
    public GameObject TargetEnemy{get;set;}
    //发出后自身的索敌,排除传入的enemy
    public void FindTarget(GameObject nowEnemy);
}