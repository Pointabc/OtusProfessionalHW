using static System.Console;
using MyPatternPrototype.Chat;

var message = new Message("Петр");
var messageCloned = message.Clone();
// Клон через стандартный ICloneable
var messageClonedStandart = (Message)((ICloneable)message).Clone();
message.Sender = "Петр111";
WriteLine($"Сообщение: {message}");
WriteLine($"Клонированное сообщение: {messageCloned}");
WriteLine($"Через ICloneable: {messageClonedStandart}");

WriteLine(new string('*', 50));
var textMessage = new TextMessage("Андрей", "Привет, Петр.");
var textMessageCloned = textMessage.Clone();
// Клон через стандартный ICloneable
var textMessageClonedStandart = (TextMessage)((ICloneable)textMessage).Clone();
textMessage.Sender = "Андрей_Edited";
WriteLine($"Текстовое сообщение: {textMessage}");
WriteLine($"Клонированное текстовое сообщение: {textMessageCloned}");

WriteLine(new string('*', 50));
var richTextMessage = new RichTextMessage("Иван", "Привет, Марина.", true);
var richTextMessageCloned = richTextMessage.Clone();
// Клон через стандартный ICloneable
var richTextMessageClonedStandart = (RichTextMessage)((ICloneable)richTextMessage).Clone();
richTextMessage.Sender = "Иван_Edited";
WriteLine(richTextMessage);
WriteLine(richTextMessageCloned);

WriteLine(new string('*', 50));

