using ARA2D.Commands.Components;
using Nez;

namespace ARA2D.Commands.Systems
{
    /// <summary>
    /// Processes entities with RawCommandScript and parses raw script in parsed CommandScript component.
    /// RawCommandScript stays on entity after parsing. We don't want to delete the source code of the user.
    /// </summary>
    public class CommandParser : EntityProcessingSystem
    {
        public CommandParser() : base(new Matcher().all(typeof(RawCommandScript)).exclude(typeof(CommandScript)))
        {
        }

        public override void process(Entity entity)
        {

        }
    }
}
