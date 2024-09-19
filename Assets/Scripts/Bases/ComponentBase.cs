using UnityEngine;

public abstract class ComponentBase : IComponent
{
    public ComponentBase(string componentName, string type, GameObject selfObj)
    {
        Type = type;
        ComponentName = componentName;
        SelfObj = selfObj;
    }

    public GameObject SelfObj { get; set; }
    public string ComponentName { get; set; }
    public string Type { get; set; }

    public abstract void TriggerExec(GameObject enemyObj);

}