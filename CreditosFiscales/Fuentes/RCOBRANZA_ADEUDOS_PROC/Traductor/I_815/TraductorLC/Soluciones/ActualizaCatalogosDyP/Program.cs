using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;


namespace ActualizaCatalogosDyP
{
    class Program
    {
        private static int IdOrigen = 0;
        private static string StringConexion = "";
        private static string LogPath = "";

        static void Main(string[] args)
        {
            bool Continuar = false;
            CatalogosDyP.TipoDocumento[] TiposDocumento=null;

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            StringConexion = config.AppSettings.Settings["StringConnexion"].Value;
            IdOrigen = Convert.ToInt32(config.AppSettings.Settings["IdOrigen"].Value);
            LogPath = "Log_" + DateTime.Now.Year + DateTime.Now.Month.ToString().PadLeft(2) + DateTime.Now.Day.ToString().PadLeft(2) + DateTime.Now.Hour.ToString().PadLeft(2)+DateTime.Now.Minute.ToString().PadLeft(2) + ".txt";
            LogPath = LogPath.Replace(" ", "0");

            EscribeLog("/******************************INICIANDO PROCESO DE ACTUALIZACION************************************************/");


            //Obtener los tipos de documento
            EscribeLog("Inicio: --> Obteniendo TIPOS DE DOCUMENTO");
            TiposDocumento = ObtieneTiposDocumentoDyP();
            if (TiposDocumento != null)
            {EscribeLog("Inicio: --> TIPOS DE DOCUMENTO Obtenidos");}
            else
            {EscribeLog("Error: --> TIPOS DE DOCUMENTO no se pudieron obtener"); }


            if (TiposDocumento  != null)
            {
                //Actualiza los tipos de documento
                EscribeLog("Inicio: --> Actualizando TIPOS DE DOCUMENTO");
                Continuar = ActualizaTiposDocumentoDyP(TiposDocumento);
                if (Continuar)
                    {EscribeLog("Inicio: --> TIPOS DE DOCUMENTO Actualizados");}
                else 
                    { EscribeLog("Error: --> TIPOS DE DOCUMENTO no se pudieron actualizar"); }
            }



            //Actualiza los conceptos de DyP
            EscribeLog("Inicio: --> Actualizando CONCEPTOS");
            Continuar = ActualizaConceptosDyP();
            if (Continuar)
            { EscribeLog("Inicio: --> CONCEPTOS Actualizados"); }
            else
            { EscribeLog("Error: --> CONCEPTOS no se pudieron actualizar"); }


            //Actualiza las transacciones
            if (TiposDocumento != null)
            {
                EscribeLog("Inicio: --> Actualizando TRANSACCIONES");
                Continuar = ActualizaTransaccionesDyP(TiposDocumento);
                if (Continuar)
                { EscribeLog("Inicio: --> TRANSACCIONES Actualizados"); }
                else
                { EscribeLog("Error: --> TRANSACCIONES no se pudieron actualizar"); }
            }

            //Actualiza las periodicidades
            EscribeLog("Inicio: --> Actualizando PERIODICIDADES");
            Continuar = ActualizaPeriodicidadesDyP();
            if (Continuar)
            { EscribeLog("Inicio: --> PERIODICIDADES Actualizados"); }
            else
            { EscribeLog("Error: --> PERIODICIDADES no se pudieron actualizar"); }


            //ActualizaPeriodos
            EscribeLog("Inicio: --> Actualizando PERIODOS");
            Continuar = ActualizaPeriodosDyP();
            if (Continuar)
            { EscribeLog("Inicio: --> PERIODOS Actualizados"); }
            else
            { EscribeLog("Error: --> PERIODOS no se pudieron actualizar"); }


            EscribeLog("/******************************FIN PROCESO DE ACTUALIZACION************************************************/");

        }


        static bool ActualizaConceptosDyP()
        {

            SqlConnection sqlCon = new SqlConnection(StringConexion);
            SqlTransaction sqlTran = null;
            try
            {
                Console.WriteLine("Inicia proceso Actualiza Conceptos");

                //Declaracion de objetos referenciados
                CatalogosDyP.ConsultaCatalogosClient CatalogosDyP = new CatalogosDyP.ConsultaCatalogosClient();
                CatalogosDyP.CatalogoFiltro filtro = new ActualizaCatalogosDyP.CatalogosDyP.CatalogoFiltro();
                CatalogosDyP.ConceptoPago[] ConceptosDyP;

                //Asignar valor para el filtro
                filtro.IdOrigen = IdOrigen;
                ConceptosDyP = CatalogosDyP.ConsultarConceptosPagos(filtro);

                //Declarar objetos de conexion a BD
                SqlCommand sqlCmd = new SqlCommand();


                //Abrir conexion
                sqlCon.Open();

                //Comienza la transaccion
                sqlTran = sqlCon.BeginTransaction();

                //Setear valores de conexion y transaccion al comando
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = sqlTran;

                //Setear comando para desactivar todos los conceptos
                sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCmd.CommandText = "spConceptosDyPDesactivar";


                //Ejecutar comando para desactivar todos los conceptos
                sqlCmd.ExecuteNonQuery();

                //Actualizar los valores de la BD a partir de los que vienen en el servicio
                foreach (ActualizaCatalogosDyP.CatalogosDyP.ConceptoPago ConceptoDyP in ConceptosDyP)
                {
                    //Especificar sp que se va a usar
                    sqlCmd.CommandText = "spUpdConceptoDyP";

                    //Agregar parametros
                    sqlCmd.Parameters.Clear();
                    sqlCmd.Parameters.Add("@ConceptoDyP", SqlDbType.VarChar, 10).Value = ConceptoDyP.IdConceptoPago;
                    sqlCmd.Parameters.Add("@Descripcion", SqlDbType.VarChar, 400).Value = ConceptoDyP.Descripcion;

                    //Ejecutar comando
                    sqlCmd.ExecuteNonQuery();

                }

                sqlTran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                EscribeLog("Error: --> " + ex.Message);
                if (sqlTran != null)
                {
                    sqlTran.Rollback();
                }

                return false;
            }

            finally
            {

                if (sqlCon.State == ConnectionState.Open)
                { sqlCon.Close(); }

                Console.WriteLine("Fin del proceso Actualiza Conceptos");

            }
        }

        static CatalogosDyP.TipoDocumento[] ObtieneTiposDocumentoDyP()
        {

            try
            {
                Console.WriteLine("Obteniendo Tipos de Documentos");
                //Declaracion de objetos referenciados
                CatalogosDyP.ConsultaCatalogosClient CatalogosDyP = new CatalogosDyP.ConsultaCatalogosClient();
                CatalogosDyP.CatalogoFiltro filtro = new ActualizaCatalogosDyP.CatalogosDyP.CatalogoFiltro();
                CatalogosDyP.TipoDocumento[] ObjetosDyP;

                //Asignar valor para el filtro
                filtro.IdOrigen = IdOrigen;
                ObjetosDyP = CatalogosDyP.ConsultarTiposDocumentos(filtro);

                Console.WriteLine("Tipo de Documentos Obtenidos");
                return (ObjetosDyP);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                EscribeLog("Error: --> " + ex.Message);
                return (null);
            }

        }

        static bool ActualizaTiposDocumentoDyP(CatalogosDyP.TipoDocumento[] TiposDocumento)
        {

            SqlConnection sqlCon = new SqlConnection(StringConexion);
            SqlTransaction sqlTran = null;
            try
            {
                Console.WriteLine("Inicia proceso Actualiza Tipos de Documento");

                //Declarar objetos de conexion a BD
                SqlCommand sqlCmd = new SqlCommand();

                //Abrir conexion
                sqlCon.Open();

                //Comienza la transaccion
                sqlTran = sqlCon.BeginTransaction();

                //Setear valores de conexion y transaccion al comando
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = sqlTran;

                //Setear comando para desactivar todos las transacciones
                sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCmd.CommandText = "spUpdTipoDocumentoDyP";

                foreach (CatalogosDyP.TipoDocumento TipoDocumento in TiposDocumento)
                {
                    //Agregar parametros
                    sqlCmd.Parameters.Clear();

                    sqlCmd.Parameters.Add("@IdTipoDocumento", SqlDbType.SmallInt).Value = TipoDocumento.IdTipoDocumento;
                    sqlCmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = TipoDocumento.Descripcion;

                    //Ejecutar comando
                    sqlCmd.ExecuteNonQuery();
                }
                sqlTran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                EscribeLog("Error: --> " + ex.Message);
                if (sqlTran != null)
                {
                    sqlTran.Rollback();
                }

                return false;
            }

            finally
            {

                if (sqlCon.State == ConnectionState.Open)
                { sqlCon.Close(); }

                Console.WriteLine("Fin del proceso actualiza Tipos de documento");

            }
        }

        static bool ActualizaTransaccionesDyP(CatalogosDyP.TipoDocumento[] TiposDocumento)
        {

            SqlConnection sqlCon = new SqlConnection(StringConexion);
            SqlTransaction sqlTran = null;
            try
            {
                Console.WriteLine("Inicia proceso Actualiza Transacciones");

                //Declaracion de objetos referenciados
                CatalogosDyP.ConsultaCatalogosClient CatalogosDyP = new CatalogosDyP.ConsultaCatalogosClient();
                CatalogosDyP.CatalogoFiltro filtro = new ActualizaCatalogosDyP.CatalogosDyP.CatalogoFiltro();
                CatalogosDyP.Transaccion[] ObjetosDyP;

                //Declarar objetos de conexion a BD
                SqlCommand sqlCmd = new SqlCommand();

                //Abrir conexion
                sqlCon.Open();

                //Comienza la transaccion
                sqlTran = sqlCon.BeginTransaction();

                //Setear valores de conexion y transaccion al comando
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = sqlTran;

                //Setear comando para desactivar todos las transacciones
                sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCmd.CommandText = "spTransaccionesDyPDesactivar";


                //Ejecutar comando para desactivar todos las transacciones
                sqlCmd.ExecuteNonQuery();


                foreach (CatalogosDyP.TipoDocumento TipoDocumento in TiposDocumento)
                {
                    //Asignar valor para el filtro
                    filtro.IdOrigen = IdOrigen;
                    filtro.TipoDocumento = TipoDocumento;

                    ObjetosDyP = CatalogosDyP.ConsultarTransacciones(filtro);

                    //Actualizar los valores de la BD a partir de los que vienen en el servicio
                    foreach (ActualizaCatalogosDyP.CatalogosDyP.Transaccion ObjetoDyP in ObjetosDyP)
                    {
                        //Especificar sp que se va a usar
                        sqlCmd.CommandText = "spUpdTransaccionDyP";

                        //Agregar parametros
                        sqlCmd.Parameters.Clear();

                        sqlCmd.Parameters.Add("@CveTransaccion", SqlDbType.VarChar, 10).Value = ObjetoDyP.IdTransaccion;
                        sqlCmd.Parameters.Add("@IdTipoDocumento", SqlDbType.SmallInt).Value = TipoDocumento.IdTipoDocumento;
                        //sqlCmd.Parameters.Add("@IdAplicacion", SqlDbType.VarChar, 10).Value = IdAplicacion;
                        sqlCmd.Parameters.Add("@Descripcion", SqlDbType.VarChar, 100).Value = ObjetoDyP.Descripcion;
                        sqlCmd.Parameters.Add("@IdTipoTransaccion", SqlDbType.Int).Value = ObjetoDyP.TipoTransaccion.IdTipoTransaccion;
                        sqlCmd.Parameters.Add("@TipoTransaccion", SqlDbType.VarChar, 20).Value = ObjetoDyP.TipoTransaccion.Descripcion;
                        //sqlCmd.Parameters.Add("@EsObligatorio", SqlDbType.VarChar, 10).Value = ObjetoDyP.TipoTransaccion.;
                        sqlCmd.Parameters.Add("@Activo", SqlDbType.Bit).Value = true;


                        //Ejecutar comando
                        sqlCmd.ExecuteNonQuery();

                    }
                }
                sqlTran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                EscribeLog("Error: --> " + ex.Message);
                if (sqlTran != null)
                {
                    sqlTran.Rollback();
                }

                return false;
            }

            finally
            {

                if (sqlCon.State == ConnectionState.Open)
                { sqlCon.Close(); }

                Console.WriteLine("Fin del proceso actualiza transacciones");

            }
        }

        static bool ActualizaPeriodicidadesDyP()
        {

            SqlConnection sqlCon = new SqlConnection(StringConexion);
            SqlTransaction sqlTran = null;
            try
            {
                Console.WriteLine("Inicia proceso Actualiza Periodicidades");

                //Declaracion de objetos referenciados
                CatalogosDyP.ConsultaCatalogosClient CatalogosDyP = new CatalogosDyP.ConsultaCatalogosClient();
                CatalogosDyP.CatalogoFiltro filtro = new ActualizaCatalogosDyP.CatalogosDyP.CatalogoFiltro();
                CatalogosDyP.Periodicidad[] ObjetosDyP;

                //Asignar valor para el filtro
                filtro.IdOrigen = IdOrigen;
                ObjetosDyP = CatalogosDyP.ConsultarPeriodicidades(filtro);

                //Declarar objetos de conexion a BD
                SqlCommand sqlCmd = new SqlCommand();

                //Abrir conexion
                sqlCon.Open();

                //Comienza la transaccion
                sqlTran = sqlCon.BeginTransaction();

                //Setear valores de conexion y transaccion al comando
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = sqlTran;

                //Setear comando para desactivar todos los conceptos
                sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCmd.CommandText = "spUpdPeriodicidadesDesactivar";


                //Ejecutar comando para desactivar todos los conceptos
                sqlCmd.ExecuteNonQuery();

                //Especificar sp que se va a usar
                sqlCmd.CommandText = "spUpdPeriodicidadDyP";

                //Actualizar los valores de la BD a partir de los que vienen en el servicio
                foreach (ActualizaCatalogosDyP.CatalogosDyP.Periodicidad ObjetoDyP in ObjetosDyP)
                {


                    //Agregar parametros
                    sqlCmd.Parameters.Clear();
                    sqlCmd.Parameters.Add("@IdPeriodicidadDyP", SqlDbType.VarChar, 10).Value = ObjetoDyP.IdPeriodicidad;
                    sqlCmd.Parameters.Add("@Descripcion", SqlDbType.VarChar, 40).Value = ObjetoDyP.Descripcion;

                    //Ejecutar comando
                    sqlCmd.ExecuteNonQuery();

                }

                sqlTran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                EscribeLog("Error: --> " + ex.Message);
                if (sqlTran != null)
                {
                    sqlTran.Rollback();
                }

                return false;
            }

            finally
            {

                if (sqlCon.State == ConnectionState.Open)
                { sqlCon.Close(); }

                Console.WriteLine("Fin del proceso Actualiza Periodicidades");

            }
        }

        static bool ActualizaPeriodosDyP()
        {

            SqlConnection sqlCon = new SqlConnection(StringConexion);
            SqlTransaction sqlTran = null;
            try
            {
                Console.WriteLine("Inicia proceso Actualiza Periodos");

                //Declaracion de objetos referenciados
                CatalogosDyP.ConsultaCatalogosClient CatalogosDyP = new CatalogosDyP.ConsultaCatalogosClient();
                CatalogosDyP.CatalogoFiltro filtro = new ActualizaCatalogosDyP.CatalogosDyP.CatalogoFiltro();
                CatalogosDyP.Periodo[] ObjetosDyP;

                //Asignar valor para el filtro
                filtro.IdOrigen = IdOrigen;
                ObjetosDyP = CatalogosDyP.ConsultarPeriodos(filtro);

                //Declarar objetos de conexion a BD
                SqlCommand sqlCmd = new SqlCommand();

                //Abrir conexion
                sqlCon.Open();

                //Comienza la transaccion
                sqlTran = sqlCon.BeginTransaction();

                //Setear valores de conexion y transaccion al comando
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = sqlTran;

                //Setear comando para desactivar todos los conceptos
                sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCmd.CommandText = "spUpdPeriodosDesactivar";


                //Ejecutar comando para desactivar todos los conceptos
                sqlCmd.ExecuteNonQuery();

                //Especificar sp que se va a usar
                sqlCmd.CommandText = "spUpdPeriodoDyP";

                //Actualizar los valores de la BD a partir de los que vienen en el servicio
                foreach (ActualizaCatalogosDyP.CatalogosDyP.Periodo ObjetoDyP in ObjetosDyP)
                {
                    //Agregar parametros
                    sqlCmd.Parameters.Clear();

                    sqlCmd.Parameters.Add("@IdPeriodoDyP", SqlDbType.VarChar, 10).Value = ObjetoDyP.IdPeriodo;
                    sqlCmd.Parameters.Add("@Descripcion", SqlDbType.VarChar, 40).Value = ObjetoDyP.Descripcion;
                    sqlCmd.Parameters.Add("@IdPeriodicidadDyP", SqlDbType.VarChar, 10).Value = ObjetoDyP.Periodicidad.IdPeriodicidad;
                    sqlCmd.Parameters.Add("@IdMesInicial", SqlDbType.Int).Value = ObjetoDyP.IdMesInicial;
                    sqlCmd.Parameters.Add("@MesInicial", SqlDbType.VarChar, 20).Value = ObjetoDyP.MesInicial;
                    sqlCmd.Parameters.Add("@IdMesFinal", SqlDbType.Int).Value = ObjetoDyP.IdMesFinal;
                    sqlCmd.Parameters.Add("@MesFinal", SqlDbType.VarChar, 20).Value = ObjetoDyP.MesFinal;
                    sqlCmd.Parameters.Add("@Activo", SqlDbType.Bit).Value = 1;

                    //Ejecutar comando
                    sqlCmd.ExecuteNonQuery();

                }

                sqlTran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                EscribeLog("Error: --> " + ex.Message);
                if (sqlTran != null)
                {
                    sqlTran.Rollback();
                }

                return false;
            }

            finally
            {

                if (sqlCon.State == ConnectionState.Open)
                { sqlCon.Close(); }

                Console.WriteLine("Fin del proceso Actualiza Periodos");

            }
        }

        static void EscribeLog(string Mensaje)
        {
            try
            {

                StreamWriter writer = new StreamWriter(LogPath,true);



                writer.WriteLine(Mensaje);

                writer.Close();


                /*
                //byte[] fileBytes = File.ReadAllBytes(LogPath);
                
                //FileStream file = File.OpenWrite(LogPath);
                

                //byte[] msgBytes;

                

                msgBytes = ASCIIEncoding.ASCII.GetBytes(Mensaje);

                file.Write(msgBytes,0,Mensaje.Length);

                file.Close();
                */

            }
            catch(Exception ex)
            { throw (ex);}



        }

    }
}