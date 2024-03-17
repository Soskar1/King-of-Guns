using KingOfGuns.Core.StageSystem;
using UnityEngine;

namespace KingOfGuns.Core.EditorScripts
{
    /// <summary>
    /// Only used in Editor scripts (see StageConstructorEditor)
    /// </summary>
    public class StageConstructor : MonoBehaviour
    {
        [SerializeField] private Stage _stagePrefab;
        public Stage StagePrefab => _stagePrefab;
    }
}