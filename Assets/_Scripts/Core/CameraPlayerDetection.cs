using UnityEngine;
using System;
using KingOfGuns.Core.Entities;

namespace KingOfGuns.Core
{
    public class CameraPlayerDetection : MonoBehaviour
    {
        private Action _collisionOccured;

        public void OnDisable() => _collisionOccured = null;

        public void Subscribe(Action action) => _collisionOccured += action;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Player>() != null)
                _collisionOccured?.Invoke();
        }
    }
}