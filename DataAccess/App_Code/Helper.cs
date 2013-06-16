using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
namespace Common
{
    public static class clsHelper
    {

        /// <summary>
        /// Trả về phần đầu của chuỗi và đảm bảo đủ từ
        /// </summary>
        /// <param name="s"></param>
        /// <param name="length">Số ký tự tối đa được cắt</param>
        /// <returns>Chuoi can cat</returns>
        public static string CatChuoi(string s, int length)
        {
            if (String.IsNullOrEmpty(s))
                return "";
            var words = s.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (words[0].Length > length)
                return s;
            var sb = new StringBuilder();
            foreach (var word in words)
            {
                if ((sb + word).Length > length)
                    return string.Format("{0}...", sb.ToString().TrimEnd(' '));
                sb.Append(word + " ");
            }
            return string.Format("{0}...", sb.ToString().TrimEnd(' '));
        }

        public static ArrayList GetImageProduct(int id)
        {
            var productFolder = AppDomain.CurrentDomain.BaseDirectory + "Images\\Product\\" + id;
            var productFrontendFolder = productFolder;
            productFolder = productFolder.Replace("Frontend", "Backend");
            var ProducImagePath = "/Images/Product/" + id;
            DirectoryInfo dir = new DirectoryInfo(productFolder);
            ArrayList list = new ArrayList();
            if (Directory.Exists(productFolder))
            {
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    if (file.Extension == ".jpg" || file.Extension == ".jpeg" || file.Extension == ".gif" || file.Extension == ".png")
                    {
                        //Kiểm tra có thư mục Images/Product ở frontend chưa.
                        if (!Directory.Exists(productFrontendFolder))
                            Directory.CreateDirectory(productFrontendFolder);

                        //Save ảnh từ backend qua frontend.
                        file.CopyTo(Path.Combine(productFrontendFolder.ToString(), file.Name), true);
                        list.Add(ProducImagePath + "/" + file.Name);
                    }
                }
            }
            return list;
        }


    }
}
