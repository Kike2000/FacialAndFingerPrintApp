using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prj_Capa_Datos
{
    public class BDConexion
    {
        public string Conectar()
        {
            return @"Data Source=LAPTOP-JPJSR5DP\SQLEXPRESS; Initial Catalog=MicroSisPlani;integrated security=true";
            //return @"Data Source=PC-ADMIN\SQLEXPRESS; Initial Catalog=MicroSisPlani;uid=sa;pwd=admin"; ;
        }

        public static string Conectar2()
        {
            return @"Data Source=LAPTOP-JPJSR5DP\SQLEXPRESS; Initial Catalog=MicroSisPlani;integrated security=true";
            //return @"Data Source=PC-ADMIN\SQLEXPRESS; Initial Catalog=MicroSisPlani;uid=sa;pwd=admin"; ;
        }
    }
}
