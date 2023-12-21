using UnityEngine;

public class LevelRestartController : MonoBehaviour
{
    public static LevelRestartController Instance { get; private set; }

    public bool ShowCountdown {
        get => _showCountdown;
        set 
        {
            _showCountdown = value;

            PlayerPrefs.SetInt("Show_Count", _showCountdown == true ? 0 : 1);
        }
    }

    private bool _showCountdown;

    private void Awake() 
    { 
        Instance = this;

        Invoke(nameof(ResetPref), 5);
    }

    private void ResetPref()
    {
        PlayerPrefs.SetInt("Show_Count", 0);
    }
}