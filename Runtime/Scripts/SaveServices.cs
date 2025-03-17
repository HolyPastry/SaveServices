
using System;
using System.IO;
using UnityEngine;

namespace Bakery.Saves
{
    public static class SaveServices
    {
        public const string DefaultSaveFilename = "Save";
        public static string DefaultSavePath => SavePath(DefaultSaveFilename);
        public static string SavePath(string fileName) => $"{Application.persistentDataPath}{Path.DirectorySeparatorChar}{fileName}.json";
        public static Action<string, string> SaveJson = (key, serializedString) => { Debug.LogWarning("No Save System"); };
        public static Func<string, string> LoadJson = (key) => { Debug.LogWarning("No Save System"); return string.Empty; };

        public static Action<string> SaveToFile = (fileName) => { Debug.LogWarning("No Save System"); };
        public static Action<string> LoadFromFile = (filename) => { Debug.LogWarning("No Save System"); };

        public static void Save(SerialData serialData)
        {
            if (string.IsNullOrEmpty(serialData.Key()))
            {
                Debug.LogWarning("Serial Data Key is empty");
                return;
            }
            serialData.Serialize();
            string json = JsonUtility.ToJson(serialData);
            SaveJson(serialData.Key(), json);
        }
        public static T Load<T>(string key) where T : SerialData
        {
            string json = LoadJson(key);
            if (string.IsNullOrEmpty(json))
                return default;

            var data = JsonUtility.FromJson<T>(json);
            data.Deserialize();
            return data;
        }

    }
}
