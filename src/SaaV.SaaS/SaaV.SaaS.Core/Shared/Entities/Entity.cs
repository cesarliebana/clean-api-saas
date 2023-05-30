using SaaV.SaaS.Core.Shared.ValueObjects;
using System.Net;

namespace SaaV.SaaS.Core.Shared.Entities
{
    public class Entity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; private set; }
        public int TenantId { get; private set; }
        public DateTime CreatedDateTime { get; private set; }
        public DateTime ModifiedDateTime { get; private set; }
        public string CreatedUserId { get; private set; }
        public string CreatedUserName { get; private set; }
        public string ModifiedUserId { get; private set; }
        public string ModifiedUserName { get; private set; }

        public Entity(int tenantId, string createdUserId, string createdUserName)
        {
            CreatedDateTime = ModifiedDateTime = DateTime.UtcNow;
            CreatedUserId = ModifiedUserId = createdUserId;
            CreatedUserName = ModifiedUserName = createdUserName;
            TenantId = tenantId;
        }

        public void MarkAsModified(string userId, string userName)
        {
            ModifiedDateTime = DateTime.UtcNow;
            ModifiedUserId = userId;
            ModifiedUserName = userName;            
        }

        public void MarkAsDeleted(string userId, string userName)
        {
            MarkAsModified(userId, userName);
            IsDeleted = true;
        }
    }
}
