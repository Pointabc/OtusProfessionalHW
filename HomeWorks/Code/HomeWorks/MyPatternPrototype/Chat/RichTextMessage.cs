namespace MyPatternPrototype.Chat;

public class RichTextMessage : TextMessage, IMyCloneable<RichTextMessage>, ICloneable
{
    public bool IsBold { get; set; }

    public RichTextMessage(string sender, string text, bool isBold)
        : base(sender, text) => IsBold = isBold;

    public RichTextMessage(RichTextMessage textMessage) : base(textMessage)
    {
        IsBold = textMessage.IsBold;
    }

    public new RichTextMessage Clone()
    {
        return new(this);
    }

    public override string ToString()
    {
        return base.ToString() + $"\nПолужирный: {IsBold}";
    }

    object ICloneable.Clone()
    {
        return Clone();
    }
}
