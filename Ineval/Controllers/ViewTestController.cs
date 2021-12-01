using AutoMapper;
using ClosedXML.Excel;
using Ineval.BO;
using Ineval.DAL;
using Ineval.Dto;
using Ineval.Dto.Dto.Procesos;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Ineval.Common.Helpers;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace Ineval.Controllers
{
    [Authorize(Roles = "Administrador, Responsable_Unidad, Analista, Visitante")]
    public class ViewTestController : Controller
    {
        SwmContext db = new SwmContext();
        public ActionResult Register()
            => PartialView();
        public ActionResult Register2()
            => PartialView();
        public ActionResult Parametros()
            => PartialView();

        public ActionResult GenerarExcelDatosSustentantes()
            => PartialView();

        public ActionResult GenerarExcelDatosPersonalTerritorio()
            => PartialView();

        public ActionResult SubirDatosSustentantes()
            => PartialView();

        public ActionResult SubirDatosPersonalTerritorio()
            => PartialView();
        public ActionResult VistaMapas()
            => PartialView();
        public ActionResult ViewProcesoAsignacion()
            => PartialView();

        public ActionResult PorArchivo()
            => PartialView();

        public ActionResult GenerarExcelDatosInstituciones()
            => PartialView();

        public ActionResult SubirDatosInstituciones()
            => PartialView();

        public async Task<ActionResult> GetFiltros(Guid? AsignacionId)
        {
            try
            {
                DatosFiltrosService entityService = new DatosFiltrosService();
                DatosFiltros result = await entityService.GetAll().Where(x => x.AsignacionId == AsignacionId).SingleOrDefaultAsync();
                DatosFiltrosViewModel resultDTO = Mapper.Map<DatosFiltrosViewModel>(result);

                return Json(new { data = resultDTO }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                Dispose();
            }
        }

        #region ListasGenericas
        public class PorAmie
        {
            public string amie { get; set; }
            public string nombre_institucion { get; set; }
            public string jornada_sustentante { get; set; }
            public string saber { get; set; }
            public string coordenada_x { get; set; }
            public string coordenada_y { get; set; }
            public int NumeroSustentates { get; set; }
        }

        public class PorSaber
        {
            public string amie { get; set; }
            public string nombre_institucion { get; set; }
            public string saber { get; set; }
            public string coordenada_x { get; set; }
            public string coordenada_y { get; set; }
            public int NumeroSustentates { get; set; }
        }

        public class PorJornada
        {
            public string amie { get; set; }
            public string nombre_institucion { get; set; }
            public string jornada_sustentante { get; set; }
            public string coordenada_x { get; set; }
            public string coordenada_y { get; set; }
            public int NumeroSustentates { get; set; }
        }

        public class PorGrado
        {
            public string amie { get; set; }
            public string nombre_institucion { get; set; }
            public string jornada_sustentante { get; set; }
            public string grado { get; set; }
            public string coordenada_x { get; set; }
            public string coordenada_y { get; set; }
            public int NumeroSustentates { get; set; }
        }
        public class ConteoDatos
        {
            public string Amie { get; set; }
            public string NombreInstitucion { get; set; }
            public int NumeroSustentates { get; set; }
            public int NumeroLaboratorios { get; set; }
            public int NumeroSessiones { get; set; }
            public int NumeroDiasEvaluar { get; set; }

        }

        public class Posiciones
        {
            public string NombreProvincia { get; set; }
            public double lat { get; set; }
            public double lng { get; set; }
        }

        public class PorPorvincia
        {
            public string id_provincia { get; set; }
            public string provincia { get; set; }
            public int NumeroSustentates { get; set; }
        }

        public class PorCanton
        {

            public string id_provincia { get; set; }
            public string provincia { get; set; }
            public string canton_id { get; set; }
            public string canton { get; set; }
            public int NumeroSustentates { get; set; }
        }

        public class PorCantonLatLng
        {
            public string id_provincia { get; set; }
            public string provincia { get; set; }
            public string canton_id { get; set; }
            public string canton { get; set; }
            public string Lat { get; set; }
            public string Lng { get; set; }
            public int NumeroSustentates { get; set; }
        }

        public class PorParroquias
        {
            public string id_provincia { get; set; }
            public string provincia { get; set; }
            public string canton_id { get; set; }
            public string canton { get; set; }
            public string id_parroquia { get; set; }
            public string parroquia { get; set; }
            public string coordenada_x { get; set; }
            public string coordenada_y { get; set; }
            public int NumeroSustentates { get; set; }
        }

        public class PorParroquiasLatLng
        {
            public string id_provincia { get; set; }
            public string provincia { get; set; }
            public string canton_id { get; set; }
            public string canton { get; set; }
            public string id_parroquia { get; set; }
            public string parroquia { get; set; }
            public string Lat { get; set; }
            public string Lng { get; set; }
            public int NumeroSustentates { get; set; }
        }


        #endregion

        public async Task<ActionResult> GetUsers()
        {
            DatosMapboxAPIKEYService service = new DatosMapboxAPIKEYService();

            List<DatosMapboxAPIKEY> result = new List<DatosMapboxAPIKEY>();
            List<DatosMapboxAPIKEYViewModel> resultDTO = new List<DatosMapboxAPIKEYViewModel>();

            string apikey = "";

            try
            {
                result = await service.GetAll().ToListAsync();
                resultDTO = Mapper.Map<List<DatosMapboxAPIKEYViewModel>>(result);

                foreach (var item in resultDTO)
                {
                    int cont = 1;
                    int x = item.NumeroMininoConsulta - item.NumeroUsadasConsultas;
                    if (x > 0)
                    {
                        apikey = item.APIKEY;
                        DatosMapboxAPIKEY resultApiKey = await db.DatosMapboxAPIKEY.Where(xx => xx.Id == item.Id).FirstOrDefaultAsync();

                        resultApiKey.NumeroUsadasConsultas = item.NumeroUsadasConsultas + cont;

                        var entry = db.Entry(resultApiKey);
                        entry.State = EntityState.Modified;
                        await db.SaveChangesAsync();
                        break;
                    }
                }

                return Json(new { Data = apikey }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { Data = apikey }, JsonRequestBehavior.AllowGet);
            }
            finally
            {
                service.Dispose();
            }
        }

        public class datosJson
        {
            public string sesion { get; set; }
            public string fecha { get; set; }
            public string hora { get; set; }

        }

        public async Task<ActionResult> Filtro(Guid? Id, int? Parametro1, string Parametro2, string Parametro3)
        {
            var userId = User.Identity.GetUserId();
            UsuarioService usuarioService = new UsuarioService();
            var usuario = usuarioService.ObtenerPorApplicationUserId(userId);

            List<DatosMapboxAPIKEY> datosMapboxAPIKEYs = new List<DatosMapboxAPIKEY>();

            datosMapboxAPIKEYs = await db.DatosMapboxAPIKEY.ToListAsync();


            List<DatosTemporales> resultTemporales = new List<DatosTemporales>();
            List<DatosTemporalesViewModel> datosTemporalesDTO = new List<DatosTemporalesViewModel>();
            resultTemporales = await db.DatosTemporales.Where(x => x.AsignacionId == Id).ToListAsync();
            datosTemporalesDTO = Mapper.Map<List<DatosTemporalesViewModel>>(resultTemporales);

            /*DATOSSEDES*/
            List<DatosSedes> ListaSedes = new List<DatosSedes>();
            /*DATOSSEDES*/

            //ELIMNACION DE SEDES
            int datoseliminacion = await db.DatosSedes.AsNoTracking().Where(x => x.AsignacionId == Id).CountAsync();
            if (datoseliminacion > 0)
            {
                var registroseliminados = db.Database.SqlQuery<List<int>>("exec sp_DeleteSedes @AsignacionId", new SqlParameter("AsignacionId", Id)).ToList();
            }
            //FIN ELIMANCION DE SEDES


            DatosFiltros datosFiltros = await db.DatosFiltros.Include(a => a.Asignacion).Where(x => x.AsignacionId == Id).FirstOrDefaultAsync();
            if (datosFiltros != null)
            {
                datosFiltros.Filtro1 = Parametro1 ?? null;
                datosFiltros.Filtro2 = Parametro2 ?? null;
                datosFiltros.Filtro3 = Parametro3 ?? null;
                datosFiltros.Filtro4 = null;
                datosFiltros.Filtro5 = null;
                datosFiltros.FechaModificacion = DateTime.Now;

                db.Entry(datosFiltros).State = EntityState.Modified;
                db.Entry(datosFiltros).Property(x => x.FechaCreacion).IsModified = false;
                db.Entry(datosFiltros).Property(x => x.FechaEliminacion).IsModified = false;

                await db.SaveChangesAsync();
            }
            else
            {
                DatosFiltros datosFiltros1 = new DatosFiltros
                {
                    AsignacionId = Id,
                    Filtro1 = Parametro1 ?? null,
                    Filtro2 = Parametro2 ?? null,
                    Filtro3 = Parametro3 ?? null,
                    Filtro4 = null,
                    Filtro5 = null
                };
                db.DatosFiltros.Add(datosFiltros1);
                await db.SaveChangesAsync();
            }


            if (datosTemporalesDTO.Count() > 0)
            {
                if (Id.HasValue)
                {
                    ParametrosIniciales parametrosIniciales = await db.ParametrosIniciales.Where(x => x.AsignacionId == Id).FirstOrDefaultAsync();
                    ParametrosInicialesViewModel parametrosInicialesDTO = Mapper.Map<ParametrosInicialesViewModel>(parametrosIniciales);

                    List<datosJson> horariossessiones = new List<datosJson>();
                    JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();                    
                    horariossessiones = jsonSerializer.Deserialize<List<datosJson>>(parametrosInicialesDTO.HorariosSesion);

                    List<DatosInstituciones> datosInstituciones = new List<DatosInstituciones>();
                    List<DatosInstitucionesViewModel> resultDTO = new List<DatosInstitucionesViewModel>();
                    datosInstituciones = await db.DatosInstituciones.Where(x => x.AsignacionId == Id).ToListAsync();
                    resultDTO = Mapper.Map<List<DatosInstitucionesViewModel>>(datosInstituciones);



                    List<Posiciones> pos = new List<Posiciones>();

                    //// FILTRO AMIE NOMBRE_INSTITUCION
                    if (Parametro1.HasValue && Parametro1.Value == 1)
                    {
                        string val_parametros2 = Parametro2 ?? "0";
                        List<PorAmie> datosporAmie = db.Database.SqlQuery<PorAmie>("exec sp_TipoAsignaciones @AsignacionId, @Param1, @Param2, @Param3, @Param4, @Param5", new SqlParameter("AsignacionId", Id),
                           new SqlParameter("Param1", Parametro1.Value), new SqlParameter("Param2", val_parametros2), new SqlParameter("Param3", DBNull.Value),
                           new SqlParameter("Param4", DBNull.Value), new SqlParameter("Param5", DBNull.Value)).ToList();

                        foreach (var item in datosporAmie)
                        {
                            int totalSuste = 0;
                            int totalLabo = 0;
                            int totalSession = 0;
                            int subtotalSession = 0;
                            int subtotalLabo = 0;
                            int rest = 0;
                            int xy = 0;
                            totalSuste = datosTemporalesDTO.Where(x => x.amie == item.amie).Count();
                            totalSession = (int)Math.Round(Double.Parse(totalSuste.ToString()) / Double.Parse(parametrosInicialesDTO.NumeroEquipos.Value.ToString()), 0);

                            //int calculosession = 0;
                            //if(parametrosInicialesDTO.NumeroDiasEvaluar.HasValue && parametrosInicialesDTO.NumeroDiasEvaluar.Value == 1)
                            //{
                            //    calculosession = parametrosInicialesDTO.NumeroDiasEvaluar.Value*parametrosInicialesDTO.NumerosSesiones.Value;
                            //}
                            //else
                            //{
                            //    int var = parametrosInicialesDTO.NumeroDiasEvaluar.Value * parametrosInicialesDTO.NumerosSesiones.Value;
                            //    if (var == parametrosInicialesDTO.NumerosSesiones.Value)
                            //    {
                            //        calculosession = parametrosInicialesDTO.NumerosSesiones.Value;
                            //    }
                            //    else
                            //    {
                            //        calculosession = var;
                            //    }
                            //}


                            totalLabo = (int)Math.Round(Double.Parse(totalSession.ToString()) / Double.Parse(parametrosInicialesDTO.NumerosSesiones.ToString()), 0);

                            if (totalLabo == 0)
                            {
                                totalLabo += 1;
                            }

                            if (totalSession == 0)
                            {
                                totalSession += 1;
                            }

                            subtotalLabo = totalLabo == 0 ? 1 : totalLabo;
                            subtotalSession = (totalSession / totalLabo);

                            if (subtotalSession == parametrosInicialesDTO.NumerosSesiones.Value) //igual al numero de sessiones
                            {
                                xy = subtotalSession * subtotalLabo;
                                rest = totalSuste - (xy * parametrosInicialesDTO.NumeroEquipos.Value);
                                if (rest > 0)
                                {
                                    subtotalLabo += 1;
                                }
                            }
                            else if (subtotalSession > parametrosInicialesDTO.NumerosSesiones.Value) //mayor al numero de sessiones
                            {
                                subtotalSession = parametrosInicialesDTO.NumerosSesiones.Value;

                                xy = (subtotalSession * subtotalLabo) * parametrosInicialesDTO.NumeroEquipos.Value;

                                rest = totalSuste - xy;

                                while (rest > 0)
                                {
                                    subtotalLabo += 1;
                                    rest -= totalSuste;
                                }
                            }
                            else
                            {                                                                       //menor que el numero de sessiones
                                if (subtotalSession == 1)
                                {
                                    xy = subtotalSession * subtotalLabo;
                                    rest = totalSuste - (xy * parametrosInicialesDTO.NumeroEquipos.Value);
                                    if (rest > 0)
                                    {
                                        subtotalSession += 1;
                                    }
                                }
                                else
                                {
                                    xy = subtotalSession * subtotalLabo;
                                    rest = totalSuste - (xy * parametrosInicialesDTO.NumeroEquipos.Value);
                                    if (rest > 0)
                                    {
                                        subtotalSession += 1;
                                    }
                                }
                            }

                            DatosSedes datosSedes = new DatosSedes
                            {
                                AsignacionId = Id,
                                NumeroSession = subtotalSession,
                                NumeroLaboratorio = subtotalLabo,
                                Code = item.amie,
                                Description = item.nombre_institucion != null ? item.nombre_institucion : item.amie,
                                NumeroTotalSustentantes = totalSuste,
                                coordenada_lat = item.coordenada_x != null ? item.coordenada_x.Replace(",", ".") : "",//coordenadas.features.FirstOrDefault().center[0].ToString().Replace(',', '.'),
                                coordenada_lng = item.coordenada_y != null ? item.coordenada_y.Replace(",", ".") : "",//coordenadas.features.FirstOrDefault().center[1].ToString().Replace(',', '.')
                            };

                            //db.DatosSedes.Add(datosSedes);
                            insertMasiveDataSedes(datosSedes);
                            //await db.SaveChangesAsync();

                            List<DatosSedesAsignacion> datosSedesAsignacions = new List<DatosSedesAsignacion>();

                            List<DatosTemporalesViewModel> listanueva = datosTemporalesDTO.Where(x => x.amie == datosSedes.Code).ToList();

                            List<double> tomardatos = MetodosUtils.GetListOfRandomDoubles((subtotalLabo * subtotalSession), listanueva.Count(), 0, parametrosInicialesDTO.NumeroEquipos.Value);

                            tomardatos.Sort();
                            int datos = 0;

                            for (int i = 1; i <= subtotalLabo; i++)
                            {
                                for (int j = 1; j <= subtotalSession; j++)
                                {
                                    //int tomardatos = 1 * parametrosInicialesDTO.NumeroEquipos.Value;
                                    List<DatosTemporalesViewModel> listatem = listanueva.Take((int)tomardatos[datos]).ToList();
                                    foreach (var idsustentante in listatem)
                                    {
                                        datosSedesAsignacions.Add(new DatosSedesAsignacion
                                        {
                                            SedeId = datosSedes.Id,
                                            SessionId = "S" + j,
                                            LaboratorioId = datosSedes.Code + "_" + (i <= 9 ? ("0" + i.ToString()) : i.ToString()),
                                            SustentanteId = idsustentante.Id.Value
                                        });
                                        listanueva.RemoveAll(x => x.Id == idsustentante.Id);
                                    }
                                    datos++;
                                }
                            }

                            insertMasiveData(datosSedesAsignacions.ToList());
                            //await db.SaveChangesAsync();
                        }

                        bool status = await EnvioCorreos.SendAsync(userId, "Se creo con exito las sedes");

                        return Json(new { result = "", message = "Se creo con exito las sedes", status = "success" }, JsonRequestBehavior.AllowGet);
                    }
                    //PROVINCIA
                    else if (Parametro1.HasValue && Parametro1.Value == 2)
                    {
                        List<PorPorvincia> datosporProvicias = db.Database.SqlQuery<PorPorvincia>("exec sp_TipoAsignaciones @AsignacionId, @Param1, @Param2, @Param3, @Param4, @Param5", new SqlParameter("AsignacionId", Id),
                            new SqlParameter("Param1", Parametro1.Value), new SqlParameter("Param2", "0"), new SqlParameter("Param3", "0"), new SqlParameter("Param4", "0"), new SqlParameter("Param5", "0")).ToList<PorPorvincia>();

                        foreach (var item in datosporProvicias)
                        {
                            foreach (var itemApiKey in datosMapboxAPIKEYs)
                            {
                                int cont = 1;
                                int ConsutasMaximas = itemApiKey.NumeroMaximoConsulta;
                                int ConsultasUsadas = itemApiKey.NumeroUsadasConsultas;
                                int ConsultasMinimas = itemApiKey.NumeroMininoConsulta;

                                int ConsultasApi = ConsultasMinimas - ConsultasUsadas;

                                if (ConsultasApi == 0)
                                {
                                    //break;
                                }
                                else
                                {
                                    int totalSuste = 0;
                                    int totalLabo = 0;
                                    int totalSession = 0;
                                    int subtotalSession = 0;
                                    int subtotalLabo = 0;
                                    int rest = 0;
                                    int xy = 0;
                                    totalSuste = datosTemporalesDTO.Where(x => x.id_provincia == item.id_provincia).Count();
                                    totalSession = (int)Math.Round(Double.Parse(totalSuste.ToString()) / Double.Parse(parametrosInicialesDTO.NumeroEquipos.Value.ToString()), 0);
                                    totalLabo = (int)Math.Round(Double.Parse(totalSession.ToString()) / Double.Parse(parametrosInicialesDTO.NumerosSesiones.Value.ToString()), 0);

                                    if (totalLabo == 0)
                                    {
                                        totalLabo += 1;
                                    }

                                    if (totalSession == 0)
                                    {
                                        totalSession += 1;
                                    }

                                    subtotalLabo = totalLabo == 0 ? 1 : totalLabo;
                                    subtotalSession = (totalSession / totalLabo);

                                    if (subtotalSession == parametrosInicialesDTO.NumerosSesiones.Value) //igual al numero de sessiones
                                    {
                                        xy = subtotalSession * subtotalLabo;
                                        rest = totalSuste - (xy * parametrosInicialesDTO.NumeroEquipos.Value);
                                        if (rest > 0)
                                        {
                                            subtotalLabo += 1;
                                        }
                                    }
                                    else if (subtotalSession > parametrosInicialesDTO.NumerosSesiones.Value) //mayor al numero de sessiones
                                    {
                                        subtotalSession = parametrosInicialesDTO.NumerosSesiones.Value;

                                        xy = (subtotalSession * subtotalLabo) * parametrosInicialesDTO.NumeroEquipos.Value;

                                        rest = totalSuste - xy;

                                        while (rest > 0)
                                        {
                                            subtotalLabo += 1;
                                            rest -= totalSuste;
                                        }
                                    }
                                    else
                                    {                                                                       //menor que el numero de sessiones
                                        if (subtotalSession == 1)
                                        {
                                            xy = subtotalSession * subtotalLabo;
                                            rest = totalSuste - (xy * parametrosInicialesDTO.NumeroEquipos.Value);
                                            if (rest > 0)
                                            {
                                                subtotalSession += 1;
                                            }
                                        }
                                        else
                                        {
                                            xy = subtotalSession * subtotalLabo;
                                            rest = totalSuste - (xy * parametrosInicialesDTO.NumeroEquipos.Value);
                                            if (rest > 0)
                                            {
                                                subtotalSession += 1;
                                            }
                                        }
                                    }


                                    ApiPosicionGeografica.Root coordenadas = await ApiPosicionGeografica.GetByPosicionGeografica(item.provincia.Trim() + "," + "Ecuador", itemApiKey.APIKEY.Trim());

                                    DatosMapboxAPIKEY resultApiKey = await db.DatosMapboxAPIKEY.Where(x => x.Id == itemApiKey.Id).FirstOrDefaultAsync();

                                    resultApiKey.NumeroUsadasConsultas = itemApiKey.NumeroUsadasConsultas + cont;

                                    var entry = db.Entry(resultApiKey);
                                    entry.State = EntityState.Modified;
                                    await db.SaveChangesAsync();


                                    DatosSedes datosSedes = new DatosSedes
                                    {
                                        AsignacionId = Id,
                                        NumeroSession = subtotalSession,
                                        NumeroLaboratorio = subtotalLabo,
                                        Code = item.id_provincia,
                                        Description = item.provincia,
                                        NumeroTotalSustentantes = totalSuste,
                                        coordenada_lat = coordenadas != null ? coordenadas.features.FirstOrDefault().center[0].ToString().Replace(',', '.') : "",
                                        coordenada_lng = coordenadas != null ? coordenadas.features.FirstOrDefault().center[1].ToString().Replace(',', '.') : ""
                                    };


                                    insertMasiveDataSedes(datosSedes);
                                    //db.DatosSedes.Add(datosSedes);
                                    //await db.SaveChangesAsync();

                                    List<DatosSedesAsignacion> datosSedesAsignacions = new List<DatosSedesAsignacion>();
                                    List<DatosTemporalesViewModel> listanueva = datosTemporalesDTO.Where(x => x.id_provincia == datosSedes.Code).ToList();

                                    List<double> tomardatos = MetodosUtils.GetListOfRandomDoubles((subtotalLabo * subtotalSession), listanueva.Count(), 0, parametrosInicialesDTO.NumeroEquipos.Value);
                                    tomardatos.Sort();
                                    int datos = 0;
                                    for (int i = 1; i <= subtotalLabo; i++)
                                    {
                                        for (int j = 1; j <= subtotalSession; j++)
                                        {
                                            List<DatosTemporalesViewModel> listatem = listanueva.Take((int)tomardatos[datos]).ToList();
                                            foreach (var idsustentante in listatem)
                                            {
                                                datosSedesAsignacions.Add(new DatosSedesAsignacion
                                                {
                                                    SedeId = datosSedes.Id,
                                                    SessionId = "S" + j,
                                                    Dia = "1",
                                                    LaboratorioId = datosSedes.Code + "_" + (i <= 9 ? ("0" + i.ToString()) : i.ToString()),
                                                    SustentanteId = idsustentante.Id.Value
                                                });
                                                listanueva.RemoveAll(x => x.Id == idsustentante.Id);
                                            }
                                            datos++;
                                        }
                                    }
                                    insertMasiveData(datosSedesAsignacions.ToList());
                                    //await db.SaveChangesAsync();
                                    break;
                                }
                            }
                        }

                        bool status = await EnvioCorreos.SendAsync(userId, "Se creo con exito las sedes");
                        return Json(new { result = "", message = "Se creo con exito las sedes", status = "success" }, JsonRequestBehavior.AllowGet);

                    }
                    //CANTONES
                    else if (Parametro1.HasValue && Parametro1.Value == 3)
                    {
                        List<PorCanton> datosporCantones = db.Database.SqlQuery<PorCanton>("exec sp_TipoAsignaciones @AsignacionId, @Param1, @Param2, @Param3, @Param4, @Param5", new SqlParameter("AsignacionId", Id),
                            new SqlParameter("Param1", Parametro1.Value), new SqlParameter("Param2", "0"), new SqlParameter("Param3", "0"), new SqlParameter("Param4", "0"), new SqlParameter("Param5", "0")).ToList<PorCanton>();

                        if (parametrosInicialesDTO.tipo.Value == 1)//NIVEL NACIONAL
                        {
                            List<PorCantonLatLng> porCantonLatLng = new List<PorCantonLatLng>();
                            foreach (var item in datosporCantones)
                            {
                                foreach (var itemApiKey in datosMapboxAPIKEYs)
                                {
                                    int cont = 1;
                                    int ConsutasMaximas = itemApiKey.NumeroMaximoConsulta;
                                    int ConsultasUsadas = itemApiKey.NumeroUsadasConsultas;
                                    int ConsultasMinimas = itemApiKey.NumeroMininoConsulta;

                                    int ConsultasApi = ConsultasMinimas - ConsultasUsadas;

                                    if (ConsultasApi == 0)
                                    {
                                        //break;
                                    }
                                    else
                                    {
                                        ApiPosicionGeografica.Root coordenadas = await ApiPosicionGeografica.GetByPosicionGeografica(item.canton.Trim() + "," + item.provincia.Trim() + "," + "Ecuador", itemApiKey.APIKEY.Trim());

                                        DatosMapboxAPIKEY resultApiKey = await db.DatosMapboxAPIKEY.Where(x => x.Id == itemApiKey.Id).FirstOrDefaultAsync();

                                        resultApiKey.NumeroUsadasConsultas = itemApiKey.NumeroUsadasConsultas + cont;

                                        var entry = db.Entry(resultApiKey);
                                        entry.State = EntityState.Modified;
                                        await db.SaveChangesAsync();

                                        porCantonLatLng.Add(new PorCantonLatLng
                                        {
                                            id_provincia = item.id_provincia,
                                            provincia = item.provincia,
                                            canton_id = item.canton_id,
                                            canton = item.canton,
                                            NumeroSustentates = item.NumeroSustentates,
                                            Lat = coordenadas != null ? coordenadas.features.FirstOrDefault().center[0].ToString().Replace(',', '.') : "",
                                            Lng = coordenadas != null ? coordenadas.features.FirstOrDefault().center[1].ToString().Replace(',', '.') : ""
                                        });
                                        break;
                                    }
                                }
                            }

                            List<PorCantonLatLng> porCantonLatLng2 = porCantonLatLng.OrderByDescending(x => x.NumeroSustentates).ToList();
                            List<DatosSedes> porsedes = new List<DatosSedes>();
                            List<string> existen = new List<string>();
                            foreach (var itemporLatLng in porCantonLatLng.OrderByDescending(x => x.NumeroSustentates).ToList())
                            {
                                bool exist = existen.Where(x => x == itemporLatLng.canton_id).Any();
                                if (!exist)
                                {
                                    int count = 0;
                                    int NumeroSustentantes = 0;
                                    string agrupados = "";
                                    agrupados += itemporLatLng.canton_id + "_" + itemporLatLng.canton + ",";
                                    NumeroSustentantes += itemporLatLng.NumeroSustentates;
                                    foreach (var itemLatLng2 in porCantonLatLng2.ToList())
                                    {
                                        bool exist2 = existen.Where(x => x == itemLatLng2.canton_id).Any();
                                        if (!exist2)
                                        {
                                            if (itemporLatLng == itemLatLng2)
                                            {
                                                existen.Add(itemLatLng2.canton_id);
                                            }
                                            else
                                            {
                                                foreach (var itemApiKey2 in datosMapboxAPIKEYs)
                                                {
                                                    int cont2 = 1;
                                                    int ConsutasMaximas2 = itemApiKey2.NumeroMaximoConsulta;
                                                    int ConsultasUsadas2 = itemApiKey2.NumeroUsadasConsultas;
                                                    int ConsultasMinimas2 = itemApiKey2.NumeroMininoConsulta;

                                                    int ConsultasApi2 = ConsultasMinimas2 - ConsultasUsadas2;
                                                    if (ConsultasApi2 == 0)
                                                    {

                                                    }
                                                    else
                                                    {
                                                        ApiDriving.Root coordenadas = await ApiDriving.GetByDriving(itemporLatLng.Lat.Trim() + "," + itemporLatLng.Lng.Trim(), itemLatLng2.Lat.Trim() + "," + itemLatLng2.Lng.Trim(), itemApiKey2.APIKEY.Trim());
                                                        DatosMapboxAPIKEY resultApiKey = await db.DatosMapboxAPIKEY.Where(x => x.Id == itemApiKey2.Id).FirstOrDefaultAsync();

                                                        resultApiKey.NumeroUsadasConsultas = itemApiKey2.NumeroUsadasConsultas + cont2;

                                                        var entry = db.Entry(resultApiKey);
                                                        entry.State = EntityState.Modified;
                                                        await db.SaveChangesAsync();


                                                        if (coordenadas.code == "Ok")
                                                        {
                                                            if (parametrosInicialesDTO.TiempoViaje.HasValue)
                                                            {
                                                                int tiempo = (int)Math.Truncate(coordenadas.routes.FirstOrDefault().duration / 60);
                                                                int Distancia = (int)Math.Round((coordenadas.routes.FirstOrDefault().distance / 100), 0);
                                                                if (tiempo == 0 && Distancia == 0)
                                                                {

                                                                }
                                                                else if (tiempo <= parametrosInicialesDTO.TiempoViaje.Value)
                                                                {
                                                                    count += 1;
                                                                    agrupados += itemLatLng2.canton_id + "_" + itemLatLng2.canton + "_" + tiempo + "_" + Distancia + ",";
                                                                    NumeroSustentantes += itemLatLng2.NumeroSustentates;

                                                                    existen.Add(itemLatLng2.canton_id);
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                        }
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    porsedes.Add(new DatosSedes
                                    {
                                        AsignacionId = Id,
                                        NumeroSession = 0,
                                        NumeroLaboratorio = 0,
                                        Code = itemporLatLng.canton_id,
                                        Description = itemporLatLng.canton,
                                        NumeroTotalSustentantes = NumeroSustentantes,
                                        Agrupados = count == 0 ? agrupados + "_Cantón Origen Lejano" : agrupados,
                                        coordenada_lat = itemporLatLng.Lat,
                                        coordenada_lng = itemporLatLng.Lng
                                    });
                                    existen.Add(itemporLatLng.canton_id);
                                }

                            }


                            foreach (var item in porsedes)
                            {
                                List<string> agrupados = new List<string>();
                                if (!string.IsNullOrEmpty(item.Agrupados))
                                {
                                    string[] cantonagrospli = item.Agrupados.Split(',');
                                    foreach (var itemagrupados in cantonagrospli)
                                    {
                                        if (!string.IsNullOrEmpty(itemagrupados))
                                        {
                                            agrupados.Add(itemagrupados.Split('_')[0]);
                                        }
                                    }
                                }

                                int totalSuste = 0;
                                int totalLabo = 0;
                                int totalSession = 0;
                                int subtotalSession = 0;
                                int subtotalLabo = 0;
                                int rest = 0;
                                int xy = 0;
                                totalSuste = datosTemporalesDTO.Where(x => agrupados.Contains(x.canton_id)).Count();
                                totalSession = (int)Math.Round(Double.Parse(totalSuste.ToString()) / Double.Parse(parametrosInicialesDTO.NumeroEquipos.Value.ToString()), 0);
                                totalLabo = (int)Math.Round(Double.Parse(totalSession.ToString()) / Double.Parse(parametrosInicialesDTO.NumerosSesiones.Value.ToString()), 0);

                                if (totalLabo == 0)
                                {
                                    totalLabo += 1;
                                }

                                if (totalSession == 0)
                                {
                                    totalSession += 1;
                                }

                                subtotalLabo = totalLabo == 0 ? 1 : totalLabo;
                                subtotalSession = (totalSession / totalLabo);

                                if (subtotalSession == parametrosInicialesDTO.NumerosSesiones.Value) //igual al numero de sessiones
                                {
                                    xy = subtotalSession * subtotalLabo;
                                    rest = totalSuste - (xy * parametrosInicialesDTO.NumeroEquipos.Value);
                                    if (rest > 0)
                                    {
                                        subtotalLabo += 1;
                                    }
                                }
                                else if (subtotalSession > parametrosInicialesDTO.NumerosSesiones.Value) //mayor al numero de sessiones
                                {
                                    subtotalSession = parametrosInicialesDTO.NumerosSesiones.Value;

                                    xy = (subtotalSession * subtotalLabo) * parametrosInicialesDTO.NumeroEquipos.Value;

                                    rest = totalSuste - xy;

                                    while (rest > 0)
                                    {
                                        subtotalLabo += 1;
                                        rest -= totalSuste;
                                    }
                                }
                                else
                                {                                                                       //menor que el numero de sessiones
                                    if (subtotalSession == 1)
                                    {
                                        xy = subtotalSession * subtotalLabo;
                                        rest = totalSuste - (xy * parametrosInicialesDTO.NumeroEquipos.Value);
                                        if (rest > 0)
                                        {
                                            subtotalSession += 1;
                                        }
                                    }
                                    else
                                    {
                                        xy = subtotalSession * subtotalLabo;
                                        rest = totalSuste - (xy * parametrosInicialesDTO.NumeroEquipos.Value);
                                        if (rest > 0)
                                        {
                                            subtotalSession += 1;
                                        }
                                    }
                                }

                                DatosSedes datosSedes = new DatosSedes
                                {
                                    AsignacionId = Id,
                                    NumeroSession = subtotalSession,
                                    NumeroLaboratorio = subtotalLabo,
                                    Code = item.Code,
                                    Description = item.Description,
                                    Agrupados = item.Agrupados,
                                    NumeroTotalSustentantes = totalSuste,
                                    coordenada_lat = item.coordenada_lat,
                                    coordenada_lng = item.coordenada_lng
                                };

                                insertMasiveDataSedes(datosSedes);
                                //db.DatosSedes.Add(datosSedes);
                                //await db.SaveChangesAsync();

                                List<DatosSedesAsignacion> datosSedesAsignacions = new List<DatosSedesAsignacion>();
                                List<DatosTemporalesViewModel> listanueva = datosTemporalesDTO.Where(x => agrupados.Contains(x.canton_id)).ToList();

                                List<double> tomardatos = MetodosUtils.GetListOfRandomDoubles((subtotalLabo * subtotalSession), listanueva.Count(), 0, parametrosInicialesDTO.NumeroEquipos.Value);
                                tomardatos.Sort();
                                int datos = 0;

                                for (int i = 1; i <= subtotalLabo; i++)
                                {
                                    for (int j = 1; j <= subtotalSession; j++)
                                    {

                                        List<DatosTemporalesViewModel> listatem = listanueva.Take((int)tomardatos[datos]).ToList();
                                        foreach (var idsustentante in listatem)
                                        {
                                            datosSedesAsignacions.Add(new DatosSedesAsignacion
                                            {
                                                SedeId = datosSedes.Id,
                                                SessionId = "S" + j,
                                                LaboratorioId = datosSedes.Code + "_" + (i <= 9 ? ("0" + i.ToString()) : i.ToString()),
                                                SustentanteId = idsustentante.Id.Value
                                            });
                                            listanueva.RemoveAll(x => x.Id == idsustentante.Id);
                                        }
                                        datos++;
                                    }
                                }

                                insertMasiveData(datosSedesAsignacions.ToList());
                                //await db.SaveChangesAsync();

                            }
                        }
                        else ///INTERCANTONAL
                        {
                            List<PorCantonLatLng> porCantonLatLng = new List<PorCantonLatLng>();
                            foreach (var item in datosporCantones)
                            {
                                foreach (var itemApiKey in datosMapboxAPIKEYs)
                                {
                                    int cont = 1;
                                    int ConsutasMaximas = itemApiKey.NumeroMaximoConsulta;
                                    int ConsultasUsadas = itemApiKey.NumeroUsadasConsultas;
                                    int ConsultasMinimas = itemApiKey.NumeroMininoConsulta;

                                    int ConsultasApi = ConsultasMinimas - ConsultasUsadas;

                                    if (ConsultasApi == 0)
                                    {
                                        //break;
                                    }
                                    else
                                    {
                                        ApiPosicionGeografica.Root coordenadas = await ApiPosicionGeografica.GetByPosicionGeografica(item.canton.Trim() + "," + item.provincia.Trim() + "," + "Ecuador", itemApiKey.APIKEY.Trim());

                                        DatosMapboxAPIKEY resultApiKey = await db.DatosMapboxAPIKEY.Where(x => x.Id == itemApiKey.Id).FirstOrDefaultAsync();

                                        resultApiKey.NumeroUsadasConsultas = itemApiKey.NumeroUsadasConsultas + cont;

                                        var entry = db.Entry(resultApiKey);
                                        entry.State = EntityState.Modified;
                                        await db.SaveChangesAsync();

                                        porCantonLatLng.Add(new PorCantonLatLng
                                        {
                                            id_provincia = item.id_provincia,
                                            provincia = item.provincia,
                                            canton_id = item.canton_id,
                                            canton = item.canton,
                                            NumeroSustentates = item.NumeroSustentates,
                                            Lat = coordenadas != null ? coordenadas.features.FirstOrDefault().center[0].ToString().Replace(',', '.') : "",
                                            Lng = coordenadas != null ? coordenadas.features.FirstOrDefault().center[1].ToString().Replace(',', '.') : ""
                                        });
                                        break;
                                    }
                                }
                            }
                            //AGRUPO POR PROVINCIAS
                            var listaAgrupada = from li in porCantonLatLng
                                                group li by new { li.id_provincia, li.provincia } into datosAgrupados
                                                select new { Clave = datosAgrupados.Key, Datos = datosAgrupados };

                            List<DatosSedes> porsedes = new List<DatosSedes>();
                            List<string> existen = new List<string>();
                            foreach (var itemProvincia in listaAgrupada)
                            {
                                List<PorCantonLatLng> porCantonLatLng2 = itemProvincia.Datos.OrderByDescending(x => x.NumeroSustentates).ToList();
                                //CREACION DE SEDES
                                foreach (var itemporLatLng in itemProvincia.Datos.OrderByDescending(x => x.NumeroSustentates).ToList())
                                {
                                    bool exist = existen.Where(x => x == itemporLatLng.canton_id).Any();
                                    if (!exist)
                                    {
                                        int count = 0;
                                        int NumeroSustentantes = 0;
                                        string agrupados = "";
                                        agrupados += itemporLatLng.canton_id + "_" + itemporLatLng.canton + ",";
                                        NumeroSustentantes += itemporLatLng.NumeroSustentates;
                                        foreach (var itemLatLng2 in porCantonLatLng2.ToList())
                                        {
                                            bool exist2 = existen.Where(x => x == itemLatLng2.canton_id).Any();
                                            if (!exist2)
                                            {
                                                if (itemporLatLng == itemLatLng2)
                                                {
                                                    existen.Add(itemLatLng2.canton_id);
                                                }
                                                else
                                                {
                                                    foreach (var itemApiKey2 in datosMapboxAPIKEYs)
                                                    {
                                                        int cont2 = 1;
                                                        int ConsutasMaximas2 = itemApiKey2.NumeroMaximoConsulta;
                                                        int ConsultasUsadas2 = itemApiKey2.NumeroUsadasConsultas;
                                                        int ConsultasMinimas2 = itemApiKey2.NumeroMininoConsulta;

                                                        int ConsultasApi2 = ConsultasMinimas2 - ConsultasUsadas2;
                                                        if (ConsultasApi2 == 0)
                                                        {

                                                        }
                                                        else
                                                        {
                                                            ApiDriving.Root coordenadas = await ApiDriving.GetByDriving(itemporLatLng.Lat.Trim() + "," + itemporLatLng.Lng.Trim(), itemLatLng2.Lat.Trim() + "," + itemLatLng2.Lng.Trim(), itemApiKey2.APIKEY.Trim());
                                                            DatosMapboxAPIKEY resultApiKey = await db.DatosMapboxAPIKEY.Where(x => x.Id == itemApiKey2.Id).FirstOrDefaultAsync();

                                                            resultApiKey.NumeroUsadasConsultas = itemApiKey2.NumeroUsadasConsultas + cont2;

                                                            var entry = db.Entry(resultApiKey);
                                                            entry.State = EntityState.Modified;
                                                            await db.SaveChangesAsync();


                                                            if (coordenadas != null)
                                                            {
                                                                if (coordenadas.code == "Ok")
                                                                {
                                                                    if (parametrosInicialesDTO.TiempoViaje.HasValue)
                                                                    {
                                                                        int tiempo = (int)Math.Truncate(coordenadas.routes.FirstOrDefault().duration / 60);
                                                                        int Distancia = (int)Math.Round((coordenadas.routes.FirstOrDefault().distance / 100), 0);
                                                                        if (tiempo == 0 && Distancia == 0)
                                                                        {

                                                                        }
                                                                        else if (tiempo <= parametrosInicialesDTO.TiempoViaje.Value)
                                                                        {
                                                                            count += 1;
                                                                            agrupados += itemLatLng2.canton_id + "_" + itemLatLng2.canton + "_" + tiempo + "_" + Distancia + ",";
                                                                            NumeroSustentantes += itemLatLng2.NumeroSustentates;

                                                                            existen.Add(itemLatLng2.canton_id);
                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                }
                                                            }
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        porsedes.Add(new DatosSedes
                                        {
                                            AsignacionId = Id,
                                            NumeroSession = 0,
                                            NumeroLaboratorio = 0,
                                            Code = itemporLatLng.canton_id,
                                            Description = itemporLatLng.canton,
                                            NumeroTotalSustentantes = NumeroSustentantes,
                                            Agrupados = count == 0 ? agrupados + "_Cantón Origen Lejano" : agrupados,
                                            coordenada_lat = itemporLatLng.Lat,
                                            coordenada_lng = itemporLatLng.Lng
                                        });
                                        existen.Add(itemporLatLng.canton_id);
                                    }
                                }
                            }

                            //RECORRIENDO SEDES Y ASIGNANDO SUSTENTANTES
                            foreach (var item in porsedes)
                            {
                                List<string> agrupados = new List<string>();
                                if (!string.IsNullOrEmpty(item.Agrupados))
                                {
                                    string[] cantonagrospli = item.Agrupados.Split(',');
                                    foreach (var itemagrupados in cantonagrospli)
                                    {
                                        if (!string.IsNullOrEmpty(itemagrupados))
                                        {
                                            agrupados.Add(itemagrupados.Split('_')[0]);
                                        }
                                    }
                                }

                                int totalSuste = 0;
                                int totalLabo = 0;
                                int totalSession = 0;
                                int subtotalSession = 0;
                                int subtotalLabo = 0;
                                int rest = 0;
                                int xy = 0;
                                totalSuste = datosTemporalesDTO.Where(x => agrupados.Contains(x.canton_id)).Count();
                                totalSession = (int)Math.Round(Double.Parse(totalSuste.ToString()) / Double.Parse(parametrosInicialesDTO.NumeroEquipos.Value.ToString()), 0);
                                totalLabo = (int)Math.Round(Double.Parse(totalSession.ToString()) / Double.Parse(parametrosInicialesDTO.NumerosSesiones.Value.ToString()), 0);

                                if (totalLabo == 0)
                                {
                                    totalLabo += 1;
                                }

                                if (totalSession == 0)
                                {
                                    totalSession += 1;
                                }

                                subtotalLabo = totalLabo == 0 ? 1 : totalLabo;
                                subtotalSession = (totalSession / totalLabo);

                                if (subtotalSession == parametrosInicialesDTO.NumerosSesiones.Value) //igual al numero de sessiones
                                {
                                    xy = subtotalSession * subtotalLabo;
                                    rest = totalSuste - (xy * parametrosInicialesDTO.NumeroEquipos.Value);
                                    if (rest > 0)
                                    {
                                        subtotalLabo += 1;
                                    }
                                }
                                else if (subtotalSession > parametrosInicialesDTO.NumerosSesiones.Value) //mayor al numero de sessiones
                                {
                                    subtotalSession = parametrosInicialesDTO.NumerosSesiones.Value;

                                    xy = (subtotalSession * subtotalLabo) * parametrosInicialesDTO.NumeroEquipos.Value;

                                    rest = totalSuste - xy;

                                    while (rest > 0)
                                    {
                                        subtotalLabo += 1;
                                        rest -= totalSuste;
                                    }
                                }
                                else
                                {                                                                       //menor que el numero de sessiones
                                    if (subtotalSession == 1)
                                    {
                                        xy = subtotalSession * subtotalLabo;
                                        rest = totalSuste - (xy * parametrosInicialesDTO.NumeroEquipos.Value);
                                        if (rest > 0)
                                        {
                                            subtotalSession += 1;
                                        }
                                    }
                                    else
                                    {
                                        xy = subtotalSession * subtotalLabo;
                                        rest = totalSuste - (xy * parametrosInicialesDTO.NumeroEquipos.Value);
                                        if (rest > 0)
                                        {
                                            subtotalSession += 1;
                                        }
                                    }
                                }

                                DatosSedes datosSedes = new DatosSedes
                                {
                                    AsignacionId = Id,
                                    NumeroSession = subtotalSession,
                                    NumeroLaboratorio = subtotalLabo,
                                    Code = item.Code,
                                    Description = item.Description,
                                    Agrupados = item.Agrupados,
                                    NumeroTotalSustentantes = totalSuste,
                                    coordenada_lat = item.coordenada_lat,
                                    coordenada_lng = item.coordenada_lng
                                };

                                insertMasiveDataSedes(datosSedes);
                                //db.DatosSedes.Add(datosSedes);
                                //await db.SaveChangesAsync();

                                List<DatosSedesAsignacion> datosSedesAsignacions = new List<DatosSedesAsignacion>();
                                List<DatosTemporalesViewModel> listanueva = datosTemporalesDTO.Where(x => agrupados.Contains(x.canton_id)).ToList();

                                List<double> tomardatos = MetodosUtils.GetListOfRandomDoubles((subtotalLabo * subtotalSession), listanueva.Count(), 0, parametrosInicialesDTO.NumeroEquipos.Value);
                                tomardatos.Sort();
                                int datos = 0;

                                for (int i = 1; i <= subtotalLabo; i++)
                                {
                                    for (int j = 1; j <= subtotalSession; j++)
                                    {

                                        List<DatosTemporalesViewModel> listatem = listanueva.Take((int)tomardatos[datos]).ToList();
                                        foreach (var idsustentante in listatem)
                                        {
                                            datosSedesAsignacions.Add(new DatosSedesAsignacion
                                            {
                                                SedeId = datosSedes.Id,
                                                SessionId = "S" + j,
                                                LaboratorioId = datosSedes.Code + "_" + (i <= 9 ? ("0" + i.ToString()) : i.ToString()),
                                                SustentanteId = idsustentante.Id.Value
                                            });
                                            listanueva.RemoveAll(x => x.Id == idsustentante.Id);
                                        }
                                        datos++;
                                    }
                                }

                                insertMasiveData(datosSedesAsignacions.ToList());
                                //await db.SaveChangesAsync();

                            }

                        }

                        bool status = await EnvioCorreos.SendAsync(userId, "Se creo con Exito las Sedes");

                        return Json(new { result = "", message = "Se creo con exito las sedes", status = "success" }, JsonRequestBehavior.AllowGet);
                    }
                    //PARROQUIAS
                    else if (Parametro1.HasValue && Parametro1.Value == 4)
                    {
                        List<PorParroquias> datosporParroquias = db.Database.SqlQuery<PorParroquias>("exec sp_TipoAsignaciones @AsignacionId, @Param1, @Param2, @Param3, @Param4, @Param5", new SqlParameter("AsignacionId", Id),
                            new SqlParameter("Param1", Parametro1.Value), new SqlParameter("Param2", "0"), new SqlParameter("Param3", "0"), new SqlParameter("Param4", "0"), new SqlParameter("Param5", "0")).ToList<PorParroquias>();

                        if (parametrosInicialesDTO.tipo.Value == 1) //NIVEL NACIONAL
                        {
                            List<PorParroquiasLatLng> porParroquiasLatLngs = new List<PorParroquiasLatLng>();
                            foreach (var item in datosporParroquias)
                            {
                                porParroquiasLatLngs.Add(new PorParroquiasLatLng
                                {
                                    id_provincia = item.id_provincia,
                                    provincia = item.provincia,
                                    canton_id = item.canton_id,
                                    canton = item.canton,
                                    id_parroquia = item.id_parroquia,
                                    parroquia = item.parroquia,
                                    NumeroSustentates = item.NumeroSustentates,
                                    Lat = item.coordenada_x != null ? item.coordenada_x.Replace(',', '.') : null,
                                    Lng = item.coordenada_y != null ? item.coordenada_y.Replace(',', '.') : null,
                                });
                            }
                            //OBTENER DATOS DE LA BASE DE DATOS
                            List<PorParroquiasLatLng> porParroquiasLatLngs2 = porParroquiasLatLngs.OrderByDescending(x => x.NumeroSustentates).ToList();
                            List<DatosSedes> porsedes = new List<DatosSedes>();
                            List<string> existen = new List<string>();

                            foreach (var item in porParroquiasLatLngs.OrderByDescending(x => x.NumeroSustentates).ToList())
                            {
                                bool exist = existen.Where(x => x == item.id_parroquia).Any();
                                if (!exist)
                                {
                                    int count = 0;
                                    int NumeroSustentantes = 0;
                                    string agrupados = "";
                                    agrupados += item.id_parroquia + "_" + item.parroquia + ",";
                                    NumeroSustentantes += item.NumeroSustentates;

                                    foreach (var itemLatLng2 in porParroquiasLatLngs2.ToList())
                                    {
                                        bool exist2 = existen.Where(x => x == itemLatLng2.id_parroquia).Any();
                                        if (!exist2)
                                        {
                                            if (item == itemLatLng2)
                                            {
                                                existen.Add(itemLatLng2.id_parroquia);
                                            }
                                            else
                                            {
                                                foreach (var itemApiKey2 in datosMapboxAPIKEYs)
                                                {
                                                    int cont2 = 1;
                                                    int ConsutasMaximas2 = itemApiKey2.NumeroMaximoConsulta;
                                                    int ConsultasUsadas2 = itemApiKey2.NumeroUsadasConsultas;
                                                    int ConsultasMinimas2 = itemApiKey2.NumeroMininoConsulta;

                                                    int ConsultasApi2 = ConsultasMinimas2 - ConsultasUsadas2;
                                                    if (ConsultasApi2 == 0)
                                                    {

                                                    }
                                                    else
                                                    {
                                                        ApiDriving.Root coordenadas = await ApiDriving.GetByDriving(item.Lat.Trim() + "," + item.Lng.Trim(), itemLatLng2.Lat.Trim() + "," + itemLatLng2.Lng.Trim(), itemApiKey2.APIKEY.Trim());
                                                        DatosMapboxAPIKEY resultApiKey = await db.DatosMapboxAPIKEY.Where(x => x.Id == itemApiKey2.Id).FirstOrDefaultAsync();

                                                        resultApiKey.NumeroUsadasConsultas = itemApiKey2.NumeroUsadasConsultas + cont2;

                                                        var entry = db.Entry(resultApiKey);
                                                        entry.State = EntityState.Modified;
                                                        await db.SaveChangesAsync();


                                                        if (coordenadas.code == "Ok")
                                                        {
                                                            if (parametrosInicialesDTO.TiempoViaje.HasValue)
                                                            {
                                                                int tiempo = (int)Math.Truncate(coordenadas.routes.FirstOrDefault().duration / 60);
                                                                int Distancia = (int)Math.Round((coordenadas.routes.FirstOrDefault().distance / 100), 0);
                                                                if (tiempo == 0 && Distancia == 0)
                                                                {

                                                                }
                                                                else if (tiempo <= parametrosInicialesDTO.TiempoViaje.Value)
                                                                {
                                                                    count += 1;
                                                                    agrupados += itemLatLng2.id_parroquia + "_" + itemLatLng2.parroquia + "_" + tiempo + "_" + Distancia + ",";
                                                                    NumeroSustentantes += itemLatLng2.NumeroSustentates;
                                                                    existen.Add(itemLatLng2.id_parroquia);
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                        }
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    porsedes.Add(new DatosSedes
                                    {
                                        AsignacionId = Id,
                                        NumeroSession = 0,
                                        NumeroLaboratorio = 0,
                                        Code = item.id_parroquia,
                                        Description = item.parroquia,
                                        NumeroTotalSustentantes = NumeroSustentantes,
                                        Agrupados = count == 0 ? agrupados + "_Cantón Origen Lejano" : agrupados,
                                        coordenada_lat = item.Lat,
                                        coordenada_lng = item.Lng
                                    });
                                    existen.Add(item.id_parroquia);
                                }
                            }

                            foreach (var item in porsedes)
                            {
                                List<string> agrupados = new List<string>();
                                if (!string.IsNullOrEmpty(item.Agrupados))
                                {
                                    string[] cantonagrospli = item.Agrupados.Split(',');
                                    foreach (var itemagrupados in cantonagrospli)
                                    {
                                        if (!string.IsNullOrEmpty(itemagrupados))
                                        {
                                            agrupados.Add(itemagrupados.Split('_')[0]);
                                        }
                                    }
                                }

                                int totalSuste = 0;
                                int totalLabo = 0;
                                int totalSession = 0;
                                int subtotalSession = 0;
                                int subtotalLabo = 0;
                                int rest = 0;
                                int xy = 0;
                                totalSuste = datosTemporalesDTO.Where(x => agrupados.Contains(x.id_parroquia)).Count();
                                totalSession = (int)Math.Round(Double.Parse(totalSuste.ToString()) / Double.Parse(parametrosInicialesDTO.NumeroEquipos.Value.ToString()), 0);
                                totalLabo = (int)Math.Round(Double.Parse(totalSession.ToString()) / Double.Parse(parametrosInicialesDTO.NumerosSesiones.Value.ToString()), 0);

                                if (totalLabo == 0)
                                {
                                    totalLabo += 1;
                                }

                                if (totalSession == 0)
                                {
                                    totalSession += 1;
                                }

                                subtotalLabo = totalLabo == 0 ? 1 : totalLabo;
                                subtotalSession = (totalSession / totalLabo);

                                if (subtotalSession == parametrosInicialesDTO.NumerosSesiones.Value) //igual al numero de sessiones
                                {
                                    xy = subtotalSession * subtotalLabo;
                                    rest = totalSuste - (xy * parametrosInicialesDTO.NumeroEquipos.Value);
                                    if (rest > 0)
                                    {
                                        subtotalLabo += 1;
                                    }
                                }
                                else if (subtotalSession > parametrosInicialesDTO.NumerosSesiones.Value) //mayor al numero de sessiones
                                {
                                    subtotalSession = parametrosInicialesDTO.NumerosSesiones.Value;

                                    xy = (subtotalSession * subtotalLabo) * parametrosInicialesDTO.NumeroEquipos.Value;

                                    rest = totalSuste - xy;

                                    while (rest > 0)
                                    {
                                        subtotalLabo += 1;
                                        rest -= totalSuste;
                                    }
                                }
                                else
                                {                                                                       //menor que el numero de sessiones
                                    if (subtotalSession == 1)
                                    {
                                        xy = subtotalSession * subtotalLabo;
                                        rest = totalSuste - (xy * parametrosInicialesDTO.NumeroEquipos.Value);
                                        if (rest > 0)
                                        {
                                            subtotalSession += 1;
                                        }
                                    }
                                    else
                                    {
                                        xy = subtotalSession * subtotalLabo;
                                        rest = totalSuste - (xy * parametrosInicialesDTO.NumeroEquipos.Value);
                                        if (rest > 0)
                                        {
                                            subtotalSession += 1;
                                        }
                                    }
                                }

                                DatosSedes datosSedes = new DatosSedes
                                {
                                    AsignacionId = Id,
                                    NumeroSession = subtotalSession,
                                    NumeroLaboratorio = subtotalLabo,
                                    Code = item.Code,
                                    Description = item.Description,
                                    Agrupados = item.Agrupados,
                                    NumeroTotalSustentantes = totalSuste,
                                    coordenada_lat = item.coordenada_lat,
                                    coordenada_lng = item.coordenada_lng
                                };

                                try
                                {
                                    using (DatosSedesService datosSedesService = new DatosSedesService())
                                    {
                                        var result = await datosSedesService.SaveAsync(datosSedes);
                                        if (result.Succeeded)
                                        {

                                        }
                                    }
                                }
                                catch (Exception ex)
                                {

                                    throw;
                                }
                                //db.DatosSedes.Add(datosSedes);
                                //await db.SaveChangesAsync();

                                List<DatosSedesAsignacion> datosSedesAsignacions = new List<DatosSedesAsignacion>();
                                List<DatosTemporalesViewModel> listanueva = datosTemporalesDTO.Where(x => agrupados.Contains(x.id_parroquia)).ToList();

                                List<double> tomardatos = MetodosUtils.GetListOfRandomDoubles((subtotalLabo * subtotalSession), listanueva.Count(), 0, parametrosInicialesDTO.NumeroEquipos.Value);
                                tomardatos.Sort();
                                int datos = 0;

                                for (int i = 1; i <= subtotalLabo; i++)
                                {
                                    for (int j = 1; j <= subtotalSession; j++)
                                    {

                                        List<DatosTemporalesViewModel> listatem = listanueva.Take((int)tomardatos[datos]).ToList();
                                        foreach (var idsustentante in listatem)
                                        {
                                            datosSedesAsignacions.Add(new DatosSedesAsignacion
                                            {
                                                SedeId = datosSedes.Id,
                                                SessionId = "S" + j,
                                                LaboratorioId = datosSedes.Code + "_" + (i <= 9 ? ("0" + i.ToString()) : i.ToString()),
                                                SustentanteId = idsustentante.Id.Value
                                            });
                                            listanueva.RemoveAll(x => x.Id == idsustentante.Id);
                                        }
                                        datos++;
                                    }
                                }

                                insertMasiveData(datosSedesAsignacions.ToList());
                                //await db.SaveChangesAsync();

                            }

                        }
                        else                                        //NIVEL INTERNO
                        {
                            List<PorParroquiasLatLng> porParroquiasLatLngs = new List<PorParroquiasLatLng>();
                            foreach (var item in datosporParroquias)
                            {
                                porParroquiasLatLngs.Add(new PorParroquiasLatLng
                                {
                                    id_provincia = item.id_provincia,
                                    provincia = item.provincia,
                                    canton_id = item.canton_id,
                                    canton = item.canton,
                                    id_parroquia = item.id_parroquia,
                                    parroquia = item.parroquia,
                                    NumeroSustentates = item.NumeroSustentates,
                                    Lat = item.coordenada_x != null ? item.coordenada_x.Replace(',', '.') : null,
                                    Lng = item.coordenada_y != null ? item.coordenada_y.Replace(',', '.') : null,
                                });
                            }

                            //AGUPO POR CANTONES
                            var listaAgrupada = from li in porParroquiasLatLngs
                                                group li by new { li.canton_id, li.canton } into datosAgrupados
                                                select new { Clave = datosAgrupados.Key, Datos = datosAgrupados };

                            //OBTENER DATOS DE LA BASE DE DATOS

                            List<DatosSedes> porsedes = new List<DatosSedes>();
                            List<string> existen = new List<string>();

                            foreach (var itemCantones in listaAgrupada)
                            {
                                List<PorParroquiasLatLng> porParroquiasLatLngs2 = itemCantones.Datos.OrderByDescending(x => x.NumeroSustentates).ToList();
                                //CRACION DE SEDES

                                foreach (var item in itemCantones.Datos.OrderByDescending(x => x.NumeroSustentates).ToList())
                                {
                                    bool exist = existen.Where(x => x == item.id_parroquia).Any();
                                    if (!exist)
                                    {
                                        int count = 0;
                                        int NumeroSustentantes = 0;
                                        string agrupados = "";
                                        agrupados += item.id_parroquia + "_" + item.parroquia + ",";
                                        NumeroSustentantes += item.NumeroSustentates;

                                        foreach (var itemLatLng2 in porParroquiasLatLngs2.ToList())
                                        {
                                            bool exist2 = existen.Where(x => x == itemLatLng2.id_parroquia).Any();
                                            if (!exist2)
                                            {
                                                if (item == itemLatLng2)
                                                {
                                                    existen.Add(itemLatLng2.id_parroquia);
                                                }
                                                else
                                                {
                                                    foreach (var itemApiKey2 in datosMapboxAPIKEYs)
                                                    {
                                                        int cont2 = 1;
                                                        int ConsutasMaximas2 = itemApiKey2.NumeroMaximoConsulta;
                                                        int ConsultasUsadas2 = itemApiKey2.NumeroUsadasConsultas;
                                                        int ConsultasMinimas2 = itemApiKey2.NumeroMininoConsulta;

                                                        int ConsultasApi2 = ConsultasMinimas2 - ConsultasUsadas2;
                                                        if (ConsultasApi2 == 0)
                                                        {

                                                        }
                                                        else
                                                        {
                                                            ApiDriving.Root coordenadas = await ApiDriving.GetByDriving(item.Lat.Trim() + "," + item.Lng.Trim(), itemLatLng2.Lat.Trim() + "," + itemLatLng2.Lng.Trim(), itemApiKey2.APIKEY.Trim());
                                                            DatosMapboxAPIKEY resultApiKey = await db.DatosMapboxAPIKEY.Where(x => x.Id == itemApiKey2.Id).FirstOrDefaultAsync();

                                                            resultApiKey.NumeroUsadasConsultas = itemApiKey2.NumeroUsadasConsultas + cont2;

                                                            var entry = db.Entry(resultApiKey);
                                                            entry.State = EntityState.Modified;
                                                            await db.SaveChangesAsync();


                                                            if (coordenadas.code == "Ok")
                                                            {
                                                                if (parametrosInicialesDTO.TiempoViaje.HasValue)
                                                                {
                                                                    int tiempo = (int)Math.Truncate(coordenadas.routes.FirstOrDefault().duration / 60);
                                                                    int Distancia = (int)Math.Round((coordenadas.routes.FirstOrDefault().distance / 100), 0);
                                                                    if (tiempo == 0 && Distancia == 0)
                                                                    {

                                                                    }
                                                                    else if (tiempo <= parametrosInicialesDTO.TiempoViaje.Value)
                                                                    {
                                                                        count += 1;
                                                                        agrupados += itemLatLng2.id_parroquia + "_" + itemLatLng2.parroquia + "_" + tiempo + "_" + Distancia + ",";
                                                                        NumeroSustentantes += itemLatLng2.NumeroSustentates;
                                                                        existen.Add(itemLatLng2.id_parroquia);
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                            }
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        porsedes.Add(new DatosSedes
                                        {
                                            AsignacionId = Id,
                                            NumeroSession = 0,
                                            NumeroLaboratorio = 0,
                                            Code = item.id_parroquia,
                                            Description = item.parroquia,
                                            NumeroTotalSustentantes = NumeroSustentantes,
                                            Agrupados = count == 0 ? agrupados + "_Cantón Origen Lejano" : agrupados,
                                            coordenada_lat = item.Lat,
                                            coordenada_lng = item.Lng
                                        });
                                        existen.Add(item.id_parroquia);
                                    }
                                }
                            }

                            foreach (var item in porsedes)
                            {
                                List<string> agrupados = new List<string>();
                                if (!string.IsNullOrEmpty(item.Agrupados))
                                {
                                    string[] cantonagrospli = item.Agrupados.Split(',');
                                    foreach (var itemagrupados in cantonagrospli)
                                    {
                                        if (!string.IsNullOrEmpty(itemagrupados))
                                        {
                                            agrupados.Add(itemagrupados.Split('_')[0]);
                                        }
                                    }
                                }

                                int totalSuste = 0;
                                int totalLabo = 0;
                                int totalSession = 0;
                                int subtotalSession = 0;
                                int subtotalLabo = 0;
                                int rest = 0;
                                int xy = 0;
                                totalSuste = datosTemporalesDTO.Where(x => agrupados.Contains(x.id_parroquia)).Count();
                                totalSession = (int)Math.Round(Double.Parse(totalSuste.ToString()) / Double.Parse(parametrosInicialesDTO.NumeroEquipos.Value.ToString()), 0);
                                totalLabo = (int)Math.Round(Double.Parse(totalSession.ToString()) / Double.Parse(parametrosInicialesDTO.NumerosSesiones.Value.ToString()), 0);

                                if (totalLabo == 0)
                                {
                                    totalLabo += 1;
                                }

                                if (totalSession == 0)
                                {
                                    totalSession += 1;
                                }

                                subtotalLabo = totalLabo == 0 ? 1 : totalLabo;
                                subtotalSession = (totalSession / totalLabo);

                                if (subtotalSession == parametrosInicialesDTO.NumerosSesiones.Value) //igual al numero de sessiones
                                {
                                    xy = subtotalSession * subtotalLabo;
                                    rest = totalSuste - (xy * parametrosInicialesDTO.NumeroEquipos.Value);
                                    if (rest > 0)
                                    {
                                        subtotalLabo += 1;
                                    }
                                }
                                else if (subtotalSession > parametrosInicialesDTO.NumerosSesiones.Value) //mayor al numero de sessiones
                                {
                                    subtotalSession = parametrosInicialesDTO.NumerosSesiones.Value;

                                    xy = (subtotalSession * subtotalLabo) * parametrosInicialesDTO.NumeroEquipos.Value;

                                    rest = totalSuste - xy;

                                    while (rest > 0)
                                    {
                                        subtotalLabo += 1;
                                        rest -= totalSuste;
                                    }
                                }
                                else
                                {                                                                       //menor que el numero de sessiones
                                    if (subtotalSession == 1)
                                    {
                                        xy = subtotalSession * subtotalLabo;
                                        rest = totalSuste - (xy * parametrosInicialesDTO.NumeroEquipos.Value);
                                        if (rest > 0)
                                        {
                                            subtotalSession += 1;
                                        }
                                    }
                                    else
                                    {
                                        xy = subtotalSession * subtotalLabo;
                                        rest = totalSuste - (xy * parametrosInicialesDTO.NumeroEquipos.Value);
                                        if (rest > 0)
                                        {
                                            subtotalSession += 1;
                                        }
                                    }
                                }

                                DatosSedes datosSedes = new DatosSedes
                                {
                                    AsignacionId = Id,
                                    NumeroSession = subtotalSession,
                                    NumeroLaboratorio = subtotalLabo,
                                    Code = item.Code,
                                    Description = item.Description,
                                    Agrupados = item.Agrupados,
                                    NumeroTotalSustentantes = totalSuste,
                                    coordenada_lat = item.coordenada_lat,
                                    coordenada_lng = item.coordenada_lng
                                };

                                try
                                {
                                    using (DatosSedesService datosSedesService = new DatosSedesService())
                                    {
                                        var result = await datosSedesService.SaveAsync(datosSedes);
                                        if (result.Succeeded)
                                        {

                                        }
                                    }
                                }
                                catch (Exception ex)
                                {

                                    throw;
                                }
                                //db.DatosSedes.Add(datosSedes);
                                //await db.SaveChangesAsync();

                                List<DatosSedesAsignacion> datosSedesAsignacions = new List<DatosSedesAsignacion>();
                                List<DatosTemporalesViewModel> listanueva = datosTemporalesDTO.Where(x => agrupados.Contains(x.id_parroquia)).ToList();

                                List<double> tomardatos = MetodosUtils.GetListOfRandomDoubles((subtotalLabo * subtotalSession), listanueva.Count(), 0, parametrosInicialesDTO.NumeroEquipos.Value);
                                tomardatos.Sort();
                                int datos = 0;

                                for (int i = 1; i <= subtotalLabo; i++)
                                {
                                    for (int j = 1; j <= subtotalSession; j++)
                                    {

                                        List<DatosTemporalesViewModel> listatem = listanueva.Take((int)tomardatos[datos]).ToList();
                                        foreach (var idsustentante in listatem)
                                        {
                                            datosSedesAsignacions.Add(new DatosSedesAsignacion
                                            {
                                                SedeId = datosSedes.Id,
                                                SessionId = "S" + j,
                                                LaboratorioId = datosSedes.Code + "_" + (i <= 9 ? ("0" + i.ToString()) : i.ToString()),
                                                SustentanteId = idsustentante.Id.Value
                                            });
                                            listanueva.RemoveAll(x => x.Id == idsustentante.Id);
                                        }
                                        datos++;
                                    }
                                }

                                insertMasiveData(datosSedesAsignacions.ToList());
                                // await db.SaveChangesAsync();

                            }
                        }

                        bool status = await EnvioCorreos.SendAsync(userId, "Se creo con Exito las Sedes");

                        return Json(new { result = "", message = "Se creo con exito las sedes", status = "success" }, JsonRequestBehavior.AllowGet);
                    }
                    else if (Parametro1.HasValue && Parametro1.Value == 5) //CIRCUITO
                    {

                    }
                    else if (Parametro1.HasValue && Parametro1.Value == 6) //DISTRITO
                    {

                    }
                    else if (Parametro1.HasValue && Parametro1.Value == 7) //ZONA
                    {

                    }
                }
                else
                {
                    return Json(new { result = "", message = "No existe Datos Cargados", status = "error" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { result = "", message = "No existe Datos Cargados", status = "error" }, JsonRequestBehavior.AllowGet);
            }

            return null;
        }


        public void insertMasiveDataSedes(DatosSedes datosSedes)
        {
            var table = new DataTable();
            table.Columns.Add("Id", typeof(Guid));
            table.Columns.Add("AsignacionId", typeof(Guid));
            table.Columns.Add("NumeroSession", typeof(int));
            table.Columns.Add("NumeroLaboratorio", typeof(int));
            table.Columns.Add("coordenada_lat", typeof(string));
            table.Columns.Add("coordenada_lng", typeof(string));
            table.Columns.Add("Code", typeof(string));
            table.Columns.Add("Description", typeof(string));
            table.Columns.Add("FechaCreacion", typeof(DateTime));
            table.Columns.Add("FechaModificacion", typeof(DateTime));
            table.Columns.Add("FechaEliminacion", typeof(DateTime));
            table.Columns.Add("Estado", typeof(EstadoEnum));
            table.Columns.Add("NumeroTotalSustentantes", typeof(int));
            table.Columns.Add("Agrupados", typeof(string));
            //table.Columns.Add("Dia", typeof(string));


            table.Rows.Add(new object[]{
                   datosSedes.Id=Guid.NewGuid(),
                   datosSedes.AsignacionId,
                   datosSedes.NumeroSession,
                   datosSedes.NumeroLaboratorio,
                   datosSedes.coordenada_lat,
                  datosSedes.coordenada_lng,
                  datosSedes.Code,
                  datosSedes.Description,
                  datosSedes.FechaCreacion,
                  datosSedes.FechaModificacion,
                  datosSedes.FechaEliminacion,
                  datosSedes.Estado,
                  datosSedes.NumeroTotalSustentantes,
                  datosSedes.Agrupados
                });



            using (var connection = ConnectionToSql.getConnection())
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
                    {
                        try
                        {
                            bulkCopy.DestinationTableName = "DatosSedes";
                            bulkCopy.BulkCopyTimeout = 0;
                            bulkCopy.WriteToServer(table);
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            connection.Close();
                        }
                    }
                }

            }
        }

        public void insertMasiveData(IEnumerable<DatosSedesAsignacion> datosSedesAsignacion)
        {
            var table = new DataTable();
            table.Columns.Add("Id", typeof(Guid));
            table.Columns.Add("SedeId", typeof(Guid));
            table.Columns.Add("SessionId", typeof(string));
            table.Columns.Add("LaboratorioId", typeof(string));
            table.Columns.Add("SustentanteId", typeof(Guid));
            table.Columns.Add("Code", typeof(string));
            table.Columns.Add("Description", typeof(string));
            table.Columns.Add("FechaCreacion", typeof(DateTime));
            table.Columns.Add("FechaModificacion", typeof(DateTime));
            table.Columns.Add("FechaEliminacion", typeof(DateTime));
            table.Columns.Add("Estado", typeof(EstadoEnum));
            table.Columns.Add("DatosSedes_Id", typeof(Guid));
            //table.Columns.Add("Dia", typeof(string));

            foreach (var item in datosSedesAsignacion)
            {
                table.Rows.Add(new object[]{
                   item.Id=Guid.NewGuid(),
                   item.SedeId,
                   item.SessionId
                  ,item.LaboratorioId
                  ,item.SustentanteId
                  ,item.Code
                  ,item.Description
                  ,item.FechaCreacion
                  ,item.FechaModificacion
                  ,item.FechaEliminacion
                  ,item.Estado
                  //item.Dia

                });

            }

            using (var connection = ConnectionToSql.getConnection())
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
                    {
                        try
                        {
                            bulkCopy.DestinationTableName = "DatosSedesAsignacion";
                            bulkCopy.BulkCopyTimeout = 0;
                            bulkCopy.WriteToServer(table);
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            connection.Close();
                        }
                    }
                }

            }
        }


        ///EXPORTACIONES EXCEL
        ///
        public ActionResult ExportarSedeExcel(Guid? Id)
        {
            string NombreDocumento = "ReporteSede" + DateTime.Now;
            XLWorkbook wb = new XLWorkbook();
            var ws1 = wb.Worksheets.Add("Reportes");

            List<DatosSedes> listasedes = db.DatosSedes.Include(i => i.Asignacion).Where(x => x.AsignacionId == Id).ToList();

            ws1.Cell("A1").Value = "#";
            ws1.Cell("A1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("A1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("A1").Style.Font.Bold = true;

            ws1.Cell("B1").Value = "FECHA";
            ws1.Cell("B1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("B1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("B1").Style.Font.Bold = true;

            ws1.Cell("C1").Value = "Proceso";
            ws1.Cell("C1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("C1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("C1").Style.Font.Bold = true;

            ws1.Cell("D1").Value = "Sede Id";
            ws1.Cell("D1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("D1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("D1").Style.Font.Bold = true;

            ws1.Cell("E1").Value = "Sede Nombre";
            ws1.Cell("E1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("E1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("E1").Style.Font.Bold = true;

            ws1.Cell("F1").Value = "Numero de Laboratorios";
            ws1.Cell("F1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("F1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("F1").Style.Font.Bold = true;

            ws1.Cell("G1").Value = "Numero de Sesiones";
            ws1.Cell("G1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("G1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("G1").Style.Font.Bold = true;

            ws1.Cell("H1").Value = "Coordenada Lat";
            ws1.Cell("H1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("H1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("H1").Style.Font.Bold = true;

            ws1.Cell("I1").Value = "Coordenada Lng";
            ws1.Cell("I1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("I1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("I1").Style.Font.Bold = true;

            ws1.Cell("J1").Value = "Numero Total de Sustentantes";
            ws1.Cell("J1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("J1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("J1").Style.Font.Bold = true;

            ws1.Cell("k1").Value = "Agrupación";
            ws1.Cell("k1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("k1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("k1").Style.Font.Bold = true;

            var cont = 0;
            var cont1 = 2;

            foreach (var item in listasedes.OrderBy(o => o.Code))
            {
                cont++;
                ws1.Cell(cont1, 1).Value = cont;
                ws1.Cell(cont1, 2).Value = item.FechaCreacion.ToString("dd/MM/yyyy");
                ws1.Cell(cont1, 3).Value = item.Asignacion.NombreProceso.Description ?? null;
                ws1.Cell(cont1, 4).Value = "'" + item.Code;
                ws1.Cell(cont1, 5).Value = "'" + item.Description;
                ws1.Cell(cont1, 6).Value = item.NumeroLaboratorio;
                ws1.Cell(cont1, 7).Value = item.NumeroSession;
                ws1.Cell(cont1, 8).Value = item.coordenada_lat;
                ws1.Cell(cont1, 9).Value = item.coordenada_lng;
                ws1.Cell(cont1, 10).Value = item.NumeroTotalSustentantes;

                if (!string.IsNullOrEmpty(item.Agrupados))
                {
                    string datos = "";
                    string[] agrupadoslist = item.Agrupados.Split(',');
                    foreach (var itemagru in agrupadoslist)
                    {
                        if (!string.IsNullOrEmpty(itemagru))
                        {
                            datos += itemagru + "\n" + " ";
                        }

                    }

                    ws1.Cell(cont1, 11).Value = datos;
                }


                cont1++;
            }

            ws1.Cell(1, 13).Style.Alignment.WrapText = true;

            ws1.Columns().AdjustToContents();

            return new ExcelResult(wb, NombreDocumento + DateTime.Now.ToString("dd/MM/yyyy"));
        }
        //REPORTE ASIGNACION ID
        public class PorAsignacion
        {
            public string Code { get; set; }
            public string Description { get; set; }
            public string SessionId { get; set; }
            public string LaboratorioId { get; set; }
            public string usu_id { get; set; }
            public string tipo_identificacion { get; set; }
            public string identificacion { get; set; }
            public string primer_nombre { get; set; }
            public string segundo_nombre { get; set; }
            public string primer_apellido { get; set; }
            public string segundo_apellido { get; set; }
            public string sexo { get; set; }
            public string grado { get; set; }
            public string paralelo { get; set; }
            public string dia_nacimiento { get; set; }
            public string mes_nacimiento { get; set; }
            public string anio_nacimiento { get; set; }
            public string pais_nacimiento { get; set; }
            public string provincia_nacimiento { get; set; }
            public string discapacidad { get; set; }
            public string tipo_discapacidad { get; set; }
            public string porcentaje_discapacidad { get; set; }
            public string correo_sustentante { get; set; }
            public string telefono_sustentante { get; set; }
            public string telefono_sustentante_secundario { get; set; }
            public string celular_sustentante { get; set; }
            public string jornada_sustentante { get; set; }
            public string saber { get; set; }
            public string amie { get; set; }
            public string nombre_institucion { get; set; }
            public string id_provincia { get; set; }
            public string provincia { get; set; }
            public string canton_id { get; set; }
            public string canton { get; set; }
            public string id_parroquia { get; set; }
            public string parroquia { get; set; }
            public string id_circuito { get; set; }
            public string circuito { get; set; }
            public string id_distrito { get; set; }
            public string distrito { get; set; }
            public string id_zona { get; set; }
            public string sostenimiento_institucion { get; set; }
            public string regimen_institucion { get; set; }
            public string ciclo { get; set; }
            public string poblacion { get; set; }
            public string modalidad { get; set; }
            public string coordenada_x { get; set; }
            public string coordenada_y { get; set; }
            public string computador { get; set; }
            public string internet { get; set; }
            public string conexion_internet { get; set; }
            public string camara_web { get; set; }
            public string microfono { get; set; }
        }
        public ActionResult ExportarPorAsignacionExcel(Guid? Id)
        {
            string NombreDocumento = "ReportePorAsignacion" + DateTime.Now;
            XLWorkbook wb = new XLWorkbook();
            var ws1 = wb.Worksheets.Add("Reportes");
            var libro2 = wb.Worksheets.Add("Sustentantes sin Asignar");

            List<PorAsignacion> porAsignaciones = db.Database.SqlQuery<PorAsignacion>("exec sp_ReporteAsignaciones @AsignacionId", new SqlParameter("AsignacionId", Id)).ToList<PorAsignacion>();


            ws1.Cell("A1").Value = "#";
            ws1.Cell("A1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("A1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("A1").Style.Font.Bold = true;

            ws1.Cell("B1").Value = "Sede Id";
            ws1.Cell("B1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("B1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("B1").Style.Font.Bold = true;

            ws1.Cell("C1").Value = "Sede Nombre";
            ws1.Cell("C1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("C1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("C1").Style.Font.Bold = true;

            ws1.Cell("D1").Value = "Laboratorio";
            ws1.Cell("D1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("D1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("D1").Style.Font.Bold = true;

            ws1.Cell("E1").Value = "Sessión";
            ws1.Cell("E1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("E1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("E1").Style.Font.Bold = true;

            ws1.Cell("F1").Value = "Usuario Id";
            ws1.Cell("F1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("F1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("F1").Style.Font.Bold = true;

            ws1.Cell("G1").Value = "Tipo de Identificación";
            ws1.Cell("G1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("G1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("G1").Style.Font.Bold = true;

            ws1.Cell("H1").Value = "Identificación";
            ws1.Cell("H1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("H1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("H1").Style.Font.Bold = true;

            ws1.Cell("I1").Value = "Primer Nombre";
            ws1.Cell("I1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("I1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("I1").Style.Font.Bold = true;

            ws1.Cell("J1").Value = "Segundo Nombre";
            ws1.Cell("J1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("J1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("J1").Style.Font.Bold = true;

            ws1.Cell("K1").Value = "Primer Apellido";
            ws1.Cell("K1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("K1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("K1").Style.Font.Bold = true;

            ws1.Cell("L1").Value = "Segundo Apellido";
            ws1.Cell("L1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("L1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("L1").Style.Font.Bold = true;

            ws1.Cell("M1").Value = "Sexo";
            ws1.Cell("M1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("M1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("M1").Style.Font.Bold = true;

            ws1.Cell("N1").Value = "Grado";
            ws1.Cell("N1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("N1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("N1").Style.Font.Bold = true;

            ws1.Cell("O1").Value = "Paralelo";
            ws1.Cell("O1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("O1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("O1").Style.Font.Bold = true;

            ws1.Cell("P1").Value = "Dia Nacimiento";
            ws1.Cell("P1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("P1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("P1").Style.Font.Bold = true;

            ws1.Cell("Q1").Value = "Mes Nacimiento";
            ws1.Cell("Q1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("Q1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("Q1").Style.Font.Bold = true;

            ws1.Cell("R1").Value = "Año Nacimiento";
            ws1.Cell("R1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("R1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("R1").Style.Font.Bold = true;

            ws1.Cell("S1").Value = "Pais Nacimiento";
            ws1.Cell("S1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("S1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("S1").Style.Font.Bold = true;

            ws1.Cell("T1").Value = "Provincia Nacimiento";
            ws1.Cell("T1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("T1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("T1").Style.Font.Bold = true;

            ws1.Cell("U1").Value = "Discapacidad";
            ws1.Cell("U1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("U1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("U1").Style.Font.Bold = true;

            ws1.Cell("V1").Value = "Tipo Discapacidad";
            ws1.Cell("V1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("V1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("V1").Style.Font.Bold = true;

            ws1.Cell("W1").Value = "Porcentaje de Discapacidad";
            ws1.Cell("W1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("W1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("W1").Style.Font.Bold = true;

            ws1.Cell("X1").Value = "Correo Sustentante";
            ws1.Cell("X1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("X1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("X1").Style.Font.Bold = true;

            ws1.Cell("Y1").Value = "Teléfono Sustentante";
            ws1.Cell("Y1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("Y1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("Y1").Style.Font.Bold = true;

            ws1.Cell("Z1").Value = "Teléfono Sustentante Secundario";
            ws1.Cell("Z1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("Z1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("Z1").Style.Font.Bold = true;

            ws1.Cell("AA1").Value = "Celular Sustentante";
            ws1.Cell("AA1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("AA1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("AA1").Style.Font.Bold = true;

            ws1.Cell("AB1").Value = "Jornada";
            ws1.Cell("AB1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("AB1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("AB1").Style.Font.Bold = true;

            ws1.Cell("AC1").Value = "Saber";
            ws1.Cell("AC1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("AC1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("AC1").Style.Font.Bold = true;

            ws1.Cell("AD1").Value = "Amie";
            ws1.Cell("AD1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("AD1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("AD1").Style.Font.Bold = true;

            ws1.Cell("AE1").Value = "Nombre Institución";
            ws1.Cell("AE1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("AE1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("AE1").Style.Font.Bold = true;

            ws1.Cell("AF1").Value = "Provincia Id";
            ws1.Cell("AF1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("AF1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("AF1").Style.Font.Bold = true;

            ws1.Cell("AG1").Value = "Provincia";
            ws1.Cell("AG1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("AG1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("AG1").Style.Font.Bold = true;

            ws1.Cell("AH1").Value = "Cantón Id";
            ws1.Cell("AH1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("AH1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("AH1").Style.Font.Bold = true;

            ws1.Cell("AI1").Value = "Cantón";
            ws1.Cell("AI1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("AI1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("AI1").Style.Font.Bold = true;

            ws1.Cell("AJ1").Value = "Parroquia Id";
            ws1.Cell("AJ1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("AJ1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("AJ1").Style.Font.Bold = true;

            ws1.Cell("AK1").Value = "Parroquia";
            ws1.Cell("AK1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("AK1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("AK1").Style.Font.Bold = true;

            ws1.Cell("AL1").Value = "Cuircuito Id";
            ws1.Cell("AL1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("AL1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("AL1").Style.Font.Bold = true;

            ws1.Cell("AM1").Value = "Circuito";
            ws1.Cell("AM1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("AM1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("AM1").Style.Font.Bold = true;

            ws1.Cell("AN1").Value = "Distrito Id";
            ws1.Cell("AN1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("AN1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("AN1").Style.Font.Bold = true;

            ws1.Cell("AO1").Value = "Distrito";
            ws1.Cell("AO1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("AO1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("AO1").Style.Font.Bold = true;

            ws1.Cell("AP1").Value = "Zona Id";
            ws1.Cell("AP1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("AP1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("AP1").Style.Font.Bold = true;

            ws1.Cell("AQ1").Value = "Sostenimiento Institución";
            ws1.Cell("AQ1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("AQ1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("AQ1").Style.Font.Bold = true;

            ws1.Cell("AR1").Value = "Régimen Institución";
            ws1.Cell("AR1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("AR1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("AR1").Style.Font.Bold = true;

            ws1.Cell("AS1").Value = "Ciclo";
            ws1.Cell("AS1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("AS1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("AS1").Style.Font.Bold = true;

            ws1.Cell("AT1").Value = "Población";
            ws1.Cell("AT1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("AT1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("AT1").Style.Font.Bold = true;

            ws1.Cell("AU1").Value = "Modalidad";
            ws1.Cell("AU1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("AU1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("AU1").Style.Font.Bold = true;

            ws1.Cell("AV1").Value = "Coordenada X";
            ws1.Cell("AV1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("AV1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("AV1").Style.Font.Bold = true;

            ws1.Cell("AW1").Value = "Coordenada Y";
            ws1.Cell("AW1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("AW1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("AW1").Style.Font.Bold = true;

            ws1.Cell("AX1").Value = "Computador";
            ws1.Cell("AX1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("AX1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("AX1").Style.Font.Bold = true;

            ws1.Cell("AY1").Value = "Internet";
            ws1.Cell("AY1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("AY1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("AY1").Style.Font.Bold = true;

            ws1.Cell("AZ1").Value = "Conexión Internet";
            ws1.Cell("AZ1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("AZ1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("AZ1").Style.Font.Bold = true;

            ws1.Cell("BA1").Value = "Cámara Web";
            ws1.Cell("BA1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("BA1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("BA1").Style.Font.Bold = true;

            ws1.Cell("BB1").Value = "Micrófono";
            ws1.Cell("BB1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("BB1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("BB1").Style.Font.Bold = true;

            var cont = 0;
            var cont1 = 2;

            foreach (var item in porAsignaciones.OrderBy(o => o.Code))
            {
                cont++;
                ws1.Cell(cont1, 1).Value = cont;
                ws1.Cell(cont1, 2).Value = "'" + item.Code;
                ws1.Cell(cont1, 3).Value = "'" + item.Description;
                ws1.Cell(cont1, 4).Value = item.LaboratorioId;
                ws1.Cell(cont1, 5).Value = item.SessionId;
                ws1.Cell(cont1, 6).Value = item.usu_id;
                ws1.Cell(cont1, 7).Value = item.tipo_identificacion;
                ws1.Cell(cont1, 8).Value = "'" + item.identificacion;
                ws1.Cell(cont1, 9).Value = item.primer_nombre;
                ws1.Cell(cont1, 10).Value = item.segundo_nombre;
                ws1.Cell(cont1, 11).Value = item.primer_apellido;
                ws1.Cell(cont1, 12).Value = item.segundo_apellido;
                ws1.Cell(cont1, 13).Value = item.sexo;
                ws1.Cell(cont1, 14).Value = item.grado;
                ws1.Cell(cont1, 15).Value = item.paralelo;
                ws1.Cell(cont1, 16).Value = item.dia_nacimiento;
                ws1.Cell(cont1, 17).Value = item.mes_nacimiento;
                ws1.Cell(cont1, 18).Value = item.anio_nacimiento;
                ws1.Cell(cont1, 19).Value = item.pais_nacimiento;
                ws1.Cell(cont1, 20).Value = item.provincia_nacimiento;
                ws1.Cell(cont1, 21).Value = item.discapacidad;
                ws1.Cell(cont1, 22).Value = item.tipo_discapacidad;
                ws1.Cell(cont1, 23).Value = item.porcentaje_discapacidad;
                ws1.Cell(cont1, 24).Value = item.correo_sustentante;
                ws1.Cell(cont1, 25).Value = item.telefono_sustentante;
                ws1.Cell(cont1, 26).Value = item.telefono_sustentante_secundario;
                ws1.Cell(cont1, 27).Value = item.celular_sustentante;
                ws1.Cell(cont1, 28).Value = item.jornada_sustentante;
                ws1.Cell(cont1, 29).Value = item.saber;
                ws1.Cell(cont1, 30).Value = item.amie;
                ws1.Cell(cont1, 31).Value = item.nombre_institucion;
                ws1.Cell(cont1, 32).Value = item.id_provincia;
                ws1.Cell(cont1, 33).Value = item.provincia;
                ws1.Cell(cont1, 34).Value = item.canton_id;
                ws1.Cell(cont1, 35).Value = item.canton;
                ws1.Cell(cont1, 36).Value = item.id_parroquia;
                ws1.Cell(cont1, 37).Value = item.parroquia;
                ws1.Cell(cont1, 38).Value = item.id_circuito;
                ws1.Cell(cont1, 39).Value = item.circuito;
                ws1.Cell(cont1, 40).Value = item.id_distrito;
                ws1.Cell(cont1, 41).Value = item.distrito;
                ws1.Cell(cont1, 42).Value = item.id_zona;
                ws1.Cell(cont1, 43).Value = item.sostenimiento_institucion;
                ws1.Cell(cont1, 44).Value = item.regimen_institucion;
                ws1.Cell(cont1, 45).Value = item.ciclo;
                ws1.Cell(cont1, 46).Value = item.poblacion;
                ws1.Cell(cont1, 47).Value = item.modalidad;
                ws1.Cell(cont1, 48).Value = item.coordenada_x;
                ws1.Cell(cont1, 49).Value = item.coordenada_y;
                ws1.Cell(cont1, 50).Value = item.computador;
                ws1.Cell(cont1, 51).Value = item.internet;
                ws1.Cell(cont1, 52).Value = item.conexion_internet;
                ws1.Cell(cont1, 53).Value = item.camara_web;
                ws1.Cell(cont1, 54).Value = item.microfono;

                cont1++;
            }

            ws1.Cell(1, 13).Style.Alignment.WrapText = true;

            List<DatosTemporales> porSinAsignacion = db.Database.SqlQuery<DatosTemporales>("exec sp_SustentantesSinAsignacion @AsignacionID", new SqlParameter("AsignacionID", Id)).ToList<DatosTemporales>();
            ///SUSTENTATESSIN ASIGNACION
            ///
            libro2.Cell("A1").Value = "#";
            libro2.Cell("A1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("A1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("A1").Style.Font.Bold = true;

            libro2.Cell("B1").Value = "usu_id";
            libro2.Cell("B1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("B1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("B1").Style.Font.Bold = true;

            libro2.Cell("C1").Value = "tipo_identificacion";
            libro2.Cell("C1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("C1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("C1").Style.Font.Bold = true;

            libro2.Cell("D1").Value = "identificacion";
            libro2.Cell("D1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("D1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("D1").Style.Font.Bold = true;

            libro2.Cell("E1").Value = "primer_nombre";
            libro2.Cell("E1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("E1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("E1").Style.Font.Bold = true;

            libro2.Cell("F1").Value = "segundo_nombre";
            libro2.Cell("F1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("F1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("F1").Style.Font.Bold = true;

            libro2.Cell("G1").Value = "primer_apellido";
            libro2.Cell("G1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("G1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("G1").Style.Font.Bold = true;

            libro2.Cell("H1").Value = "segundo_apellido";
            libro2.Cell("H1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("H1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("H1").Style.Font.Bold = true;

            libro2.Cell("I1").Value = "sexo";
            libro2.Cell("I1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("I1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("I1").Style.Font.Bold = true;

            libro2.Cell("J1").Value = "grado";
            libro2.Cell("J1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("J1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("J1").Style.Font.Bold = true;

            libro2.Cell("k1").Value = "paralelo";
            libro2.Cell("k1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("k1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("k1").Style.Font.Bold = true;

            libro2.Cell("L1").Value = "dia_nacimiento";
            libro2.Cell("L1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("L1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("L1").Style.Font.Bold = true;

            libro2.Cell("M1").Value = "mes_nacimiento";
            libro2.Cell("M1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("M1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("M1").Style.Font.Bold = true;

            libro2.Cell("N1").Value = "anio_nacimiento";
            libro2.Cell("N1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("N1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("N1").Style.Font.Bold = true;

            libro2.Cell("O1").Value = "pais_nacimiento";
            libro2.Cell("O1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("O1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("O1").Style.Font.Bold = true;

            libro2.Cell("P1").Value = "provincia_nacimiento";
            libro2.Cell("P1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("P1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("P1").Style.Font.Bold = true;

            libro2.Cell("Q1").Value = "discapacidad";
            libro2.Cell("Q1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("Q1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("Q1").Style.Font.Bold = true;

            libro2.Cell("R1").Value = "tipo_discapacidad";
            libro2.Cell("R1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("R1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("R1").Style.Font.Bold = true;

            libro2.Cell("S1").Value = "porcentaje_discapacidad";
            libro2.Cell("S1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("S1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("S1").Style.Font.Bold = true;

            libro2.Cell("T1").Value = "correo_sustentante";
            libro2.Cell("T1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("T1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("T1").Style.Font.Bold = true;

            libro2.Cell("U1").Value = "telefono_sustentante";
            libro2.Cell("U1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("U1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("U1").Style.Font.Bold = true;

            libro2.Cell("V1").Value = "telefono_sustentante_secundario";
            libro2.Cell("V1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("V1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("V1").Style.Font.Bold = true;

            libro2.Cell("W1").Value = "celular_sustentante";
            libro2.Cell("W1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("W1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("W1").Style.Font.Bold = true;

            libro2.Cell("X1").Value = "jornada_sustentante";
            libro2.Cell("X1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("X1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("X1").Style.Font.Bold = true;

            libro2.Cell("Y1").Value = "saber";
            libro2.Cell("Y1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("Y1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("Y1").Style.Font.Bold = true;

            libro2.Cell("Z1").Value = "amie";
            libro2.Cell("Z1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("Z1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("Z1").Style.Font.Bold = true;

            libro2.Cell("AA1").Value = "nombre_institucion";
            libro2.Cell("AA1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("AA1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("AA1").Style.Font.Bold = true;

            libro2.Cell("AB1").Value = "id_provincia";
            libro2.Cell("AB1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("AB1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("AB1").Style.Font.Bold = true;

            libro2.Cell("AC1").Value = "provincia";
            libro2.Cell("AC1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("AC1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("AC1").Style.Font.Bold = true;

            libro2.Cell("AD1").Value = "canton_id";
            libro2.Cell("AD1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("AD1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("AD1").Style.Font.Bold = true;

            libro2.Cell("AE1").Value = "canton";
            libro2.Cell("AE1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("AE1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("AE1").Style.Font.Bold = true;

            libro2.Cell("AF1").Value = "id_parroquia";
            libro2.Cell("AF1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("AF1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("AF1").Style.Font.Bold = true;

            libro2.Cell("AG1").Value = "parroquia";
            libro2.Cell("AG1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("AG1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("AG1").Style.Font.Bold = true;

            libro2.Cell("AH1").Value = "id_circuito";
            libro2.Cell("AH1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("AH1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("AH1").Style.Font.Bold = true;

            libro2.Cell("AI1").Value = "circuito";
            libro2.Cell("AI1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("AI1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("AI1").Style.Font.Bold = true;

            libro2.Cell("AJ1").Value = "id_distrito";
            libro2.Cell("AJ1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("AJ1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("AJ1").Style.Font.Bold = true;

            libro2.Cell("AK1").Value = "distrito";
            libro2.Cell("AK1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("AK1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("AK1").Style.Font.Bold = true;

            libro2.Cell("AL1").Value = "id_zona";
            libro2.Cell("AL1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("AL1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("AL1").Style.Font.Bold = true;

            libro2.Cell("AM1").Value = "sostenimiento_institucion";
            libro2.Cell("AM1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("AM1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("AM1").Style.Font.Bold = true;

            libro2.Cell("AN1").Value = "regimen_institucion";
            libro2.Cell("AN1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("AN1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("AN1").Style.Font.Bold = true;

            libro2.Cell("AO1").Value = "ciclo";
            libro2.Cell("AO1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("AO1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("AO1").Style.Font.Bold = true;


            libro2.Cell("AP1").Value = "poblacion";
            libro2.Cell("AP1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("AP1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("AP1").Style.Font.Bold = true;

            libro2.Cell("AQ1").Value = "modalidad";
            libro2.Cell("AQ1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("AQ1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("AQ1").Style.Font.Bold = true;

            libro2.Cell("AR1").Value = "coordenada_x";
            libro2.Cell("AR1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("AR1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("AR1").Style.Font.Bold = true;

            libro2.Cell("AS1").Value = "coordenada_y";
            libro2.Cell("AS1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("AS1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("AS1").Style.Font.Bold = true;

            libro2.Cell("AT1").Value = "computador";
            libro2.Cell("AT1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("AT1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("AT1").Style.Font.Bold = true;

            libro2.Cell("AU1").Value = "internet";
            libro2.Cell("AU1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("AU1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("AU1").Style.Font.Bold = true;

            libro2.Cell("AV1").Value = "conexion_internet";
            libro2.Cell("AV1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("AV1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("AV1").Style.Font.Bold = true;

            libro2.Cell("AW1").Value = "camara_web";
            libro2.Cell("AW1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("AW1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("AW1").Style.Font.Bold = true;

            libro2.Cell("AX1").Value = "microfono";
            libro2.Cell("AX1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            libro2.Cell("AX1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            libro2.Cell("AX1").Style.Font.Bold = true;

            cont = 0;
            cont1 = 2;

            foreach (var item in porSinAsignacion)
            {
                cont++;
                libro2.Cell(cont1, 1).Value = cont;
                libro2.Cell(cont1, 2).Value = item.usu_id;
                libro2.Cell(cont1, 3).Value = item.tipo_identificacion;
                libro2.Cell(cont1, 4).Value = item.identificacion;
                libro2.Cell(cont1, 5).Value = item.primer_nombre;
                libro2.Cell(cont1, 6).Value = item.segundo_nombre;
                libro2.Cell(cont1, 7).Value = item.primer_apellido;
                libro2.Cell(cont1, 8).Value = item.segundo_apellido;
                libro2.Cell(cont1, 9).Value = item.sexo;
                libro2.Cell(cont1, 10).Value = item.grado;
                libro2.Cell(cont1, 11).Value = item.paralelo;
                libro2.Cell(cont1, 12).Value = item.dia_nacimiento;
                libro2.Cell(cont1, 13).Value = item.mes_nacimiento;
                libro2.Cell(cont1, 14).Value = item.anio_nacimiento;
                libro2.Cell(cont1, 15).Value = item.pais_nacimiento;
                libro2.Cell(cont1, 16).Value = item.provincia_nacimiento;
                libro2.Cell(cont1, 17).Value = item.discapacidad;
                libro2.Cell(cont1, 18).Value = item.tipo_discapacidad;
                libro2.Cell(cont1, 19).Value = item.porcentaje_discapacidad;
                libro2.Cell(cont1, 20).Value = item.correo_sustentante;
                libro2.Cell(cont1, 21).Value = item.telefono_sustentante;
                libro2.Cell(cont1, 22).Value = item.telefono_sustentante_secundario;
                libro2.Cell(cont1, 23).Value = item.celular_sustentante;
                libro2.Cell(cont1, 24).Value = item.jornada_sustentante;
                libro2.Cell(cont1, 25).Value = item.saber;
                libro2.Cell(cont1, 26).Value = item.amie;
                libro2.Cell(cont1, 27).Value = item.nombre_institucion;
                libro2.Cell(cont1, 28).Value = item.id_provincia;
                libro2.Cell(cont1, 29).Value = item.provincia;
                libro2.Cell(cont1, 30).Value = item.canton_id;
                libro2.Cell(cont1, 31).Value = item.canton;
                libro2.Cell(cont1, 32).Value = item.id_parroquia;
                libro2.Cell(cont1, 33).Value = item.parroquia;
                libro2.Cell(cont1, 34).Value = item.id_circuito;
                libro2.Cell(cont1, 35).Value = item.circuito;
                libro2.Cell(cont1, 36).Value = item.id_distrito;
                libro2.Cell(cont1, 37).Value = item.distrito;
                libro2.Cell(cont1, 38).Value = item.id_zona;
                libro2.Cell(cont1, 39).Value = item.sostenimiento_institucion;
                libro2.Cell(cont1, 40).Value = item.regimen_institucion;
                libro2.Cell(cont1, 41).Value = item.ciclo;
                libro2.Cell(cont1, 42).Value = item.poblacion;
                libro2.Cell(cont1, 43).Value = item.modalidad;
                libro2.Cell(cont1, 44).Value = item.coordenada_x;
                libro2.Cell(cont1, 45).Value = item.coordenada_y;
                libro2.Cell(cont1, 46).Value = item.computador;
                libro2.Cell(cont1, 47).Value = item.internet;
                libro2.Cell(cont1, 48).Value = item.conexion_internet;
                libro2.Cell(cont1, 49).Value = item.camara_web;
                libro2.Cell(cont1, 50).Value = item.microfono;

                cont1++;
            }


            ws1.Columns().AdjustToContents();
            libro2.Columns().AdjustToContents();

            return new ExcelResult(wb, NombreDocumento + DateTime.Now.ToString("dd/MM/yyyy"));
        }

    }
}