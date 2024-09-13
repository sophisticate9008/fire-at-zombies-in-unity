using VContainer;
using VContainer.Unity;
using MyComponents;
using UnityEngine;
using R3;
public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] // 在 Unity Inspector 中手动拖入 Gun 对象
    private GameObject arms;
    protected override void Configure(IContainerBuilder builder)
    {

        // 注册 PenetrableComponent
        builder.Register<PenetrableComponent>(Lifetime.Transient);
        // 注册 Gun 组件中的依赖
        builder.RegisterComponentInHierarchy<Bullet>();
        builder.RegisterComponent(arms);
        
    }


}
