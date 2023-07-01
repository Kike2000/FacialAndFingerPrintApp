using Prj_Capa_Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MicroSisPlani.Informes
{
    public partial class Frm_PrintJustificaciones : Form
    {
        public Frm_PrintJustificaciones()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            Generar_Justificaciones();
        }
        private void Generar_Justificaciones()
        {
        }

    }
}
