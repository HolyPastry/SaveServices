using System.IO;
using UnityEditor;
using UnityEngine;

namespace Bakery.Saves.Editor
{
    public static class SaveMenu
    {
#if UNITY_EDITOR

        [MenuItem("Bakery/Reset Save File")]
        public static void ResetSaveFile()
        {
            string path = $"{Application.persistentDataPath}{Path.DirectorySeparatorChar}{SaveServices.DefaultSaveFilename}.json";
            if (File.Exists(path))
            {
                File.Delete(path);
            }

        }
#endif
    }
}
