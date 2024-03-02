using UnityEngine;

namespace KingOfGuns.Core.Entities
{
    public class Flipping : MonoBehaviour
    {
        [SerializeField] private bool _facingRight = true;
        [SerializeField] private SpriteRenderer _visual;
        [SerializeField] private bool _flipX;
        [SerializeField] private bool _flipY;
        public bool FacingRight => _facingRight;

        public void Flip()
        {
            _facingRight = !_facingRight;

            if (_flipX)
                _visual.flipX = !_visual.flipX;

            if (_flipY)
                _visual.flipY = !_visual.flipY;
        }
    }
}