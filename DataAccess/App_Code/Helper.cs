using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System;

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
        
        /// <summary>
        /// Đường dẫn thư mục cần lấy ảnh
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public static ArrayList GetImageInFolder(string strPath,string returnFolder)
        {
            var list = new ArrayList();

            var dir = new DirectoryInfo(strPath);
            if (Directory.Exists(strPath))
            {
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    var extension = file.Extension.ToLower();
                    if (extension == ".jpg" || extension == ".jpeg" || extension == ".gif" || extension == ".png")
                    {
                        list.Add(returnFolder + "/" + file.Name);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// Đọc nội dung của file
        /// </summary>
        /// <param name="strPath">đ</param>
        /// <returns></returns>
        public static string ReadFile(string strPath)
        {

            string strReturn = "";
            try
            {
                // create reader & open file
                using (StreamReader tr = new StreamReader(HttpContext.Current.Server.MapPath(strPath)))
                {
                    // read a line of text
                    strReturn = tr.ReadToEnd();
                    // close the stream
                    tr.Close();
                }
            }
            catch (Exception ex)
            {
                //string strContentLog = DateTime.Now.ToString() + "OSPortal\\ReadFile " + ex.ToString();
            }
            return strReturn;
        }

        /// <summary>
        /// Ghi file txt
        /// </summary>
        /// <param name="strContent">Nội dung cần ghi</param>
        /// <param name="strPath">Đường dẫn lưu file</param>
        public static void WriteFile(string strContent, string strPath)
        {
            // create a writer and open the file
            using (TextWriter tw = new StreamWriter(HttpContext.Current.Server.MapPath(strPath)))
            {
                // write a line of text to the file
                tw.WriteLine(strContent);
                // close the stream
                tw.Close();
            }
        }

        /// <summary>
        /// Xóa bỏ các thẻ html
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ClearTagHTML(string str)
        {
            return Regex.Replace(str, @"<(.|\n)*?>", string.Empty);
        }


        /// <summary>
        /// Get Route
        /// </summary>
        /// <param name="objPage">this.Page</param>
        /// <param name="strParam">Param</param>
        /// <returns></returns>
        //public string fncGetRoute(System.Web.UI.Page objPage, string strParam)
        //{
        //    string strRoute = fncCnvNullToString(objPage.RouteData.Values[strParam]);
        //    return strRoute;
        //}

        //******************************************************************
        //　　　FUNCTION     : Check is numeric
        //　　　MEMO         : 無し 
        //　　　VALUE        : Boolean      Nullチェック済みの値
        //      PARAMS       : Object       値
        //      CREATE       : 2011/08/02   AKB Thuan
        //      UPDATE       : 
        //******************************************************************
        public static bool IsNumeric(object vobjValue)
        {
            try
            {
                Convert.ToDouble(vobjValue);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //******************************************************************
        //　　　FUNCTION     : 空白値を確認 
        //　　　MEMO         : 無し 
        //　　　VALUE        : Boolean   True:空白　False:空白無い 
        //      PARAMS       : Object    確認したい値 
        //      CREATE       : 2009/08/28   AKB 
        //      UPDATE       : 
        //******************************************************************
        public static bool fncIsBlankString(object vstrValue)
        {

            try
            {
                string strTmp = null;

                strTmp = fncCnvNullToString(vstrValue).Trim();


                if (string.IsNullOrEmpty(strTmp))
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                //OsPortal.oFileHelper.WriteLogErr("clsCommon", "fncIsBlankString", ex.ToString());
            }

            return false;

        }

        //******************************************************************
        //　　　FUNCTION     : Nullの場合""にして返す 
        //　　　MEMO         : 無し 
        //　　　VALUE        : String Nullチェック済みの値 
        //      PARAMS       : Object 値 
        //      CREATE       : 2009/09/02   AKB 
        //      UPDATE       : 
        //******************************************************************
        public static string fncCnvNullToString(object vobjValue)
        {
            string strRet = null;

            try
            {
                strRet = "";

                if ((vobjValue != null))
                {
                    strRet = Convert.ToString(vobjValue);
                }

                return strRet;
            }
            catch (Exception ex)
            {
                //OsPortal.oFileHelper.WriteLogErr("clsCommon", "fncCnvNullToString", ex.ToString());
            }
            return strRet;

        }

        //******************************************************************
        //　　　FUNCTION     : Nullの場合 0 にして返す
        //　　　MEMO         : 無し 
        //　　　VALUE        : integer      Nullチェック済みの値
        //      PARAMS       : Object       値
        //      CREATE       : 2009/09/02   ThuanNH 
        //      UPDATE       : 
        //******************************************************************
        public static int fncCnvNullToInt(object vobjValue)
        {
            int intValue = 0;
            try
            {
                if (fncIsBlankString(vobjValue))
                    return 0;

                if (!IsNumeric(vobjValue))
                {
                    return 0;
                }
                else
                {
                    intValue = Convert.ToInt32(vobjValue);
                }

            }
            catch (Exception ex)
            {

            }

            return intValue;
        }

    }
}
