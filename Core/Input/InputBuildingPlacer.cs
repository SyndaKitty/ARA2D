using System;
using System.Numerics;
using DefaultEcs.System;
using Microsoft.Xna.Framework.Input;

namespace Core.Input
{
    public class InputBuildingPlacer : ISystem<FrameContext>
    {
        public bool IsEnabled { get; set; } = true;

        public void Update(FrameContext state)
        {
            // TODO: Have building ghost
            if (state.Input.MouseState.LeftButton == ButtonState.Released) return;

            var camera = state.Factory.CameraSet.GetEntities()[0];
            var viewMatrix = camera.Get<Transform>().Matrix;
            Matrix4x4.Invert(viewMatrix, out var invertedViewMatrix);

            Vector3 screenPoint = new Vector3(state.Input.MouseState.X, state.Input.MouseState.Y, 0);
            Vector3 worldPoint = Vector3.Transform(screenPoint, invertedViewMatrix);
            
            // TODO: Do something with worldPoint

        }

        public void Dispose()
        {
        }
    }
}
