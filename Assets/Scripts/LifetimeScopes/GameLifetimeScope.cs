using VContainer;
using VContainer.Unity;
using MyComponents;
using UnityEngine;
using R3;
public class GameLifetimeScope : LifetimeScope
{


    [SerializeField]
    private Bullet bulletPrefab;
    protected override void Configure(IContainerBuilder builder)
    {

        // 注册 PenetrableComponent
        builder.Register<PenetrableComponent>(Lifetime.Transient);
        // 注册 Gun 组件中的依赖
        builder.RegisterComponentInHierarchy<Bullet>();
        builder.RegisterComponentInNewPrefab(bulletPrefab, Lifetime.Scoped);
        // builder.RegisterComponentInNewPrefab(BulletPrefab, Lifetime.Scoped);
        // builder.RegisterInstance(BulletPrefab.GetComponent<Bullet>()).As<Bullet>();
    }


}
