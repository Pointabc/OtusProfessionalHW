namespace PatternPrototype;

/// <summary>
/// Первый наследник.
/// </summary>
public class Car : Vehicle, IMyCloneable<Car>
{
    public int Doors { get; set; }

    public Car(string brand, int year, int doors) : base(brand, year)
    {
        Doors = doors;
    }

    protected Car(Car other) : base(other)
    {
        Doors = other.Doors;
    }

    public override Car Clone() => new Car(this);

    public override string ToString() => base.ToString() + $", дверей: {Doors}";
}
