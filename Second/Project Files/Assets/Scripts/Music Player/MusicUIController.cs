using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MusicManager))]
public class MusicUIController : MonoBehaviour
{
    private MusicManager _musicManager;

    [SerializeField] private AudioSource _mainSource;
    [Space(12)]
    
    [Header("Play and Pause Buttons Setup")]
    [SerializeField] private GameObject _playButton;
    [SerializeField] private GameObject _pauseButton;
    [Space(7)]

    [Header("Time UI Control")]
    [SerializeField] private TMPro.TMP_Text _wholeTime;
    [SerializeField] private TMPro.TMP_Text _currentTime;
    [Space(4)]
    
    [SerializeField] private Slider _progressSlider;
    [Space(7)] 
    
    [Header("Stylistic")] 
    [SerializeField] private TMPro.TMP_Text _songTitle;
    [Space(5)]
    
    [Header("Rotating Cubes")]
    [SerializeField] private Transform _outerCube;
    [SerializeField] private float _outerSpeed;
    [Space(3)]
    [SerializeField] private Transform _innerCube;
    [SerializeField] private float _innerSpeed;
    [Space(7)] 
    
    [Header("Volume Control")] 
    [SerializeField] private Slider _volumeSlider;

    private bool _movingMusic = false;

    public void StartMoving()
    {
        _movingMusic = true;
    }

    public void FinishMoving()
    {
        _movingMusic = false;
        _musicManager.MoveTrack(_progressSlider.value);
    }
    
    private void Start()
    {
        _volumeSlider.value = _mainSource.volume;
        
        _musicManager = GetComponent<MusicManager>();
    }

    private void Update()
    {
        _songTitle.text = _musicManager.CurrentClip.name;
        
        var currentTime = _mainSource.time;
        _wholeTime.text = GetTimeToFormat(_musicManager.CurrentClip.length);
        _currentTime.text = GetTimeToFormat(currentTime);
        
        _progressSlider.maxValue = _musicManager.CurrentClip.length - 1;
        if (_movingMusic == false)
        {
            _progressSlider.value = currentTime;
            _progressSlider.interactable = false;
        }
        else
        {
            _progressSlider.interactable = true;
        }
        
        Rotate(_innerCube, _innerSpeed);
        if (_musicManager.MusicPaused == false) Rotate(_outerCube, _outerSpeed);
    }

    private void Rotate(Transform obj, float speed)
    {
        obj.Rotate(0, 0, speed * Time.deltaTime);
    }
    
    private string GetTimeToFormat(float seconds)
    {
        return (seconds / 60).ToString("0") + ":" +
               (seconds % 60).ToString("00");
    } 
    
    public void Pause()
    {
        _pauseButton.SetActive(false);
        _playButton.SetActive(true);
        
        _musicManager.PauseMusic();
    }

    public void Unpause()
    {
        _pauseButton.SetActive(true);
        _playButton.SetActive(false);
        
        _musicManager.UnpauseMusic();
    }

    public void ChangeTrack(int index)
    {
        if (index == 1) _musicManager.NextTrack();
        if (index == -1) _musicManager.PrevTrack();
    }
    
    public void ChangeVolume(float value)
    {
        _mainSource.volume = value;
    }
}
