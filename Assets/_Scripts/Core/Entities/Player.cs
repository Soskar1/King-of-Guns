using UnityEngine;

namespace KingOfGuns.Core.Entities
{
    public class Player : MonoBehaviour
    {
        private PlayerMovement _playerMovement;
        private Input input;

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            input = Input.Instance;
        }

        public void FixedUpdate() => Move();

        private void Move() => _playerMovement.Move(input.MovementInput);
    }
}