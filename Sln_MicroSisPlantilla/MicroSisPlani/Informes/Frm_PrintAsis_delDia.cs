using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Prj_Capa_Negocio;

namespace MicroSisPlani.Informes
{
    public partial class Frm_PrintAsis_delDia : Form
    {
        public Frm_PrintAsis_delDia()
        {
            InitializeComponent();
        }
        public string tipoInfo = "";
        private void Frm_PrintAsis_delDia_Load(object sender, EventArgs e)
        {
            if (tipoInfo == "deldia")
            {
                Generar_InformedelDia();
            }
            else if (tipoInfo == "delmes")
            {
                Generar_InformedelMes();
            }
            else if (tipoInfo == "porPersona")
            {
                Generar_Informe_PorPersona();
            }
        }

        private void Generar_InformedelDia()
        {
            RN_Asistencia obj = new RN_Asistencia();
            DataTable data = new DataTable();

            data = obj.RN_Ver_Todas_Asistencia_Deldia(Convert.ToDateTime(this.Tag));
            if (data.Rows.Count > 0)
            {
                Rpte_AsistenciasDelDia rpte = new Rpte_AsistenciasDelDia();
                vsr_Infodia.ReportSource = rpte;
                rpte.SetDataSource(data);
                rpte.Refresh();
                vsr_Infodia.ReportSource = rpte;
            }
        }

        private void Generar_InformedelMes()
        {
            RN_Asistencia obj = new RN_Asistencia();
            DataTable data = new DataTable();

            data = obj.RN_Ver_Todas_Asistencia_DelMes(Convert.ToDateTime(this.Tag));
            if (data.Rows.Count > 0)
            {
                Rpte_Asistencia_delMes rpte = new Rpte_Asistencia_delMes();
                vsr_Infodia.ReportSource = rpte;
                rpte.SetDataSource(data);
                rpte.Refresh();
                vsr_Infodia.ReportSource = rpte;
            }
        }
        private void Generar_Informe_PorPersona()
        {
            RN_Asistencia obj = new RN_Asistencia();
            DataTable data = new DataTable();

            data = obj.RN_Ver_Todas_Asistencia_ParaExplorador(this.Tag.ToString());
            if (data.Rows.Count > 0)
            {
                Rpte_Asistencia_porPersona rpte = new Rpte_Asistencia_porPersona();
                vsr_Infodia.ReportSource = rpte;
                rpte.SetDataSource(data);
                rpte.Refresh();
                vsr_Infodia.ReportSource = rpte;
            }
        }

        private void pnl_titulo_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button==MouseButtons.Left)
            {
                Utilitarios u = new Utilitarios();
                u.Mover_formulario(this);
            }
        }

        private void btn_Cerrar_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btn_Imprimir_Click(object sender, EventArgs e)
        {
            vsr_Infodia.PrintReport();
        }

        private void btn_exportar_Click(object sender, EventArgs e)
        {
            vsr_Infodia.ExportReport();
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            vsr_Infodia.Refresh();
        }
    }
}
