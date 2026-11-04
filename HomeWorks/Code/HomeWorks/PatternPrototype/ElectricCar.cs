namespace PatternPrototype;

/// <summary>
/// Второй наследник (второе наследование).
/// </summary>
public class ElectricCar : Car, IMyCloneable<ElectricCar>
{
    public int BatteryCapacity { get; set; }

    public ElectricCar(string brand, int year, int doors, int battery)
        : base(brand, year, doors)
    {
        BatteryCapacity = battery;
    }

    protected ElectricCar(ElectricCar other) : base(other)
    {
        BatteryCapacity = other.BatteryCapacity;
    }

    public override ElectricCar Clone() => new ElectricCar(this);

    public override string ToString() => base.ToString() + $", батарея: {BatteryCapacity} кВт·ч";
}
