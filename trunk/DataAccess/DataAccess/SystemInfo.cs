using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Showroom.Models.DataAccess
{
    public partial class ShowroomRepository
    {
        public List<SystemInfo> GetSystemInfoList()
        {
            var list = from p in _dataContext.SystemInfos select p;
            return list.ToList();
        }

        public Boolean UpdateSystemInfo(List<SystemInfo> list)
        {
            try
            {
                _dataContext.SystemInfos.DeleteAllOnSubmit(_dataContext.SystemInfos);
                _dataContext.SystemInfos.InsertAllOnSubmit(list);
                _dataContext.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
