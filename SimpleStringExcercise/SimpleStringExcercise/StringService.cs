using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStringExcercise
{
    public class StringService
    {
        public string ReplaceSpacesInString(string str)
        {
            if (str == null)
                return null;
            str = str.Trim(); // удаляем пробелы в начале и конце строки

            string outStr = "";
            for (int i = 0; i < str.Length; i++) // проходимся по каждому символу строки
            {
                if (str[i] != ' ') // если символ не равен пробелу
                    outStr = outStr + str[i]; // то просто добавляем его результирующей строке
                else if (str[i - 1] != ' ') // если предыдущий символ не равен пробелу
                    outStr += '*'; // то заменяем текущий символ звёздочкой
            }

            return outStr;
        }
    }
}
