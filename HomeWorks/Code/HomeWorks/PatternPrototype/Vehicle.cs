namespace PatternPrototype;

/// <summary>
/// Базовый класс.
/// </summary>
public class Vehicle : IMyCloneable<Vehicle>, ICloneable
{
    public string Brand { get; set; }
    public int Year { get; set; }

    public Vehicle(string brand, int year)
    {
        Brand = brand;
        Year = year;
    }

    // Защищённый конструктор копирования для наследников
    protected Vehicle(Vehicle other)
    {
        Brand = other.Brand;
        Year = other.Year;
    }

    public virtual Vehicle Clone() => new Vehicle(this);

    object ICloneable.Clone() => Clone();

    public override string ToString() => $"{GetType().Name}: {Brand}, {Year}";
}
