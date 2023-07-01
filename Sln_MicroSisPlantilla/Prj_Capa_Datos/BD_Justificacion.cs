using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prj_Capa_Entidad;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace Prj_Capa_Datos
{
    public class BD_Justificacion : Cls_Conexion
    {
        public static bool saved = false;
        public void BD_Registrar_Justificacin(EN_Justificacion jus)
        {
            SqlConnection cn = new SqlConnection(Conectar());
            SqlCommand cmd = new SqlCommand("Sp_Insert_Justification", cn);

            try
            {
                cmd.CommandTimeout = 20;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_Justi", jus.IdJusti);
                cmd.Parameters.AddWithValue("@Id_Personal", jus.Id_Personal);
                cmd.Parameters.AddWithValue("@Principalmoti", jus.PrincipalMotivo);
                cmd.Parameters.AddWithValue("@Detalle", jus.Detalle);
                cmd.Parameters.AddWithValue("@FechaJusti", jus.Fecha);
                cmd.Parameters.AddWithValue("@EstadoJus", jus.Estado);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();

                saved = true;

            }
            catch (Exception ex)
            {
                saved = false;
                if (cn.State == ConnectionState.Open) { cn.Close(); }

                MessageBox.Show("Algo pasó" + ex.Message, "Advertencia de Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static bool edited = false;
        public void BD_Editar_Justificacion(EN_Justificacion jus)
        {
            SqlConnection cn = new SqlConnection(Conectar());
            SqlCommand cmd = new SqlCommand("Sp_Modificar_justificacion", cn);

            try
            {
                cmd.CommandTimeout = 20;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_Justi", jus.IdJusti);
                cmd.Parameters.AddWithValue("@Id_Personal", jus.Id_Personal);
                cmd.Parameters.AddWithValue("@Principalmoti", jus.PrincipalMotivo);
                cmd.Parameters.AddWithValue("@Detalle", jus.Detalle);
                cmd.Parameters.AddWithValue("@FechaJusti", jus.Fecha);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
                edited = true;
            }
            catch (Exception ex)
            {
                edited = false;
                if (cn.State == ConnectionState.Open) { cn.Close(); }

                MessageBox.Show("Algo pasó" + ex.Message, "Advertencia de Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static bool tried = false;

        public void BD_Aprobar_Justificacion(string id_justi, string idperso)
        {
            SqlConnection cn = new SqlConnection(Conectar());
            SqlCommand cmd = new SqlCommand("Sp_Aprobar_justificacion", cn);

            try
            {
                cmd.CommandTimeout = 20;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdJusti", id_justi);
                cmd.Parameters.AddWithValue("@Id_Personal", idperso);

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
                tried = true;
            }
            catch (Exception ex)
            {
                tried = false;
                if (cn.State == ConnectionState.Open) { cn.Close(); }
                MessageBox.Show("Algo pasó" + ex.Message, "Advertencia de Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void BD_Desaprobar_Justificacion(string idjusti, string idperso)
        {
            SqlConnection cn = new SqlConnection(Conectar());
            SqlCommand cmd = new SqlCommand("Sp_Desaprobar_justificacion", cn);

            try
            {
                cmd.CommandTimeout = 20;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdJusti", idjusti);
                cmd.Parameters.AddWithValue("@Id_Personal", idperso);

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
                tried = true;
            }
            catch (Exception ex)
            {
                tried = false;
                if (cn.State == ConnectionState.Open) { cn.Close(); }
                MessageBox.Show("Algo pasó" + ex.Message, "Advertencia de Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public DataTable BD_Cargar_todos_Justificacion()
        {
            SqlConnection xcn = new SqlConnection();
            try
            {
                xcn.ConnectionString = Conectar();
                SqlDataAdapter da = new SqlDataAdapter("Sp_Cargar_Todas_Justificaciones", xcn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dato = new DataTable();
                da.Fill(dato);
                da = null;
                return dato;
            }
            catch (Exception ex)
            {
                if (xcn.State == ConnectionState.Open) { xcn.Close(); }
                MessageBox.Show("Algo pasó" + ex.Message, "Advertencia de Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }

        public DataTable BD_BuscarJustificacion_porValor(string xdato)
        {
            SqlConnection cn = new SqlConnection();
            try
            {
                cn.ConnectionString = Conectar();
                SqlDataAdapter da = new SqlDataAdapter("Sp_Cargar_Justificacion_xDni", cn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Id_Personal", xdato);
                DataTable dato = new DataTable();
                da.Fill(dato);
                da = null;
                return dato;
            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open) { cn.Close(); }
                cn = null;
                MessageBox.Show("Algo pasó" + ex.Message, "Advertencia de Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }

        public static bool supressed = false;

        public void BD_Eliminar_Justificacion(string idjusti)
        {
            SqlConnection cn = new SqlConnection(Conectar());
            SqlCommand cmd = new SqlCommand("Sp_Delete_Justificacion", cn);

            try
            {
                cmd.CommandTimeout = 20;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_Justi", idjusti);

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
                supressed = true;
            }
            catch (Exception ex)
            {
                supressed = false;
                if (cn.State == ConnectionState.Open) { cn.Close(); }
                cn = null;
                MessageBox.Show("Algo pasó" + ex.Message, "Advertencia de Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
