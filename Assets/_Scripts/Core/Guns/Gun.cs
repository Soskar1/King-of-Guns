using KingOfGuns.Core.Entities;
using UnityEngine;

namespace KingOfGuns.Core.Guns
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _bulletSpawnPoint;
        [SerializeField] private float _knockbackForce;
        [SerializeField] private float _reloadTime;

        [SerializeField] [Range(1, 5)] private int _bulletCount;
        [SerializeField] [Range(-45.0f, 0)] private float _minSpreadAngle;
        [SerializeField] [Range(0, 45.0f)] private float _maxSpreadAngle;

        private GunRotation _gunRotation;
        private Input _input;
        private Timer _timer;
        private ObjectPool<Bullet> _bulletPool;
        private bool _isReloading = false;

        public float KnockbackForce => _knockbackForce;
        public bool IsReloading => _isReloading;

        private void Awake()
        {
            _bulletPool = new ObjectPool<Bullet>(_bulletPrefab);
            _input = ServiceLocator.Instance.Get<Input>();
            _timer = ServiceLocator.Instance.Get<Timer>();
            _gunRotation = new GunRotation(transform);
        }

        public void Update() => _gunRotation.LookAt(_input.WorldMousePosition);

        public void Shoot()
        {
            for (int i = 0; i <  _bulletCount; ++i)
            {
                Bullet bullet = _bulletPool.Dequeue();
                bullet.Initialize(_bulletPool); // TODO: remove initialization (same object could be initalized multiple times)
                bullet.transform.position = _bulletSpawnPoint.position;

                float spread = Random.Range(_minSpreadAngle, _maxSpreadAngle);
                //bullet.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + spread);
                bullet.transform.rotation = transform.rotation;
                bullet.transform.Rotate(0, 0, spread);
            }
            
            _isReloading = true;
            Debug.Log("Reloading...");
            _timer.StartTimer(_reloadTime, () => { Debug.Log("Reloaded");  _isReloading = false; });
        }
    }
}