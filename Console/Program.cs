using Core;
using Core.Plugins;
using System;
using System.Text;
using System.Threading;

namespace Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            ConsoleTimeService time = new ConsoleTimeService();
            ConsoleInputService input = new ConsoleInputService();

            EnginePlugins plugins = new EnginePlugins(null, time, input);
            Engine engine = new Engine(plugins);

            ConsoleCommandRunner commandRunner = new ConsoleCommandRunner(time);

            while (true)
            {
                // Because the command runner should effect the LogicContext via the time service ..
                // we have to run this outside of the normal loop.
                //commandRunner.Update(Engine);

                engine.Render();
                engine.Update();

                time.ForceTick = false;

                ReadInput();
                WriteInput();
                Thread.Sleep((int)(time.DeltaTime * 1000));
            }
        }

        static StringBuilder inputBuffer = new StringBuilder();
        static void ReadInput()
        {
            if (System.Console.KeyAvailable)
            {
                var keyChar = System.Console.ReadKey().KeyChar;

                if (keyChar == '\r')
                {
                    var commandEntity = Engine.World.CreateEntity();
                    commandEntity.Set(new ConsoleCommand(inputBuffer.ToString()));
                    inputBuffer.Clear();
                }
                else if (keyChar == 8) // Backspace
                {
                    inputBuffer.Length = Math.Max(0, inputBuffer.Length - 1);
                }
                else
                {
                    inputBuffer.Append(keyChar);
                }
            }
        }

        static void WriteInput()
        {
            // Clear previous values
            //System.Console.SetCursorPosition(0, ConsoleRenderSystem.Height);
            System.Console.Write(new string(' ', System.Console.WindowWidth - 1) + "\r");
            System.Console.Write(inputBuffer.ToString());
        }
    }
}
