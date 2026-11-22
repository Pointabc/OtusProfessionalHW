using System.Text;

namespace MyPatternPrototype.Chat;

/// <summary>
/// Текстовое сообщение.
/// </summary>
public class TextMessage : Message, IMyCloneable<TextMessage>, ICloneable
{
    public string Text { get; set; }

    public TextMessage(string sender, string text) : base(sender)
    {
        Text = new StringBuilder(text).ToString();
    }

    protected TextMessage(TextMessage textMessage) : base(textMessage)
    {
        Text = new StringBuilder(textMessage.Text).ToString();
    }

    public new TextMessage Clone()
    {
        return new(this);
    }

    public override string ToString()
    {
        var message = string.IsNullOrWhiteSpace(Text) ? "Отсутствует сообщение" : Text;
        return base.ToString() + $"\nСообщение: {message}";
    }

    object ICloneable.Clone()
    {
        return Clone();
    }
}
