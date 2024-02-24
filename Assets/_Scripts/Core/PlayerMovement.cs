using UnityEngine;

namespace KingOfGuns.Core
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        private Rigidbody2D _rigidbody;

        private void Awake() => _rigidbody = GetComponent<Rigidbody2D>();

        public void Move(float direction)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x + direction * _speed * Time.fixedDeltaTime, _rigidbody.velocity.y);
        }
    }
}