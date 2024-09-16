using System;
using MyBase;
using TheBuffs;
using UnityEngine;

namespace Factorys
{
    public class BuffFactory
    {
        public static BuffBase Create<T>(string buffName, float duration, GameObject obj, T args = default)
        {
            return buffName switch
            {
                // 不需要自定义参数的 Buffs
                "DebuffDizzy" => new DebuffDizzy(buffName, duration, obj),
                "DebuffFreeze" => new DebuffFreeze(buffName, duration, obj),
                "DebuffPalsy" => new DebuffPalsy(buffName, duration, obj),

                // 需要自定义参数的 Buffs
                "BuffSlow" => args is float slowRate
                    ? new DebuffSlow(buffName, duration, obj, slowRate)
                    : throw new ArgumentException($"Invalid argument for BuffSlow: expected float, got {typeof(T)}"),

                // 处理未知的 Buff 类型
                _ => throw new ArgumentException($"Unknown debuff: {buffName}"),
            };
        }
    }
}