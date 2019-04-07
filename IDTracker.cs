using System;
using System.Collections.Generic;

namespace ARA2D
{
    public class IDTracker
    {
        // TODO: Implement a way to load from file
        int currentTileEntityID = 1;
        readonly Queue<int> ReleasedTileEntityIDs = new Queue<int>(128);

        public int GetNextID()
        {
            if (ReleasedTileEntityIDs.Count > 0)
            {
                return ReleasedTileEntityIDs.Dequeue();
            }

            return currentTileEntityID++;
        }

        public void ReleaseID(int ID)
        {
            ReleasedTileEntityIDs.Enqueue(ID);
        }
    }
}
