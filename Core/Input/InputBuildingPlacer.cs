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

            int width = menuState.SelectedBuildingWidth;
            int height = menuState.SelectedBuildingHeight;

            float centerX = width % 2 == 0 ? 
                (float)Math.Round(worldPoint.X) : 
                (float)Math.Round(worldPoint.X + .5f) - .5f;
            float centerY = height % 2 == 0 ?
                (float)Math.Round(worldPoint.Y) :
                (float)Math.Round(worldPoint.Y + .5f) - .5f;

            int anchorX = (int)(centerX - width / 2);
            int anchorY = (int)(centerY - height / 2);

            TileCoords mouseAnchor = TileCoords.Create(0, 0, anchorX, anchorY);

            if (state.Input.MouseState.LeftButton == ButtonState.Pressed)
            {
                state.Factory.TryPlaceBuilding(mouseAnchor, width, height, menuState.SelectedBuildingType);
            }
            
            var ghosts = state.Factory.BuildingGhostSet.GetEntities();
            if (ghosts.Length > 0)
            {
                state.Factory.CheckBuildingPlacement(ghosts[0], mouseAnchor, width, height);
                if (!ghosts[0].Has<GridTransform>())
                {
                    ghosts[0].Set(new GridTransform(mouseAnchor, width, height));
                }
                else
                {
                    ghosts[0].Get<GridTransform>().Coords = mouseAnchor;
                }
            }

        }

        public void Dispose()
        {
        }
    }
}
