using UnityEngine;

namespace KingOfGuns.Core.Entities
{
    public class PhysicsMovement : MonoBehaviour, IMovable
    {
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _acceleration;
        [SerializeField] private float _decceleration;
        [SerializeField] private float _velocityPower;
        private Rigidbody2D _rigidbody;

        private const float EPSILON = 0.01f;

        private void Awake() => _rigidbody = GetComponent<Rigidbody2D>();

        public void Move(Vector2 direction)
        {
            Vector2 targetVelocity = direction * _maxSpeed;
            Vector2 velocityDifference = targetVelocity - _rigidbody.velocity;
            float accelerationRate = (Mathf.Abs(targetVelocity.x) > EPSILON) ? _acceleration : _decceleration;
            float movementX = Mathf.Pow(Mathf.Abs(velocityDifference.x) * accelerationRate, _velocityPower) * Mathf.Sign(velocityDifference.x);

            _rigidbody.AddForce(new Vector2(movementX, _rigidbody.velocity.y));
        }
    }
}