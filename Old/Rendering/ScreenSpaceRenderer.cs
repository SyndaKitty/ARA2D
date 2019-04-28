using System;
using Nez;

namespace ARA2D.Rendering
{
    /// <summary>
    /// A lot of this code was ripped straight from Nez, but since I wanted more control over the camera.
    /// </summary>
    public class ScreenSpaceRenderer : Renderer
    {
        public int[] renderLayers;

        public ScreenSpaceRenderer(Nez.Camera camera, int renderOrder, params int[] renderLayers) : base(renderOrder, camera)
        {
            this.camera = camera;
            Array.Sort(renderLayers);
            Array.Reverse(renderLayers);
            this.renderLayers = renderLayers;
            wantsToRenderAfterPostProcessors = true;
        }

        public override void render(Scene scene)
        {
            beginRender(camera);

            for (var i = 0; i < renderLayers.Length; i++)
            {
                var renderables = scene.renderableComponents.componentsWithRenderLayer(renderLayers[i]);
                for (var j = 0; j < renderables.length; j++)
                {
                    var renderable = renderables.buffer[j];
                    if (renderable.enabled && renderable.isVisibleFromCamera(camera))
                        renderAfterStateCheck(renderable, camera);
                }
            }

            if (shouldDebugRender && Nez.Core.debugRenderEnabled)
                debugRender(scene, camera);

            endRender();
        }

        protected override void debugRender(Scene scene, Nez.Camera cam)
        {
            Graphics.instance.batcher.end();
            Graphics.instance.batcher.begin(cam.transformMatrix);

            for (var i = 0; i < renderLayers.Length; i++)
            {
                var renderables = scene.renderableComponents.componentsWithRenderLayer(renderLayers[i]);
                for (var j = 0; j < renderables.length; j++)
                {
                    var entity = renderables.buffer[j];
                    if (entity.enabled)
                        entity.debugRender(Graphics.instance);
                }
            }
        }
    }
}
