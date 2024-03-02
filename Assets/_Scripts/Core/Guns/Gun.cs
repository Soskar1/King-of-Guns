using KingOfGuns.Core.Entities;
using UnityEngine;

namespace KingOfGuns.Core.Guns
{
    [RequireComponent(typeof(Flipping))]
    public class Gun : MonoBehaviour
    {
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _bulletSpawnPoint;
        [SerializeField] private float _knockbackForce;
        [SerializeField] private float _reloadTime;

        [SerializeField] [Range(1, 5)] private int _bulletCount;
        [SerializeField] [Range(-45.0f, 0)] private float _minSpreadAngle;
        [SerializeField] [Range(0, 45.0f)] private float _maxSpreadAngle;

        private Flipping _flipping;
        private GunRotation _gunRotation;
        private Input _input;
        private ObjectPool<Bullet> _bulletPool;

        private Timer _timer;
        private bool _isReloading = false;
        private Coroutine _currentReloadTimer = null;

        public float KnockbackForce => _knockbackForce;
        public bool IsReloading => _isReloading;

        private void Awake()
        {
            _bulletPool = new ObjectPool<Bullet>(_bulletPrefab);
            _input = ServiceLocator.Instance.Get<Input>();
            _timer = ServiceLocator.Instance.Get<Timer>();
            _gunRotation = new GunRotation(transform);
            _flipping = GetComponent<Flipping>();
        }

        public void Update()
        {
            float rotZ = transform.rotation.eulerAngles.z;
            if ((_flipping.FacingRight && rotZ > 90.0f && rotZ < 270.0f) ||
                (!_flipping.FacingRight && (rotZ < 90.0f || rotZ > 270.0f)))
                _flipping.Flip();

            _gunRotation.LookAt(_input.WorldMousePosition);
        }

        public void Shoot()
        {
            for (int i = 0; i <  _bulletCount; ++i)
            {
                Bullet bullet = _bulletPool.Dequeue();
                bullet.Initialize(_bulletPool); // TODO: remove initialization (same object could be initalized multiple times)
                bullet.transform.position = _bulletSpawnPoint.position;

                float spread = Random.Range(_minSpreadAngle, _maxSpreadAngle);
                bullet.transform.rotation = transform.rotation;
                bullet.transform.Rotate(0, 0, spread);
            }

            StartReloading();
        }

        private void StartReloading()
        {
            _isReloading = true;
            Debug.Log("Reloading...");
            _currentReloadTimer = _timer.StartTimer(_reloadTime, () => { Debug.Log("Reloaded"); _isReloading = false; });
        }

        public void InstantReloading()
        {
            if (_currentReloadTimer != null)
                _timer.StopTimer(_currentReloadTimer);

            _currentReloadTimer = null;
            _isReloading = false;
            Debug.Log("Reloaded instantly");
        }
    }
}