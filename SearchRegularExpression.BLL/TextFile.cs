using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SearchRegularExpression.BLL
{
    public class TextFile : File
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public TextFile(string file) : base(file)
        {
            logger.Info("TextFile Object was created");
        }
        public override int Recognition(string regex)
        {
            try
            {
                logger.Info("Start read text file");
                using (var reader = new StreamReader(FileLocation))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        bool regexFound = Regex.IsMatch(line, regex, RegexOptions.IgnoreCase);
                        if (regexFound)
                            Count++;
                    }
                    logger.Info("End read text file");
                }
                logger.Info("Number of matches: " + Count);
                return Count;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return 0;
            }
        }
    }
}
