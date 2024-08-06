



namespace SectionProject;

public interface ISectionManager
{
    public void AddSection(ISection section);
    public void PrintAllSections();
}

public class SectionManager : ISectionManager
{
    private List<ISection> _sections = new List<ISection>();

    public List<ISection> Sections
    {
        get { return _sections; }
    }

    public void AddSection(ISection section)
    {
        if (section != null) _sections.Add(section);
    }

    public void PrintAllSections()
    {
        foreach (var item in _sections)
        {
            Console.WriteLine("Sectionen " + item.SectionName);
        }
    }
}

public interface ISection
{
    double Length { get; set; }
    string SectionName { get; set; } 
}

public class Section : ISection
{
    public double Length { get; set; }
    public virtual string SectionName { get; set; } = string.Empty;
}

public class Corridor : Section
{
    public override string SectionName { get; set; } = "Corridor";
}
public class Room : Section
{
    public override string SectionName { get; set; } = "Room";
}
public class Stairs : Section
{
    public override string SectionName { get; set; } = "Stairs";
}


