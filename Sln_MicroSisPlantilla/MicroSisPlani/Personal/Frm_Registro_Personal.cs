﻿using System;
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


namespace MicroSisPlani.Personal
{
    public partial class Frm_Registro_Personal : Form
    {
        public Frm_Registro_Personal()
        {
            InitializeComponent();
        }

        //public bool editPerso = false;

        public bool saved = false;
        private void Frm_Registro_Personal_Load(object sender, EventArgs e)
        {

            if (saved == false)
            {
                Cargar_rol();
                Cargar_Distrito();
            }

        }
        private void Cargar_rol()
        {
            RN_Rol obj = new RN_Rol();
            DataTable dt = new DataTable();
            try
            {
                dt = obj.RN_Buscar_Todos_Roles();
                if (dt.Rows.Count > 0)
                {
                    var cbo = cbo_rol;
                    cbo.DataSource = dt;
                    cbo.DisplayMember = "NomRol";
                    cbo.ValueMember = "Id_Rol";
                }
                cbo_rol.SelectedIndex = -1;
            }
            catch (Exception ex)
            {

            }
        }

        private bool validarCajasTexto()
        {
            Frm_Advertencia adv = new Frm_Advertencia();
            Frm_Filtro fil = new Frm_Filtro();
            if (txt_Dni.Text.Trim().Length < 8) { fil.Show(); adv.Lbl_Msm1.Text = "El Nro de Dni debe tener 8 dígitos"; adv.ShowDialog(); fil.Hide(); txt_Dni.Focus(); return false; }

            if (txt_Dni.Text.Length < 8) { fil.Show(); adv.Lbl_Msm1.Text = "El Nro de Dni debe tener 8 dígitos"; adv.ShowDialog(); fil.Hide(); txt_Dni.Focus(); return false; }
            if (txt_nombres.Text.Length < 4) { fil.Show(); adv.Lbl_Msm1.Text = "Ingresa el nombre del personal"; adv.ShowDialog(); fil.Hide(); txt_Dni.Focus(); return false; }
            if (txt_direccion.Text.Length < 4) { fil.Show(); adv.Lbl_Msm1.Text = "Ingresa la dirección del personal"; adv.ShowDialog(); fil.Hide(); txt_Dni.Focus(); return false; }
            if (txt_correo.Text.Length < 4) { fil.Show(); adv.Lbl_Msm1.Text = "Ingresa el correo del personal"; adv.ShowDialog(); fil.Hide(); txt_Dni.Focus(); return false; }
            if (txt_NroCelular.Text.Length < 8) { fil.Show(); adv.Lbl_Msm1.Text = "Ingresa el número celular del personal"; adv.ShowDialog(); fil.Hide(); txt_Dni.Focus(); return false; }
            if (txt_IdPersona.Text.Length < 8) { fil.Show(); adv.Lbl_Msm1.Text = "Id no generado"; adv.ShowDialog(); fil.Hide(); txt_Dni.Focus(); return false; }
            if (dtp_fecha == null) { fil.Show(); adv.Lbl_Msm1.Text = "Id no generado"; adv.ShowDialog(); fil.Hide(); txt_Dni.Focus(); return false; }

            if (cbo_Distrito.SelectedIndex == -1) { fil.Show(); adv.Lbl_Msm1.Text = "Seleccione el distrito del personal"; adv.ShowDialog(); fil.Hide(); txt_Dni.Focus(); return false; }
            if (cbo_sexo.SelectedIndex == -1) { fil.Show(); adv.Lbl_Msm1.Text = "Seleccione el sexo del personal "; adv.ShowDialog(); fil.Hide(); txt_Dni.Focus(); return false; }
            if (cbo_rol.SelectedIndex == -1) { fil.Show(); adv.Lbl_Msm1.Text = "Seleccione el distrito del personal"; adv.ShowDialog(); fil.Hide(); txt_Dni.Focus(); return false; }

            return true;
        }

        private void Guardar_Personal()
        {
            Frm_Msm_Bueno ok = new Frm_Msm_Bueno();
            Frm_Filtro fil = new Frm_Filtro();
            RN_Personal obj = new RN_Personal();
            EN_Persona per = new EN_Persona();
            try
            {
                per.Idpersonal = txt_IdPersona.Text;
                per.Dni = txt_Dni.Text;
                per.Nombres = txt_nombres.Text;
                per.anoNacimiento = dtp_fecha.Value;
                per.Sexo = cbo_sexo.Text;
                per.Direccion = txt_direccion.Text;
                per.Correo = txt_correo.Text;
                per.Celular = Convert.ToString(txt_NroCelular.Text);
                per.IdRol = Convert.ToString(cbo_rol.SelectedValue);
                per.xImagen = "";
                per.IdDistrito = Convert.ToString(cbo_Distrito.SelectedValue);
                obj.RN_Registral_Personal(per);
                if (BD_Personal.saved == true)
                {
                    fil.Show();
                    ok.Lbl_msm1.Text = "Los datos se han guardado de forma correcta";
                    ok.ShowDialog();
                    fil.Hide();
                    this.Tag = "A";
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Editar_Personal()
        {
            Frm_Msm_Bueno ok = new Frm_Msm_Bueno();
            Frm_Filtro fil = new Frm_Filtro();
            RN_Personal obj = new RN_Personal();
            EN_Persona per = new EN_Persona();
            try
            {
                per.Idpersonal = txt_IdPersona.Text;
                per.Dni = txt_Dni.Text;
                per.Nombres = txt_nombres.Text;
                per.anoNacimiento = dtp_fechaNaci.Value;
                per.Sexo = cbo_sexo.Text;
                per.Direccion = txt_direccion.Text;
                per.Correo = txt_correo.Text;
                per.Celular = Convert.ToString(txt_NroCelular.Text);
                per.IdRol = Convert.ToString(cbo_rol.SelectedValue);
                per.xImagen = xFotoruta;
                per.IdDistrito = Convert.ToString(cbo_Distrito.SelectedValue);
                per.Estado = cbo_status.Text;

                obj.RN_Editar_Personal(per);
                if (BD_Personal.edited == true)
                {
                    fil.Show();
                    ok.Lbl_msm1.Text = "Los datos se han editado de forma correcta";
                    ok.ShowDialog();
                    fil.Hide();
                    this.Tag = "B";
                    this.Close();
                }
            }
            catch (Exception ex)
            {

            }
        }
        string xFotoruta;
        private void Cargar_Distrito()
        {
            RN_Distrito obj = new RN_Distrito();
            DataTable dt = new DataTable();
            try
            {
                dt = obj.RN_Buscar_Todos_Distrito();
                if (dt.Rows.Count > 0)
                {
                    var cbo = cbo_Distrito;
                    cbo.DataSource = dt;
                    cbo.DisplayMember = "Distrito";
                    cbo.ValueMember = "Id_Distrito";
                }
                cbo_Distrito.SelectedIndex = -1;
            }
            catch (Exception ex)
            {

            }
        }

        private void Pic_persona_Click(object sender, EventArgs e)
        {
            var filepath = string.Empty;
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    xFotoruta = openFileDialog1.FileName;
                    Pic_persona.Load(xFotoruta);
                }
            }
            catch (Exception ex)
            {
                xFotoruta = Application.StartupPath + @"\user.png";
                Pic_persona.Load(Application.StartupPath + @"\user.png");
            }
        }

        private void btn_aceptar_Click(object sender, EventArgs e)
        {
            Frm_Advertencia ok = new Frm_Advertencia();
            Frm_Filtro fil = new Frm_Filtro();
            RN_Personal objper = new RN_Personal();
            if (validarCajasTexto() == false) return;
            if (xedit == false)
            {
                if (objper.RN_Verificar_DniPersonal(txt_Dni.Text) == true)
                {
                    MessageBox.Show("Número de Dni ya existe en la base de datos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return;
                }
                Guardar_Personal();
            }
            else
            {
                Editar_Personal();
            }
        }
        public bool xedit = false;
        public void Buscar_Personal_ParaEditar(string idpersonal)
        {
            try
            {
                RN_Personal obj = new RN_Personal();
                DataTable data = new DataTable();

                Cargar_rol();
                Cargar_Distrito();

                data = obj.RN_Buscar_Personal_xValor(idpersonal);
                if (data.Rows.Count == 0) return;
                {
                    txt_Dni.Text = Convert.ToString(data.Rows[0]["DNIPR"]);
                    txt_nombres.Text = Convert.ToString(data.Rows[0]["Nombre_Completo"]);
                    txt_direccion.Text = Convert.ToString(data.Rows[0]["Domicilio"]);
                    txt_correo.Text = Convert.ToString(data.Rows[0]["Correo"]);
                    txt_NroCelular.Text = Convert.ToString(data.Rows[0]["Celular"]);
                    dtp_fechaNaci.Value = Convert.ToDateTime(data.Rows[0]["Fec_Naci"]);

                    cbo_sexo.Text = Convert.ToString(data.Rows[0]["Sexo"]);
                    cbo_rol.SelectedValue = data.Rows[0]["Id_rol"];
                    cbo_Distrito.SelectedValue = data.Rows[0]["Id_Distrito"];
                    xFotoruta = Convert.ToString(data.Rows[0]["Foto"]);
                    cbo_status.Text = Convert.ToString(data.Rows[0]["Estado_Per"]);
                    //Pic_persona.Load(xFotoruta);
                }
                xedit = true;
                btn_aceptar.Enabled = true;
                cbo_status.Visible = true;
                Pic_persona.Load(xFotoruta);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar los datos: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            this.Tag = "";
            this.Close();
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
                Utilitarios u = new Utilitarios();
                u.Mover_formulario(this);
            }
        }

        private void txt_NroCelular_OnValueChanged(object sender, EventArgs e)
        {
            string xcar1, xcar2;
            if (xedit == false)
            {
                if (txt_Dni.Text.Length == 0) return;
                if (txt_nombres.Text.Length == 0) return;
                xcar1 = Convert.ToString(txt_Dni.Text).Substring(3, 5);
                xcar2 = Convert.ToString(txt_nombres.Text).Substring(1, 4);
                txt_IdPersona.Text = xcar1 + "-" + xcar2;
            }
        }

        private void cbo_rol_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
