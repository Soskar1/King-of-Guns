using KingOfGuns.Core.Entities;
using UnityEngine;

namespace KingOfGuns.Core
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private PlayerFacade _playerPrefab;
        [SerializeField] private Transform _playerSpawnPosition;

        private void Awake()
        {
            Input.Instance.Enable();
        }

        private void Start()
        {
            Instantiate(_playerPrefab, _playerSpawnPosition.position, Quaternion.identity);
        }
    }
}
