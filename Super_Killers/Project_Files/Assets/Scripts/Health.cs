using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private float maxHealth;
    private float _currentHealth;

    private UiUpdater _healthUI;
    
    private void Awake()
    {
        _currentHealth = maxHealth;
        if (TryGetComponent(out PlayerMovement playerMovement))
            _healthUI = FindObjectOfType<UiUpdater>();
        
        if (_healthUI != null) _healthUI.SetUI(_currentHealth, maxHealth);
    }

    public bool TakeDamage(float damage)
    {
        if (_currentHealth <= 0) return false;
        _currentHealth -= damage;

        if (_healthUI != null) _healthUI.SetUI(_currentHealth, maxHealth);
        if (_currentHealth <= 0)
        {
            Death();
            return true;
        }

        return false;
    }

    private void Death()
    {
        if (TryGetComponent(out PlayerMovement move))
            _healthUI.ShowFinish(false);
        Destroy(gameObject);
    }
}
