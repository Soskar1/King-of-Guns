using UnityEngine;

namespace KingOfGuns.Core.Entities
{
    [RequireComponent(typeof(GroundCheck))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Jumping : MonoBehaviour
    {
        [SerializeField] private float _force;
        private bool _jumped = false;
        private GroundCheck _groundCheck;
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _groundCheck = GetComponent<GroundCheck>();
        }

        private void LateUpdate()
        {
            if (!_jumped || _rigidbody.velocity.y > 0)
                return;

            _jumped = !_groundCheck.CheckForGround();
        }

        public void Jump()
        {
            if (_jumped)
                return;

            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _force);
            _jumped = true;
        }
    }
}