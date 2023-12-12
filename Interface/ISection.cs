namespace Restaurant_Management.Interface;

public interface ISection : ISpecial<Section>
{
    public int? GetId(string name);
}
