using UnityEngine;
using UnityEngine.InputSystem;

namespace KingOfGuns.Core.Entities
{
    public class PlayerFacade : MonoBehaviour
    {
        private Player _player;
        private Input _input;

        public void Awake()
        {
            _input = Input.Instance;

            IMovable movement = GetComponent<IMovable>();
            _player = new Player(movement, GetComponent<Jumping>(), GetComponent<GroundCheck>());
        }

        private void OnEnable() => _input.Controls.Player.Jump.performed += Jump;
        private void OnDisable() => _input.Controls.Player.Jump.performed -= Jump;

        public void FixedUpdate() => _player.Move(Vector2.right * _input.MovementInput);
        private void Jump(InputAction.CallbackContext context) => _player.Jump();
    }
}