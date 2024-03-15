using KingOfGuns.Core.Entities;
using KingOfGuns.Core.UI;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

namespace KingOfGuns.Core.Guns
{
    [RequireComponent(typeof(Flipping))]
    [RequireComponent(typeof(Animator))]
    public class Gun : MonoBehaviour
    {
        [SerializeField] private Transform _bulletSpawnPoint;
        [SerializeField] private float _knockbackForce;
        [SerializeField] private float _reloadTime;
        [SerializeField] private int _maxAmmo;
        private int _ammoLeft;

        [SerializeField] [Range(1, 5)] private int _bulletCount;
        [SerializeField] [Range(-45.0f, 0)] private float _minSpreadAngle;
        [SerializeField] [Range(0, 45.0f)] private float _maxSpreadAngle;
        public Action OnRanOutOfAmmo;
        public Action Reloading;
        public Action OnGunFired;
        public Action<int> OnGunReloaded;
        public Action<float> OnApplyKnockback;

        private Flipping _flipping;
        private GunRotation _gunRotation;
        private Input _input;
        private ObjectPool<Bullet> _bulletPool;

        private Timer _timer;
        private bool _isReloading = false;
        private Coroutine _currentReloadTimer = null;

        private Animator _animator;
        private AnimationClip _reloadingAnimationClip;
        private const string _SHOOT_TRIGGER = "Shoot";
        private const string _RELOAD_TRIGGER = "Reload";

        public int MaxAmmo => _maxAmmo;
        private bool IsFull => _ammoLeft == _maxAmmo;

        public void Initialize(Input input, Timer timer, ObjectPool<Bullet> objectPool)
        {
            _input = input;
            _timer = timer;
        
            _bulletPool = objectPool;
            _gunRotation = new GunRotation(transform);
            
            _flipping = GetComponent<Flipping>();
            _animator = GetComponent<Animator>();

            _ammoLeft = _maxAmmo;
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
            if (_isReloading)
                return;

            if (_ammoLeft <= 0 && !_isReloading)
            {
                StartReloading();
                return;
            }

            _animator.SetTrigger(_SHOOT_TRIGGER);
            for (int i = 0; i <  _bulletCount; ++i)
            {
                Bullet bullet = _bulletPool.Dequeue();
                bullet.Initialize(_bulletPool, _timer);
                bullet.transform.position = _bulletSpawnPoint.position;

                float spread = Random.Range(_minSpreadAngle, _maxSpreadAngle);
                bullet.transform.rotation = transform.rotation;
                bullet.transform.Rotate(0, 0, spread);
            }

            --_ammoLeft;
            OnGunFired?.Invoke();
            OnApplyKnockback?.Invoke(_knockbackForce);

            if (_ammoLeft == 0)
                OnRanOutOfAmmo?.Invoke();
        }

        public void StartReloading()
        {
            if (IsFull || _isReloading)
                return;

            Debug.Log("Reloading...");
            Reloading?.Invoke();
            _animator.SetTrigger(_RELOAD_TRIGGER);
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
            _ammoLeft += amountToReload;
            if (_ammoLeft > _maxAmmo)
                _ammoLeft = _maxAmmo;

            _currentReloadTimer = null;
            _isReloading = false;
            OnGunReloaded?.Invoke(_ammoLeft);
        }
    }
}