using KingOfGuns.Core.Entities;
using UnityEngine;

namespace KingOfGuns.Core
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private Transform _playerSpawnPosition;
        [SerializeField] private Spawner _spawner;

        private void Awake()
        {
            Input input = new Input();
            input.Enable();

            ServiceLocator serviceLocator = ServiceLocator.Instance;
            serviceLocator.Register(input);
            serviceLocator.Register(_spawner);
        }

        private void Start() => _spawner.Spawn(_playerPrefab, _playerSpawnPosition.position, Quaternion.identity);
    }
}
