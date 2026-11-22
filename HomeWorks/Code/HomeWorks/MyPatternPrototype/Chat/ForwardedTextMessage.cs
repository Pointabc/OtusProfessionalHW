using System.Text;

namespace MyPatternPrototype.Chat;

/// <summary>
/// Пересланное сообщение.
/// </summary>
public class ForwardedTextMessage : RichTextMessage, IMyCloneable<ForwardedTextMessage>
{
    public string OriginalSender { get; set; }
    public DateTime OriginalSentAt { get; set; }

    public ForwardedTextMessage(string sender, RichTextMessage originalMessage)
        : base(sender, originalMessage.Text, originalMessage.IsBold)
    {
        OriginalSender = originalMessage.Sender;
        OriginalSentAt = originalMessage.SentAt;
    }

    public ForwardedTextMessage(ForwardedTextMessage forwardedTextMessage) : base(forwardedTextMessage)
    {
        OriginalSender = new StringBuilder(forwardedTextMessage.Sender).ToString();
        OriginalSentAt = forwardedTextMessage.SentAt;
        Text = new StringBuilder(forwardedTextMessage.Text).ToString();
    }

    public new ForwardedTextMessage Clone()
    {
        return new(this);
    }

    public override string ToString()
    {
        return base.ToString() + $"; От кого сообщение: {OriginalSender}; Когда было создано сообщение: {OriginalSentAt}";
    }
}
