using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Prj_Capa_Datos
{
    public class BD_Asistencia : Cls_Conexion
    {
        public DataTable BD_Ver_Todas_Asistencia()
        {
            SqlConnection cn = new SqlConnection();
            try
            {
                cn.ConnectionString = Conectar();
                SqlDataAdapter da = new SqlDataAdapter("Sp_Cargar_Todas_Asistencias", cn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dato = new DataTable();
                da.Fill(dato);
                return dato;
            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                    throw new Exception("Error" + ex.Message, ex);
                }
            }
            return null;
        }

        public DataTable BD_Ver_Todas_Asistencia_DelDia(DateTime xfecha)
        {
            SqlConnection cn = new SqlConnection();
            try
            {
                cn.ConnectionString = Conectar();
                SqlDataAdapter da = new SqlDataAdapter("Sp_Cargar_Asistencia_deldia", cn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@fecha", xfecha);
                DataTable dato = new DataTable();
                da.Fill(dato);
                return dato;
            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                    throw new Exception("Error" + ex.Message, ex);
                }
            }
            return null;
        }
        public DataTable BD_Ver_Todas_Asistencia_DelMes(DateTime xfecha)
        {
            SqlConnection cn = new SqlConnection();
            try
            {
                cn.ConnectionString = Conectar();
                SqlDataAdapter da = new SqlDataAdapter("Sp_Cargar_Asistencia_xFecha", cn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@fecha", xfecha);
                DataTable dato = new DataTable();
                da.Fill(dato);
                return dato;
            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                    throw new Exception("Error" + ex.Message, ex);
                }
            }
            return null;
        }

        public DataTable BD_Ver_Todas_Asistencia_ParaExplorador(string valor)
        {
            SqlConnection cn = new SqlConnection();
            try
            {
                cn.ConnectionString = Conectar();
                SqlDataAdapter da = new SqlDataAdapter("Sp_Buscar_Asistencia_paraExplorador", cn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Id_Asis", valor);
                DataTable dato = new DataTable();
                da.Fill(dato);
                return dato;
            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                    throw new Exception("Error" + ex.Message, ex);
                }
            }
            return null;
        }
        public static bool entrada = false;
        public void BD_Registrar_Entrada_Personal(string idAsis, string idPerso, string HoIngreso, double tarde, int totalHora, string justificado)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = Conectar();
            SqlCommand cmd = new SqlCommand("Sp_Registrar_Entrada", cn);
            try
            {
                cmd.CommandTimeout = 20;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdAsis", idAsis);
                cmd.Parameters.AddWithValue("@Id_Persol", idPerso);
                cmd.Parameters.AddWithValue("@Hoingre", HoIngreso);
                cmd.Parameters.AddWithValue("@tardanza", tarde);
                cmd.Parameters.AddWithValue("@TotalHora", totalHora);
                cmd.Parameters.AddWithValue("@justificado", justificado);

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
                entrada = true;
            }
            catch (Exception ex)
            {
                entrada = false;
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
                throw new Exception("Error al guardar" + ex.Message);
            }
        }
        public static bool salida = false;
        public void BD_Registrar_Salida_Personal(string idAsis, string idPerso, string HoSalida, int totalHora)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = Conectar();
            SqlCommand cmd = new SqlCommand("Sp_Registrar_Salida", cn);
            try
            {
                cmd.CommandTimeout = 20;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdAsis", idAsis);
                cmd.Parameters.AddWithValue("@Id_Personal", idPerso);
                cmd.Parameters.AddWithValue("@HoSalida", HoSalida);
                cmd.Parameters.AddWithValue("@TotalHora", totalHora);

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
                salida = true;
            }
            catch (Exception ex)
            {
                salida = false;
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
                throw new Exception("Error al guardar" + ex.Message);
            }
        }

        public bool BD_Checar_SiPersonal_YaMarco_Asistencia(string xidPerso)
        {
            bool functionReturnValue = false;
            Int32 xfil = 0;

            SqlConnection cn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            cn.ConnectionString = Conectar();
            var _with1 = cmd;
            _with1.CommandText = "Sp_Validar_RegistroAsistencia";
            _with1.Connection = cn;
            _with1.CommandTimeout = 20;
            _with1.CommandType = CommandType.StoredProcedure;
            _with1.Parameters.AddWithValue("@Id_Personal", xidPerso);

            try
            {
                cn.Open();
                xfil = (Int32)cmd.ExecuteScalar();
                if (xfil > 0)
                {
                    functionReturnValue = true;
                }
                else
                {
                    functionReturnValue = false;
                }
                cmd.Parameters.Clear();
                cmd.Dispose();
                cmd = null;
                cn.Close();
                cn = null;
            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open) cn.Close();
                cmd.Dispose();
                cmd = null;
                cn.Close();
                cn = null;
                MessageBox.Show("Algo pasó: " + ex.Message, "Adventencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            return functionReturnValue;
        }
        public bool BD_Checar_SiPersonal_YaMarco_Entrada(string xidPerso)
        {
            bool functionReturnValue = false;
            Int32 xfil = 0;

            SqlConnection cn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            cn.ConnectionString = Conectar();
            var _with1 = cmd;
            _with1.CommandText = "Sp_verificar_IngresoAsis";
            _with1.Connection = cn;
            _with1.CommandTimeout = 20;
            _with1.CommandType = CommandType.StoredProcedure;
            _with1.Parameters.AddWithValue("@Id_Personal", xidPerso);

            try
            {
                cn.Open();
                xfil = (Int32)cmd.ExecuteScalar();
                if (xfil > 0)
                {
                    functionReturnValue = true;
                }
                else
                {
                    functionReturnValue = false;
                }
                cmd.Parameters.Clear();
                cmd.Dispose();
                cmd = null;
                cn.Close();
                cn = null;
            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open) cn.Close();
                cmd.Dispose();
                cmd = null;
                cn.Close();
                cn = null;
                MessageBox.Show("Algo pasó: " + ex.Message, "Adventencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            return functionReturnValue;
        }

        public bool BD_Verificar_Justificacion_Aprobado(string idpers)
        {
            bool functionReturnValue = false;
            Int32 xfil = 0;

            SqlConnection cn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            cn.ConnectionString = Conectar();
            var _with1 = cmd;
            _with1.CommandText = "SP_VerificarJustificacion_Aprobada";
            _with1.Connection = cn;
            _with1.CommandTimeout = 20;
            _with1.CommandType = CommandType.StoredProcedure;
            _with1.Parameters.AddWithValue("@Id_Personal", idpers);

            try
            {
                cn.Open();
                xfil = (Int32)cmd.ExecuteScalar();
                if (xfil > 0)
                {
                    functionReturnValue = true;
                }
                else
                {
                    functionReturnValue = false;
                }
                cmd.Parameters.Clear();
                cmd.Dispose();
                cmd = null;
                cn.Close();
                cn = null;
            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open) cn.Close();
                cmd.Dispose();
                cmd = null;
                cn.Close();
                cn = null;
                MessageBox.Show("Algo pasó: " + ex.Message, "Adventencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            return functionReturnValue;
        }
        public DataTable BD_Buscar_Asistencia_deEntrada(string idperso)
        {
            SqlConnection cn = new SqlConnection();
            try
            {
                cn.ConnectionString = Conectar();
                SqlDataAdapter da = new SqlDataAdapter("Sp_Leer_asistencia_reciente", cn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@idper", idperso);
                DataTable dato = new DataTable();
                da.Fill(dato);
                da = null;
                return dato;
            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
                MessageBox.Show("Algo pasó" + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            return null;
        }

        public bool BN_Checar_SiPersonal_TieneAsistencia_Registrada(string xidPerso)
        {
            bool functionReturn = false;
            Int32 xfil = 0;

            SqlConnection cn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            cn.ConnectionString = Conectar();
            var with = cmd;
            with.CommandText = "Sp_Ver_sihay_Registro";
            with.Connection = cn;
            with.CommandTimeout = 20;
            with.CommandType = CommandType.StoredProcedure;
            with.Parameters.AddWithValue("@Id_Personal", xidPerso);

            try
            {
                cn.Open();
                xfil = (Int32)cmd.ExecuteScalar();
                if (xfil > 0)
                {
                    functionReturn = true;
                }
                else
                {
                    functionReturn = false;
                }
                cmd.Parameters.Clear();
                cmd.Dispose();
                cmd = null;
                cn.Close();
                cn = null;

            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open) cn.Close();
                cmd.Dispose();
                cmd = null;
                cn.Close();
                cn = null;
                MessageBox.Show("Algo pasó: " + ex.Message, "Adventencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return functionReturn;
        }

        public bool BD_Checar_SiPersonal_YaMarco_suFalta(string xidPerso)
        {
            bool functionReturn = false;
            Int32 xfil = 0;

            SqlConnection cn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            cn.ConnectionString = Conectar();
            var with = cmd;
            with.CommandText = "Sp_Verificar_siMarco_Falta";
            with.Connection = cn;
            with.CommandTimeout = 20;
            with.CommandType = CommandType.StoredProcedure;
            with.Parameters.AddWithValue("@Id_Personal", xidPerso);

            try
            {
                cn.Open();
                xfil = (Int32)cmd.ExecuteScalar();
                if (xfil > 0)
                {
                    functionReturn = true;
                }
                else
                {
                    functionReturn = false;
                }
                cmd.Parameters.Clear();
                cmd.Dispose();
                cmd = null;
                cn.Close();
                cn = null;

            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open) cn.Close();
                cmd.Dispose();
                cmd = null;
                cn.Close();
                cn = null;
                MessageBox.Show("Algo pasó: " + ex.Message, "Adventencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return functionReturn;
        }
        public static bool falta = false;
        public void BD_Registrar_Falta_Personal(string idAsis, string idPerso, string justificado)
        {
            SqlConnection cn = new SqlConnection(Conectar());
            SqlCommand cmd = new SqlCommand("Sp_Registrar_Falta", cn);
            try
            {
                cmd.CommandTimeout = 20;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdAsis", idAsis);
                cmd.Parameters.AddWithValue("@Id_Persol", idPerso);
                cmd.Parameters.AddWithValue("@justificado", justificado);

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
                falta = true;
            }
            catch (Exception ex)
            {
                falta = false;
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
                throw new Exception("Error al guardar" + ex.Message);
            }
        }

        public void BD_Eliminar_Asistencia(string xvalor)
        {
            SqlConnection cn = new SqlConnection(Conectar());
            SqlCommand cmd = new SqlCommand("Sp_Delete_Asistencia", cn);
            try
            {
                cmd.CommandTimeout = 20;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_Asis", xvalor);

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
                throw new Exception("Error al guardar" + ex.Message);
            }
        }

    }
}
