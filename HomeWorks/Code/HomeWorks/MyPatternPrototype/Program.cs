using static System.Console;
using MyPatternPrototype.Chat;

var message = new Message("Петр");
var messageCloned = message.Clone();
WriteLine("Меняем изначальное сообщение");
message.Sender = "Петр111";
WriteLine($"Сообщение: {message}");
WriteLine($"Клонированное сообщение: {messageCloned}");

WriteLine(new string('*', 50));
var textMessage = new TextMessage("Андрей", "Привет, Петр.");
var textMessageCloned = textMessage.Clone();
textMessage.Sender = "Андрей_Edited";
WriteLine($"Текстовое сообщение: {textMessage}");
WriteLine($"Клонированное текстовое сообщение: {textMessageCloned}");

WriteLine(new string('*', 50));
var richTextMessage = new RichTextMessage("Иван", "Привет, Марина.", true);
var richTextMessageCloned = richTextMessage.Clone();
richTextMessage.Sender = "Иван_Edited";
WriteLine(richTextMessage);
WriteLine(richTextMessageCloned);

WriteLine(new string('*', 50));

