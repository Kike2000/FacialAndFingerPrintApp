using MicroSisPlani.Personal;
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
using Prj_Capa_Entidad;
using Prj_Capa_Negocio;
using MicroSisPlani.Msm_Forms;
using MicroSisPlani.Informes;
using System.IO;
using FacialRecognition;
using System.Net.Mail;

namespace MicroSisPlani
{
    public partial class Frm_Principal : Form
    {
        public bool Close;
        public Frm_Principal()
        {
            InitializeComponent();
        }

        private void Frm_Principal_Load(object sender, EventArgs e)
        {
            ConfigurarListView();
            ConfigurarListView_Asis();
            ConfigurarListView_Vacaciones();
            ConfiguraListView_Justi();
            CargarHorarios();
            Cargar_Todas_Asistencias_DelDia(DateTime.Now);
            Verificar_Robot_de_faltas();
            Cargar_todas_Justificaciones();
            Cargar_todo_Personal();
            Cargar_Todas_Vacaciones();
            this.elTabPage4.Visible = false;
        }
        

        private void Verificar_Robot_de_faltas()
        {
            string tipo;
            tipo = RN_Utilitario.RN_Listar_TipoFalta(5);
            if (tipo.Trim() == "Si")
            {
                timerFalta.Start();
                rdb_ActivarRobot.Checked = true;
            }
            else
            {
                timerFalta.Stop();
                rdb_Desact_Robot.Checked = true;
            }
        }
        #region "Persona"
        private void ConfigurarListView()
        {
            var lis = lsv_person;
            lis.Columns.Clear();
            lis.View = View.Details;
            lis.GridLines = false;
            lis.FullRowSelect = true;
            lis.Scrollable = true;
            lis.HideSelection = false;
            lis.Columns.Add("Id Persona", 0, HorizontalAlignment.Left);
            lis.Columns.Add("Dni", 95, HorizontalAlignment.Left);
            lis.Columns.Add("Nombres del socio", 316, HorizontalAlignment.Left);
            lis.Columns.Add("Direccion", 0, HorizontalAlignment.Left);
            lis.Columns.Add("Correo", 0, HorizontalAlignment.Left);
            lis.Columns.Add("Sex", 0, HorizontalAlignment.Left);
            lis.Columns.Add("Fe Naci", 110, HorizontalAlignment.Left);
            lis.Columns.Add("Nro Celular", 120, HorizontalAlignment.Left);
            lis.Columns.Add("Rol", 100, HorizontalAlignment.Left);
            lis.Columns.Add("Distrito", 0, HorizontalAlignment.Left);
            lis.Columns.Add("Estado", 100, HorizontalAlignment.Left);
        }

        public void Cargar_todo_Personal()
        {
            RN_Personal obj = new RN_Personal();
            DataTable dt = new DataTable();
            dt = obj.RN_Leer_todoPersona();
            if (dt.Rows.Count > 0)
            {
                LlenarListView(dt);
            }
        }

        private void Buscar_Personal_PorValor(string xvalor)
        {
            RN_Personal obj = new RN_Personal();
            DataTable dt = new DataTable();
            dt = obj.RN_Buscar_Personal_xValor(xvalor);
            if (dt.Rows.Count > 0)
            {
                LlenarListView(dt);
            }
            else
            {
                lsv_person.Items.Clear();
            }
        }
        private void LlenarListView(DataTable data)
        {
            lsv_person.Items.Clear();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow dr = data.Rows[i];
                ListViewItem list = new ListViewItem(dr["Id_Pernl"].ToString());
                list.SubItems.Add(dr["DNIPR"].ToString());
                list.SubItems.Add(dr["Nombre_Completo"].ToString());
                list.SubItems.Add(dr["Domicilio"].ToString());
                list.SubItems.Add(dr["Correo"].ToString());
                list.SubItems.Add(dr["Sexo"].ToString());
                list.SubItems.Add(dr["Fec_Naci"].ToString());
                list.SubItems.Add(dr["Celular"].ToString());
                list.SubItems.Add(dr["NomRol"].ToString());
                list.SubItems.Add(dr["Distrito"].ToString());
                list.SubItems.Add(dr["Estado_Per"].ToString());
                lsv_person.Items.Add(list);
            }
            Lbl_total.Text = Convert.ToString(lsv_person.Items.Count);
        }

        private void txt_Buscar_OnValueChanged(object sender, EventArgs e)
        {
            if (txt_Buscar.Text.Trim().Length > 2)
            {
                Buscar_Personal_PorValor(txt_Buscar.Text.Trim());
            }
        }

        #endregion

        private void pnl_titu_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Utilitarios u = new Utilitarios();
                u.Mover_formulario(this);
            }
        }

        private void btn_Salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_mini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Frm_Explorador_Personal pers = new Frm_Explorador_Personal();
            pers.MdiParent = this;
            pers.Show();

        }

        private void bt_personal_Click(object sender, EventArgs e)
        {
            StartScreen f = new StartScreen();
            f.Show();
        }

        private void Frm_Principal_FormClosing(object sender, FormClosingEventArgs e)
        {
            Frm_Filtro fil = new Frm_Filtro();
            Frm_Sino sino = new Frm_Sino();

            fil.Show();
            sino.Lbl_msm1.Text = "¿Estás seguro de salir del sistema?";
            sino.ShowDialog();
            fil.Hide();

            if (Close == true)
            {
                Frm_Principal frm_Principal = new Frm_Principal();
                frm_Principal.Hide();
                Frm_Login frm_Login = new Frm_Login();
                frm_Login.Show();
            }
            else if (Convert.ToString(sino.Tag) == "Si")
            {
                Application.ExitThread();
            }
            else
            {
                e.Cancel = true;
            }
        }
        public void Cargar_Datos_Usuario()
        {
            try
            {
                Frm_Filtro xfil = new Frm_Filtro();
                /*xfil.Show();
                MessageBox.Show("Bienvenido " + Cls_Libreria.Apellidos, "Bienvenido al sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                xfil.Hide();*/

                Lbl_NomUsu.Text = Cls_Libreria.Apellidos;
                lbl_rolNom.Text = Cls_Libreria.Rol;


                if (Cls_Libreria.Foto.Trim().Length == 0 | Cls_Libreria.Foto == null) return;

                if (File.Exists(Cls_Libreria.Foto) == true)
                {
                    //pic_user.Load(Cls_Libreria.Foto);
                }
                else
                {
                    //pic_user.Image = Properties.Resources.user;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btn_nuevoAsis_Click(object sender, EventArgs e)
        {
            Frm_Filtro fil = new Frm_Filtro();
            Frm_Marcar_Asis_Manual asis = new Frm_Marcar_Asis_Manual();
            fil.Show();
            asis.ShowDialog();
            fil.Hide();
        }

        private void bt_Explo_Asis_Click(object sender, EventArgs e)
        {
            elTabPage3.Visible = true;
            elTab1.SelectedTabPageIndex = 2;
            Cargar_Todas_Asistencias_DelDia(dtp_Fecha_DelDia.Value);

        }

        private void Btn_Cerrar_TabPers_Click(object sender, EventArgs e)
        {
            elTabPage2.Visible = false;
            elTab1.SelectedTabPageIndex = 0;
        }

        private void btn_cerrarEx_Asis_Click(object sender, EventArgs e)
        {
            elTabPage3.Visible = false;
            elTab1.SelectedTabPageIndex = 0;
        }

        private void bt_copiarNroDNI_Click(object sender, EventArgs e)
        {
            Frm_Advertencia adver = new Frm_Advertencia();
            Frm_Filtro fis = new Frm_Filtro();
            if (lsv_person.SelectedIndices.Count == 0)
            {
                fis.Show();
                adver.Lbl_Msm1.Text = "Seleccione el objeto a copiar";
                adver.ShowDialog();
                fis.Hide();
                return;
            }
            else
            {
                var lsv = lsv_person.SelectedItems[0];
                string xdni = lsv.SubItems[1].Text;
                Clipboard.Clear();
                Clipboard.SetText(xdni.Trim());
            }
        }

        private void txt_Buscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Cargar_Todas_Asistencias_porValor(txt_Buscar.Text.Trim());
            }
        }

        private void bt_mostrarTodoElPersonal_Click(object sender, EventArgs e)
        {
            Cargar_todo_Personal();
        }

        private void bt_nuevoPersonal_Click(object sender, EventArgs e)
        {
            Frm_Filtro fil = new Frm_Filtro();
            Frm_Registro_Personal per = new Frm_Registro_Personal();
            fil.Show();
            per.xedit = false;
            per.ShowDialog();

            fil.Hide();
            if (Convert.ToString(per.Tag) == "") return;
            {
                Cargar_todo_Personal();
            }
        }

        private void bt_editarPersonal_Click(object sender, EventArgs e)
        {

            Frm_Filtro fil = new Frm_Filtro();
            Frm_Registro_Personal per = new Frm_Registro_Personal();
            if (lsv_person.SelectedIndices.Count == 0)
            {
                //
            }
            else
            {
                var lsv = lsv_person.SelectedItems[0];
                string Idperson = lsv.SubItems[0].Text;

                fil.Show();
                per.saved = true;
                per.Buscar_Personal_ParaEditar(Idperson);
                per.ShowDialog();
                fil.Hide();

                if (Convert.ToString(per.Tag) == "B")
                {
                    Cargar_todo_Personal();
                }
            }
        }

        private void btn_SaveHorario_Click(object sender, EventArgs e)
        {
            try
            {
                RN_Horario hora = new RN_Horario();
                EN_Horario por = new EN_Horario();
                Frm_Filtro fis = new Frm_Filtro();
                Frm_Msm_Bueno ok = new Frm_Msm_Bueno();
                Frm_Advertencia adver = new Frm_Advertencia();

                por.Idhora = lbl_idHorario.Text;
                por.HoEntrada = dtp_horaIngre.Value;
                por.HoTole = dtp_hora_tolercia.Value;
                por.HoLimite = Dtp_Hora_Limite.Value;
                por.HoSalida = dtp_horaSalida.Value;

                hora.RN_Actualizar_Horario(por);

                if (BD_Horario.saved == true)
                {
                    fis.Show();
                    ok.Lbl_msm1.Text = "Horario actualizado";
                    ok.ShowDialog();
                    fis.Hide();
                    elTabPage4.Visible = false;
                    elTab1.SelectedTabPageIndex = 0;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void bt_Config_Click(object sender, EventArgs e)
        {
            elTabPage4.Visible = true;
            elTab1.SelectedTabPageIndex = 3;
            CargarHorarios();
        }
        private void CargarHorarios()
        {
            RN_Horario obj = new RN_Horario();
            DataTable data = new DataTable();

            data = obj.RN_Leer_Horarios();
            if (data.Rows.Count == 0) return;
            lbl_idHorario.Text = Convert.ToString(data.Rows[0]["Id_Hor"]);
            dtp_horaIngre.Value = Convert.ToDateTime(data.Rows[0]["HoEntrada"]);
            dtp_horaSalida.Value = Convert.ToDateTime(data.Rows[0]["HoSalida"]);
            dtp_hora_tolercia.Value = Convert.ToDateTime(data.Rows[0]["MiTolrncia"]);
            Dtp_Hora_Limite.Value = Convert.ToDateTime(data.Rows[0]["HoLimite"]);
        }
        #region "asistencia"
        private void ConfigurarListView_Asis()
        {
            var lis = lsv_asis;
            lis.Columns.Clear();
            lis.Items.Clear();
            lis.View = View.Details;
            lis.GridLines = false;
            lis.FullRowSelect = true;
            lis.Scrollable = true;
            lis.HideSelection = false;
            lis.Columns.Add("Id Asis", 0, HorizontalAlignment.Left);
            lis.Columns.Add("Dni", 80, HorizontalAlignment.Left);
            lis.Columns.Add("Nombres del Personal", 316, HorizontalAlignment.Left);
            lis.Columns.Add("Fecha", 90, HorizontalAlignment.Left);
            lis.Columns.Add("Dia", 80, HorizontalAlignment.Left);
            lis.Columns.Add("Ho Ingreso", 90, HorizontalAlignment.Left);
            lis.Columns.Add("Tardnza", 70, HorizontalAlignment.Left);
            lis.Columns.Add("Ho Salida", 90, HorizontalAlignment.Left);
            lis.Columns.Add("Adelanto", 90, HorizontalAlignment.Left);
            lis.Columns.Add("Justificacion", 0, HorizontalAlignment.Left);
            lis.Columns.Add("Estado", 100, HorizontalAlignment.Left);

        }
        private void Cargar_Todas_Asistencias()
        {
            RN_Asistencia obj = new RN_Asistencia();
            DataTable dt = new DataTable();

            dt = obj.RN_Ver_Todas_Asistencia();
            if (dt.Rows.Count > 0)
            {
                LLenarListView_Asis(dt);
            }

        }
        public void Cargar_Todas_Asistencias_DelDia(DateTime fechas)
        {
            RN_Asistencia obj = new RN_Asistencia();
            DataTable dt = new DataTable();

            dt = obj.RN_Ver_Todas_Asistencia_Deldia(fechas);
            if (dt.Rows.Count > 0)
            {
                LLenarListView_Asis(dt);
            }
            else
            {
                lsv_asis.Items.Clear();
            }

        }
        private void Cargar_Todas_Asistencias_DelMes(DateTime fechas)
        {
            RN_Asistencia obj = new RN_Asistencia();
            DataTable dt = new DataTable();

            dt = obj.RN_Ver_Todas_Asistencia_DelMes(fechas);
            if (dt.Rows.Count > 0)
            {
                LLenarListView_Asis(dt);
            }

        }
        private void Cargar_Todas_Asistencias_porValor(string xvalor)
        {
            RN_Asistencia obj = new RN_Asistencia();
            DataTable dt = new DataTable();

            dt = obj.RN_Ver_Todas_Asistencia_ParaExplorador(xvalor);
            if (dt.Rows.Count > 0)
            {
                LLenarListView_Asis(dt);
            }

        }

        private void LLenarListView_Asis(DataTable data)
        {
            lsv_asis.Items.Clear();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow dr = data.Rows[i];
                ListViewItem list = new ListViewItem(dr["Id_asis"].ToString());
                list.SubItems.Add(dr["DNIPR"].ToString());
                list.SubItems.Add(dr["Nombre_Completo"].ToString());
                list.SubItems.Add(dr["FechaAsis"].ToString());
                list.SubItems.Add(dr["Nombre_dia"].ToString());
                list.SubItems.Add(dr["HoIngreso"].ToString());
                list.SubItems.Add(dr["HoSalida"].ToString());
                list.SubItems.Add(dr["Total_HrsWorked"].ToString());

                lsv_asis.Items.Add(list);
            }
            Lbl_total.Text = Convert.ToString(lsv_asis.Items.Count);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Cargar_Todas_Asistencias();
        }


        private void txt_buscarAsis_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Buscar_Personal_PorValor(txt_buscarAsis.Text);
            }
        }

        private void lbl_lupaAsis_Click(object sender, EventArgs e)
        {
            Cargar_Todas_Asistencias_porValor(txt_buscarAsis.Text);
        }

        private void verAsistenciasDelDíaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Filtro fil = new Frm_Filtro();
            Frm_Solo_Fecha solo = new Frm_Solo_Fecha();

            fil.Show();
            solo.ShowDialog();
            fil.Hide();

            if (Convert.ToString(solo.Tag) == "") return;
            DateTime xfecha = solo.dtp_fecha.Value;
            Cargar_Todas_Asistencias_DelDia(xfecha);

        }

        #endregion

        private void bt_registrarHuellaDigital_Click(object sender, EventArgs e)
        {

        }

        private void btn_Asis_With_Huella_Click(object sender, EventArgs e)
        {
            Frm_Filtro fis = new Frm_Filtro();
            Frm_Marcar_Asistencia asis = new Frm_Marcar_Asistencia();
            fis.Show();
            asis.ShowDialog();
            fis.Hide();

        }

        private void btn_Savedrobot_Click(object sender, EventArgs e)
        {
            RN_Utilitario uti = new RN_Utilitario();

            if (rdb_ActivarRobot.Checked == true)
            {
                uti.RN_Actualizar_RobotFalta(5, "Si");
                if (BD_Utilitario.falta == true)
                {
                    Frm_Msm_Bueno ok = new Frm_Msm_Bueno();
                    ok.Lbl_msm1.Text = "El robot fue actualizado";
                    ok.ShowDialog();
                    elTab1.SelectedTabPageIndex = 0;
                    elTabPage4.Visible = false;
                }
            }
            else if (rdb_Desact_Robot.Checked == true)
            {
                uti.RN_Actualizar_RobotFalta(5, "No");
                if (BD_Utilitario.falta == true)
                {
                    Frm_Msm_Bueno ok = new Frm_Msm_Bueno();
                    ok.Lbl_msm1.Text = "El robot fue actualizado";
                    ok.ShowDialog();
                    elTab1.SelectedTabPageIndex = 0;
                    elTabPage4.Visible = false;
                }
            }
        }

        private void timerFalta_Tick(object sender, EventArgs e)
        {
            RN_Asistencia obj = new RN_Asistencia();
            Frm_Filtro fil = new Frm_Filtro();
            Frm_Advertencia adver = new Frm_Advertencia();
            Frm_Msm_Bueno ok = new Frm_Msm_Bueno();
            DataTable dataper = new DataTable();
            RN_Personal objper = new RN_Personal();

            int HoLimite = Dtp_Hora_Limite.Value.Hour;
            int MiLimite = Dtp_Hora_Limite.Value.Minute;

            int horaCaptu = DateTime.Now.Hour;
            int minutoCaptu = DateTime.Now.Minute;
            string DniPer = "";
            int cantidad = 0;
            int TotalItem = 0;
            string xidPersona = "";
            string IdAsistencia = "";

            string xjustificacion = "";

            if (horaCaptu >= HoLimite)
            {
                if (minutoCaptu > MiLimite)
                {
                    dataper = objper.RN_Leer_todoPersona();
                    if (dataper.Rows.Count <= 0) return;
                    TotalItem = dataper.Rows.Count;

                    foreach (DataRow registro in dataper.Rows)
                    {
                        DniPer = Convert.ToString(registro["DNIPR"]);
                        xidPersona = Convert.ToString(registro["Id_Pernl"]);
                        if (obj.RN_Checar_SiPersonal_TieneAsistencia_Registrada(xidPersona.Trim()) == false)
                        {
                            if (obj.RN_Checar_SiPersonal_YaMarco_suFalta(xidPersona.Trim()) == true)
                            {
                                RN_Asistencia objA = new RN_Asistencia();
                                EN_Asistencia asi = new EN_Asistencia();
                                IdAsistencia = RN_Utilitario.RN_NroDoc(3);

                                if (objA.RN_Verificar_Justificacion_Aprobado(xidPersona) == true)
                                {
                                    xjustificacion = "Falta tiene justificación";
                                }
                                else
                                {
                                    xjustificacion = "No tiene justificación";
                                }

                                obj.RN_Registrar_Falta_Personal(IdAsistencia, xidPersona, xjustificacion);
                                if (BD_Asistencia.falta == true)
                                {
                                    RN_Utilitario.RN_Actualiza_Tipo_Doc(3);
                                    cantidad += 1;
                                }

                            }
                        }
                    }

                    if (cantidad > 1)
                    {
                        timerFalta.Stop();
                        fil.Show();
                        ok.Lbl_msm1.Text = "Un total de: " + cantidad.ToString() + "/" + TotalItem + " Faltas se han registrado exitosamente";
                        ok.ShowDialog();
                        fil.Hide();
                    }
                    else
                    {
                        timerFalta.Stop();
                        fil.Show();
                        ok.Lbl_msm1.Text = "El día de hoy naide faltó a la chamba" + cantidad.ToString() + "/" + TotalItem + " personas marcaron su asistencia";
                        //ok.ShowDialog();
                        fil.Hide();
                    }
                }
            }
        }

        private void imprimirAsistenciasDelDíaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Filtro fil = new Frm_Filtro();
            Frm_PrintAsis_delDia asis = new Frm_PrintAsis_delDia();
            Frm_Solo_Fecha solo = new Frm_Solo_Fecha();

            fil.Show();
            solo.ShowDialog();
            fil.Hide();

            if (solo.Tag.ToString() == "") return;

            DateTime xdia = solo.dtp_fecha.Value;
            fil.Show();
            asis.Tag = xdia;
            asis.tipoInfo = "deldia";
            asis.ShowDialog();

            fil.Hide();
        }

        #region "Todo Justificacion"

        private void ConfiguraListView_Justi()
        {
            var lis = lsv_justifi;
            lis.Columns.Clear();
            lis.Items.Clear();
            lis.View = View.Details;
            lis.GridLines = false;
            lis.FullRowSelect = true;
            lis.Scrollable = true;
            lis.HideSelection = false;

            lis.Columns.Add("IdJusti", 0, HorizontalAlignment.Left);
            lis.Columns.Add("IdPerso", 0, HorizontalAlignment.Left);
            lis.Columns.Add("Nombres del Personal", 316, HorizontalAlignment.Left);
            lis.Columns.Add("Motivo", 110, HorizontalAlignment.Left);
            lis.Columns.Add("Fecha", 120, HorizontalAlignment.Left);
            lis.Columns.Add("Fecha", 120, HorizontalAlignment.Left);
            lis.Columns.Add("Estado", 120, HorizontalAlignment.Left);
            lis.Columns.Add("Detalle Justifica", 100, HorizontalAlignment.Left);
        }

        private void LlenarListView_Justi(DataTable data)
        {
            lsv_justifi.Items.Clear();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow dr = data.Rows[i];
                ListViewItem list = new ListViewItem(dr["Id_Justi"].ToString());
                list.SubItems.Add(dr["Id_Pernl"].ToString());
                list.SubItems.Add(dr["Nombre_COmpleto"].ToString());
                list.SubItems.Add(dr["PrincipalMotivo"].ToString());
                list.SubItems.Add(dr["FechaEmi"].ToString());
                list.SubItems.Add(dr["FechaJusti"].ToString());
                list.SubItems.Add(dr["EstadoJus"].ToString());
                list.SubItems.Add(dr["Detalle_Justi"].ToString());
                lsv_justifi.Items.Add(list);
            }
            lbl_totaljusti.Text = Convert.ToString(lsv_justifi.Items.Count);
        }

        private void Cargar_todas_Justificaciones()
        {
            RN_Justificacion obj = new RN_Justificacion();
            DataTable dt = new DataTable();

            dt = obj.RN_Cargar_todos_Justificacion();
            if (dt.Rows.Count > 0)
            {
                LlenarListView_Justi(dt);
            }
            else
            {
                lsv_justifi.Items.Clear();
            }
        }


        private void bt_mostrarJusti_Click(object sender, EventArgs e)
        {
            Cargar_todas_Justificaciones();
        }

        private void bt_editJusti_Click(object sender, EventArgs e)
        {
            Frm_Filtro fil = new Frm_Filtro();
            Frm_Reg_Justificacion per = new Frm_Reg_Justificacion();
            if (lsv_justifi.SelectedItems.Count == 0)
            {
                fil.Show();
                MessageBox.Show("Seleccione una justificación", "Advertencia de Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                fil.Hide();
            }
            else
            {
                var lsv = lsv_justifi.SelectedItems[0];
                string xidsocio = lsv.SubItems[1].Text;
                string xidJusti = lsv.SubItems[0].Text;
                string xnombre = lsv.SubItems[2].Text;

                fil.Show();
                per.xedit = false;
                per.txt_IdPersona.Text = xidsocio;
                per.txt_nompersona.Text = xnombre;
                per.txt_idjusti.Text = xidJusti;
                per.BuscarJustificacion(xidJusti);
                per.ShowDialog();
                fil.Hide();

                if (Convert.ToString(per.Tag) == "")
                    return;
                {
                    Cargar_todas_Justificaciones();
                    elTab1.SelectedTabPageIndex = 3;
                    elTabPage5.Visible = true;
                }

            }
        }
        #endregion

        private void bt_solicitarJustificacion_Click(object sender, EventArgs e)
        {
            Frm_Filtro fil = new Frm_Filtro();
            Frm_Reg_Justificacion per = new Frm_Reg_Justificacion();

            if (lsv_person.SelectedIndices.Count == 0)
            {
                fil.Show();
                MessageBox.Show("Selecciona el empleado", "Advertencia de Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                fil.Hide();
            }
            else
            {
                var lsv = lsv_person.SelectedItems[0];
                string xidsocio = lsv.SubItems[0].Text;
                string xnombre = lsv.SubItems[2].Text;

                fil.Show();
                per.xedit = false;
                per.txt_IdPersona.Text = xidsocio;
                per.txt_nompersona.Text = xnombre;
                per.txt_idjusti.Text = RN_Utilitario.RN_NroDoc(4);
                per.ShowDialog();
                fil.Hide();

                if (Convert.ToString(per.Tag) == "")
                    return;
                {
                    Cargar_todas_Justificaciones();
                    elTab1.SelectedTabPageIndex = 3;
                }
            }
        }

        private void bt_aprobarJustificacion_Click(object sender, EventArgs e)
        {
            Frm_Advertencia adver = new Frm_Advertencia();
            Frm_Sino sino = new Frm_Sino();
            Frm_Msm_Bueno ok = new Frm_Msm_Bueno();
            Frm_Filtro fis = new Frm_Filtro();
            RN_Justificacion obj = new RN_Justificacion();

            if (lsv_justifi.SelectedItems.Count == 0)
            {
                fis.Show();
                adver.Lbl_Msm1.Text = "Seleccione una justificación ";
                adver.ShowDialog();
                fis.Hide(); return;
            }
            else
            {
                var lsv = lsv_justifi.SelectedItems[0];
                string xidjus = lsv.SubItems[0].Text;
                string xidper = lsv.SubItems[1].Text;
                string xstadojus = lsv.SubItems[6].Text;

                if (xstadojus.Trim() == "Aprobado") { fis.Show(); adver.Lbl_Msm1.Text = "La justificación, ya fue aprobada"; adver.ShowDialog(); fis.Hide(); return; }

                sino.Lbl_msm1.Text = "Confirmar justificación";
                fis.Show();
                sino.ShowDialog();
                fis.Hide();

                if (Convert.ToString(sino.Tag) == "Si")
                {
                    obj.RN_Aprobar_Justificacion(xidjus, xidper);
                    if (BD_Justificacion.tried == true)
                    {
                        fis.Show();
                        ok.Lbl_msm1.Text = "Justificación aprobada";
                        ok.ShowDialog();
                        fis.Hide();
                        Buscar_Justificacion_porValor(xidjus);
                    }
                }

            }

        }
        private void Buscar_Justificacion_porValor(string xvalor)
        {
            RN_Justificacion obj = new RN_Justificacion();
            DataTable dt = new DataTable();

            dt = obj.RN_BuscarJustificacion_porValor(xvalor.Trim());

            if (dt.Rows.Count > 0)
            {
                LlenarListView_Justi(dt);
            }
            else { lsv_justifi.Items.Clear(); }
        }

        private void bt_desaprobarJustificacion_Click(object sender, EventArgs e)
        {
            Frm_Advertencia adver = new Frm_Advertencia();
            Frm_Sino sino = new Frm_Sino();
            Frm_Msm_Bueno ok = new Frm_Msm_Bueno();
            Frm_Filtro fis = new Frm_Filtro();
            RN_Justificacion obj = new RN_Justificacion();

            if (lsv_justifi.SelectedItems.Count == 0)
            {
                fis.Show();
                adver.Lbl_Msm1.Text = "Seleccione una justificación ";
                adver.ShowDialog();
                fis.Hide(); return;
            }
            else
            {
                var lsv = lsv_justifi.SelectedItems[0];
                string xidjus = lsv.SubItems[0].Text;
                string xidper = lsv.SubItems[1].Text;
                string xstadojus = lsv.SubItems[6].Text;

                if (xstadojus.Trim() == "Falta Aprobar") { fis.Show(); adver.Lbl_Msm1.Text = "La justificación falta de aprobar"; adver.ShowDialog(); fis.Hide(); return; }

                sino.Lbl_msm1.Text = "Confirmar justificación";
                fis.Show();
                sino.ShowDialog();
                fis.Hide();

                if (Convert.ToString(sino.Tag) == "Si")
                {
                    obj.RN_Desaprobar_Justificacion(xidjus, xidper);
                    if (BD_Justificacion.tried == true)
                    {
                        fis.Show();
                        ok.Lbl_msm1.Text = "Justificación Desaprobada";
                        ok.ShowDialog();
                        fis.Hide();
                        Buscar_Justificacion_porValor(xidjus);
                    }
                }

            }

        }

        private void bt_ElimiJusti_Click(object sender, EventArgs e)
        {
            Frm_Advertencia adver = new Frm_Advertencia();
            Frm_Sino sino = new Frm_Sino();
            Frm_Msm_Bueno ok = new Frm_Msm_Bueno();
            Frm_Filtro fis = new Frm_Filtro();
            RN_Justificacion obj = new RN_Justificacion();

            if (lsv_justifi.SelectedItems.Count == 0)
            {
                fis.Show();
                adver.Lbl_Msm1.Text = "Seleccione una justificación ";
                adver.ShowDialog();
                fis.Hide(); return;
            }
            else
            {
                var lsv = lsv_justifi.SelectedItems[0];
                string xidjus = lsv.SubItems[0].Text;

                sino.Lbl_msm1.Text = "Confirmar justificación";
                fis.Show();
                sino.ShowDialog();
                fis.Hide();

                if (Convert.ToString(sino.Tag) == "Si")
                {
                    obj.RN_Eliminar_Justificacion(xidjus);
                    if (BD_Justificacion.supressed == true)
                    {
                        fis.Show();
                        ok.Lbl_msm1.Text = "Justificación eliminada";
                        ok.ShowDialog();
                        fis.Hide();
                        Cargar_todas_Justificaciones();
                    }
                }

            }

        }

        private void txt_buscarjusti_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txt_buscarjusti.Text.Trim().Length > 2)
                {
                    Buscar_Justificacion_porValor(txt_buscarjusti.Text);
                }
                else
                {
                    Cargar_todas_Justificaciones();
                }
            }
        }

        private void bt_cerrarjusti_Click(object sender, EventArgs e)
        {
            elTabPage5.Visible = false;
            elTabPage1.Visible = true;
            elTab1.SelectedTabPageIndex = 0;
        }

        private void lsv_justifi_MouseClick(object sender, MouseEventArgs e)
        {
            var lsv = lsv_justifi.SelectedItems[0];
            string xnombre = lsv.SubItems[7].Text;

            lbl_Detalle.Text = xnombre.Trim();
        }

        private void bt_exploJusti_Click(object sender, EventArgs e)
        {
            elTab1.SelectedTabPageIndex = 4;
            elTabPage5.Visible = true;
            Cargar_todas_Justificaciones();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Frm_Advertencia adver = new Frm_Advertencia();
            Frm_Sino sino = new Frm_Sino();
            Frm_Msm_Bueno ok = new Frm_Msm_Bueno();
            Frm_Filtro fis = new Frm_Filtro();
            RN_Asistencia obj = new RN_Asistencia();

            if (lsv_asis.SelectedItems.Count == 0)
            {
                fis.Show();
                adver.Lbl_Msm1.Text = "Seleccione una asistencia ";
                adver.ShowDialog();
                fis.Hide(); return;
            }
            else
            {
                var lsv = lsv_asis.SelectedItems[0];
                string xidasis = lsv.SubItems[0].Text;

                sino.Lbl_msm1.Text = "Eliminar Asistencia";
                fis.Show();
                sino.ShowDialog();
                fis.Hide();

                if (Convert.ToString(sino.Tag) == "Si")
                {
                    obj.RN_Eliminar_Asistencia(xidasis);

                    fis.Show();
                    ok.Lbl_msm1.Text = "Asistencia eliminada";
                    ok.ShowDialog();
                    fis.Hide();
                    Cargar_Todas_Asistencias_DelDia(dtp_Fecha_DelDia.Value);

                }

            }
        }

        private void Bt_NewPerso_Click(object sender, EventArgs e)
        {
            Frm_Filtro fil = new Frm_Filtro();
            Frm_Registro_Personal per = new Frm_Registro_Personal();
            fil.Show();
            per.xedit = false;
            per.ShowDialog();

            fil.Hide();
            if (Convert.ToString(per.Tag) == "") return;
            {
                Cargar_todo_Personal();
            }
        }

        private void btn_Asis_With_Huella_Click_1(object sender, EventArgs e)
        {
            Frm_Filtro fis = new Frm_Filtro();
            Frm_Marcar_Asistencia asis = new Frm_Marcar_Asistencia();
            fis.Show();
            asis.ShowDialog();
            fis.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Frm_Filtro fis = new Frm_Filtro();
            Frm_Marcar_Asistencia asis = new Frm_Marcar_Asistencia();
            fis.Show();
            asis.ShowDialog();
            fis.Hide();
        }

        private void btn_Asis_With_Huella_Click_2(object sender, EventArgs e)
        {
            Frm_Filtro fil = new Frm_Filtro();
            Frm_Marcar_Asis_Manual asis = new Frm_Marcar_Asis_Manual();
            fil.Show();
            asis.ShowDialog();
            fil.Hide();
        }

        private void btn_VerTodoPerso_Click(object sender, EventArgs e)
        {
            Cargar_todo_Personal();
        }

        private void bt_imprimirAsistenciaDelMes_Click(object sender, EventArgs e)
        {
            Frm_Filtro fil = new Frm_Filtro();
            Frm_PrintAsis_delDia asis = new Frm_PrintAsis_delDia();
            Frm_Solo_Fecha solo = new Frm_Solo_Fecha();

            fil.Show();
            solo.ShowDialog();
            fil.Hide();

            if (solo.Tag.ToString() == "") return;

            DateTime xdia = solo.dtp_fecha.Value;
            fil.Show();
            asis.Tag = xdia;
            asis.tipoInfo = "delmes";
            asis.ShowDialog();

            fil.Hide();
        }

        private void huellaDigitalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Filtro fill = new Frm_Filtro();
            Frm_Regis_Huella per = new Frm_Regis_Huella();

            if (lsv_person.SelectedIndices.Count == 0)
            {
                MessageBox.Show("Seleccione un empleado para registro", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                var lsv = lsv_person.SelectedItems[0];
                string xidsocio = lsv.SubItems[0].Text;

                fill.Show();
                per.Buscar_Personal_ParaEditar(xidsocio);
                per.ShowDialog();
                fill.Hide();

                if (Convert.ToString(per.Tag) == "") return;
                {
                    Cargar_todo_Personal();
                }
            }
        }

        private void rostroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var lsv = lsv_person.SelectedItems[0];
            string xidsocio = lsv.SubItems[1].Text;
            if (xidsocio != null)
            {

                Form1 form = new Form1(xidsocio);
                form.Show();
            }
            else
            {
                MessageBox.Show("Seleccione un empleado");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Recognizer recognizer = new Recognizer();
            //recognizer.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close = true;
            this.Close();
        }
        public List<string> adjuntos { get; set; }
        private void button4_Click(object sender, EventArgs e)
        {
            SmtpClient smtp = new SmtpClient("smtp.office365.com");
            //mail.From = new MailAddress("drycleaningexpress@outlook.com", "DryCleaning Express");

            //smtp.Send(mail);

            MailMessage mail = new MailMessage();
            try
            {
                mail.IsBodyHtml = true;
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.Subject = textBox4.Text;
                mail.Body = textBox1.Text;
                mail.To.Add(textBox2.Text);
                if (textBox3.Text != "")
                {
                    mail.CC.Add(textBox3.Text);
                }

                mail.From = new MailAddress("ilomogto@outlook.es", "Ilomo");
                //Esto es para ver si llegan los correos.
                mail.To.Add("pedro.crodriguez@alumnos.udg.mx");
                foreach (DataGridViewRow fila in dataGridView1.Rows)
                {
                    if (fila.Cells["cnArchivo"].Value != null)
                    {
                        string destino = fila.Cells["cnArchivo"].Value.ToString();
                        if (!string.IsNullOrWhiteSpace(destino))
                        {
                            mail.Attachments.Add(new Attachment(destino));
                        }
                    }
                }
                smtp.Port = 587;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new System.Net.NetworkCredential("ilomogto@outlook.es", "test");
                smtp.EnableSsl = true;
                smtp.Timeout = 10000;

                smtp.Send(mail);
                MessageBox.Show("Mensaje enviado");
                textBox4.Text = "";
                textBox3.Text = "";
                textBox2.Text = "";
                textBox1.Text = "";
                dataGridView1.Rows.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title = "Seleccionar Archivo";
            open.Multiselect = true;

            if (open.ShowDialog() == DialogResult.OK)
            {
                var archivos = open.FileNames;
                foreach (var item in archivos)
                {
                    dataGridView1.Rows.Add(item);
                }
            }
            open.Dispose();
        }

        private void bt_imprimirAsistenciaPorPersonal_Click(object sender, EventArgs e)
        {

        }

        private void imprimirTodasLasJustificacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void reportePorDíaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Filtro fil = new Frm_Filtro();
            Frm_PrintAsis_delDia asis = new Frm_PrintAsis_delDia();
            fil.Show();
            var lsv = lsv_person.SelectedItems[0];
            string xidasis = lsv.SubItems[0].Text;

            asis.Tag = xidasis;
            asis.tipoInfo = "porPersona";
            asis.ShowDialog();

            fil.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|* .xls", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Add(Microsoft.Office.Interop.Excel.XlSheetType.xlWorksheet);
                    Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)app.ActiveSheet;
                    app.Visible = false;
                    ws.Cells[1, 1] = "Código";
                    ws.Cells[1, 2] = "Nombre Completo";
                    ws.Cells[1, 3] = "Fecha";
                    ws.Cells[1, 4] = "Día";
                    ws.Cells[1, 5] = "Hora Ingreso";
                    ws.Cells[1, 6] = "Hora Salida";
                    ws.Cells[1, 7] = "Total";
                    int i = 2;
                    foreach (ListViewItem item in lsv_asis.Items)
                    {
                        ws.Cells[i, 1] = item.SubItems[1].Text;
                        ws.Cells[i, 2] = item.SubItems[2].Text;
                        ws.Cells[i, 3] = item.SubItems[3].Text;
                        ws.Cells[i, 4] = item.SubItems[4].Text;
                        ws.Cells[i, 5] = item.SubItems[5].Text;
                        ws.Cells[i, 6] = item.SubItems[6].Text;
                        ws.Cells[i, 7] = item.SubItems[7].Text;
                        i++;
                    }
                    wb.SaveAs(sfd.FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("Datos guardados en Excel");
                }
            }
        }

        private void elTabPage2_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|* .xls", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Add(Microsoft.Office.Interop.Excel.XlSheetType.xlWorksheet);
                    Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)app.ActiveSheet;
                    app.Visible = false;
                    ws.Cells[1, 1] = "Código";
                    ws.Cells[1, 2] = "Nombre Completo";
                    ws.Cells[1, 3] = "Fecha de Nacimiento";
                    ws.Cells[1, 4] = "Nro. Celular";
                    ws.Cells[1, 5] = "Cargo";
                    ws.Cells[1, 6] = "Estado";
                    int i = 2;
                    foreach (ListViewItem item in lsv_person.Items)
                    {
                        ws.Cells[i, 1] = item.SubItems[1].Text;
                        ws.Cells[i, 2] = item.SubItems[2].Text;
                        ws.Cells[i, 3] = item.SubItems[6].Text;
                        ws.Cells[i, 4] = item.SubItems[7].Text;
                        ws.Cells[i, 5] = item.SubItems[8].Text;
                        ws.Cells[i, 6] = item.SubItems[10].Text;
                        i++;
                    }
                    wb.SaveAs(sfd.FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("Datos guardados en Excel");
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|* .xls", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Add(Microsoft.Office.Interop.Excel.XlSheetType.xlWorksheet);
                    Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)app.ActiveSheet;
                    app.Visible = false;
                    ws.Cells[1, 1] = "Nombre del empleado";
                    ws.Cells[1, 2] = "Motivo";
                    ws.Cells[1, 3] = "Fecha de Emisión";
                    ws.Cells[1, 4] = "Fecha Justificación";
                    ws.Cells[1, 5] = "Estado";
                    int i = 2;
                    foreach (ListViewItem item in lsv_justifi.Items)
                    {
                        ws.Cells[i, 1] = item.SubItems[2].Text;
                        ws.Cells[i, 2] = item.SubItems[3].Text;
                        ws.Cells[i, 3] = Convert.ToDateTime(item.SubItems[4].Text).ToString("dd'/'MM'/'yyyy");
                        ws.Cells[i, 4] = Convert.ToDateTime(item.SubItems[5].Text).ToString("dd'/'MM'/'yyyy");
                        ws.Cells[i, 5] = item.SubItems[6].Text;
                        i++;
                    }
                    wb.SaveAs(sfd.FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("Datos guardados en Excel");
                }
            }
        }

        private void pnl_titulo1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            Cargar_Todas_Asistencias_DelDia(DateTime.Now);
        }

        private void agregarVacacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lsv_person.SelectedIndices.Count == 0)
            {
                //
            }
            else
            {
                Frm_Filtro fil = new Frm_Filtro();                

                var lsv = lsv_person.SelectedItems[0];
                string Idperson = lsv.SubItems[0].Text;
                RN_Vacaciones vacaciones = new RN_Vacaciones();
                if (vacaciones.RN_Verificar_SiTieneVacaciones(Idperson) == false)
                {
                    fil.Show();
                    Frm_RegistroVacaciones per = new Frm_RegistroVacaciones();
                    per.Buscar_Personal_ParaEditar(Idperson);
                    per.ShowDialog();
                    fil.Hide();
                    if (Convert.ToString(per.Tag) == "") return;
                    {
                        Cargar_Vacaciones();
                    }
                }
                else
                {
                    MessageBox.Show("Empleado ya tiene vacaciones asignadas");
                }
            }
        }

        private void lsv_asis_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ConfigurarListView_Vacaciones()
        {
            var lis = listView1;
            lis.Columns.Clear();
            lis.Items.Clear();
            lis.View = View.Details;
            lis.GridLines = false;
            lis.FullRowSelect = true;
            lis.Scrollable = true;
            lis.HideSelection = false;
            lis.Columns.Add("Id", 0, HorizontalAlignment.Left);
            lis.Columns.Add("Nombre", 200, HorizontalAlignment.Center);
            lis.Columns.Add("Desde", 80, HorizontalAlignment.Center);
            lis.Columns.Add("Hasta", 86, HorizontalAlignment.Center);
        }

        private void Cargar_Todas_Vacaciones()
        {
            RN_Vacaciones obj = new RN_Vacaciones();
            DataTable dt = new DataTable();

            dt = obj.RN_VerVacaciones();
            if (dt.Rows.Count > 0)
            {
                LlenarListView_Vacaciones(dt);
            }

        }
        private void LlenarListView_Vacaciones(DataTable data)
        {
            listView1.Items.Clear();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow dr = data.Rows[i];
                ListViewItem list = new ListViewItem(dr["Id"].ToString());
                list.SubItems.Add(dr["Nombre_Completo"].ToString());
                list.SubItems.Add(dr["Desde"].ToString());
                list.SubItems.Add(dr["Hasta"].ToString());
                listView1.Items.Add(list);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Cargar_todas_Justificaciones();
        }

        private void editarVacacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var lsv = listView1.SelectedItems[0];
            string Idperson = lsv.SubItems[0].Text;
            Frm_RegistroVacaciones per = new Frm_RegistroVacaciones();
            Frm_Filtro fil = new Frm_Filtro();

            fil.Show();
            per.Buscar_Vacaciones_ParaEditar(Idperson);
            per.ShowDialog();
            fil.Hide();
            if (Convert.ToString(per.Tag) == "A")
            {
                Cargar_Vacaciones();
            }
        }
        public void Cargar_Vacaciones()
        {
            RN_Vacaciones obj = new RN_Vacaciones();
            DataTable dt = new DataTable();
            dt = obj.RN_VerVacaciones();
            if (dt.Rows.Count > 0)
            {
                LlenarListView_Vacaciones(dt);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
