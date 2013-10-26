using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadeInHouse.Manager
{
    class TableManager
    {

        public EntityManager getInstance(int indexTable) {
            EntityManager eM=null;

          

               if (indexTable == EntityName.Proveedor )
                    eM = new ProveedorManager();
       
        

            return eM;
        }
    }
}
