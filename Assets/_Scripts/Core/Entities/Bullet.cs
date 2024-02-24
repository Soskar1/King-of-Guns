using UnityEngine;

namespace KingOfGuns.Core.Entities
{
    public class Bullet : Entity
    {
        public void Update()
        {
            Move(Vector2.right);
        }
    }
}