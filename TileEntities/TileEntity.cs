using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace ARA2D
{
    public interface ITileEntity
    {
        int ID { get; set; }
        int Width { get; set; }
        int Height { get; set; }
        // TODO: Figure out a better way to do this, we want protected setters.. or do we?
        Vector2 BaseScale { get; set; }
        Vector2 Origin { get; set; }
        Texture2D Texture { get; set; }
        Entity Entity { get; set; }
        Sprite Sprite { get; set; }

        void CreateEntity(Scene scene, long tx, long ty);
        void DeleteEntity();
        
        void Update();
        ITileEntity Clone();
    }
}
