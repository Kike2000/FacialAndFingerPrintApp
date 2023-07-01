using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using Prj_Capa_Entidad;

namespace Prj_Capa_Datos
{
    public class BD_Horario : Cls_Conexion
    {
        public static bool saved = false;
        public void BD_Actualizar_Horario(EN_Horario P)
        {
            SqlConnection cn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cn.ConnectionString = Conectar();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_Update_Horario";
                cmd.Connection = cn;
                cmd.CommandTimeout = 20;
                cmd.Parameters.AddWithValue("@Idhor", P.Idhora);
                cmd.Parameters.AddWithValue("@HoEntrada", P.HoEntrada);
                cmd.Parameters.AddWithValue("@HoTolere", P.HoTole);
                cmd.Parameters.AddWithValue("@Holimite", P.HoLimite);
                cmd.Parameters.AddWithValue("@HoraSalida", P.HoSalida);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
                cmd.Dispose();
                cmd = null;
                cn.Dispose();
                cn = null;

                saved = true;
            }
            catch (Exception ex)
            {
                saved = false;
                MessageBox.Show("Error al editar: " + ex.Message, "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (cn.State == ConnectionState.Open) cn.Close();
                cmd.Dispose();
                cmd = null;
                cn.Dispose();
                cn = null;
            }

        }
        public DataTable BD_Leer_Horarios()
        {
            SqlConnection cn = new SqlConnection();
            try
            {
                cn.ConnectionString = Conectar();
                SqlDataAdapter adapter = new SqlDataAdapter("Sp_Buscar_Todos_Horarios", cn);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable datos = new DataTable();
                adapter.Fill(datos);
                adapter = null;
                return datos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar horario " + ex.Message, "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (cn.State == ConnectionState.Open) cn.Close();
                cn.Dispose();
                cn = null;
                return null;
            }
        }

    }
}
