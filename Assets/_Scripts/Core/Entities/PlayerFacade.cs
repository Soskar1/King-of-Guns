using KingOfGuns.Core.Guns;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KingOfGuns.Core.Entities
{
    public class PlayerFacade : MonoBehaviour
    {
        [SerializeField] private Gun _gun;
        private Player _player;
        private Input _input;

        public void Awake()
        {
            _input = Input.Instance;

            IMovable movement = GetComponent<IMovable>();
            _player = new Player(movement, GetComponent<Jumping>(), GetComponent<GroundCheck>());
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

        public void FixedUpdate() => _player.Move(Vector2.right * _input.MovementInput);
        private void Jump(InputAction.CallbackContext context) => _player.Jump();
        private void Shoot(InputAction.CallbackContext context) => _gun.Shoot();
    }
}