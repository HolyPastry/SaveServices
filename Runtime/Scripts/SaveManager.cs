
using System;
using System.Collections.Generic;
using System.IO;
using Holypastry.Bakery.Flow;
using UnityEngine;

namespace Bakery.Saves
{
    public class SaveManager : Service
    {
        [SerializeField] private bool _autoSave = true;
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
            if (PlayerPrefs.HasKey("SavePath"))
                _savePath = PlayerPrefs.GetString("SavePath");
            else
                _savePath = SaveServices.DefaultSavePath;
            LoadFile();
        }
        void OnDisable()
        {
            SaveServices.IsEnabled = () => false;
            SaveServices.SaveJson = (key, json) => { Debug.LogWarning("No Save System"); };
            SaveServices.LoadJson = (key) => { Debug.LogWarning("No Save System"); return null; };
            SaveServices.SaveToFile = (fileName) => { Debug.LogWarning("No Save System"); };
            SaveServices.LoadFromFile = (filename) => { Debug.LogWarning("No Save System"); };
        }

        void OnEnable()
        {
            SaveServices.IsEnabled = () => true;
            SaveServices.SaveJson = Save;
            SaveServices.LoadJson = Load;
            SaveServices.SaveToFile = SaveToFile;
            SaveServices.LoadFromFile = LoadFromFile;
        }

        private void LoadFromFile(string fileName)
        {
            PlayerPrefs.SetString("SavePath", fileName);
            _savePath = SaveServices.SavePath(fileName);
            if (!File.Exists(_savePath))
            {
                using var str = File.Create(_savePath);
                str.Close();
            }
            string raw = File.ReadAllText(_savePath);
            _cached = JsonUtility.FromJson<SerialList>(raw) ?? new();
        }

        private void SaveToFile(string fileName)
        {
            string json = JsonUtility.ToJson(_cached);
            File.WriteAllText(SaveServices.SavePath(fileName), json);
        }

        private void Save(string key, string json)
        {
            _cached.List.RemoveAll(s => s.Key == key);
            _cached.List.Add(new(key, json));
            _needSaving = true;
        }

        private string Load(string key)
        {
            Serial serial = _cached.List.Find(s => s.Key == key);
            return serial.Json;
        }

        private void LoadFile()
        {
            if (!File.Exists(SaveServices.DefaultSavePath))
            {
                using var str = File.Create(SaveServices.DefaultSavePath);
                str.Close();
            }
            string raw = File.ReadAllText(SaveServices.DefaultSavePath);
            _cached = JsonUtility.FromJson<SerialList>(raw) ?? new();
        }

        void Update()
        {
            if (_needSaving && _autoSave)
            {
                WriteFile();
                _needSaving = false;
            }
        }

        private void WriteFile()
        {
            string json = JsonUtility.ToJson(_cached);
            File.WriteAllText(_savePath, json);
        }
    }
}
