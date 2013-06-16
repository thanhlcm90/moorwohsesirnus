using System;
using System.Collections.Generic;
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

    }
}
