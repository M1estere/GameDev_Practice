using System.Linq;
using UnityEngine;

[RequireComponent(typeof(LevelController))]
public class StartLevelCheck : MonoBehaviour
{
    private Level[] _levelConfigurations;
    private LevelController _levelController;
    
    private void Awake()
    {
        _levelConfigurations = Resources.LoadAll("Levels", typeof(Level)).Cast<Level>().ToArray();
        
        _levelController = GetComponent<LevelController>();
        
        int id = PlayerPrefs.GetInt("Current Level");
        _levelController.LevelConfiguration = _levelConfigurations[id];
    }
}
