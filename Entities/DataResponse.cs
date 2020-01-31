using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class DataResponse<T> : Response
    {
         public List<T> Data { get; set; }
    }
}
