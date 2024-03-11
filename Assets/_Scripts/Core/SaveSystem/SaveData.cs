using MessagePack;
using UnityEngine;

namespace KingOfGuns.Core.SaveSystem
{
    public class SaveData
    {
        [Key(0)]
        public Vector2 worldPosition;
    }
}