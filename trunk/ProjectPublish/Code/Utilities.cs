using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace SunriseShowroom.Code
{
    public class Utilities
    {
        public static string ConvertToUnSign(string s)
        {
            var regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            var temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D').Replace(' ', '-').Replace("---", "-").Replace("--", "-");
        }

        /// <summary>
        /// Xóa files trong thư mục
        /// </summary>
        /// <param name="directoryPath"></param>
        public  static  void DeleteFiles(string directoryPath)
        {
            try
            {
                if (Directory.Exists(directoryPath))
                {
                    var dir1 = new DirectoryInfo(directoryPath);
                    FileInfo[] fis = dir1.GetFiles();
                    foreach (FileInfo fi in fis)
                    {
                        fi.Delete();
                    }
                }
            }
            catch
            {
            }
        }
    }
}