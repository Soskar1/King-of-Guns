using UnityEngine;

namespace KingOfGuns.Core.Entities
{
    public class BulletFacade : MonoBehaviour
    {
        private Bullet _bullet;

        private void Awake()
        {
            IMovable movement = GetComponent<IMovable>();
            _bullet = new Bullet(movement);
        }

        public void Update()
        {
            _bullet.Move(Vector2.right);
        }
    }
}