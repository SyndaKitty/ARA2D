using System;
namespace ARA2D
{
    public static class Events
    {
        public delegate void TileChunkGenerated(ChunkCoords coords, TileChunk chunk);
        public static event TileChunkGenerated OnTileChunkGenerated;

        public static void TriggerTileChunkGenerated(ChunkCoords coords, TileChunk chunk)
        {
            Console.WriteLine($"TileChunkGenerated {coords.Cx},{coords.Cy}");
            OnTileChunkGenerated?.Invoke(coords, chunk);
        }

        public delegate void TileChunkRemoved(ChunkCoords coords);
        public static event TileChunkRemoved OnTileChunkRemoved;
        
        public static void TriggerTileChunkRemoved(ChunkCoords coords)
        {
            Console.WriteLine($"TileChunkRemoved {coords.Cx},{coords.Cy}");
            OnTileChunkRemoved?.Invoke(coords);
        }

        // TODO: Is there a better way to make passive requests?
        public delegate void PassiveTileChunkRequest(ChunkCoords coords);
        public static event PassiveTileChunkRequest OnPassiveTileChunkRequest;

        public static void TriggerPassiveTileChunkRequest(ChunkCoords coords)
        {
            Console.WriteLine($"PassiveTileChunkRequest {coords.Cx},{coords.Cy}");
            OnPassiveTileChunkRequest?.Invoke(coords);
        }
    }
}
