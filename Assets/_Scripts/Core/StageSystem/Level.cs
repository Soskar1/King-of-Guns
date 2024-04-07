using UnityEngine;
using KingOfGuns.Core.SaveSystem;
using KingOfGuns.Core.Entities;
using UnityEngine.SceneManagement;

namespace KingOfGuns.Core.StageSystem
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private Stage _startStage;
        [SerializeField] private Transform _playerDefaultPosition;
        private string _worldName;

        private Player _player;
        private Camera _camera;
        private SaveService _saveService;
        private Stage[] _stages;
        private Stage _currentStage;

        private static bool _loadSave = false;

        private void Awake()
        {
            _camera = Camera.main;
            _worldName = SceneManager.GetActiveScene().name;
        }

        public void Initialize(Stage[] stages, SaveService saveService, Player player)
        {
            _stages = stages;
            _saveService = saveService;
            _player = player;

            foreach (Stage stage in _stages)
                stage.OnStageEnter += ActivateStage;

            if (_loadSave)
            {
                LoadSaveFile();
                _loadSave = false;
            }
        }

        public Stage GetStage(int id)
        {
            if (id < 0 || id >= _stages.Length)
            {
                Debug.LogWarning("Provided stage id is out of range!");
                return null;
            }

            return _stages[id];
        }

        public void Save(int stageID) => _saveService.SaveToJson(_player.transform, stageID, _worldName);

        public void LoadSaveFile()
        {
            SaveData saveData = _saveService.LoadFromJson();
            Stage stage = _startStage;
            
            _player.Reset();
            if (saveData != null)
            {
                if (saveData.worldName != _worldName)
                {
                    _loadSave = true;
                    SceneManager.LoadScene(saveData.worldName);
                    return;
                }

                stage = GetStage(saveData.stageID);
                _player.transform.position = new Vector2(saveData.worldPositionX, saveData.worldPositionY);
            }
            else
            {
                _player.transform.position = _playerDefaultPosition.position;
            }    

            ActivateStage(stage);
        }

        private void ActivateStage(Stage stage)
        {
            if (stage == null)
            {
                Debug.LogWarning("Provided stage is null. Ignoring transition");
                return;
            }

            if (_currentStage != null)
            {
                _currentStage.Reset();
                _currentStage.Disable();
            }
            
            _currentStage = stage;
            _currentStage.Enable();
            MoveCamera(_currentStage);
        }

        private void MoveCamera(Stage stage) => _camera.transform.position = new Vector3(stage.transform.position.x, stage.transform.position.y, _camera.transform.position.z);
    }
}