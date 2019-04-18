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
	return true
end

function right()
    print('lua: right()')
	coroutine.yield(2)
	return true
end

function left()
    print('lua: left()')
	coroutine.yield(3)
	return true
end";

            repo.Script.DoString(defaultCommands);
            foreach (var key in repo.Script.Globals.Keys)
            {
                if (!(repo.Script.Globals[key] is Closure)) continue;
                repo.Commands.Add(key.String, (Closure)repo.Script.Globals[key]);
            }
        }
    }
}
