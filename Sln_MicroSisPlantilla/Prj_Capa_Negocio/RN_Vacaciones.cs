using Prj_Capa_Datos;
using Prj_Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prj_Capa_Negocio
{
    public class RN_Vacaciones
    {
        public void RN_RegistrarVacaciones(DateTime Desde, DateTime Hasta, string IdEmpleado)
        {
            BD_Vacaciones obj = new BD_Vacaciones();
            obj.BD_RegistrarVacaciones(Desde, Hasta, IdEmpleado);
        }
        public DataTable RN_VerVacaciones()
        {
            BD_Vacaciones obj = new BD_Vacaciones();
            return obj.BD_VerVacaciones();
        }
        public DataTable RN_Buscar_Vacaciones_xValor(string valor)
        {
            BD_Vacaciones obj = new BD_Vacaciones();
            return obj.BD_Buscar_Vacaciones_xValor(valor);
        }
        public void RN_Editar_Vacaciones(EN_Vacaciones per)
        {
            BD_Vacaciones obj = new BD_Vacaciones();
            obj.BD_Editar_Vacaciones(per);
        }
        public bool RN_Verificar_SiTieneVacaciones(string IdVacaciones)
        {
            BD_Vacaciones obj = new BD_Vacaciones();
            return obj.BD_Verificar_SiTieneVacaciones(IdVacaciones);
        }
    }
}
