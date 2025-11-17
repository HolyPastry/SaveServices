using System.IO;
using UnityEditor;
using UnityEngine;

namespace Bakery.Editor
{
    public static class SaveMenu
    {
#if UNITY_EDITOR

        [MenuItem("Bakery/Reset Save File")]
        public static void ResetSaveFile()
        {
            string path = $"{Application.persistentDataPath}{Path.DirectorySeparatorChar}{SaveManager.DefaultSaveFilename}.json";
            if (File.Exists(path))
            {
                File.Delete(path);
            }

        }
#endif
    }
}
