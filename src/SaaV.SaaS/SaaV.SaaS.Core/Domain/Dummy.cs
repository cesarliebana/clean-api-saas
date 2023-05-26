using SaaV.SaaS.Core.Shared.Entities;

namespace SaaV.SaaS.Core.Domain
{
    public class Dummy: Entity
    {
        public string Name { get; set; }

        public Dummy(string name): base()
        {
            Name = name;
        }

        public void Update(string name)
        {
            Name = name;
            MarkAsModified();
        }
    }
}
