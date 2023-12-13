namespace Restaurant_Management.Interface;

public interface ISection : ICrud<Section>
{
    public int? GetId(string name);
}
