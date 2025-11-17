
using System;
using UnityEngine;

namespace Bakery
{
    public static class Persistence
    {
        public static Func<ISaveManager> Manager = UnregisterManager;

        internal static ISaveManager UnregisterManager()
        {
            Debug.LogWarning("No Save Manager Registered");
            Manager = () => new SaveMock();
            return Manager();
        }
        private class SaveMock : ISaveManager
        {
            public bool IsEnabled => throw new NotImplementedException();

            public void Cache(string key, ISerialData serialData)
            { }

            public void ChangeSavePath(string filename)
            {
                throw new NotImplementedException();
            }

            public void DeleteSaveFile()
            {
                throw new NotImplementedException();
            }

            public void LoadFile()
            {
                throw new NotImplementedException();
            }

            public T LoadOrCreate<T>(string key) where T : ISerialData
            {
                throw new NotImplementedException();
            }

            public void SaveFile()
            {
                throw new NotImplementedException();
            }
        }
    }
}
