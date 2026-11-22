namespace MyPatternPrototype.Chat;

public class ReplyMessage : RichTextMessage, IMyCloneable<ReplyMessage>
{
    public Message QuotedMessage { get; set; }

    public ReplyMessage(string sender, string text, bool isBold, Message quotedMessage)
        : base(sender, text, isBold)
    {
        QuotedMessage = quotedMessage;
    }

    public ReplyMessage(ReplyMessage replyMessage) : base(replyMessage)
    {
        QuotedMessage = replyMessage.QuotedMessage;
    }

    public new ReplyMessage Clone()
    {
        return new(this);
    }

    public override string ToString()
    {
        return base.ToString() + $"; Процитированное сообщение: {QuotedMessage}";
    }
}
