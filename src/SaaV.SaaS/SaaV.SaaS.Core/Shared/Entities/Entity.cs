using SaaV.SaaS.Core.Shared.ValueObjects;
using System.Net;

namespace SaaV.SaaS.Core.Shared.Entities
{
    public class Entity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public int TenantId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public string CreatedUserId { get; set; }
        public string CreatedUserName { get; set; }
        public string ModifiedUserId { get; set; }
        public string ModifiedUserName { get; set; }

        public Entity(int tenantId, string createdUserId, string createdUserName)
        {
            CreatedDateTime = ModifiedDateTime = DateTime.UtcNow;
            CreatedUserId = ModifiedUserId = createdUserId;
            CreatedUserName = ModifiedUserName = createdUserName;
            TenantId = tenantId;
        }

        public void MarkAsModified(Credential credential)
        {
            ModifiedDateTime = DateTime.UtcNow;
            ModifiedUserId = credential.UserId;
            ModifiedUserName = credential.UserName;            
        }

        public void MarkAsDeleted(Credential credential)
        {
            MarkAsModified(credential);
            IsDeleted = true;
        }
    }
}
