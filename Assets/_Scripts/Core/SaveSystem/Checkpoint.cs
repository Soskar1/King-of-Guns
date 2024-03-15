using KingOfGuns.Core.Entities;
using UnityEngine;

namespace KingOfGuns.Core.SaveSystem
{
    public class Checkpoint : MonoBehaviour
    {
        private SaveService _saveService;

        public void Initalize(SaveService saveService) => _saveService = saveService;

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Bullet>() != null)
                _saveService.SaveToJson();
        }
    }
}