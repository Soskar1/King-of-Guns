using KingOfGuns.Core.StageSystem;
using UnityEditor;
using UnityEngine;

namespace KingOfGuns.Core.EditorScripts
{
    [CustomEditor(typeof(StageConstructor))]
    public class StageConstructorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            StageConstructor stageConstructor = (StageConstructor)target;
            Stage currentStage = stageConstructor.GetComponent<Stage>();
            Camera camera = Camera.main;

            float height = 2f * camera.orthographicSize;
            float width = height * camera.aspect;

            if (GUILayout.Button("Create Stage on the right"))
            {
                Vector2 position = new Vector2(stageConstructor.transform.position.x + width, stageConstructor.transform.position.y);
                CreateStage(stageConstructor.StagePrefab, currentStage.transform.parent, position);
            }

            if (GUILayout.Button("Create Stage on the left"))
            {
                Vector2 position = new Vector2(stageConstructor.transform.position.x - width, stageConstructor.transform.position.y);
                CreateStage(stageConstructor.StagePrefab, currentStage.transform.parent, position);
            }

            if (GUILayout.Button("Create Stage on the top"))
            {
                Vector2 position = new Vector2(stageConstructor.transform.position.x, stageConstructor.transform.position.y + height);
                CreateStage(stageConstructor.StagePrefab, currentStage.transform.parent, position);
            }

            if (GUILayout.Button("Create Stage on the bottom"))
            {
                Vector2 position = new Vector2(stageConstructor.transform.position.x, stageConstructor.transform.position.y - height);
                CreateStage(stageConstructor.StagePrefab, currentStage.transform.parent, position);
            }
        }

        private Stage CreateStage(Stage stagePrefab, Transform parent, Vector2 position)
        {
            Stage stageInstance = Instantiate(stagePrefab, position, Quaternion.identity);
            stageInstance.transform.SetParent(parent);
            stageInstance.name = stagePrefab.name;

            return stageInstance;
        }
    }
}