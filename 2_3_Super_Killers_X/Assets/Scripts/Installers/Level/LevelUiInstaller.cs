using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LevelUiInstaller : MonoInstaller
{
    [SerializeField] private Image _healthBar;
    
    [SerializeField] private TMPro.TMP_Text _ammoText;
    [SerializeField] private TMPro.TMP_Text _waveText;
    [SerializeField] private TMPro.TMP_Text _reloadingText;

    [SerializeField] private Image _currentGunIcon;
    
    
    public override void InstallBindings()
    {
        Container.Bind<UiUpdater>().FromComponentInHierarchy().AsSingle();
        Container.Bind<SceneFader>().FromComponentInHierarchy().AsSingle();
        Container.Bind<MainUiControl>().FromComponentInHierarchy().AsSingle();
        
        Container.BindInstance(_healthBar).WithId("healthBar");
        Container.BindInstance(_currentGunIcon).WithId("gunIcon");
        
        Container.BindInstance(_waveText).WithId("waveText");
        Container.BindInstance(_reloadingText).WithId("reloadText");
        Container.BindInstance(_ammoText).WithId("ammoText");
    }
}