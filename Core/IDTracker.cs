using System.Collections.Generic;
using System.Linq;

namespace ARA2D.Core
{
    public class IDTracker
    {
        public const int StartingID = 1;

        int currentID = StartingID;
        readonly Queue<int> releasedIDs = new Queue<int>(128);

        public IDTracker(List<int> existingIDs = null)
        {
            if (existingIDs == null) return;
            existingIDs.Sort();
            currentID = existingIDs.Last() + 1;
            for (int i = StartingID, idsIndex = 0; i < currentID; i++)
            {
                if (existingIDs[idsIndex] == i)
                {
                    idsIndex++;
                }
                else
                {
                    releasedIDs.Enqueue(i);
                }
            }
        }

        public int GetNextID()
        {
            if (releasedIDs.Count > 0)
            {
                return releasedIDs.Dequeue();
            }

            return currentID++;
        }

        public void ReleaseID(int ID)
        {
            releasedIDs.Enqueue(ID);
        }
    }
}
