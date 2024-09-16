using UnityEngine;

public interface IComponent {
    public string Type { get; set; }
    public string ComponentName{get;set;}
    public GameObject SelfObj{get; set;}
    public GameObject EnemyObj{get; set;}

    public void TriggerExec(GameObject selfObj, GameObject enemyObj);
}