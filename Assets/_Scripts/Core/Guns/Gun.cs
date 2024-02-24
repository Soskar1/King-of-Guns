using UnityEngine;

namespace KingOfGuns.Core.Guns
{
    public class Gun : MonoBehaviour
    {
        private GunRotation _gunRotation;

        private void Awake()
        {
            _gunRotation = new GunRotation(transform);
        }

        public void Update()
        {
            _gunRotation.LookAt(Input.Instance.WorldMousePosition);
        }
    }
}