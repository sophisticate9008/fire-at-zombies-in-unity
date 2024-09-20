using System;
using System.Linq;
using MyBase;
using TheBuffs;
using UnityEngine;

namespace Factorys
{
    public class BuffFactory
    {
        public static BuffBase Create(string buffName, float duration, GameObject obj, params object[] args)
        {
            return buffName switch
            {
                // 不需要自定义参数的 Buffs
                "眩晕" => new DebuffDizzy(buffName, duration, obj),
                "冰冻" => new DebuffFreeze(buffName, duration, obj),
                "麻痹" => new DebuffPalsy(buffName, duration, obj),

                // 需要自定义参数的 Buffs
                "减速" => args.Length == 1 && args[0] is float slowRate
                    ? new DebuffSlow(buffName, duration, obj, slowRate)
                    : throw new ArgumentException($"Invalid argument for BuffSlow: expected float, got {string.Join(", ", args.Select(a => a?.GetType().Name ?? "null"))}"),

                // 处理未知的 Buff 类型
                _ => throw new ArgumentException($"Unknown debuff: {buffName}"),
            };
        }
    }
}