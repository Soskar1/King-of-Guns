using UnityEngine;

namespace KingOfGuns.Core.Guns
{
    public class GunRotation
    {
        private Transform _transform;

        public GunRotation(Transform transform) => _transform = transform;

        public void LookAt(Vector2 point)
        {
            Vector2 diff = point - (Vector2)_transform.position;
            float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            _transform.rotation = Quaternion.Euler(new Vector3(_transform.rotation.x, _transform.rotation.y, rotZ));
        }
    }
}