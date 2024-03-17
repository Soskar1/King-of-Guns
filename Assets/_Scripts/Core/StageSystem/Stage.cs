using KingOfGuns.Core.Entities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace KingOfGuns.Core.StageSystem
{
    public class Stage : MonoBehaviour
    {
        private List<IStageObject> _stageObjects = new List<IStageObject>();
        public Action<Stage> OnStageEnter;
        private int _id;
        private bool _isActive;

        public int ID => _id;

        private void Awake()
        {
            IStageObject[] childrens = GetComponentsInChildren<IStageObject>();
            _stageObjects.AddRange(childrens);
            Disable();
        }

        public void Initialize(int id) => _id = id;

        public void Enable()
        {
            _stageObjects.ForEach(x => x.Enable());
            _isActive = true;
        }

        public void Disable()
        {
            _stageObjects.ForEach(_x => _x.Disable());
            _isActive = false;
        }

        public void Reload() => _stageObjects.ForEach(_x => _x.Reload());

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!_isActive && collision.GetComponent<Player>() != null)
                OnStageEnter?.Invoke(this);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Camera camera = Camera.main;

            float _height = 2f * camera.orthographicSize;
            float _width = _height * camera.aspect;

            _height /= 2;
            _width /= 2;

            Gizmos.DrawLine(new Vector2(-_width + transform.position.x, _height + transform.position.y),
                new Vector2(_width + transform.position.x, _height + transform.position.y));
            Gizmos.DrawLine(new Vector2(_width + transform.position.x, _height + transform.position.y),
                new Vector2(_width + transform.position.x, -_height + transform.position.y));
            Gizmos.DrawLine(new Vector2(_width + transform.position.x, -_height + transform.position.y),
                new Vector2(-_width + transform.position.x, -_height + transform.position.y));
            Gizmos.DrawLine(new Vector2(-_width + transform.position.x, -_height + transform.position.y),
                new Vector2(-_width + transform.position.x, _height + transform.position.y));
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Camera camera = Camera.main;

            float _height = 2f * camera.orthographicSize;
            float _width = _height * camera.aspect;

            _height /= 2;
            _width /= 2;

            Gizmos.DrawLine(new Vector2(-_width + transform.position.x, _height + transform.position.y),
                new Vector2(_width + transform.position.x, _height + transform.position.y));
            Gizmos.DrawLine(new Vector2(_width + transform.position.x, _height + transform.position.y),
                new Vector2(_width + transform.position.x, -_height + transform.position.y));
            Gizmos.DrawLine(new Vector2(_width + transform.position.x, -_height + transform.position.y),
                new Vector2(-_width + transform.position.x, -_height + transform.position.y));
            Gizmos.DrawLine(new Vector2(-_width + transform.position.x, -_height + transform.position.y),
                new Vector2(-_width + transform.position.x, _height + transform.position.y));
        }
    }
}