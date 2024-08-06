

using System.ComponentModel;

namespace SectionProject;

public interface IDeviceManager
{
    void AddDevice(IDevice device);
    void AssignConsumerToDevice(IDevice device, IConsumer consumer);
    int DeviceAmount();
    IEnumerable<IDevice> GetAllDevices();
    void PrintAllDevices();
}


public class DeviceManager : IDeviceManager
{
    IList<IDevice> _devices;
    IConsumerManager _consumerManager = FactoryContainer.ManagerFactory.CreateConsumerManager();

    public DeviceManager(ConsumerManager consumerManager)
    {
        _devices = new List<IDevice>();
        _consumerManager = consumerManager;
    }

    public void AssignConsumerToDevice(IDevice device, IConsumer consumer)
    {
        var ecologicalFactors = consumer.GetEcologicalFactors();
        foreach (var factor in ecologicalFactors)
        {
            _consumerManager.AddEcologicalFactors(factor, consumer);
        }
    }

    public void AddDevice(IDevice device)
    {
        _devices.Add(device);
    }

    public IEnumerable<IDevice> GetAllDevices()
    {
        return _devices;
    }

    public void PrintAllDevices()
    {
        foreach (var item in _devices)
        {
            Console.WriteLine("--> " + item.Notification + " Energie " + item.Energy);
        }
    }

    public int DeviceAmount()
    {
        return _devices.Count();
    }
}

public static class DeviceManagerExtensions
{
    public static decimal TotalRequiredEnergy(this DeviceManager manager)
    {
        decimal totalEnergy = 0;
        foreach (var device in manager.GetAllDevices())
        {
            totalEnergy += device.RequiredEnergy();
        }
        return totalEnergy;
    }
}

public interface IDevice
{
    int Id { get; set; }
    string Notification { get; set; }
    double Weight { get; set; }
    string Hint { get; set; }

    decimal Energy { get; set; }
    decimal RequiredEnergy();
}

public class Device : IDevice
{
    private readonly IDeviceSensor _deviceSensor = FactoryContainer.SensorFactory.GetDeviceSensor();

    public Device()
    {
        AddDeviceEvents();
    }

    private void AddDeviceEvents()
    {
        _deviceSensor.WeightToLowEventArgs += _deviceSensor_WeightToLow;
    }

    private void _deviceSensor_WeightToLow(object? sender, InvalidDeviceEventArgs e)
    {
        Console.WriteLine("Device Sensor " + e.InvalidValue + " " + e.EventTime);
    }

    public int Id { get; set; }
    public virtual string Notification { get; set; } = string.Empty;
    private double _weight;
    public virtual double Weight
    {
        get { return _weight; }
        set {
            if(value <= 10)
            {
                _deviceSensor.OnSensorWeightToLow(FactoryContainer.EventArgsFactory.GetDeviceEventArgs(value));
            }
        }
    }
    public string Hint { get; set; } = string.Empty;
    private decimal _energy;
    public decimal Energy
    {
        get { return _energy; }
        set { _energy = value; }
    }

    public decimal RequiredEnergy()
    {
        return _energy;
    }
}

public class DeA6BC : Device
{
    public override string Notification { get; set; } = "DeA6BC";
    public override double Weight { get { return base.Weight; } }
}

public class DeC671 : Device
{
    public override string Notification { get; set; } = "DeC671";
    public override double Weight { get { return base.Weight; } }
}
public class DeA3B0 : Device
{
    public override string Notification { get; set; } = "DeA3B0";
    public override double Weight { get { return base.Weight; } }
}

//  ------------------------    Sensor

public class DeviceSensor : IDeviceSensor
{
    EventHandlerList _eventHandlerList = new EventHandlerList();
    static readonly object _weightToLow = new Object();

    void IDeviceSensor.OnSensorWeightToLow(InvalidDeviceEventArgs invalidDeviceEventArgs)
    {
        EventHandler<InvalidDeviceEventArgs>? handler = _eventHandlerList[_weightToLow] as EventHandler<InvalidDeviceEventArgs>;
        handler?.Invoke(this, invalidDeviceEventArgs);
    }

    event EventHandler<InvalidDeviceEventArgs> IDeviceSensor.WeightToLowEventArgs
    {
        add { _eventHandlerList.AddHandler(_weightToLow, value); }
        remove { _eventHandlerList.RemoveHandler(_eventHandlerList, value); }
    }
}

public interface IDeviceSensor
{
    void OnSensorWeightToLow(InvalidDeviceEventArgs invalidDeviceEventArgs);
    public event EventHandler<InvalidDeviceEventArgs> WeightToLowEventArgs;
}

public class InvalidDeviceEventArgs : EventArgs
{
    private double _invalidValue;
    public double InvalidValue
    {
        get { return _invalidValue; }
    }

    private DateTime _eventTime;
    public DateTime EventTime
    {
        get { return _eventTime; }
    }

    public InvalidDeviceEventArgs(double invalidValue, DateTime dateTime)
    {
        _invalidValue = invalidValue;
        _eventTime = dateTime;
    }
}

