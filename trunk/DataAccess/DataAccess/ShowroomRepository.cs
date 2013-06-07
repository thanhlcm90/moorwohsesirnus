using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Showroom.Models.DataBusiness;

namespace Showroom.Models.DataAccess
{
    public partial class ShowroomRepository : IShowroomRepository 
    {
        private ShowroomDataContext _dataContext;

        public ShowroomRepository()
        {
            _dataContext = new ShowroomDataContext();
        }
        public void Dispose()
        {
            _dataContext.Dispose();
        }
        
    }
}
