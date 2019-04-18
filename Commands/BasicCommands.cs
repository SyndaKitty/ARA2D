using System.IO;
using ARA2D.Commands.Components;
using ARA2D.ComponentProvider;
using MoonSharp.Interpreter;

namespace ARA2D.Commands
{
    public class BasicCommands
    {
        public BasicCommands(IComponentProvider componentProvider)
        {
            var repo = componentProvider.GetComponent<CommandRepo>();

            var defaultCommands =
@"function move()
    coroutine.yield(0)
	return true
end

function back()
	coroutine.yield(1)
	return true
end

function right()
	coroutine.yield(2)
	return true
end

function left()
	coroutine.yield(3)
	return true
end";

            Script script = new Script();
            script.DoString(defaultCommands);

            repo.Commands["move"] = script.Globals["move"] as Closure;
            repo.Commands["back"] = script.Globals["back"] as Closure;
            repo.Commands["right"] = script.Globals["right"] as Closure;
            repo.Commands["left"] = script.Globals["left"] as Closure;
        }
    }
}
