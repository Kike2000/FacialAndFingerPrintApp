using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prj_Capa_Datos
{
   public class Cls_Conexion
    {
        public string Conectar()
        {
            return @"Data Source=LAPTOP-JPJSR5DP\SQLEXPRESS; Initial Catalog=BaseDeDatosIlomo;integrated security=true";
            
        }

        public static string Conectar2()
        {
            return @"Data Source=LAPTOP-JPJSR5DP\SQLEXPRESS; Initial Catalog=BaseDeDatosIlomo;integrated security=true";
            
        }
    }
}
