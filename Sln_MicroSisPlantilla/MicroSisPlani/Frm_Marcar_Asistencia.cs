using DPFP;
using MicroSisPlani.Msm_Forms;
using Prj_Capa_Datos;
using Prj_Capa_Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Layout;



namespace MicroSisPlani
{
    public partial class Frm_Marcar_Asistencia : Form
    {
        private DPFP.Verification.Verification Verificar;
        private DPFP.Verification.Verification.Result Resultado;
        public Frm_Marcar_Asistencia()
        {
            InitializeComponent();

        }
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

        private void pnl_titulo_MouseMove(object sender, MouseEventArgs e)
        {
            Complementos move = new Complementos();
            if (e.Button == MouseButtons.Left)
            {
                move.Mover_formulario(this);
            }


        }

        private void btn_Salir_Click(object sender, EventArgs e)
        {
            this.Close();
            Frm_Principal frm = new Frm_Principal();
            frm.Cargar_Todas_Asistencias_DelDia(DateTime.Now);
        }

        private void Frm_Marcar_Asistencia_Load(object sender, EventArgs e)
        {
            Verificar = new DPFP.Verification.Verification();
            Resultado = new DPFP.Verification.Verification.Result();
            CargarHorarios();
        }

        private void xVerificationControl_OnComplete(object Control, FeatureSet FeatureSet, ref DPFP.Gui.EventHandlerStatus EventHandlerStatus)
        {
            DPFP.Template TemplateBD = new DPFP.Template();
            RN_Personal obj = new RN_Personal();
            RN_Asistencia objas = new RN_Asistencia();
            DataTable datos = new DataTable();
            DataTable dataAsis = new DataTable();

            string NroIDPersona = "";
            int xitn = 1;
            byte[] fingerByte;
            string rutaFoto;
            bool TerminarBucle = false;
            int totalFila = 0;
            Frm_Filtro fil = new Frm_Filtro();

            int HoLimite = 20;//Dtp_Hora_Limite.Value.Hour;
            int MiLimite = Dtp_Hora_Limite.Value.Minute;

            int horaCaptu = DateTime.Now.Hour;
            int minutoCaptu = DateTime.Now.Minute;

            try
            {
                datos = obj.RN_Leer_todoPersona();
                totalFila = datos.Rows.Count;
                if (datos.Rows.Count <= 0) return;

                var datoPer = datos.Rows[0];
                foreach (DataRow xitem in datos.Rows)
                {
                    if (TerminarBucle == true) return;

                    fingerByte = (byte[])xitem["FinguerPrint"];
                    NroIDPersona = Convert.ToString(xitem["Id_Pernl"]);

                    TemplateBD.DeSerialize(fingerByte);
                    Verificar.Verify(FeatureSet, TemplateBD, ref Resultado);

                    if (Resultado.Verified == true)
                    {
                        rutaFoto = Convert.ToString(xitem["Foto"]);
                        lbl_nombresocio.Text = Convert.ToString(xitem["Nombre_Completo"]);
                        Lbl_Idperso.Text = Convert.ToString(xitem["Id_Pernl"]);
                        lbl_Dni.Text = Convert.ToString(xitem["DNIPR"]);

                        if (File.Exists(rutaFoto) == true)
                        { picSocio.Load(rutaFoto.Trim()); }
                        else
                        {
                            //picSocio.Load(Application.StartupPath + @"\user.png");
                        }
                        if (objas.RN_Checar_SiPersonal_YaMarco_Asistencia(Lbl_Idperso.Text.Trim()) == true)
                        {
                            lbl_msm.BackColor = Color.MistyRose;
                            lbl_msm.ForeColor = Color.Red;
                            lbl_msm.Text = "El sistema verificó, que el personal ya marcó su asistencia";
                            //tocar_timbre();
                            lbl_Cont.Text = "10";
                            xVerificationControl.Enabled = true;
                            pnl_Msm.Visible = true;
                            tmr_Conta.Enabled = true;
                            return;

                        }
                        if (objas.RN_Checar_SiPersonal_YaMarco_Entrada(Lbl_Idperso.Text.Trim()) == true)
                        {
                            Frm_Sinox sino = new Frm_Sinox();
                            TerminarBucle = true;

                            dataAsis = objas.RN_Buscar_Asistencia_deEntrada(Lbl_Idperso.Text);
                            if (dataAsis.Rows.Count < 1) return;
                            lbl_IdAsis.Text = Convert.ToString(dataAsis.Rows[0]["Id_asis"]);
                            var horaIngreso = Convert.ToDateTime(dataAsis.Rows[0]["HoIngreso"]);

                            var horas = (Convert.ToDateTime(lbl_hora.Text) - horaIngreso).TotalHours;
                            int b = (int)horas;
                            objas.RN_Registrar_Salida_Personal(lbl_IdAsis.Text, Lbl_Idperso.Text, lbl_hora.Text, b);

                            if (BD_Asistencia.salida == true)
                            {
                                lbl_msm.BackColor = Color.YellowGreen;
                                lbl_msm.ForeColor = Color.White;
                                lbl_msm.Text = "La salida del Personal fue Registrado exitosamente";
                                xVerificationControl.Enabled = false;
                                pnl_Msm.Visible = true;

                                lbl_Cont.Text = "10";
                                tmr_Conta.Enabled = true;

                                TerminarBucle = true;
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
                            objas.RN_Registrar_Entrada_Personal(Asis2, Lbl_Idperso.Text, lbl_hora.Text, Convert.ToDouble(lbl_totaltarde.Text), 0, lbl_justifi.Text);

                            if (BD_Asistencia.entrada == true)
                            {
                                RN_Utilitario.RN_Actualiza_Tipo_Doc(3);
                                lbl_msm.BackColor = Color.YellowGreen;
                                lbl_msm.ForeColor = Color.White;
                                lbl_msm.Text = "La entrada del personal fue registrada correctamente";
                                pnl_Msm.Visible = true;
                                tmr_Conta.Enabled = true;
                                xVerificationControl.Enabled = false;
                                lbl_Cont.Text = "10";
                                TerminarBucle = true;
                            }
                        }
                    }
                    else
                    {
                        if (xitn == totalFila)
                        {
                            if (TerminarBucle == false)
                            {
                                lbl_msm.Text = "La huella dactilar no existe en la base de datos";
                                lbl_msm.BackColor = Color.MistyRose;
                                lbl_msm.ForeColor = Color.Red;
                                lbl_Cont.Text = "10";
                                xVerificationControl.Enabled = false;
                                pnl_Msm.Visible = true;
                                tmr_Conta.Enabled = true;
                            }
                        }
                    }
                    xitn += 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo pasó: " + ex.Message, "Advertencia de seguridad", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                xVerificationControl.Enabled = true;
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
            xVerificationControl.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbl_hora.Text = DateTime.Now.ToString("hh:mm:ss");
        }

        private void xVerificationControl_Load(object sender, EventArgs e)
        {

        }
    }
}
