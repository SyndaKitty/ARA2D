using ARA2D.Components;
using Nez;

namespace ARA2D.Systems
{
    public class UICollisionDetector : EntityProcessingSystem
    {
        public UICollisionDetector() : base(new Matcher().all(typeof(UICanvas)))
        {
        }

        public override void process(Entity entity)
        {
            var collisionMarker = entity.getOrCreateComponent<UICollided>();
            collisionMarker.Collided = entity.getComponent<UICanvas>().stage.hit(Input.mousePosition) != null;
        }
    }
}
