using PatternPrototype;

var original = new SelfDrivingCar("Tesla", 2024, 4, 100, "FSD v12");

// Клон через IMyCloneable<T>
var clone1 = original.Clone();
clone1.Brand = "Tesla Clone";
clone1.AutopilotVersion = "FSD v13";

// Клон через стандартный ICloneable
var clone2 = (SelfDrivingCar)((ICloneable)original).Clone();

Console.WriteLine("Оригинал: " + original);
Console.WriteLine("Клон 1:   " + clone1);
Console.WriteLine("Клон 2:   " + clone2);

Console.WriteLine($"Оригинал == Клон1? {ReferenceEquals(original, clone1)}");
Console.WriteLine($"Оригинал тип: {original.GetType().Name}, Клон тип: {clone1.GetType().Name}");
