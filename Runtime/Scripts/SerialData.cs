namespace Bakery.Saves
{
    public abstract class SerialData
    {
        public abstract string Key();
        public virtual void Deserialize() { }
        public virtual void Serialize() { }
    }
}
