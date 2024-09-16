using UnityEngine;

public abstract class ComponentBase : IComponent
{
    public ComponentBase(string componentName, string type, GameObject selfObj, GameObject enemyObj)
    {
        Type = type;
        ComponentName = componentName;
        SelfObj = selfObj;
        EnemyObj = enemyObj;
    }

    public GameObject SelfObj { get; set; }
    public GameObject EnemyObj { get; set; }
    public string ComponentName { get; set; }
    public string Type { get; set; }

    public abstract void TriggerExec(GameObject selfObj, GameObject enemyObj);

}