using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private float _maxHealth;
    private float _currentHealth;

    private bool _isPlayer = false;
    private UiUpdater _healthUI;
    
    private void Awake()
    {
        _currentHealth = _maxHealth;
        if (TryGetComponent(out PlayerMovement playerMovement))
        {
            _isPlayer = true;
            _healthUI = FindObjectOfType<UiUpdater>();
        }
           
        if (_healthUI != null) _healthUI.SetUI(_currentHealth, _maxHealth);
    }

    public bool TakeDamage(float damage)
    {
        if (_currentHealth <= 0) return false;
        _currentHealth -= damage;

        if (_healthUI != null) _healthUI.SetUI(_currentHealth, _maxHealth);
        if (_currentHealth <= 0)
        {
            Death();
            return true;
        }

        return false;
    }

    private void Death()
    {
        if (_isPlayer == true)
            _healthUI.ShowFinish(false);
        Destroy(gameObject);
    }
}