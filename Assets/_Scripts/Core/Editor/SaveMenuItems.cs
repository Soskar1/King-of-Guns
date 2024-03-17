using UnityEditor;
using System.IO;
using KingOfGuns.Core.SaveSystem;

namespace KingOfGuns.Core.EditorScripts
{
    public class SaveMenuItems
    {
        [MenuItem("Tools/Delete save files")]
        private static void DeleteSaveFiles() => Directory.Delete(SaveConfiguration.saveFileLocation, true);
    }
}