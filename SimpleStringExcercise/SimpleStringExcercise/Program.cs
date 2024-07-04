using SimpleStringExcercise;

var stringService = new StringService();
while (true)
{
    Console.WriteLine("Введите строку:");
    var str = Console.ReadLine();

    Console.WriteLine("Заменяем пробелы на звёздочку:");
    var outStr = stringService.ReplaceSpacesInString(str);

    Console.WriteLine("Результат: " + outStr);    
}
