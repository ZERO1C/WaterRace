using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Code.UI;
using Code.Pooling;

public class GameplaySceneInstaller : MonoInstaller
{
    public FactoryMap FactoryMap;
    public UIController UIController;
    public MovePlayer MovePlayer;
    public ParticlesPooling ParticlesPooling;
    public ObjectPooling ObjectPooling;

    public override void InstallBindings()
    {
        Container.Bind<FactoryMap>().FromInstance(FactoryMap).AsSingle().NonLazy();
        Container.Bind<UIController>().FromInstance(UIController).AsSingle().NonLazy();
        Container.Bind<MovePlayer>().FromInstance(MovePlayer).AsSingle().NonLazy();
        Container.Bind<ParticlesPooling>().FromInstance(ParticlesPooling).AsSingle().NonLazy();
        Container.Bind<ObjectPooling>().FromInstance(ObjectPooling).AsSingle().NonLazy();
    }
}
