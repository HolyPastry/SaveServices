
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Bakery
{
    public class SaveManager : MonoBehaviour, ISaveManager
    {
        [SerializeField] private bool _autoSave = true;

        public const string DefaultSaveFilename = "Save";
        public const string PlayerPrefsSaveKey = "SavePath";
        public static string DefaultSavePath => SavePath(DefaultSaveFilename);

        public bool IsEnabled => true;


        [Serializable]
        public struct Serial
        {
            public string Key;
            public string Json;

            public Serial(string key, string json)
            {
                Key = key;
                Json = json;
            }
        }

        [Serializable]
        public class SerialList
        {
            public List<Serial> List = new();
        }

        private SerialList _cached;

        private bool _needSaving;

        private string _savePath;


        void Awake()
        {
            if (PlayerPrefs.HasKey(PlayerPrefsSaveKey))
                _savePath = PlayerPrefs.GetString(PlayerPrefsSaveKey);
            else
                _savePath = DefaultSavePath;
            LoadFile();
        }
        void OnDisable()
         => Persistence.Manager = Persistence.UnregisterManager;


        void OnEnable()
         => Persistence.Manager = () => this;


        void Update()
        {
            if (!_needSaving || !_autoSave) return;

            SaveFile();
            _needSaving = false;
        }

        void ISaveManager.LoadFile()
        {
            LoadFile();
        }
        void LoadFile()
        {
            if (!File.Exists(_savePath))
            {
                using var str = File.Create(_savePath);
                str.Close();
            }
            string raw = File.ReadAllText(_savePath);
            _cached = JsonUtility.FromJson<SerialList>(raw) ?? new();
        }

        public void SaveFile()
        {
            string json = JsonUtility.ToJson(_cached);
            File.WriteAllText(_savePath, json);
        }


        public static void DeleteSave(string filename)
        {
            if (File.Exists(filename))
            {
                File.Delete(filename);
                Debug.Log("Save Deleted");
            }
            else
            {
                Debug.LogWarning("No Save to delete");
            }
        }

        public void DeleteSaveFile()
         => DeleteSave(_savePath);

        public static string SavePath(string fileName) =>
            $"{Application.persistentDataPath}{Path.DirectorySeparatorChar}{fileName}.json";

        public void ChangeSavePath(string filename)
        {
            _savePath = SavePath(filename);
            PlayerPrefs.SetString(PlayerPrefsSaveKey, filename);
        }

        public void Cache(string key, ISerialData serialData)
        {
            if (string.IsNullOrEmpty(key))
            {
                Debug.LogWarning("Serial Data Key is empty");
                return;
            }
            serialData.Serialize();
            string json = JsonUtility.ToJson(serialData);
            _cached.List.RemoveAll(s => s.Key == key);
            _cached.List.Add(new(key, json));
            _needSaving = true;
        }

        public T LoadOrCreate<T>(string key) where T : ISerialData
        {
            Serial serial = _cached.List.Find(s => s.Key == key);
            if (string.IsNullOrEmpty(serial.Json))
                return Activator.CreateInstance<T>();

            var data = JsonUtility.FromJson<T>(serial.Json);
            data.Deserialize();
            return data;
        }


    }
}
