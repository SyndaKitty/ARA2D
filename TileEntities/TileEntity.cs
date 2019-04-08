using Nez;

namespace ARA2D
{
    public interface ITileEntity
    {
        int ID { get; set; }
        int Width { get; set; }
        int Height { get; set; }

        void CreateEntity(Scene scene, long tx, long ty);
        void DeleteEntity();
        
        void Update();
    }
}
