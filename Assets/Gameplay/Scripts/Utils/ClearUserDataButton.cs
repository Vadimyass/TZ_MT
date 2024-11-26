using Gameplay.Scripts.DataManagement;
using UnityEditor;

namespace Gameplay.Scripts.Utils
{
    public static class ClearUserDataButton
    {
        [MenuItem("Tools/ResetData")]
        public static void DeleteData()
        {
            PlayerPrefsSaveManager.DeleteData();
        }
    }
}