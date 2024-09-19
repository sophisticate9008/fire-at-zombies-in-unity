using System.Collections.Generic;
using UnityEngine;


public interface IArmChild
{
    public List<IComponent> InstalledComponents { get; set; }
    //第一次忽略的物体，用于衍生技能排除本体
    public List<GameObject> FirstExceptList { get; }
    public float Speed { get; set; }
    public Vector3 Direction { get; set; }
    public Vector3 EulerAngle {get;set;}
    public void CopyComponents<T>(T prefab) where T : MonoBehaviour
    {

    }
    public GameObject TargetEnemy{get;set;}
    //发出后自身的索敌,排除传入的enemy
    public void FindTarget(GameObject nowEnemy);
}