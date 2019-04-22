using System.Collections.Generic;
using System.Text.RegularExpressions;
using ARA2D.Core;
using MoonSharp.Interpreter;
using Nez;

namespace ARA2D.Commands
{
    /// <summary>
    /// Processes entities with RawCommandScript and parses raw script in parsed CommandScript component.
    /// RawCommandScript stays on entity after parsing. We don't want to delete the source code of the user.
    /// </summary>
    public class CommandParser : EntityProcessingSystem
    {
        IComponentProvider componentProvider;

        public CommandParser(IComponentProvider componentProvider) : base(new Matcher().all(typeof(RawCommandScript)).exclude(typeof(CommandScript)))
        {
            this.componentProvider = componentProvider;
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

            entity.addComponent(new CommandScript(InitializeScript(), commandCalls));
        }

        // TODO: Should probably find a better home for this code. Doesn't make sense for the parse to have to initialize a lua script
        public Script InitializeScript()
        {
            var repo = componentProvider.GetComponent<CommandRepository>();
            var defaultCommands =
                @"function wait()
    local args = args or 1
    for i = 1,args do
        print('lua: wait()')
        coroutine.yield(-1)
    end
end

function move()
    print('lua: move()')
    coroutine.yield(0)
    return true
end

function back()
    print('lua: back()')
    coroutine.yield(1)
end

function right()
    print('lua: right()')
    coroutine.yield(2)
end

function left()
    print('lua: left()')
    coroutine.yield(3)
end";
            var script = new Script();

            script.DoString(defaultCommands);
            foreach (var key in script.Globals.Keys)
            {
                if (!(script.Globals[key] is Closure)) continue;
                repo.Commands.Add(key.String, (Closure)script.Globals[key]);
            }

            return script;
        }
    }
}