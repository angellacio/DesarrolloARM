
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Seguridad:Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Seguridad.AccesoIdc:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sat.CreditosFiscales.Comunes.Entidades.AccesoLogEventos;
using Sat.CreditosFiscales.Comunes.Entidades.Catalogos;
using Sat.CreditosFiscales.Comunes.Entidades.CodigosError;
using Sat.CreditosFiscales.Comunes.Entidades.Seguridad;
using Sat.CreditosFiscales.Datos.AccesoDatos.Catalogos;
using Sat.CreditosFiscales.Procesamiento.LogicaNegocio.AccesoLogEventos;
using Sat.CreditosFiscales.Procesamiento.LogicaNegocio.WsIdcInterno;

namespace Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Seguridad
{
    public class AccesoIdc
    {
        #region Constantes
        /// <summary>
        /// Seccion Identificación del serivio Idc
        /// </summary>
        private const string IDENTIFICACION = "Identificacion";

        /// <summary>
        /// Seccion Ubicación del serivio Idc
        /// </summary>
        private const string UBICACION = "Ubicacion";

        /// <summary>
        /// Seccion Obligaciones del serivio Idc
        /// </summary>
        private const string OBLIGACIONES = "Obligaciones";

        /// <summary>
        /// Seccion Regimenes del serivio Idc
        /// </summary>
        private const string REGIMENES = "Regimenes";

        /// <summary>
        /// Seccion Roles del serivio Idc
        /// </summary>
        private const string ROLES = "Roles";

        /// <summary>
        /// Seccion Actividades del serivio Idc
        /// </summary>
        private const string ACTIVIDADES = "Actividades";

        /// <summary>
        /// Seccion Representantes legales del serivio Idc
        /// </summary>
        private const string REP_LEGALES = "Rep_legales";

        /// <summary>
        /// Seccion Sucursales del serivio Idc
        /// </summary>
        private const string SUCURSALES = "Sucursales";

        /// <summary>
        /// Seccion Grupo empresa del serivio Idc
        /// </summary>
        private const string GRUPOEMPRESA = "GrupoEmpresa";

        /// <summary>
        /// Seccion Rfc relación del serivio Idc
        /// </summary>
        private const string RFCRELACION = "rfcrelacion";

        /// <summary>
        /// Seccion Ubicación especifica del serivio Idc
        /// </summary>
        private const string UBICACIONESPECIFICA = "UbicacionEspecifica";

        /// <summary>
        /// Seccion Datos complementarios del serivio Idc
        /// </summary>
        private const string DATOSCOMPLEMENTARIOS = "datosComplementarios";

        private const string PERSONAFISICA = "F";
        #endregion

        #region Campos
        private string[] secciones;
        private string[] seccionesHistoricas;
        private string[] t_relacion = new string[0];
        private string rfc_Vigente;
        private string rfc_Solicitado;
        private string boid;
        private string personid;
        private identificacion identificacion;
        private ubicacion ubicacion;
        private obligacion[] obligaciones;
        private regimen[] regimenes;
        private rol[] roles;
        private actividad[] actividades;
        private rep_legal[] rep_Legales;
        private sucursal[] sucursales;
        private mensajes mensajes;
        private grupoEmpresa[] grupoEmpresas;
        private relacion[] relaciones;
        private ubicacion[] ubicacionEspecifica;
        private rfcHistorico[] rFCHistoricos;
        private patente[] patente;
        private sector[] sector;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RfcContribuyente"></param>
        /// <returns></returns>
        public InfoContribuyente ObtenerInfoContribuyente(string RfcContribuyente)
        {
            try
            {
                List<ApplicationSetting> parametros = new List<ApplicationSetting>();


                DalApplicationSettings dal = new DalApplicationSettings();
                parametros = dal.RecuperaGrupoConfiguracion("Seguridad:IDC");

                if (parametros.Count.Equals(2))
                {
                    InfoContribuyente infoContribuyente = new InfoContribuyente();
                    string[] seccionesIdc = new string[3];
                    string[] seccionesHistoricasIdc = new string[3];


                    seccionesIdc[0] = ROLES;
                    seccionesIdc[1] = IDENTIFICACION;
                    seccionesIdc[2] = UBICACION;
                    seccionesHistoricasIdc[0] = IDENTIFICACION;

                    IdCInternoClient clientIdcInterno = new IdCInternoClient();

                    string strResultado = clientIdcInterno.getIdCInterno(
                           RfcContribuyente,
                           null,
                           null,
                           seccionesIdc,
                           seccionesHistoricasIdc,
                           null,
                           parametros[0].SettingValue.ToString(),
                           parametros[1].SettingValue.ToString(),
                           this.t_relacion,
                           null,
                           string.Empty,
                           out this.rfc_Vigente,
                           out this.rfc_Solicitado,
                           out this.boid,
                           out this.personid,
                           out this.identificacion,
                           out this.ubicacion,
                           out this.obligaciones,
                           out this.regimenes,
                           out this.roles,
                           out this.actividades,
                           out this.rep_Legales,
                           out this.sucursales,
                           out this.mensajes,
                           out this.grupoEmpresas,
                           out this.relaciones,
                           out this.ubicacionEspecifica,
                           out this.rFCHistoricos,
                           out this.patente,
                           out this.sector);
                    clientIdcInterno.Close();

                    ValidaResultadosIDC();

                    AsignaNombreTipoPersona(infoContribuyente);

                    AsignaAlr(infoContribuyente);


                    infoContribuyente.RfcSolicitado = this.rfc_Solicitado;
                    infoContribuyente.RfcVigente = this.rfc_Vigente;
                    infoContribuyente.Rfcs = ObtieneListaRfcs();
                    infoContribuyente.BoId = this.boid;
                    infoContribuyente.PersonId = this.personid;

                    return infoContribuyente;
                }
                else
                {
                    var err = new Exception("No se pudo obtener los parametros de conexion a IDC");
                    string ticket = LogEventos.EscribirEntradaLog((int)EnumErroresIDC.ErrorNoExisteParametroIdc, err);
                    throw new ExcepcionTipificada(err.Message, err, ticket);

                }
            }
            catch (ExcepcionTipificada err)
            {
                throw err;
            }
            catch (Exception err)
            {
                string ticket = LogEventos.EscribirEntradaLog((int)EnumErroresIDC.ErrorNoExisteRazonSocialoNombre, err);
                throw new ExcepcionTipificada(err.Message, err, ticket);
            }
        }

        private List<string> ObtieneListaRfcs()
        {
            List<string> rfcs = new List<string>();
            rfcs.Add(this.rfc_Vigente);
            if (this.rFCHistoricos != null)
            {
                foreach (rfcHistorico rfcH in this.rFCHistoricos)
                {
                    if (rfcH.rfc_anterior != null)
                    {
                        foreach (string rfcAnterior in rfcH.rfc_anterior)
                        {
                            rfcs.Add(rfcAnterior);
                        }
                    }
                }
            }

            return rfcs.Distinct().ToList();
        }

        private void AsignaAlr(InfoContribuyente infoContribuyente)
        {
            if (this.ubicacion != null)
            {
                infoContribuyente.IdALR = int.Parse(this.ubicacion.c_ALR);
            }
            else
            {
                Exception err = new Exception("El RFC no tiene una ALR asociada");
                string ticket = LogEventos.EscribirEntradaLog((int)EnumErroresIDC.ErrorNoExisteALR, err);
                throw new ExcepcionTipificada(err.Message, err, ticket);
            }
        }

        private void AsignaNombreTipoPersona(InfoContribuyente infoContribuyente)
        {
            ////Se forma el nombre o razon social con base al tipo de persona físcal.
            infoContribuyente.PersonaFisica = identificacion.t_persona.Equals(PERSONAFISICA);

            if (infoContribuyente.PersonaFisica)
            {
                infoContribuyente.Nombre = this.identificacion.Nombre;
                infoContribuyente.ApellidoPaterno = this.identificacion.Ap_Paterno;
                infoContribuyente.ApellidoMaterno = this.identificacion.Ap_Materno;
            }
            else
            {
                if (this.identificacion.t_Sociedad.ToUpper() == "SIN TIPO DE SOCIEDAD")
                    infoContribuyente.RazonSocial = this.identificacion.Razon_Soc;
                else
                    infoContribuyente.RazonSocial = this.identificacion.Razon_Soc + " " + this.identificacion.t_Sociedad;
            }

            ////Si no tiene razón social o nombre completo es un error
            if (string.IsNullOrEmpty(infoContribuyente.NombreCompleto) && string.IsNullOrEmpty(infoContribuyente.RazonSocial))
            {
                Exception err = new Exception("El RFC no cuenta razón social o nombre");
                string ticket = LogEventos.EscribirEntradaLog((int)EnumErroresIDC.ErrorNoExisteRazonSocialoNombre, err);
                throw new ExcepcionTipificada(err.Message, err, ticket);
            }
        }

        private void ValidaResultadosIDC()
        {
            if (this.identificacion == null)
            {
                Exception err = new Exception("El RFC no cuenta con datos de IDC");
                string ticket = LogEventos.EscribirEntradaLog((int)EnumErroresIDC.ErrorNoExisteInformacionIDC, err);
                throw new ExcepcionTipificada(err.Message, err, ticket);
            }
            if (this.roles == null)
            {
                Exception err = new Exception("El RFC no tiene asociados roles de IDC");
                string ticket = LogEventos.EscribirEntradaLog((int)EnumErroresIDC.ErrorNoExisteInformacionIDC, err);
                throw new ExcepcionTipificada(err.Message, err, ticket);
            }
            if (string.IsNullOrEmpty(this.boid.Trim()))
            {
                Exception err = new Exception("El RFC no tiene asociados roles de IDC");
                string ticket = LogEventos.EscribirEntradaLog((int)EnumErroresIDC.ErrorNoExisteInformacionIDC, err);
                throw new ExcepcionTipificada(err.Message, err, ticket);
            }
        }
    }
}
