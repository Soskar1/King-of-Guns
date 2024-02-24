using UnityEngine;
using UnityEngine.InputSystem;

namespace KingOfGuns.Core.Entities
{
    public class Player : MonoBehaviour
    {
        private PhysicsMovement _movement;
        private Jumping _jumping;
        private GroundCheck _groundCheck;
        private Input input;

        private void Awake()
        {
            _movement = GetComponent<PhysicsMovement>();
            _jumping = GetComponent<Jumping>();
            _groundCheck = GetComponent<GroundCheck>();
            input = Input.Instance;
        }

        private void OnEnable() => input.Controls.Player.Jump.performed += Jump;
        private void OnDisable() => input.Controls.Player.Jump.performed -= Jump;

        public void FixedUpdate() => Move();

        private void Move() => _movement.Move(input.MovementInput);
        private void Jump(InputAction.CallbackContext context)
        {
            if (_groundCheck.CheckForGround())
                _jumping.Jump();
        }
    }
}