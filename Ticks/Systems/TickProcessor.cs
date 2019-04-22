using ARA2D.ComponentProvider;
using ARA2D.Ticks.Components;
using Nez;

namespace ARA2D.Ticks.Systems
{
    public class TickProcessor : ProcessingSystem
    {
        public float TickLength = .4f;
        readonly IComponentProvider componentProvider;

        public TickProcessor(IComponentProvider componentProvider)
        {
            this.componentProvider = componentProvider;
        }

        public override void process()
        {
            var tickInfo = componentProvider.GetComponent<TickInfo>();
            tickInfo.Progress += Time.deltaTime;
            if (tickInfo.Progress >= TickLength)
            {
                tickInfo.Ticking = true;
                tickInfo.Progress %= TickLength;
            }

            tickInfo.PercentProgress = tickInfo.Progress / TickLength;
        }
    }
}
