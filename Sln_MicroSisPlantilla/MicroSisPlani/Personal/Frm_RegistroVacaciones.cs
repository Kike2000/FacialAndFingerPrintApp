using MicroSisPlani.Msm_Forms;
using Prj_Capa_Entidad;
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

namespace MicroSisPlani.Personal
{
    public partial class Frm_RegistroVacaciones : Form
    {
        public static bool edit = false;
        public Frm_RegistroVacaciones()
        {
            InitializeComponent();
        }

        private void Frm_RegistroAsistencias_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Frm_Msm_Bueno ok = new Frm_Msm_Bueno();
            Frm_Filtro fil = new Frm_Filtro();
            RN_Vacaciones obj = new RN_Vacaciones();
            EN_Vacaciones per = new EN_Vacaciones();

            try
            {
                per.Desde = Convert.ToDateTime(dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                per.Hasta = Convert.ToDateTime(dateTimePicker2.Value.ToString("yyyy-MM-dd"));
                per.IdEmpleado = label1.Text;
                per.Id = label2.Text;
                if (per.Desde <= per.Hasta)
                {
                    if (edit == false)
                    {
                        obj.RN_RegistrarVacaciones(per.Desde, per.Hasta, per.IdEmpleado);
                        fil.Show();
                        ok.Lbl_msm1.Text = "Los datos se han guardado de forma correcta";
                        ok.ShowDialog();
                        fil.Hide();
                        this.Tag = "A";
                        this.Close();
                    }
                    else
                    {
                        obj.RN_Editar_Vacaciones(per);
                        fil.Show();
                        ok.Lbl_msm1.Text = "Los datos se han guardado de forma correcta";
                        ok.ShowDialog();
                        fil.Hide();
                        this.Tag = "A";
                        this.Close();
                    }
                    edit = false;
                }
                else
                {
                    MessageBox.Show("La fecha para iniciar vacaciones es menor a la fecha de termino");
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void Buscar_Personal_ParaEditar(string idpersonal)
        {
            label1.Text = idpersonal;
        }
        public void Buscar_Vacaciones_ParaEditar(string idpersonal)
        {
            try
            {
                RN_Vacaciones obj = new RN_Vacaciones();
                DataTable data = new DataTable();

                data = obj.RN_Buscar_Vacaciones_xValor(idpersonal);
                if (data.Rows.Count == 0) return;
                {
                    label2.Text = Convert.ToString(data.Rows[0]["Id"]);
                    dateTimePicker1.Value = Convert.ToDateTime(data.Rows[0]["Desde"]);
                    dateTimePicker2.Value = Convert.ToDateTime(data.Rows[0]["Hasta"]);
                    edit = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar los datos: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
