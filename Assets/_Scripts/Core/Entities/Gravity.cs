using UnityEngine;

namespace KingOfGuns.Core.Entities
{
    public class Gravity : MonoBehaviour
    {
        [SerializeField] private float _force;
        private Rigidbody2D _rigidbody;

        private void Awake() => _rigidbody = GetComponent<Rigidbody2D>();

        public void FixedUpdate()
        {
            if (_rigidbody.velocity.y >= 0)
                return;

            _rigidbody.AddForce(Vector2.down * _force);
        }
    }
}