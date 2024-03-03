using KingOfGuns.Core.Entities;
using KingOfGuns.Core.UI;
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
        [SerializeField] private int _maxAmmo;
        private int _ammoLeft;

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

        private AmmoUI _ammoUI;

        public float KnockbackForce => _knockbackForce;
        public bool IsReloading => _isReloading;
        public int MaxAmmo => _maxAmmo;

        private void Awake()
        {
            _bulletPool = new ObjectPool<Bullet>(_bulletPrefab);
            _gunRotation = new GunRotation(transform);
            
            _input = ServiceLocator.Instance.Get<Input>();
            _timer = ServiceLocator.Instance.Get<Timer>();
            _ammoUI = ServiceLocator.Instance.Get<AmmoUI>();
            
            _flipping = GetComponent<Flipping>();
            _ammoLeft = _maxAmmo;
        }

        private void Start()
        {
            for (int i = _maxAmmo; i > 0; --i)
                _ammoUI.AddAmmo();
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
            if (_ammoLeft <= 0 && !_isReloading)
            {
                Debug.Log("Reloading...");
                StartReloading();
                return;
            } 

            for (int i = 0; i <  _bulletCount; ++i)
            {
                Bullet bullet = _bulletPool.Dequeue();
                bullet.Initialize(_bulletPool); // TODO: remove initialization (same object could be initalized multiple times)
                bullet.transform.position = _bulletSpawnPoint.position;

                float spread = Random.Range(_minSpreadAngle, _maxSpreadAngle);
                bullet.transform.rotation = transform.rotation;
                bullet.transform.Rotate(0, 0, spread);
            }

            --_ammoLeft;
            _ammoUI.HideAmmo(_ammoLeft);
        }

        private void StartReloading()
        {
            _isReloading = true;
            _currentReloadTimer = _timer.StartTimer(_reloadTime, () => { Reload(_maxAmmo); });
        }

        public void InstantReload(int amountToReload)
        {
            if (_currentReloadTimer != null)
                _timer.StopTimer(_currentReloadTimer);

            Reload(amountToReload);
        }

        private void Reload(int amountToReload)
        {
            _ammoLeft = _maxAmmo;
            _currentReloadTimer = null;
            _isReloading = false;
            _ammoUI.ShowAmmo(amountToReload);
        }
    }
}