using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Showroom.Models.DataBusiness
{
    public interface  IShowroomRepository
    {
        void Dispose();

        List<Catalogue> GetProductCatalogueList();
        Catalogue GetProductCatalogueInfo(int id);
        bool InsertProductCatalogue(Catalogue item);
        bool UpdateProductCatalogue(Catalogue item);
        bool DeleteProductCatalogue(int Id);

    }
}
