using System;
using Entitas;

namespace Core.Contexts
{
    public class MetaContext : Context<Entity>
    {
        public MetaContext(int totalComponents, Func<Entity> entityFactory) : base(totalComponents, entityFactory)
        {
        }
    }
}
