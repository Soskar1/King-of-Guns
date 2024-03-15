using KingOfGuns.Core.Entities;
using KingOfGuns.Core.Guns;
using KingOfGuns.Core.SaveSystem;
using KingOfGuns.Core.UI;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KingOfGuns.Core
{
    [RequireComponent(typeof(Level))]
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private Gun _startGunPrefab;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _playerSpawnPosition;
        [SerializeField] private Spawner _spawner;
        [SerializeField] private Timer _timer;
        [SerializeField] private AmmoUI _ammoUI;
        [SerializeField] private string _jsonFileNameSave;
        private Level _level;
        private Input _input;

        private void Awake()
        {
            _input = new Input();
            _level = GetComponent<Level>();

            _level.Register(Camera.main.GetComponent<CameraMovement>());
        }

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

        private void Start()
        {
            ObjectPool<Bullet> objectPool = new ObjectPool<Bullet>(_spawner, _bulletPrefab);

            Gun gunInstance = _spawner.Spawn<Gun>(_startGunPrefab, _playerSpawnPosition.position, Quaternion.identity);
            gunInstance.Initialize(_input, _timer, objectPool);

            Player playerInstance = _spawner.Spawn<Player>(_playerPrefab, _playerSpawnPosition.position, Quaternion.identity);
            playerInstance.Initialize(_input, gunInstance, _ammoUI);

            InitializeSaveService(playerInstance);
        }

        private void InitializeSaveService(Player playerInstance)
        {
            SaveService saveService = new SaveService(playerInstance, _jsonFileNameSave);
            _level.SaveService = saveService;

            Checkpoint[] checkpoints = FindObjectsOfType<Checkpoint>();
            foreach (Checkpoint checkpoint in checkpoints)
                checkpoint.Initalize(saveService);
        }

        private void ReloadLevel(InputAction.CallbackContext context) => _level.Reload();
    }
}
