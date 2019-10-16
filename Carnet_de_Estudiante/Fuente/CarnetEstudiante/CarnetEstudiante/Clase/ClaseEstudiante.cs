using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


namespace CarnetEstudiante.Clase
{
    class ClaseEstudiante
    {
        private Conexion conex = new Conexion();
        private SqlConnection cn;
        private SqlConnection cnn = new SqlConnection
          (ConfigurationManager.ConnectionStrings["cadena"].ConnectionString);

        //PARA LISTAR
        public DataTable Listar()
        {
            DataTable data = new DataTable();

            SqlDataAdapter adap = new SqlDataAdapter
                ("sp_listar", cnn);
            adap.SelectCommand.CommandType = CommandType.StoredProcedure;
            adap.Fill(data);

            return data;
        }
        //PARA REGISTRAR
        public string Registrar(string id, string dni, string nombre, string paterno, string materno, byte[] foto)
        {
            cn = conex.Conectar();
            string mensaje = string.Empty;
            SqlTransaction trans = null;
            SqlCommand cmd = new SqlCommand("sp_registrar", cn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@dni", dni);
            cmd.Parameters.AddWithValue("@nombre", nombre);
            cmd.Parameters.AddWithValue("@paterno", paterno);
            cmd.Parameters.AddWithValue("@materno", materno);
            cmd.Parameters.AddWithValue("@foto", foto);

            int valor = 0;

            try
            {
                cn.Open();
                trans = cn.BeginTransaction(IsolationLevel.Serializable);
                cmd.Transaction = trans;
                valor = cmd.ExecuteNonQuery();
                trans.Commit();
                mensaje = "Registrado";
            }
            catch (Exception ex)
            {
                trans.Rollback();
                mensaje = ex.Message;
            }
            finally
            {
                cn.Close();
            }
            return mensaje;
        }


        //PARA FILTRAR
        public DataTable ListarXDnioCod(string filtro)
        {
            DataTable dtb = new DataTable();

            SqlDataAdapter dap = new SqlDataAdapter
                ("sp_listaralumnoxdniocod", cnn);
            dap.SelectCommand.CommandType = CommandType.StoredProcedure;
            dap.SelectCommand.Parameters.AddWithValue("@filtro", filtro);
            dap.Fill(dtb);

            return dtb;
        }


        //EL GRAN AUTOGENERADO
        public string CodigoAutogenerar()
        {
            cn = conex.Conectar();
            string codigo = string.Empty;
            SqlCommand cmd = new SqlCommand
                ("SP_Autogenerar", cn);
            cmd.CommandType = CommandType.StoredProcedure;
          
            try
            {
                cn.Open();
                codigo = cmd.ExecuteScalar().ToString();
            }
            finally
            {
                cn.Close();
            }
            return codigo;
        }


    }
   

}
