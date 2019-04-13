using System.Collections.Generic;
using System.Linq;

namespace ARA2D
{
    public class IDTracker
    {
        int currentID = 1;
        readonly Queue<int> ReleasedIDs = new Queue<int>(128);

        public IDTracker(List<int> existingIDs = null)
        {
            if (existingIDs == null) return;
            existingIDs.Sort();
            currentID = existingIDs.Last();
            for (int i = 0, idsIndex = 0; i < currentID; i++)
            {
                if (existingIDs[idsIndex] == i)
                {
                    idsIndex++;
                }
                else
                {
                    ReleasedIDs.Enqueue(i);
                }
            }
        }

        public int GetNextID()
        {
            if (ReleasedIDs.Count > 0)
            {
                return ReleasedIDs.Dequeue();
            }

            return currentID++;
        }

        public void ReleaseID(int ID)
        {
            ReleasedIDs.Enqueue(ID);
        }
    }
}
