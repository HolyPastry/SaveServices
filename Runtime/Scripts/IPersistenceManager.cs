namespace Bakery
{
    public interface IPersistenceManager
    {
        bool IsEnabled { get; }
        void ChangeSavePath(string filename);

        void Cache(string key, ISerialData serialData);
        void SaveFile();
        void LoadFile();
        T LoadOrCreate<T>(string key) where T : ISerialData;
        void DeleteSaveFile();
    }
}
