namespace SaaV.SaaS.Core.Shared.Exceptions
{
    [Serializable]
    public class ItemNotFoundException : Exception
    {
        public int Id { get; private set; }
        public Type Type { get; private set; }

        public ItemNotFoundException(Type type, int id) : base($"Item[{id}] of type {type.Name} not found")
        {
            Id = id;
            Type = type;
        }
    }
}
