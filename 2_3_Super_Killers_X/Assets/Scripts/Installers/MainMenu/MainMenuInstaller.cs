using UnityEngine;
using Zenject;

public class MainMenuInstaller : MonoInstaller
{
    [SerializeField] private GameConfig _gameConfig;
    
    public override void InstallBindings()
    {
        Container.Bind<Camera>().FromComponentInHierarchy().AsSingle();
        Container.Bind<SceneFader>().FromComponentInHierarchy().AsSingle();

        Container.BindInstance(_gameConfig);
    }
}
