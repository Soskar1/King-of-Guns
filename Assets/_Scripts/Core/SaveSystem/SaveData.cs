using MessagePack;
using UnityEngine;

namespace KingOfGuns.Core.SaveSystem
{
    public class SaveData
    {
        [Key(0)]
        public float worldPositionX;

        [Key(1)]
        public float worldPositionY;
    
        public SaveData(Vector2 worldPosition)
        {
            worldPositionX = worldPosition.x;
            worldPositionY = worldPosition.y;
        }
    }
}