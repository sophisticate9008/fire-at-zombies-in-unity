using System.Collections.Generic;
using ArmConfigs;
using MyBase;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public GlobalConfig globalConfig = new GlobalConfig();
    public BulletConfig bulletConfig = new BulletConfig();
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
        }
    }

    // 其他管理逻辑可以在此添加
    void Start()
    {
        ObjectPoolManager.Instance.CreatePool("BulletPool", bulletConfig.Prefab, 50, 100);
        ObjectPoolManager.Instance.CreatePool("BulletFissionPool", bulletConfig.BulletFissionConfig.Prefab, 100, 200);
    }
    public ArmConfigBase GetArmConfigByClassName(string armName)
    {
        return armName switch
        {
            "Bullet" => bulletConfig,
            "BulletFission" => bulletConfig.BulletFissionConfig,
            _ => throw new System.NotImplementedException(),
        };
    }
}