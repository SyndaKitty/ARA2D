using System;
using System.Numerics;
using Core.Position;
using DefaultEcs.System;
using Microsoft.Xna.Framework.Input;

namespace Core.Input
{
    public class InputBuildingPlacer : ISystem<FrameContext>
    {
        public bool IsEnabled { get; set; } = true;

        public void Update(FrameContext state)
        {
            BuildingMenu menuState = state.GlobalEntity.Get<BuildingMenu>();

            if (!menuState.Enabled) return;
            
            var camera = state.Factory.CameraSet.GetEntities()[0];
            var viewMatrix = camera.Get<Transform>().Matrix;
            Matrix4x4.Invert(viewMatrix, out var invertedViewMatrix);

            Vector3 screenPoint = new Vector3(state.Input.MouseState.X, state.Input.MouseState.Y, 0);
            Vector3 worldPoint = Vector3.Transform(screenPoint, invertedViewMatrix);

            float centerX;
            float centerY;

            centerX = menuState.SelectedBuildingWidth % 2 == 0 ? 
                (float)Math.Round(worldPoint.X) : 
                (float)Math.Round(worldPoint.X + .5f) - .5f;
            centerY = menuState.SelectedBuildingHeight % 2 == 0 ?
                (float)Math.Round(worldPoint.Y) :
                (float)Math.Round(worldPoint.Y + .5f) - .5f;

            int anchorX = (int)(centerX - menuState.SelectedBuildingWidth / 2);
            int anchorY = (int)(centerY - menuState.SelectedBuildingHeight / 2);

            if (state.Input.MouseState.LeftButton == ButtonState.Pressed)
            {
                state.Factory.PlaceBuilding(new TileCoords(0, anchorX, 0, anchorY), menuState.SelectedBuildingWidth, menuState.SelectedBuildingHeight, menuState.SelectedBuildingType);
            }
            else
            {
                state.Factory.CheckBuildingPlacement(new TileCoords(0, anchorX, 0, anchorY), menuState.SelectedBuildingWidth, menuState.SelectedBuildingHeight);
            }
        }

        public void Dispose()
        {
        }
    }
}
