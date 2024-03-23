using KingOfGuns.Core.UI;
using UnityEngine;

namespace KingOfGuns.Core.Guns
{
    public class GunHandler : MonoBehaviour
    {
        [SerializeField] private GameObject _reloadText;
        [SerializeField] private Rigidbody2D _rigidbody;

        private Gun _currentGun;
        private AmmoUI _ammoUI;

        public void Initialize(Gun gun, AmmoUI ammoUI)
        {
            _currentGun = gun;
            _ammoUI = ammoUI;

            gun.transform.SetParent(transform);

            _currentGun.OnRanOutOfAmmo += ShowReloadText;
            _currentGun.Reloading += HideReloadText;
            _currentGun.OnGunReloaded += ShowAmmo;
            _currentGun.OnApplyKnockback += ApplyKnockback;
            _currentGun.OnGunFired += HideAmmo;

            for (int i = _currentGun.MaxAmmo; i > 0; --i)
                _ammoUI.AddAmmo();
        }

        private void OnEnable()
        {
            if (_currentGun is null)
                return;

            _currentGun.OnRanOutOfAmmo += ShowReloadText;
            _currentGun.Reloading += HideReloadText;
            _currentGun.OnGunReloaded += ShowAmmo;
            _currentGun.OnApplyKnockback += ApplyKnockback;
            _currentGun.OnGunFired += HideAmmo;
        }

        private void OnDisable()
        {
            _currentGun.OnRanOutOfAmmo -= ShowReloadText;
            _currentGun.Reloading -= HideReloadText;
            _currentGun.OnGunReloaded -= ShowAmmo;
            _currentGun.OnApplyKnockback -= ApplyKnockback;
            _currentGun.OnGunFired -= HideAmmo;
        }

        private void ShowReloadText() => _reloadText.SetActive(true);
        private void HideReloadText() => _reloadText.SetActive(false);
        private void ShowAmmo(int amount) => _ammoUI.ShowAmmo(amount);
        private void HideAmmo() => _ammoUI.HideAmmo();

        public void Shoot() => _currentGun.Shoot();
        public void ReloadGun() => _currentGun.StartReloading();
        public void InstantReload(int amountOfAmmo) => _currentGun.InstantReload(amountOfAmmo);

        private void ApplyKnockback(float force)
        {
            Vector2 direction = (_currentGun.transform.rotation * Vector2.right).normalized;
            float forceX = -direction.x * force;
            float forceY = -direction.y * force;
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x + forceX, forceY);
        }

        public void Reset()
        {
            _currentGun.InstantReload(_currentGun.MaxAmmo);
            _reloadText.SetActive(false);
            _ammoUI.ShowAmmo(_currentGun.MaxAmmo);
        }
    }
}