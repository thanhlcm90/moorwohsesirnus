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
        ///  Thực hiện chức năng copy ảnh từ folder này sang folder khác.
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <param name="newDirectoryPath"></param>
        /// <param name="deleteOld">Xóa tất cả ảnh cũ </param>
        /// <returns></returns>
        public static  bool PublishImages (string directoryPath,string newDirectoryPath,bool deleteOld)
        {
            var dir = new DirectoryInfo(directoryPath);
            if (Directory.Exists(directoryPath))
            {
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    if (file.Extension == ".jpg" || file.Extension == ".jpeg" || file.Extension == ".gif" || file.Extension == ".png")
                    {
                        //Kiểm tra có thư mục Images/Product ở frontend chưa.
                        if (!Directory.Exists(newDirectoryPath))
                            Directory.CreateDirectory(newDirectoryPath);
                        //Save ảnh từ backend qua frontend.
                        try
                        {
                            //Xóa ảnh cũ ở front end.
                            if (deleteOld)
                            {
                                var dir1 = new DirectoryInfo(newDirectoryPath);
                                FileInfo[] fis = dir1.GetFiles();
                                 foreach (FileInfo fi in fis)
                                 {
                                     fi.Delete();
                                 }
                            }
                            file.CopyTo(Path.Combine(newDirectoryPath, file.Name), true);
                        }
                        catch (Exception)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            return false;
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