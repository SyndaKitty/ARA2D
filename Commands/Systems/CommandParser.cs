using ARA2D.Commands.Components;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
            var rawScriptComponent = entity.getComponent<RawCommandScript>();
            string[] lines = Regex.Split(rawScriptComponent.Script, "\r\n|\r|\n");

            List<CommandCall> commandCalls = new List<CommandCall>();

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                int firstSpaceIndex = line.IndexOf(' ');

                string command = "";
                string args = "";
                if (firstSpaceIndex < 0)
                {
                    command = line;
                }
                else
                {
                    command = line.Substring(0, firstSpaceIndex);
                    // Guaranteed to be something at the next index since we did Trim() beforehand
                    args = line.Substring(firstSpaceIndex + 1);
                }

                commandCalls.Add(new CommandCall(command, args));
            }

            entity.addComponent(new CommandScript(commandCalls));
        }
    }
}