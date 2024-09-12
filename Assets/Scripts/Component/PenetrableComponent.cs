using R3;
using UnityEngine;



public class PenetrableComponent : MonoBehaviour, IPenetrable
{
    private ReactiveProperty<int> _penetrationLevel = new ReactiveProperty<int>(0);

    public int PenetrationLevel
    {
        get => _penetrationLevel.Value;
        set => _penetrationLevel.Value = value;
    }

    private void Start()
    {
        _penetrationLevel.Subscribe(level =>
        {
            if (level <= 0)
            {
                HandleDestruction(); // 当穿透值为0时处理销毁
            }
        });
    }

    public void IncreasePenetration(int amount)
    {
        PenetrationLevel += amount;
    }

    public void DecreasePenetration(int amount)
    {
        PenetrationLevel -= amount;
    }

    private void HandleDestruction()
    {
        // 处理销毁逻辑，如播放特效、通知其他组件等
        Debug.Log("PenetrableComponent is being destroyed.");
        // 可以在这里添加额外的销毁逻辑
        Destroy(gameObject); // 销毁 GameObject
    }

    public void Destory()
    {
        throw new System.NotImplementedException();
    }
}
