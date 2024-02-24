using UnityEngine;

public class Entity : MonoBehaviour
{
    private IMovable _movement;

    protected virtual void Awake() => _movement = GetComponent<IMovable>();

    public void Move(Vector2 direction) => _movement.Move(direction);
}
