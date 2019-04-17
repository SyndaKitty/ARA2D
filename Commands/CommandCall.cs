namespace ARA2D.Commands
{
    public struct CommandCall
    {
        public string CommandName;
        public string Arguments;

        public CommandCall(string commandName, string arguments)
        {
            CommandName = commandName;
            Arguments = arguments;
        }
    }
}
