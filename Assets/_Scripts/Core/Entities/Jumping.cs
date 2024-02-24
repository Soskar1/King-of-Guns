using UnityEngine;

namespace KingOfGuns.Core.Entities
{
    public class Jumping : MonoBehaviour
    {
        [SerializeField] private float _force;
        private Rigidbody2D _rigidbody;

        private void Awake() => _rigidbody = GetComponent<Rigidbody2D>();

        public void Jump() => _rigidbody.AddForce(Vector2.up * _force, ForceMode2D.Impulse);
    }
}