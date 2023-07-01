using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MicroSisPlani;
using Prj_Capa_Negocio;

namespace Inter
{
    public class Intermedio
    {
        public System.Windows.Forms.Form Inicio()
        {
            Frm_Login f = new Frm_Login();
            return f;
        }
        public bool Registro(string name)
        {
            RN_Personal obj = new RN_Personal();
            RN_Asistencia objas = new RN_Asistencia();
            DataTable datoPersona = new DataTable();
            DataTable dataAsis = new DataTable();
            datoPersona = obj.RN_Buscar_Personal_xValor(name.Trim());
            var dt = datoPersona.Rows[0];
            var Lbl_Idperso = Convert.ToString(dt["Id_Pernl"]);
            var lbl_IdAsis = RN_Utilitario.RN_NroDoc(3);
            objas.RN_Registrar_Entrada_Personal(lbl_IdAsis, Lbl_Idperso, DateTime.Now.ToString(), 8, 8, "Tardanza Justificada");
            MessageBox.Show("Entrada registrada");
            return true;
        }
    }
}
