using UnityEngine;

namespace KingOfGuns.Core
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _acceleration;
        [SerializeField] private float _decceleration;
        [SerializeField] private float _velocityPower;
        private Rigidbody2D _rigidbody;

        private const float EPSILON = 0.01f;

        private void Awake() => _rigidbody = GetComponent<Rigidbody2D>();

        public void Move(float direction)
        {
            float targetVelocity = direction * _maxSpeed;
            float velocityDifference = targetVelocity - _rigidbody.velocity.x;
            float accelerationRate = (Mathf.Abs(targetVelocity) > EPSILON) ? _acceleration : _decceleration;
            float movementX = Mathf.Pow(Mathf.Abs(velocityDifference) * accelerationRate, _velocityPower) * Mathf.Sign(velocityDifference);

            _rigidbody.AddForce(new Vector2(movementX, _rigidbody.velocity.y));
        }
    }
}