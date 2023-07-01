using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Prj_Capa_Datos;
using Prj_Capa_Negocio;
using Prj_Capa_Entidad;
using MicroSisPlani.Msm_Forms;
using System.IO;

namespace MicroSisPlani
{
    public partial class Frm_Marcar_Asis_Manual : Form
    {
        public Frm_Marcar_Asis_Manual()
        {
            InitializeComponent();
        }

        private void Frm_Marcar_Asis_Manual_Load(object sender, EventArgs e)
        {
            CargarHorarios();
            txt_dni_Buscar.Focus();

        }
        private DPFP.Verification.Verification Verificar;
        private DPFP.Verification.Verification.Result Resultado;
        private void CargarHorarios()
        {
            RN_Horario obj = new RN_Horario();
            DataTable data = new DataTable();

            data = obj.RN_Leer_Horarios();
            if (data.Rows.Count == 0) return;
            dtp_horaIngre.Value = Convert.ToDateTime(data.Rows[0]["HoEntrada"]);
            Lbl_HoraEntrada.Text = dtp_horaIngre.Value.Hour.ToString() + ":" + dtp_horaIngre.Value.Minute.ToString();
            dtp_horaSalida.Value = Convert.ToDateTime(data.Rows[0]["HoSalida"]);
            dtp_hora_tolercia.Value = Convert.ToDateTime(data.Rows[0]["MiTolrncia"]);
            Dtp_Hora_Limite.Value = Convert.ToDateTime(data.Rows[0]["HoLimite"]);
        }
        private void txt_dni_Buscar_Click(object sender, EventArgs e)
        {

        }

        private void btn_buscar_Click(object sender, EventArgs e)
        {
            RN_Personal obj = new RN_Personal();
            RN_Asistencia objas = new RN_Asistencia();
            DataTable datoPersona = new DataTable();
            DataTable dataAsis = new DataTable();
            Frm_Filtro fil = new Frm_Filtro();
            Frm_Advertencia adver = new Frm_Advertencia();

            string NroIdPersona;
            int cont = 1;
            string turaFoto;

            int HoLimite = 20;//Dtp_Hora_Limite.Value.Hour;
            int MiLimite = Dtp_Hora_Limite.Value.Minute;

            int horaCaptu = DateTime.Now.Hour;
            int minutoCaptu = DateTime.Now.Minute;

            try
            {
                datoPersona = obj.RN_Buscar_Personal_xValor(txt_dni_Buscar.Text.Trim());
                if (datoPersona.Rows.Count <= 0)
                {
                    lbl_msm.BackColor = Color.MistyRose;
                    lbl_msm.ForeColor = Color.Red;
                    lbl_msm.Text = "El código no existe";
                    //tocar_timbre();
                    lbl_Cont.Text = "10";
                    pnl_Msm.Visible = true;
                    tmr_Conta.Enabled = true;
                    return;
                }
                else
                {
                    var dt = datoPersona.Rows[0];
                    turaFoto = Convert.ToString(dt["Foto"]);
                    lbl_nombresocio.Text = Convert.ToString(dt["Nombre_Completo"]);
                    lbl_Dni.Text = Convert.ToString(dt["DNIPR"]);
                    lbl_Dni.Text = Convert.ToString(dt["Id_Pernl"]);
                    Lbl_Idperso.Text = Convert.ToString(dt["Id_Pernl"]);
                    if (File.Exists(turaFoto) == true)
                    {
                        picSocio.Load(Application.StartupPath + @"\user.png");
                    }
                    else
                    {
                        picSocio.Load(Application.StartupPath + @"\user.png");
                    }

                    if (objas.RN_Checar_SiPersonal_YaMarco_Asistencia(Lbl_Idperso.Text) == true)
                    {
                        lbl_msm.BackColor = Color.MistyRose;
                        lbl_msm.ForeColor = Color.Red;
                        lbl_msm.Text = "El sistema verificó, que el empleado ya marcó su entrada";
                        //tocar_timbre();
                        lbl_Cont.Text = "10";
                        //xVerificationControl.Enabled = true;
                        pnl_Msm.Visible = true;
                        tmr_Conta.Enabled = true;
                        return;
                    }

                    if (objas.RN_Checar_SiPersonal_YaMarco_Entrada(Lbl_Idperso.Text.Trim()) == true)
                    {
                        dataAsis = objas.RN_Buscar_Asistencia_deEntrada(Lbl_Idperso.Text);
                        if (dataAsis.Rows.Count < 1) return;

                        lbl_IdAsis.Text = Convert.ToString(dataAsis.Rows[0]["Id_asis"]);
                        objas.RN_Registrar_Salida_Personal(lbl_IdAsis.Text, Lbl_Idperso.Text, lbl_hora.Text, 8);

                        if (BD_Asistencia.salida == true)
                        {
                            lbl_msm.BackColor = Color.YellowGreen;
                            lbl_msm.ForeColor = Color.White;
                            lbl_msm.Text = "Salida Registrada";
                            //tocar_timbre();
                            //xVerificationControl.Enabled = true;
                            pnl_Msm.Visible = true;
                            lbl_Cont.Text = "10";
                            tmr_Conta.Enabled = true;

                        }
                    }
                    else
                    {
                        Calcular_Minutos_Tardanza();
                        lbl_IdAsis.Text = RN_Utilitario.RN_NroDoc(3);
                        var fe = lbl_IdAsis.Text.Substring(3);
                        var fes = Convert.ToInt32(fe);
                        var Asis = fes + 1;
                        var Asis2 = "AS-000" + Asis.ToString();
                        objas.RN_Registrar_Entrada_Personal(Asis2, Lbl_Idperso.Text, lbl_hora.Text, Convert.ToDouble(lbl_totaltarde.Text), 8, lbl_justifi.Text);

                        if (BD_Asistencia.entrada == true)
                        {
                            RN_Utilitario.RN_Actualiza_Tipo_Doc(3);
                            lbl_msm.BackColor = Color.YellowGreen;
                            lbl_msm.ForeColor = Color.White;
                            lbl_msm.Text = "Entrada Registrada";
                            lbl_Cont.Text = "10";
                            pnl_Msm.Visible = true;
                            tmr_Conta.Enabled = true;

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo malo pasó" + ex.Message, "Advertencia de Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        void Calcular_Minutos_Tardanza()
        {
            RN_Asistencia obj = new RN_Asistencia();
            int horaCaptu = DateTime.Now.Hour;
            int minutoCaptu = DateTime.Now.Minute;

            int horaIn = dtp_horaIngre.Value.Hour;
            int MinuIn = dtp_horaIngre.Value.Minute;

            int MinutoTole = dtp_hora_tolercia.Value.Minute;

            int totalMinutofijo;
            int totaltardanza;

            if (obj.RN_Verificar_Justificacion_Aprobado(Lbl_Idperso.Text) == true)
            {
                lbl_justifi.Text = "Tardanza Justificada";
            }
            else
            {
                lbl_justifi.Text = "Tardanza no justificada";
                totalMinutofijo = MinuIn + MinutoTole;
                if (horaCaptu == horaIn & minutoCaptu > totalMinutofijo)
                {
                    totaltardanza = minutoCaptu - totalMinutofijo;
                    lbl_totaltarde.Text = Convert.ToString(totaltardanza);
                }
                else if (horaCaptu > horaIn)
                {
                    int horaTarde;
                    horaTarde = horaCaptu - horaIn;
                    int HoraenMinuto;
                    HoraenMinuto = horaTarde * 60;
                    int totaltardexx = HoraenMinuto - totalMinutofijo;

                    totaltardanza = minutoCaptu + totaltardexx;
                    lbl_totaltarde.Text = Convert.ToString(totaltardanza);
                }
            }
        }

        private void btn_Salir_Click(object sender, EventArgs e)
        {
            this.Tag = "";
            this.Close();
        }

        private void pnl_titulo_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Utilitarios ui = new Utilitarios();
                ui.Mover_formulario(this);
            }
        }

        private void txt_dni_Buscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_buscar_Click(sender, e);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbl_hora.Text = DateTime.Now.ToString("hh:mm:ss");
        }
        private int sec = 10;
        private void tmr_Conta_Tick(object sender, EventArgs e)
        {
            sec -= 1;
            lbl_Cont.Text = sec.ToString();
            lbl_Cont.Refresh();

            if (sec == 0)
            {
                LimpiarFormulario();
                sec = 10;
                tmr_Conta.Stop();
                pnl_Msm.Visible = false;
                lbl_Cont.Text = "10";
            }
        }
        private void LimpiarFormulario()
        {
            lbl_nombresocio.Text = "";
            lbl_totaltarde.Text = "0";
            lbl_TotalHotrabajda.Text = "0";
            lbl_Dni.Text = "";
            lbl_Cont.Text = "0";
            lbl_IdAsis.Text = "";
            Lbl_Idperso.Text = "";
            lbl_justifi.Text = "";
            lbl_msm.BackColor = Color.Transparent;
            lbl_msm.Text = "";
            picSocio.Image = null;
            txt_dni_Buscar.Text = null;
        }
    }
}
