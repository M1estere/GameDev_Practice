using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BoostUI : MonoBehaviour
{
    [SerializeField] private Image _boostGauge;

    [SerializeField] private GameObject _boostButton;
    [SerializeField] private GameObject _boostTitle;
    
    private BoostController _boostController;

    private void Start() => _boostController = FindObjectOfType<BoostController>();

    public void UpdateGauge(float value, float boostDuration)
    {
        if (value >= 1)
        {
            value = 1;
            _boostButton.SetActive(true);
            _boostTitle.SetActive(true);
        }
        float duration = .5f;

        if (value == 0)
        {
            _boostButton.SetActive(false);
            _boostTitle.SetActive(false);
            
            duration = boostDuration;
        }
        
        _boostGauge.DOFillAmount(value, duration);
    }

    public void LaunchBoost()
    {
        _boostController.Boost();
    }
}
