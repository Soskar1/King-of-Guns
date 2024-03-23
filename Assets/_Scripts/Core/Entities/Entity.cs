using KingOfGuns.Core.Entities;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Entity : MonoBehaviour
{
    private Health _health;
    private IMovable _movement;

    protected Health Health => _health;

    protected virtual void Awake()
    {
        _movement = GetComponent<IMovable>();
        _health = GetComponent<Health>();
    }

    public virtual void Reset() => _health.Reset();

    public void Move(Vector2 direction) => _movement.Move(direction);

    public void Kill() => _health.Die();
}
