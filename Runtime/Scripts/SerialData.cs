namespace Bakery.Saves
{
    public abstract class SerialData
    {
        public virtual void Deserialize() { }
        public virtual void Serialize() { }
    }
}
