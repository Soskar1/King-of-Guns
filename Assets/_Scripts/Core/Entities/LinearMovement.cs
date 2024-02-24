using UnityEngine;

public class LinearMovement : MonoBehaviour, IMovable
{
    [SerializeField] private float _speed;
    private Transform _transform;

    private void Awake() => _transform = transform;
    
    public void Move(Vector2 direction) => _transform.Translate(direction * _speed * Time.deltaTime);
}
