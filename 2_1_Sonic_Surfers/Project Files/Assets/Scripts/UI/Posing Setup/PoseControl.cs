using System;
using UnityEngine;
using UnityEngine.UI;

public class PoseControl : MonoBehaviour
{
    [Serializable]
    private struct Characters
    {
        public GameObject Character => _character;
        public Color MainColor => _mainColor;
        public Color AddColor => _addColor;
        public Color BgColor => _bgColor;

        [SerializeField] private GameObject _character;
        [Space(5)]
        
        [SerializeField] private Color _bgColor;
        [SerializeField] private Color _mainColor;
        [SerializeField] private Color _addColor;
    }
    
    [Header("General Setup")]
    [Header("Cameras Setup")]
    [SerializeField] private GameObject _poseCamera;
    [SerializeField] private GameObject _mainCamera;
    [Space(2)]
    
    [Header("Transition and Action Setup")]
    [SerializeField] private GameObject _transitionCanvas;
    [Space(5)]
    
    [SerializeField] private PoseCanvas _poseCanvas;
    [Space(2)] 
    
    [Header("Style")]
    [SerializeField] private Characters[] _characters;
    [SerializeField] private ParticleSystem _bgParticles;
    [SerializeField] private Image _bg;
    
    private int _characterId;

    private void Start()
    {
        _characterId = PlayerPrefs.GetInt("Character");
        
        ParticleSystem.MainModule mainModule = _bgParticles.main;
        mainModule.startColor = new ParticleSystem.MinMaxGradient(_characters[_characterId].MainColor, _characters[_characterId].AddColor);
    }

    public void StartPosingCycle()
    {
        Time.timeScale = 0;

        _poseCanvas.gameObject.SetActive(true);
        
        _mainCamera.SetActive(false);
        _poseCamera.SetActive(true);
        
        _transitionCanvas.SetActive(true);
        
        _poseCanvas.StartPosing(_characters[_characterId].Character, this);

        _bgParticles.gameObject.SetActive(true);

        _bg.color = _characters[_characterId].BgColor;
    }

    public void EndPosingCycle()
    {
        _poseCamera.SetActive(false);
        _mainCamera.SetActive(true);
        _transitionCanvas.SetActive(false);
        
        _bgParticles.gameObject.SetActive(false);
        _poseCanvas.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
