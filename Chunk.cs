using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARA2D
{
    public class Chunk
    {
        public readonly ChunkCoords Coords;
        public Tile[,] tiles;

        public Chunk(ChunkCoords coords)
        {
            Coords = coords;
        }
    }
}
