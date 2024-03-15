using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace KingOfGuns.Core.UI
{
    public class AmmoUI : MonoBehaviour
    {
        [SerializeField] private GameObject _ammoImagePrefab;
        private List<Image> _ammo;

        private void Awake() => _ammo = new List<Image>();

        public void AddAmmo()
        {
            GameObject ammoImageInstance = Instantiate(_ammoImagePrefab);
            ammoImageInstance.transform.SetParent(transform, false);
            _ammo.Add(ammoImageInstance.GetComponent<Image>());
        }

        public void HideAmmo()
        {
            if (_ammo.Count <= 0)
                return;

            int index = _ammo.Where(_ammo => _ammo.enabled).Count() - 1; 
            _ammo[index].enabled = false;
        }

        public void ShowAmmo(int amountOfAmmoToShow)
        {
            if (_ammo.Count <= 0)
                return;

            int displayed = 0;
            foreach (Image image in _ammo)
            {
                if (!image.enabled && displayed < amountOfAmmoToShow) { 
                    image.enabled = true;
                    ++displayed;
                }
            }
        }
    }
}