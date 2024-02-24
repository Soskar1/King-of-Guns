using UnityEngine;

namespace KingOfGuns.Core.Entities
{
    public class GroundCheck : MonoBehaviour
    {
        [SerializeField] private Transform _point;
        [SerializeField] private float _radius;
        [SerializeField] private LayerMask _groundLayers;

        public bool CheckForGround()
        {
            Collider2D overlapInfo = Physics2D.OverlapCircle(_point.position, _radius, _groundLayers);

            if (overlapInfo != null)
                return true;

            return false;
        }
    }
}