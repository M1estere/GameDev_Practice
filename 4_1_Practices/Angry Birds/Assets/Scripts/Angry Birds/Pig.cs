using UnityEngine;

public class Pig : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 3;
    [SerializeField] private float _damageNeeded;

    private float _currentHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void Damage(float damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0) Death();
    }

    private void Death()
    {
        GameController.Instance.DeletePig(this);

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float impactForce = collision.relativeVelocity.magnitude;
    
        if (impactForce > _damageNeeded)
            Damage(impactForce);
    }
}
