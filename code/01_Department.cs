

namespace SectionProject;


public class Department
{
    private ISectionManager _sectionManager;

    public Department()
    {
        _sectionManager = FactoryContainer.ManagerFactory.CreateSectionManager();
    }

    public ISectionManager GetSectionManager()
    {
        return _sectionManager;
    }

    public void PrintAllSections()
    {
        _sectionManager.PrintAllSections();
    }
}

public static class FactoryContainer
{
    public static class SensorFactory
    {
        public static IDeviceSensor GetDeviceSensor()
        {
            return new DeviceSensor();
        }
    }

    public static class EventArgsFactory
    {
        public static InvalidDeviceEventArgs GetDeviceEventArgs(double value)
        {
            return new InvalidDeviceEventArgs(value, DateTime.Now);
        }
        public static InvalidEventArgs GetInvalidEventArgs(Exception error, int invalidValue, DateTime dateTime)
        {
            return new InvalidEventArgs(error, invalidValue, dateTime);
        }
    }

    public static class ManagerFactory
    {
        public static ISectionManager CreateSectionManager()
        {
            return new SectionManager();
        }
        public static IDeviceManager CreateDeviceManager()
        {
            return new DeviceManager(new ConsumerManager());
        }
        public static IConsumerManager CreateConsumerManager()
        {
            return new ConsumerManager();
        }
    }

    public static class Sectionfactory
    {
        public static ISection CreateRoom()
        {
            return new Room();
        }
        public static ISection CreateStairs()
        {
            return new Stairs();
        }
        public static ISection CreateCorridor()
        {
            return new Corridor();
        }
    }

    public static class DeviceFactory
    {
        public static IDevice CreateDevice(string type)
        {
            switch (type.ToLower())
            {
                case "dea6bc":
                    return new DeA6BC();
                case "dec671":
                    return new DeC671();
                case "dea3b0":
                    return new DeA3B0();
                default:
                    throw new ArgumentException("Unbekannter Gerätetyp", nameof(type));
            }
        }
    }
}