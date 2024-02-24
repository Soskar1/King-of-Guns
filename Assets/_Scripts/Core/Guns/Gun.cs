using KingOfGuns.Core.Entities;
using UnityEngine;

namespace KingOfGuns.Core.Guns
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private BulletFacade _bulletPrefab;
        [SerializeField] private Transform _bulletSpawnPoint;
        private GunRotation _gunRotation;

        private void Awake() => _gunRotation = new GunRotation(transform);

        public void Update() => _gunRotation.LookAt(Input.Instance.WorldMousePosition);

        public void Shoot() => Instantiate(_bulletPrefab, _bulletSpawnPoint.position, transform.rotation);
    }
}