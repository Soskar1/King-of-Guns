using UnityEngine;

namespace KingOfGuns.Core.SaveSystem
{
    [System.Serializable]
    public class SaveData
    {
        public float worldPositionX;
        public float worldPositionY;
        public int stageID;
    
        public SaveData(Vector2 worldPosition, int stageID)
        {
            worldPositionX = worldPosition.x;
            worldPositionY = worldPosition.y;
            this.stageID = stageID;
        }
    }
}