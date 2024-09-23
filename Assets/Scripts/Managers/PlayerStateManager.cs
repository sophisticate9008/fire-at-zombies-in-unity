using System.Collections.Generic;
using ArmConfigs;
using Unity.VisualScripting.FullSerializer;
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

    }
    public ArmConfigBase GetArmConfigByClassName(string armName)
    {
        return armName switch
        {
            "BulletArm" => bulletConfig,
            _ => throw new System.NotImplementedException(),
        };
    }
}