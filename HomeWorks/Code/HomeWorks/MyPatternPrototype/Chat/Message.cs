using System.Text;

namespace MyPatternPrototype.Chat;

/// <summary>
/// Базовый класс Сообщение.
/// </summary>
public class Message : IMyCloneable<Message>, ICloneable
{
    /// <summary>
    /// ИД сообщения.
    /// </summary>
    public Guid Id { get; } = Guid.NewGuid();
    /// <summary>
    /// Отправитель.
    /// </summary>
    public string Sender { get; set; }
    public DateTime SentAt { get; } = DateTime.UtcNow;

    public Message(string sender)
    {
        Sender = new StringBuilder(sender).ToString();
    }

    protected Message(Message message)
    {
        Id = Guid.NewGuid(); // Не копируем, тк должен быть уникальным.
        Sender = new StringBuilder(message.Sender).ToString();
        SentAt = message.SentAt;
    }

    /// <summary>
    /// Реализовать глубокое клонирование.
    /// </summary>
    /// <returns></returns>
    public Message Clone()
    {
        return new(this);
    }

    public override string ToString()
    {
        return $"ИД сообщения: {Id}\nОтправлено: {SentAt:HH:mm}\nОтправитель: {Sender}";
    }

    object ICloneable.Clone()
    {
        return Clone();
    }
}
