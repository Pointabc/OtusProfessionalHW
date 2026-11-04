namespace PatternPrototype;

public interface IMyCloneable<out T>
{
    T Clone();
}
