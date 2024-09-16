
using System;
using MyComponents;
using UnityEngine;

namespace Factorys
{
    public class ComponentFactory
    {
        public static ComponentBase Creat(string componentName, GameObject selfObj, GameObject enemyObj) {
            return componentName switch {
                "穿透" => new PenetrableComponent(componentName, "enter" , selfObj, enemyObj),
                _ => throw new ArgumentException($"Unknown debuff: {componentName}")
            };
        }
    }
}
