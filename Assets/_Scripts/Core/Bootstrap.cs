using KingOfGuns.Core.Entities;
using KingOfGuns.Core.Guns;
using KingOfGuns.Core.SaveSystem;
using KingOfGuns.Core.StageSystem;
using KingOfGuns.Core.UI;
using System.Collections.Generic;
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

        [Header("Save System")]
        [SerializeField] private string _jsonFileNameSave;

        [Header("Level")]
        [SerializeField] private Stage _startStage;

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

            SaveService saveService = InitializeSaveService(playerInstance);
            Dictionary<int, Stage> stages = InitializeStages();
            _level.Initialize(stages, saveService);
        }

        private SaveService InitializeSaveService(Player playerInstance)
        {
            SaveService saveService = new SaveService(playerInstance, _jsonFileNameSave);

            Checkpoint[] checkpoints = FindObjectsOfType<Checkpoint>();
            foreach (Checkpoint checkpoint in checkpoints)
                checkpoint.Initalize(saveService);

            return saveService;
        }

        private Dictionary<int, Stage> InitializeStages()
        {
            Dictionary<int, Stage> stageDictionary = new Dictionary<int, Stage>();
            Stage[] stages = GetComponentsInChildren<Stage>();

            for (int i = 0; i < stages.Length; i++)
            {
                stages[i].Initialize(i);
                stageDictionary.Add(i, stages[i]);
            }

            return stageDictionary;
        }

        private void ReloadLevel(InputAction.CallbackContext context) => _level.Reload();
    }
}
