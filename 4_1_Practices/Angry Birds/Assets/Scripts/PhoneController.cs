using UnityEngine;
using System;

public class PhoneController : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _currentTimeText;

    private void Start()
    {
        Application.targetFrameRate = -1;
    }

    private void Update()
    {
        _currentTimeText.SetText(DateTime.Now.ToString("HH:mm"));
    }
}
