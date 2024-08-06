// See https://aka.ms/new-console-template for more information
using SectionProject;

Console.WriteLine("Hello, World!");

Department depItHF3C = new Department();

ISection Room01 = new Room();
ISection Corridor01 = new Corridor();
ISection Stairs01 = new Stairs();

ISectionManager sectionManager = depItHF3C.GetSectionManager();
sectionManager.AddSection(Room01);
sectionManager.AddSection(Corridor01);
sectionManager.AddSection(Stairs01);

// depItHF3C.PrintAllSectiona();
// sectionManager.PrintAllSections();

Department depStoB25C = new Department();
ISectionManager sectionManager2 = depStoB25C.GetSectionManager();
sectionManager2.PrintAllSections();

IDevice deA3B0 = new DeA3B0();
deA3B0.Energy = 210;
IDevice deC671 = new DeC671();
deC671.Energy = 300;
IDevice deA6BC = new DeA6BC();
deA6BC.Energy = 100;



DeviceManager deviceManager = new DeviceManager(new ConsumerManager());
deviceManager.AddDevice(deA3B0);
deviceManager.AddDevice(deC671);
deviceManager.AddDevice(deA3B0);

deviceManager.PrintAllDevices();
Console.WriteLine(DeviceManagerExtensions.TotalRequiredEnergy(deviceManager));


IConsumer water1 = new Water(new ConsumerManager());
IConsumer power1 = new Power(new ConsumerManager());
IConsumer air1 = new Air(new ConsumerManager());
IEcologicalFactor pressure1 = new Pressure();

// pressure1.MinValue = 3;

IEcologicalFactor pollution1 = new Pollution();

// pollution1.MinValue = 4;

ConsumerManager consumerManager = new ConsumerManager();
consumerManager.AddEcologicalFactors(pressure1, water1);
consumerManager.AddEcologicalFactors(pollution1, water1);
consumerManager.PrintEcologicalFactors();


deA3B0.Weight = 3;