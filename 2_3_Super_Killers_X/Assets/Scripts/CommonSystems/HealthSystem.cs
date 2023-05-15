using UnityEngine;
using Zenject;

public class HealthSystem : MonoBehaviour
{
    [Inject(Id = "maxHealth")] private float _maxHealth;

    [Inject] private UiUpdater _uiUpdater;
    
    private float _currentHealth;
    private bool _isPlayer = false;
    
    private void Awake()
    {
        _currentHealth = _maxHealth;
        if (TryGetComponent(out Player player)) _isPlayer = true;
        
        if (_isPlayer && _uiUpdater != null) _uiUpdater.SetHealth(_currentHealth, _maxHealth);
    }
    
    public void TakeDamage(float damage)
    {
        if (_currentHealth >= 0)
            _currentHealth -= damage;
        
        if (_isPlayer && _uiUpdater != null) _uiUpdater.SetDamage(_currentHealth, _maxHealth);
        if (_currentHealth <= 0)
            Death();
    }

    private void Death()
    {
        if (_isPlayer && _uiUpdater != null) _uiUpdater.ShowFinish(false);
        gameObject.SetActive(false);
    }
}
