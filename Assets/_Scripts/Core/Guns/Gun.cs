using KingOfGuns.Core.Entities;
using UnityEngine;

namespace KingOfGuns.Core.Guns
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _bulletSpawnPoint;
        [SerializeField] private float _knockbackForce;
        private GunRotation _gunRotation;

        public float KnockbackForce => _knockbackForce;

        private void Awake() => _gunRotation = new GunRotation(transform);

        public void Update() => _gunRotation.LookAt(Input.Instance.WorldMousePosition);

        public void Shoot() => Instantiate(_bulletPrefab, _bulletSpawnPoint.position, transform.rotation);
    }
}