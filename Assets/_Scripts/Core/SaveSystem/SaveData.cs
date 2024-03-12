using UnityEngine;

namespace KingOfGuns.Core.SaveSystem
{
    public class SaveData
    {
        public float worldPositionX;
        public float worldPositionY;
    
        public SaveData(Vector2 worldPosition)
        {
            worldPositionX = worldPosition.x;
            worldPositionY = worldPosition.y;
        }
    }
}