using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class MixerController : MonoBehaviour
{
    [SerializeField] private AudioMixer _master;

    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;

    [SerializeField] private TMP_Text _masterVolumeText;
    [SerializeField] private TMP_Text _musicVolumeText;
    [SerializeField] private TMP_Text _sfxVolumeText;

    private void Start()
    {
        LoadValues(1);
        LoadValues(2);
        LoadValues(3);
    }

    private void Update()
    {
        _masterVolumeText.text = _masterSlider.value <= 0.1f ? (_masterSlider.value).ToString("0") : (_masterSlider.value * 10).ToString("0");
        _musicVolumeText.text = _musicSlider.value <= 0.1f ? (_musicSlider.value).ToString("0") : (_musicSlider.value * 10).ToString("0");
        _sfxVolumeText.text = _sfxSlider.value <= 0.1f ? (_sfxSlider.value).ToString("0") : (_sfxSlider.value * 10).ToString("0");
    }

    public void SaveVolume(int id)
    {
        switch (id)
        {
            case 1:
                if (_masterSlider.value <= .11f)
                {
                    _master.SetFloat("Master Volume", -80);
                    PlayerPrefs.SetFloat("Master Volume", _masterSlider.value);
                }
                else
                {
                    _master.SetFloat("Master Volume", Mathf.Log10(_masterSlider.value) * 20);
                    PlayerPrefs.SetFloat("Master Volume", _masterSlider.value);
                }
                break;
            case 2:
                if (_musicSlider.value <= .11f)
                {
                    _master.SetFloat("Music Volume", -80);
                    PlayerPrefs.SetFloat("Music Volume", _musicSlider.value);
                }
                else
                {
                    _master.SetFloat("Music Volume", Mathf.Log10(_musicSlider.value) * 20);
                    PlayerPrefs.SetFloat("Music Volume", _musicSlider.value);
                }
                break;
            case 3:
                if (_sfxSlider.value <= .11f)
                {
                    _master.SetFloat("SFX Volume", -80);
                    PlayerPrefs.SetFloat("SFX Volume", _sfxSlider.value);
                }
                else
                {
                    _master.SetFloat("SFX Volume", Mathf.Log10(_sfxSlider.value) * 20);
                    PlayerPrefs.SetFloat("SFX Volume", _sfxSlider.value);
                }
                break;
        }
    }

    private void LoadValues(int id)
    {
        switch (id)
        {
            case 1:
                if (!PlayerPrefs.HasKey("Master Volume"))
                {
                    _masterSlider.value = 1;
                    _master.SetFloat("Master Volume", Mathf.Log10(_masterSlider.value) * 20);
                }
                else
                {
                    _masterSlider.value = PlayerPrefs.GetFloat("Master Volume");
                    if (_masterSlider.value <= .11f)
                        _master.SetFloat("Master Volume", -80);
                    else
                        _master.SetFloat("Master Volume", Mathf.Log10(_masterSlider.value) * 20);
                }
                break;
            case 2:
                if (!PlayerPrefs.HasKey("Music Volume"))
                {
                    _musicSlider.value = 1.5f;
                    _master.SetFloat("Music Volume", Mathf.Log10(_musicSlider.value) * 20);
                }
                else
                {
                    _musicSlider.value = PlayerPrefs.GetFloat("Music Volume");
                    if (_musicSlider.value <= .11f)
                        _master.SetFloat("Music Volume", -80);
                    else
                        _master.SetFloat("Music Volume", Mathf.Log10(_musicSlider.value) * 20);
                }
                break;
            case 3:
                if (!PlayerPrefs.HasKey("SFX Volume"))
                {
                    _sfxSlider.value = 2;
                    _master.SetFloat("SFX Volume", Mathf.Log10(_sfxSlider.value) * 20);
                }
                else
                {
                    _sfxSlider.value = PlayerPrefs.GetFloat("SFX Volume");
                    if (_sfxSlider.value <= .11f)
                        _master.SetFloat("SFX Volume", -80);
                    else
                        _master.SetFloat("SFX Volume", Mathf.Log10(_sfxSlider.value) * 20);
                }
                break;
        }
    }
}
