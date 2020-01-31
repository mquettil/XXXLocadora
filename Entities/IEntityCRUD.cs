using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    //CRUD -> CREATE READ UPDATE DELETE
    public interface IEntityCRUD<T> 
    {
        Response Insert(T item);
        Response Update(T item);
        Response Delete(int id);
        DataResponse<T> GetData();
        DataResponse<T> GetByID(int id);
    }
}
