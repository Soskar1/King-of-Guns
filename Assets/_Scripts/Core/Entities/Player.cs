using KingOfGuns.Core.Guns;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KingOfGuns.Core.Entities
{
    public class Player : Entity
    {
        [SerializeField] private Gun _gun;
        private Rigidbody2D _rigidbody;
        private Input _input;
        private Jumping _jumping;
        private GroundCheck _groundCheck;

        protected override void Awake()
        {
            base.Awake();
            _input = Input.Instance;
            _rigidbody = GetComponent<Rigidbody2D>();
            _jumping = GetComponent<Jumping>();
            _groundCheck = GetComponent<GroundCheck>();
        }

        private void OnEnable()
        {
            _input.Controls.Player.Jump.performed += Jump;
            _input.Controls.Player.Shoot.performed += Shoot;
        }

        private void OnDisable()
        {
            _input.Controls.Player.Jump.performed -= Jump;
            _input.Controls.Player.Shoot.performed -= Shoot;
        }

        public void FixedUpdate() => Move(Vector2.right * _input.MovementInput);

        private void Jump(InputAction.CallbackContext context)
        {
            if (_groundCheck.CheckForGround())
                _jumping.Jump();
        }

        private void Shoot(InputAction.CallbackContext context)
        {
            _gun.Shoot();
            Vector3 direction = (Vector3.right * (Vector2)_gun.transform.rotation.eulerAngles).normalized;
            Debug.Log(direction);
            _rigidbody.AddForce(direction * _gun.KnockbackForce, ForceMode2D.Impulse);
        }
    }
}