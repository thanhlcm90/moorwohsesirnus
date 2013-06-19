using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
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
