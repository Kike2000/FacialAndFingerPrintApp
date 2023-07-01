using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prj_Capa_Entidad
{
    public class EN_Vacaciones
    {
        string IdVacaciones;
        string _Id_Personal;
        DateTime _FechaDesde;
        DateTime _FechaHasta;

        public string Id { get => IdVacaciones; set => IdVacaciones = value; }
        public string IdEmpleado { get => _Id_Personal; set => _Id_Personal = value; }
        public DateTime Desde { get => _FechaDesde; set => _FechaDesde = value; }
        public DateTime Hasta { get => _FechaHasta; set => _FechaHasta = value; }

    }
}
