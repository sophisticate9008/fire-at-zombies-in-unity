
using System;
using MyComponents;
using UnityEngine;

namespace Factorys
{
    public class ComponentFactory
    {
        public static ComponentBase Creat(string componentName, GameObject selfObj) {
            return componentName switch {
                "穿透" => new PenetrableComponent(componentName, "enter" , selfObj),
                "反弹" => new ReboundComponent(componentName, "update", selfObj),
                _ => throw new ArgumentException($"Unknown component: {componentName}")
            };
        }
    }
}
