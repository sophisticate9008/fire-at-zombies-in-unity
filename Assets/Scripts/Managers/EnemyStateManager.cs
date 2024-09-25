using System.Collections.Generic;
using System.IO;
using Factorys;
using MyBase;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    public static EnemyStateManager Instance { get; private set; }
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


}