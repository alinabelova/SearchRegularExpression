using SearchRegularExpression.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchRegularExpression.BLL
{
    public class SelectionFileFactory : IFileFactory
    {
        public File CreateFile(string fileLocation) //входной параметр - путь к файлу
        {
            //проверяем тип файла, и в зависимости от типа возвращаем необходимый класс
            if (fileLocation.EndsWith(".xls") || fileLocation.EndsWith(".xlsx"))
                return new ExcelFile(fileLocation);
            else if(fileLocation.EndsWith(".txt"))
                return new TextFile(fileLocation);
            return null;
        }
    }
}
