namespace ARA2D
{
    public class TileEntityChunk
    {
        public int[,] TileEntityIDs;

        public readonly ChunkCoords Coords;

        public TileEntityChunk(ChunkCoords coords)
        {
            Coords = coords;
            TileEntityIDs = new int[TileChunk.Size,TileChunk.Size];
        }
    }
}
