using KingOfGuns.Core.Entities;
using KingOfGuns.Core.SaveSystem;
using KingOfGuns.Core.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KingOfGuns.Core
{
    [RequireComponent(typeof(Level))]
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private Transform _playerSpawnPosition;
        [SerializeField] private Spawner _spawner;
        [SerializeField] private Timer _timer;
        [SerializeField] private AmmoUI _ammoUI;
        private List<Checkpoint> _checkpoints;
        private Level _level;
        private Input _input;

        private void OnEnable()
        {
            _input.Enable();
            _input.Controls.Player.LevelReload.performed += ReloadLevel;
        }

        private void OnDisable()
        {
            _input.Disable();
            _input.Controls.Player.LevelReload.performed -= ReloadLevel;
        }

        private void Awake()
        {
            _input = new Input();
            _level = GetComponent<Level>();

            ServiceLocator serviceLocator = ServiceLocator.Instance;
            serviceLocator.Register(_input);
            serviceLocator.Register(_spawner);
            serviceLocator.Register(_timer);
            serviceLocator.Register(_level);
            serviceLocator.Register(_ammoUI);

            _level.Register(Camera.main.GetComponent<CameraMovement>());
        }

        private void Start()
        {
            Player playerInstance = _spawner.Spawn<Player>(_playerPrefab, _playerSpawnPosition.position, Quaternion.identity);
            playerInstance.SetSpawnPoint(_playerSpawnPosition);
        }

        private void ReloadLevel(InputAction.CallbackContext context) => _level.Reload();
    }
}
