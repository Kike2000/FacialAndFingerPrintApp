﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Prj_Capa_Datos
{
    public class BD_Utilitario : Cls_Conexion
    {

        public static string BD_NroDoc(int Id_Tipo)
        {
            SqlConnection cn = new SqlConnection();
            try
            {
                cn.ConnectionString = Conectar2();
                SqlCommand cmd = new SqlCommand("Sp_Listado_Tipo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Tipo", Id_Tipo);
                string NroDoc;

                cn.Open();
                NroDoc = Convert.ToString(cmd.ExecuteScalar());
                cn.Close();
                return NroDoc;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (cn.State == ConnectionState.Open) cn.Close();
                cn.Dispose();
                cn = null;
                return null;
            }
        }
        public static void BD_Actualiza_Tipo_Doc(int Id_Tipo)
        {
            SqlConnection cn = new SqlConnection(Conectar2());
            SqlCommand cmd = new SqlCommand("Sp_Actualiza_Tipo_Doc", cn);
            try
            {
                cmd.CommandTimeout = 20;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Tipo", Id_Tipo);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();

                cmd.Dispose();
                cmd = null;
                cn = null;

            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open) cn.Close();
                cmd = null;
                cmd.Dispose();
                MessageBox.Show("Error: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


            }
        }

        public static string BD_Listar_TipoFalta(int Id_Tipo)
        {
            SqlConnection cn = new SqlConnection();
            try
            {
                cn.ConnectionString = Conectar2();
                SqlCommand cmd = new SqlCommand("Sp_Listado_TipoFalta", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Tipo", Id_Tipo);
                string NroDoc;
                cn.Open();
                NroDoc = Convert.ToString(cmd.ExecuteScalar());
                cn.Close();
                return NroDoc;
            }catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message, "Adventencia de seguridad", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (cn.State == ConnectionState.Open) cn.Close();
                cn.Dispose();
                cn = null;
                return null;
            }
        }
        public static bool falta = false;
        public void BD_Actualizar_RobotFalta(int IdTipo, string serie)
        {
            SqlConnection cn = new SqlConnection(Conectar());
            SqlCommand cmd = new SqlCommand("Sp_Activar_Desac_RobotFalta", cn);
            try
            {
                cmd.CommandTimeout = 20;
                cmd.CommandType= CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdTipo",IdTipo);
                cmd.Parameters.AddWithValue("@serie",serie);

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();

                falta = true;
            }
            catch (Exception ex)
            {
                falta = false;
                MessageBox.Show("Error" + ex.Message, "Adventencia de seguridad", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (cn.State == ConnectionState.Open) cn.Close();
            }
        }




    }
}
