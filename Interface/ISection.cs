namespace Restaurant_Management.Interface;

public interface ISection : ICRUD<Section>
{
    public int? GetId(string name);
}
