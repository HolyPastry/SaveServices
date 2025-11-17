namespace Bakery
{
    public interface ISaveManager
    {

        void ChangeSavePath(string filename);
        bool IsEnabled { get; }
        void Cache(string key, ISerialData serialData);
        void SaveFile();
        void LoadFile();
        T LoadOrCreate<T>(string key) where T : ISerialData;
        void DeleteSaveFile();
    }
}
