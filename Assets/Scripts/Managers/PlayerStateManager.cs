using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public int bulletFissionCount = 2;
    //总穿透
    public int allPenetrationLevel = 1;
    //枪械穿透
    public int bulletPenetrationLevel = 1;
    public int reboundCount= 1;
    // 静态实例
    public static PlayerStateManager Instance { get; private set; }

    // Unity Awake 方法，确保在所有其他组件之前初始化
    private void Awake()
    {
        // 检查是否已经有实例存在
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // 防止创建多个实例
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 保持跨场景存在
        }
    }

    // 其他管理逻辑可以在此添加
    void Start()
    {
        allPenetrationLevel = 1;
        bulletPenetrationLevel = 1;
        reboundCount = 1;
    }
}