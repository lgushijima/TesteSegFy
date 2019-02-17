using SegFy.Business.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegFy.Business
{
    public class DBContext : SegFyDBEntities
    {
        public DBContext()
        {
            var type = typeof(System.Data.Entity.SqlServer.SqlProviderServices);

            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }
    }
}
