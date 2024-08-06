



namespace SectionProject;

public interface IConsumerManager
{
    void AddEcologicalFactors(IEcologicalFactor ecologicalFactor, IConsumer consumer);
    IEnumerable<IEcologicalFactor> GetEcologicalFactorsForConsumer(IConsumer consumer);
    void PrintEcologicalFactors();
}

public class ConsumerManager : IConsumerManager
{
    public Dictionary<IEcologicalFactor, IConsumer> _ecologicalFaktors = new Dictionary<IEcologicalFactor, IConsumer>();
    public void AddEcologicalFactors(IEcologicalFactor ecologicalFactor, IConsumer consumer)
    {
        _ecologicalFaktors[ecologicalFactor] = consumer;
    }

    public void PrintEcologicalFactors()
    {
        foreach (var item in _ecologicalFaktors)
        {
            Console.WriteLine("\tEcologicalFactor: " + item.Key.EcologicalType + " " + item.Value.ConsumptionType);
        }
    }

    public IEnumerable<IEcologicalFactor> GetEcologicalFactorsForConsumer(IConsumer consumer)
    {
        return _ecologicalFaktors.Where(f => f.Value == consumer).Select(f => f.Key);
    }
}

public interface IConsumer
{
    string ConsumptionType { get; set; }
    IEnumerable<IEcologicalFactor> GetEcologicalFactors();
}

public class Consumer : IConsumer
{
    public string ConsumptionType { get; set; } = string.Empty;

    private readonly IConsumerManager _consumerManager;

    public Consumer(ConsumerManager consumerManager)
    {
        _consumerManager = consumerManager;
    }

    public IEnumerable<IEcologicalFactor> GetEcologicalFactors()
    {
        return _consumerManager.GetEcologicalFactorsForConsumer(this);
    }
}

public class Power : Consumer
{
    public Power(ConsumerManager consumerManager) : base(consumerManager)
    {
        ConsumptionType = "Power";
    }
}
public class Water : Consumer
{
    public Water(ConsumerManager consumerManager) : base(consumerManager)
    {
        ConsumptionType = "Water";
    }
}
public class Air : Consumer
{
    public Air(ConsumerManager consumerManager) : base(consumerManager)
    {
        ConsumptionType = "Air";
    }
}