namespace SaaV.SaaS.Core.Shared.Entities
{
    public class Entity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public int TenantId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }

        public Entity()
        {
            CreatedDateTime = ModifiedDateTime = DateTime.UtcNow;
        }

        public void MarkAsModified()
        {
            ModifiedDateTime = DateTime.UtcNow;
        }

        public void MarkAsDeleted()
        {
            ModifiedDateTime = DateTime.UtcNow;
            IsDeleted = true;
        }
    }
}
