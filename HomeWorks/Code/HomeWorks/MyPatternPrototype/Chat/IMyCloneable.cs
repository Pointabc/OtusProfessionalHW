namespace MyPatternPrototype;

public interface IMyCloneable<out T>
{
    T Clone();
}
