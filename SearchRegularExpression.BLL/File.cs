using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchRegularExpression.BLL
{
    public abstract class File
    {
        public string FileLocation;
        public int Count = 0; //кол-во найденых совпадений
        public File(string file)
        {
            FileLocation = file;
        }        
        public abstract int Recognition(string regex);
    }
}
