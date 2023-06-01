using UnityEngine;
using Zenject;

public abstract class Enemy : MonoBehaviour, IFixedTickable
{
    public class Factory : PlaceholderFactory<Enemy, Enemy>
    {
        public override Enemy Create(Enemy prefab) => base.Create(prefab);
    }

    public class CustomFactory : IFactory<Enemy, Enemy>
    {
        private readonly DiContainer _container;

        public CustomFactory(DiContainer container) => _container = container;
        
        public Enemy Create(Enemy prefab) => _container.InstantiatePrefabForComponent<Enemy>(prefab);
    }

    [Inject] protected Rigidbody _rigidbody;
    [Inject] protected Player _player;
    
    [Inject]
    private void Constructor(TickableManager tickableManager) => tickableManager.AddFixed(this);
    public virtual void FixedTick() { }

    public abstract void Move(Vector3 force);

    public abstract void RotateTowardsPlayer(Vector3 force);
    public abstract void Attack();
}
