using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SAT.DyP.Util.Data;
using System.Data;
using RecepcionIDEWEB.Negocio.Contratos;

namespace RecepcionIDEWEB.Negocio.AccesoDatos
{
    public class Datos
    {
        public const int idError = 6382;
        public RecSalida generarFolio(RecEntrada declaracion)
        {
            RecSalida RS = null;
            DataAccessHelper c = null;
            IDataParameter[] parametros = null;
            try
            {
                parametros = new IDataParameter[12];
                parametros[0] = new SqlParameter("@IdMedioRecepcion", declaracion.IdMedioRecepcion);
                parametros[1] = new SqlParameter("@IdEntidadReceptora", declaracion.IdEntidadReceptora);
                parametros[2] = new SqlParameter("@RfcAutenticacion", declaracion.RfcAutenticacion);
                parametros[3] = new SqlParameter("@RfcContribuyente", declaracion.RfcContribuyente);
                parametros[4] = new SqlParameter("@DireccionIP", declaracion.DireccionIP);
                parametros[5] = new SqlParameter("@NombreArchivo", declaracion.NombreArchivo);
                parametros[6] = new SqlParameter("@TamañoArchivo", declaracion.TamañoArchivo);
                parametros[7] = new SqlParameter("@Formato", declaracion.Formato);
                parametros[8] = new SqlParameter("@Materia", declaracion.Materia);
                parametros[9] = new SqlParameter("@EsNormal", declaracion.EsNormal);
                parametros[10] = new SqlParameter("@EsAnual", declaracion.EsAnual);
                parametros[11] = new SqlParameter("@ArchivoFisico", declaracion.ArchivoFisico);

                RS = new RecSalida();

                c = this.abrirConexion();

                using (IDataReader reader = c.ExecuteReaderStoreProcedure("RecepcionArchivo", parametros))
                {
                    if (reader.Read())
                    {
                        RS.Folio = reader.GetInt32(0);
                        RS.Rfc = reader.GetString(1);
                        RS.Nombre = reader.GetString(2);
                        RS.Tamano = reader.GetInt32(3);
                        RS.Fecha = reader.GetDateTime(4);
                    }
                }
            }
            catch (Exception e)
            {
                SAT.DyP.Util.Logging.EventLogHelper.WriteErrorEntry(e, idError);
            }
            finally
            {
                if (c != null) c.CloseConnection();
            }
            return RS;
        }
        //public RecSalida generarFolioModulo(RecEntrada declaracion)
        //{
        //    RecepcionIDEWEB.Negocio.Contratos.RecSalida RS = new RecepcionIDEWEB.Negocio.Contratos.RecSalida();
        //    DataAccessHelper c = null;
        //    IDataParameter[] parametros = new IDataParameter[12];
        //    string sql = "RecepcionArchivo";

        //    int ENTIDAD = declaracion.IdEntidadReceptora;
        //    if (declaracion.IdMedioRecepcion != 3)
        //    {
        //        DataAccessHelper sp = null;
        //        IDataParameter[] parSPED = new IDataParameter[1];
        //        parSPED[0] = new SqlParameter("@Usuario", declaracion.RfcAutenticacion);
        //        string SQ2 = "SCADENET_ObtenerEntidadReceptora";

        //        sp = this.abrirConexionSPED();
        //        using (IDataReader reader2 = sp.ExecuteReaderStoreProcedure(SQ2, parSPED))
        //        {
        //            if (reader2.Read())
        //            {
        //                ENTIDAD = reader2.GetInt16(0);

        //            }
        //        }
        //        sp.CloseConnection();

        //    }

        //    parametros[0] = new SqlParameter("@IdMedioRecepcion", declaracion.IdMedioRecepcion);
        //    parametros[1] = new SqlParameter("@IdEntidadReceptora", ENTIDAD);
        //    parametros[2] = new SqlParameter("@RfcAutenticacion", declaracion.RfcAutenticacion);
        //    parametros[3] = new SqlParameter("@RfcContribuyente", declaracion.RfcContribuyente);
        //    parametros[4] = new SqlParameter("@DireccionIP", declaracion.DireccionIP);
        //    parametros[5] = new SqlParameter("@NombreArchivo", declaracion.NombreArchivo);
        //    parametros[6] = new SqlParameter("@TamañoArchivo", declaracion.TamañoArchivo);
        //    parametros[7] = new SqlParameter("@Formato", declaracion.Formato);
        //    parametros[8] = new SqlParameter("@Materia", declaracion.Materia);
        //    parametros[9] = new SqlParameter("@EsNormal", declaracion.EsNormal);
        //    parametros[10] = new SqlParameter("@EsAnual", declaracion.EsAnual);
        //    parametros[11] = new SqlParameter("@ArchivoFisico", declaracion.ArchivoFisico);
        //    try
        //    {
        //        c = this.abrirConexion();

        //        using (IDataReader reader = c.ExecuteReaderStoreProcedure(sql, parametros))
        //        {
        //            if (reader.Read())
        //            {
        //                RS.Folio = reader.GetInt32(0);
        //                RS.Rfc = reader.GetString(1);
        //                RS.Nombre = reader.GetString(2);
        //                RS.Tamano = reader.GetInt32(3);
        //                RS.Fecha = reader.GetDateTime(4);
        //            }

        //            ServicioValidacion.ValidacionClient VC = new ServicioValidacion.ValidacionClient("BasicHttpBinding_IValidacion");
        //            VC.BegininiciarValidacion(RS.Folio, null, null);

        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        SAT.DyP.Util.Logging.EventLogHelper.WriteErrorEntry(e, idError);
        //        return null;
        //    }
        //    finally
        //    {
        //        if (c != null)
        //            c.CloseConnection();

        //    }


        //    return RS;
        //}

        public RecVerifica VerificarExistArch(string archivo)
        {

            RecVerifica RV = new RecVerifica();
            DataAccessHelper c = null;
            IDataParameter[] parametros = new IDataParameter[1];
            string sql = "ExisteArchivo";
            parametros[0] = new SqlParameter("@NombreArchivo", archivo);

            try
            {
                c = this.abrirConexion();

                using (IDataReader reader = c.ExecuteReaderStoreProcedure(sql, parametros))
                {
                    if (reader.Read())
                    {

                        RV.Folio = reader.GetInt32(0);
                        RV.Existencia = reader.GetInt32(1);
                        RV.Fecha = reader.GetDateTime(2);
                    }
                }
            }
            catch (Exception e)
            {
                SAT.DyP.Util.Logging.EventLogHelper.WriteErrorEntry(e, idError);
                return null;
            }
            finally
            {
                if (c != null)
                    c.CloseConnection();

            }

            return RV;
        }

        public DataAccessHelper abrirConexion()
        {
            DataAccessHelper con = new DataAccessHelper("Sat.DyP.IDE::BaseDatos", DataProviderType.SqlServer);
            return con;
        }

        public DataAccessHelper abrirConexionSPED()
        {
            DataAccessHelper con = new DataAccessHelper("SAT.DyP.Negocio.Comun::BD_SPED", DataProviderType.SqlServer);
            return con;
        }

        public List<Mensaje> GetMensajes(int idTipoMensaje)
        {
            List<Mensaje> listMsg = new List<Mensaje>();
            DataAccessHelper c = null;
            IDataParameter[] parametros = new IDataParameter[1];
            string sql = "GetMensajes";
            parametros[0] = new SqlParameter("@IdTipoMensaje", idTipoMensaje);

            try
            {
                c = this.abrirConexion();
                using (IDataReader reader = c.ExecuteReaderStoreProcedure(sql, parametros))
                {
                    while (reader.Read())
                    {
                        Mensaje msg = new Mensaje();
                        msg.IdTipoMensaje = reader.GetInt32(0);
                        msg.IdMensaje = reader.GetInt32(1);
                        msg.Descripcion = reader.GetString(2);
                        listMsg.Add(msg);
                    }
                }
            }
            catch (Exception e)
            {
                SAT.DyP.Util.Logging.EventLogHelper.WriteErrorEntry(e, idError);
                return null;
            }
            finally
            {
                if (c != null)
                    c.CloseConnection();

            }

            return listMsg;
        }
    }



}