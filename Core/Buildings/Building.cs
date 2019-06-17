namespace Core.Buildings
{
    public class Building
    {
        public BuildingType Type;

        public Building(BuildingType type)
        {
            Type = type;
        }
    }

    public enum BuildingType
    {
        None,
        Test,
        Computer
    }
}
