namespace ARA2D.Commands
{
    public struct CommandCall
    {
        public string Name;
        public string Arguments;

        public CommandCall(string name, string arguments)
        {
            Name = name;
            Arguments = arguments;
        }
    }
}
