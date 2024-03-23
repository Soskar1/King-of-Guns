using KingOfGuns.Core.Entities;
using KingOfGuns.Core.Guns;
using KingOfGuns.Core.SaveSystem;
using KingOfGuns.Core.StageSystem;
using KingOfGuns.Core.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KingOfGuns.Core
{
    [RequireComponent(typeof(Level))]
    public class Bootstrap : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private Transform _playerSpawnPosition;
        [SerializeField] private Gun _startGunPrefab;
        [SerializeField] private Bullet _bulletPrefab;

        [Header("Systems")]
        [SerializeField] private Spawner _spawner;
        [SerializeField] private Timer _timer;
        private Level _level;
        private Input _input;

        [Header("UI")]
        [SerializeField] private AmmoUI _ammoUI;
        [SerializeField] private GameOverUI _gameOverUI;

        [Header("Save System")]
        [SerializeField] private string _jsonFileNameSave;

        private void Awake()
        {
            _input = new Input();
            _level = GetComponent<Level>();
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
            playerInstance.GetComponent<Health>().OnDie += _gameOverUI.Show;

            SaveService saveService = new SaveService(_jsonFileNameSave, SaveConfiguration.saveFileLocation);
            Stage[] stages = InitializeStages();
            _level.Initialize(stages, saveService, playerInstance);
        }

        private Stage[] InitializeStages()
        {
            Stage[] stages = GetComponentsInChildren<Stage>();

            for (int i = 0; i < stages.Length; i++)
                stages[i].Initialize(i);

            return stages;
        }

        private void ReloadLevel(InputAction.CallbackContext context)
        {
            _level.LoadSaveFile();
            _gameOverUI.Hide();
        }
    }
}
