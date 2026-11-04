namespace PatternPrototype;

/// <summary>
/// Третий наследник (третье наследование).
/// </summary>
public class SelfDrivingCar : ElectricCar, IMyCloneable<SelfDrivingCar>
{
    public string AutopilotVersion { get; set; }

    public SelfDrivingCar(string brand, int year, int doors, int battery, string autopilot)
        : base(brand, year, doors, battery)
    {
        AutopilotVersion = autopilot;
    }

    protected SelfDrivingCar(SelfDrivingCar other) : base(other)
    {
        AutopilotVersion = other.AutopilotVersion;
    }

    public override SelfDrivingCar Clone() => new SelfDrivingCar(this);

    public override string ToString() => base.ToString() + $", автопилот: {AutopilotVersion}";
}
