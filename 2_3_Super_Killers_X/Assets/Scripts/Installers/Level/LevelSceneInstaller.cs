using UnityEngine;
using Zenject;

public class LevelSceneInstaller : MonoInstaller
{
    [Header("Configs Setup")]
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private LevelConfig _levelConfig;
    [Space(5)]

    [Header("Enemies Setup")]
    [SerializeField] private RangedEnemyConfig _rangedEnemyConfig;
    [SerializeField] private MeleeEnemyConfig _meleeEnemyConfig;
    [Space(5)]

    [Header("Values")]
    [SerializeField] private float _maxHealth;

    public override void InstallBindings()
    {
        Container.BindInstances(_gameConfig, _rangedEnemyConfig, _meleeEnemyConfig);

        Container.Bind<float>().WithId("maxHealth").FromInstance(_maxHealth).AsCached();
    
        Container.Bind<Rigidbody>().FromComponentSibling();
        Container.Bind<ShootingController>().FromComponentSibling();
    
        Container.Bind<Player>().FromComponentInHierarchy().AsSingle();
        Container.Bind<Camera>().FromComponentInHierarchy().AsSingle();
        Container.Bind<FixedJoystick>().FromComponentInHierarchy().AsSingle();

        Container.Bind<Wave[]>().FromInstance(_levelConfig.Waves);

        //                    take   give   factory
        Container.BindFactory<Enemy, Enemy, Enemy.Factory>().FromFactory<Enemy.CustomFactory>();
    }
}
