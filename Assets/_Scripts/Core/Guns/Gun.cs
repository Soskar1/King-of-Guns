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
        private Input _input;
        private ObjectPool<Bullet> _bulletPool;

        public float KnockbackForce => _knockbackForce;

        private void Awake()
        {
            _bulletPool = new ObjectPool<Bullet>(_bulletPrefab);
            _input = ServiceLocator.Instance.Get<Input>();
            _gunRotation = new GunRotation(transform);
        }

        public void Update() => _gunRotation.LookAt(_input.WorldMousePosition);

        public void Shoot()
        {
            Bullet bullet = _bulletPool.Dequeue();
            bullet.Initialize(_bulletPool); // TODO: remove initialization (same object could be initalized multiple times)
            bullet.transform.position = _bulletSpawnPoint.position;
            bullet.transform.rotation = transform.rotation;
        }
    }
}