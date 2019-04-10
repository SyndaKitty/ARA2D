using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nez;

namespace ARA2D.Systems
{
    public class CameraController : EntityProcessingSystem
    {
        public float CameraSpeed = 600;

        public CameraController(Camera camera) : base(new Matcher().all(typeof(Camera)))
        {
            camera.setPosition(new Vector2(-Screen.width * .5f, -Screen.height * .5f));
            camera.maximumZoom = 10f;
            camera.minimumZoom = 1f;
            camera.zoom = 0f;
        }

        public override void process(Entity entity)
        {
            var camera = entity.getComponent<Camera>();

            float xInput = (Input.isKeyDown(Keys.A) ? -1 : 0) + (Input.isKeyDown(Keys.D) ? 1 : 0);
            float yInput = (Input.isKeyDown(Keys.W) ? -1 : 0) + (Input.isKeyDown(Keys.S) ? 1 : 0);
            var input = new Vector2(xInput, yInput);
            if (input.LengthSquared() != 0) input.Normalize();
            camera.setPosition(camera.position + input * CameraSpeed * Time.deltaTime);

            float dz = (Input.mouseWheelDelta) * .001f;
            camera.setZoom(camera.zoom + dz);
        }
    }
}
