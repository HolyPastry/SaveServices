
using System;
using UnityEngine;

namespace Bakery
{
    public static class Persistence
    {
        public static Func<IPersistenceManager> Manager = UnregisterManager;

        internal static IPersistenceManager UnregisterManager()
        {
            Debug.LogWarning("No Save Manager Registered");
            _cachedMock = new SaveMock();
            Manager = () => _cachedMock;
            return Manager();
        }

        private static IPersistenceManager _cachedMock;

        private class SaveMock : IPersistenceManager
        {
            public bool IsEnabled => false;
            public void Cache(string key, ISerialData serialData) { }
            public void ChangeSavePath(string filename) { }
            public void DeleteSaveFile() { }
            public void LoadFile() { }
            public T LoadOrCreate<T>(string key) where T : ISerialData { return default; }
            public void SaveFile() { }
        }

        //Cleaning stuff in case cowboys are fast reloading in the editor
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void ResetStatics()
        {
            Manager = UnregisterManager;

#if UNITY_EDITOR
            Debug.Log("[Save] Static fields reset (domain reload skipped)");
#endif
        }
    }
}
