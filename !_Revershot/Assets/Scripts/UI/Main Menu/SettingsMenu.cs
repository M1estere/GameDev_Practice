using System;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private GameObject _controls;
    [SerializeField] private GameObject _audio;
    [SerializeField] private GameObject _general;

    public void OpenControls()
    {
        _controls.SetActive(true);
        _audio.SetActive(false);
        _general.SetActive(false);
    }

    public void OpenAudio()
    {
        _controls.SetActive(false);
        _audio.SetActive(true);
        _general.SetActive(false);
    }

    public void OpenGeneral()
    {
        _controls.SetActive(false);
        _audio.SetActive(false);
        _general.SetActive(true);
    }
}
