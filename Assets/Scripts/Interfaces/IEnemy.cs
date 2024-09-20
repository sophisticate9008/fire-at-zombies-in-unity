using System.Collections.Generic;

public interface IEnemy {
    public bool CanAction{get;set;}
    IEnemyConfig Config { get; set;}
    void LoadConfig();
    Dictionary <string, IComponent> InstalledComponents{get;}
    List<IBuff> Buffs{ get;}
    void Move();
    void Attack();
    void Skill();
    void CalLife();

    //死亡火花，弹坑，追击，  
    void Die();
    void BuffEffect();
}