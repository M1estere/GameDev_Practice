using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SensitivitySavior : MonoBehaviour
{
    [SerializeField] private Slider _xSensSlider;
    [SerializeField] private Slider _ySensSlider;

    [SerializeField] private TMP_Text _xSens;
    [SerializeField] private TMP_Text _ySens;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("SensitivityX"))
        {
            PlayerPrefs.SetFloat("SensitivityX", 2.5f);
        }

        if (!PlayerPrefs.HasKey("SensitivityY"))
        {
            PlayerPrefs.SetFloat("SensitivityY", 2.5f);
        }

        LoadValues(1);
        LoadValues(2);
    }

    private void Update()
    {
        _ySens.text = _ySensSlider.value.ToString("0.0");
        _xSens.text = _xSensSlider.value.ToString("0.0");
    }

    public void SaveVolume(int id)
    {
        if (id == 1)
        {
            float sensitivityValue = _xSensSlider.value;
            PlayerPrefs.SetFloat("SensitivityX", sensitivityValue);
            LoadValues(1);
        }

        if (id == 2)
        {
            float sensitivityValue = _ySensSlider.value;
            PlayerPrefs.SetFloat("SensitivityY", sensitivityValue);
            LoadValues(2);
        }
    }

    private void LoadValues(int id)
    {
        if (id == 1)
        {
            float sensitivityValue = PlayerPrefs.GetFloat("SensitivityX");
            _xSensSlider.value = sensitivityValue;
        }

        if (id == 2)
        {
            float sensitivityValue = PlayerPrefs.GetFloat("SensitivityY");
            _ySensSlider.value = sensitivityValue;
        }
    }
}
