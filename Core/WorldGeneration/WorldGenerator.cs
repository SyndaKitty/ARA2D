using System;
using Core.Position;
using Core.Tiles;

namespace Core.WorldGeneration
{
    public class WorldGenerator
    {
        public Chunk GenerateChunk(TileCoords chunkCoords)
        {

            var chunk = new Chunk();

            for (int i = 0; i < Chunk.Size * Chunk.Size; i++)
            {
                chunk.Tiles[i] = 0;//(short)((i + i / Chunk.Size) % 2);
            }

            return chunk;
        }
    }
}