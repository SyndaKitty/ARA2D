using System;
using ARA2D.Commands.Components;
using ARA2D.ComponentProvider;
using MoonSharp.Interpreter;

namespace ARA2D.Commands
{
    public class BasicCommands
    {
        public BasicCommands(IComponentProvider componentProvider)
        {
            var repo = componentProvider.GetComponent<CommandRepository>();

            var defaultCommands =
@"function wait()
    local args = args or 1
    for i = 1,args do
        coroutine.yield(-1)
    end
end

function move()
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

            repo.Script.DoString(defaultCommands);
            foreach (var key in repo.Script.Globals.Keys)
            {
                if (!(repo.Script.Globals[key] is Closure)) continue;
                repo.Commands.Add(key.String, (Closure)repo.Script.Globals[key]);
            }

            // TODO: Take this test code out
            repo.Script.Globals["args"] = DynValue.NewNumber(4);
            DynValue coroutine = repo.Script.CreateCoroutine(repo.Commands["wait"]);
            DynValue returnValue = coroutine.Coroutine.Resume();
            Console.WriteLine(returnValue.Number);
        }
    }
}
