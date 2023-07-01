using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prj_Capa_Entidad;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using System.Windows.Forms;
namespace Prj_Capa_Datos
{
    public class BD_Vacaciones : Cls_Conexion
    {
        public DataTable BD_Buscar_Vacaciones_xValor(string valor)
        {
            SqlConnection cn = new SqlConnection();
            try
            {
                cn.ConnectionString = Conectar();
                SqlDataAdapter da = new SqlDataAdapter($"select c.Id,c.Desde,c.Hasta, p.Nombre_Completo from ControVacacional c join PERSONAL p on c.IdEmpleado= p.Id_Pernl where c.Id= '{valor}'", cn);
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
                MessageBox.Show("Error al ejecutar el SP" + ex.Message, "Advertencia de seguridad", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return null;
        }
        public DataTable BD_VerVacaciones()
        {
            SqlConnection cn = new SqlConnection();
            try
            {
                cn.ConnectionString = Conectar();
                SqlDataAdapter da = new SqlDataAdapter("select c.Id,c.Desde,c.Hasta, p.Nombre_Completo from ControVacacional c join PERSONAL p on c.IdEmpleado= p.Id_Pernl", cn);
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
        public void BD_RegistrarVacaciones(DateTime Desde, DateTime Hasta, string IdEmpleado)
        {
            SqlConnection cn = new SqlConnection(Conectar());
            SqlCommand cmd = new SqlCommand("insert into ControVacacional values (@Id ,@Desde, @Hasta, @IdEmpleado)", cn);
            try
            {
                cmd.CommandTimeout = 20;
                cmd.Parameters.AddWithValue("@Id", Guid.NewGuid());
                cmd.Parameters.AddWithValue("@Desde", Desde);
                cmd.Parameters.AddWithValue("@Hasta", Hasta);
                cmd.Parameters.AddWithValue("@IdEmpleado", IdEmpleado);
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
                MessageBox.Show("Algo pasó" + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        public void BD_Editar_Vacaciones(EN_Vacaciones per)
        {
            SqlConnection cn = new SqlConnection(Conectar());
            SqlCommand cmd = new SqlCommand($"Update ControVacacional set Desde= CAST('{per.Desde.ToString("yyyy-MM-dd")}' AS DATETIME),Hasta= CAST('{per.Hasta.ToString("yyyy-MM-dd")}' AS DATETIME) where Id='{per.Id}'", cn);
            try
            {
                cmd.CommandTimeout = 20;
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
                MessageBox.Show("Algo pasó" + ex.Message, "Adventencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        public bool BD_Verificar_SiTieneVacaciones(string IdEmpleado)
        {
            SqlConnection cn = new SqlConnection(Conectar());
            try
            {                
                SqlDataAdapter da = new SqlDataAdapter($"select * from ControVacacional where IdEmpleado='{IdEmpleado}'", cn);
                DataTable dato = new DataTable();
                da.Fill(dato);
                if (dato.Rows.Count != 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
                MessageBox.Show("Algo pasó" + ex.Message, "Adventencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return false;
        }
    }
}
