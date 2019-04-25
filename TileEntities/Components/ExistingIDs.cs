using System.Collections.Generic;
using System.Linq;
using Nez;

namespace ARA2D.TileEntities.Components
{
    public class ExistingIDs : Component
    {
        // TODO: Make this ECS friendly
        public const int StartingID = 1;

        public int CurrentID;
        readonly Queue<int> releasedIDs = new Queue<int>(128);

        public ExistingIDs()
        {
            CurrentID = StartingID;
        }

        public ExistingIDs(List<int> existingIDs = null)
        {
            if (existingIDs == null) return;
            existingIDs.Sort();
            CurrentID = existingIDs.Last() + 1;
            for (int i = StartingID, idsIndex = 0; i < CurrentID; i++)
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

            return CurrentID++;
        }

        public void ReleaseID(int ID)
        {
            releasedIDs.Enqueue(ID);
        }
    }
}
