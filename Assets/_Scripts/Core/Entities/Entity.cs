using UnityEngine;

public class Entity
{
    private IMovable _movement;

    public Entity(IMovable movement)
    {
        _movement = movement;
    }

    public void Move(Vector2 direction) => _movement.Move(direction);
}
