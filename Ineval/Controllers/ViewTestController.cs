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

namespace Ineval.Controllers
{
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


        #endregion

        public async Task<ActionResult> GetUsers()
        {
            var UserId = User.Identity.GetUserId();
            Guid Id = new Guid(UserId);

            UsuarioService usuarioService = new UsuarioService();

            Usuario user = new Usuario();
            UsuarioViewModel userDTO = new UsuarioViewModel();

            try
            {
                user = await usuarioService.GetAll().Where(X => X.ApplicationUserId == UserId).SingleOrDefaultAsync();
                userDTO = Mapper.Map<UsuarioViewModel>(user);

                return Json(new { Data = userDTO }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { Data = userDTO }, JsonRequestBehavior.AllowGet);
            }
            finally
            {
                usuarioService.Dispose();
            }
        }
        public async Task<ActionResult> Filtro(Guid? Id, int? Parametro1, int? Parametro2, int? Parametro3)
        {
            var userId = User.Identity.GetUserId();
            UsuarioService usuarioService = new UsuarioService();
            var usuario = usuarioService.ObtenerPorApplicationUserId(userId);

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
                var registroseliminados = db.Database.SqlQuery<List<int>>("exec DELETESEDES @AsignacionId", new SqlParameter("AsignacionId", Id)).ToList();
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

                    List<DatosInstituciones> datosInstituciones = new List<DatosInstituciones>();
                    List<DatosInstitucionesViewModel> resultDTO = new List<DatosInstitucionesViewModel>();
                    datosInstituciones = await db.DatosInstituciones.Where(x => x.AsignacionId == Id).ToListAsync();
                    resultDTO = Mapper.Map<List<DatosInstitucionesViewModel>>(datosInstituciones);



                    List<Posiciones> pos = new List<Posiciones>();

                    if (Parametro1.HasValue && Parametro1.Value == 1)//AMIE
                    {
                        if (Parametro2.HasValue && Parametro2.Value == 1)
                        {

                        }
                        else if (Parametro3.HasValue && Parametro3.Value == 1)
                        {

                        }
                        else
                        {
                            List<PorAmie> datosporAmie = db.Database.SqlQuery<PorAmie>("exec sp_TipoAsignaciones @AsignacionId, @Param1, @Param2, @Param3, @Param4, @Param5", new SqlParameter("AsignacionId", Id),
                            new SqlParameter("Param1", Parametro1.Value), new SqlParameter("Param2", "0"), new SqlParameter("Param3", "0"), new SqlParameter("Param4", "0"), new SqlParameter("Param5", "0")).ToList<PorAmie>();

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


                                //ApiPosicionGeografica.Root coordenadas = await ApiPosicionGeografica.GetByPosicionGeografica("Ecuador,'" + item.provincia + "'", usuario.APIKEY);

                                DatosSedes datosSedes = new DatosSedes
                                {
                                    AsignacionId = Id,
                                    NumeroSession = subtotalSession,
                                    NumeroLaboratorio = subtotalLabo,
                                    Code = item.amie,
                                    Description = item.nombre_institucion,
                                    NumeroTotalSustentantes = totalSuste,
                                    coordenada_lat = item.coordenada_x != "" ? item.coordenada_x.Replace(",", ".") : "",//coordenadas.features.FirstOrDefault().center[0].ToString().Replace(',', '.'),
                                    coordenada_lng = item.coordenada_y != "" ? item.coordenada_y.Replace(",", ".") : "",//coordenadas.features.FirstOrDefault().center[1].ToString().Replace(',', '.')
                                };

                                db.DatosSedes.Add(datosSedes);

                                await db.SaveChangesAsync();

                                List<DatosSedesAsignacion> datosSedesAsignacions = new List<DatosSedesAsignacion>();
                                List<DatosTemporalesViewModel> listanueva = datosTemporalesDTO.Where(x => x.amie == datosSedes.Code).ToList();

                                for (int i = 1; i <= subtotalLabo; i++)
                                {
                                    for (int j = 1; j <= subtotalSession; j++)
                                    {
                                        int tomardatos = 1 * parametrosInicialesDTO.NumeroEquipos.Value;
                                        List<DatosTemporalesViewModel> listatem = listanueva.Take(tomardatos).ToList();
                                        foreach (var idsustentante in listatem)
                                        {
                                            datosSedesAsignacions.Add(new DatosSedesAsignacion
                                            {
                                                SedeId = datosSedes.Id,
                                                SessionId = "S" + j,
                                                LaboratorioId = datosSedes.Code + "_" + i,
                                                SustentanteId = idsustentante.Id.Value
                                            });
                                            listanueva.RemoveAll(x => x.Id == idsustentante.Id);
                                        }
                                    }
                                }

                                insertMasiveData(datosSedesAsignacions.ToList());
                                await db.SaveChangesAsync();
                            }
                            return Json(new { result = "", message = "Se creo con exito las sedes", status = "success" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else if (Parametro1.HasValue && Parametro1.Value == 2)//PROVINCIA
                    {
                        List<PorPorvincia> datosporProvicias = db.Database.SqlQuery<PorPorvincia>("exec sp_TipoAsignaciones @AsignacionId, @Param1, @Param2, @Param3, @Param4, @Param5", new SqlParameter("AsignacionId", Id),
                            new SqlParameter("Param1", Parametro1.Value), new SqlParameter("Param2", "0"), new SqlParameter("Param3", "0"), new SqlParameter("Param4", "0"), new SqlParameter("Param5", "0")).ToList<PorPorvincia>();

                        foreach (var item in datosporProvicias)
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


                            ApiPosicionGeografica.Root coordenadas = await ApiPosicionGeografica.GetByPosicionGeografica("Ecuador,'" + item.provincia + "'", usuario.APIKEY);

                            DatosSedes datosSedes = new DatosSedes
                            {
                                AsignacionId = Id,
                                NumeroSession = subtotalSession,
                                NumeroLaboratorio = subtotalLabo,
                                Code = item.id_provincia,
                                Description = item.provincia,
                                NumeroTotalSustentantes = totalSuste,
                                coordenada_lat = coordenadas.features.FirstOrDefault().center[0].ToString().Replace(',', '.'),
                                coordenada_lng = coordenadas.features.FirstOrDefault().center[1].ToString().Replace(',', '.')
                            };

                            db.DatosSedes.Add(datosSedes);

                            await db.SaveChangesAsync();

                            List<DatosSedesAsignacion> datosSedesAsignacions = new List<DatosSedesAsignacion>();
                            List<DatosTemporalesViewModel> listanueva = datosTemporalesDTO.Where(x => x.id_provincia == datosSedes.Code).ToList();

                            for (int i = 1; i <= subtotalLabo; i++)
                            {
                                for (int j = 1; j <= subtotalSession; j++)
                                {
                                    int tomardatos = 1 * parametrosInicialesDTO.NumeroEquipos.Value;
                                    List<DatosTemporalesViewModel> listatem = listanueva.Take(tomardatos).ToList();
                                    foreach (var idsustentante in listatem)
                                    {
                                        datosSedesAsignacions.Add(new DatosSedesAsignacion
                                        {
                                            SedeId = datosSedes.Id,
                                            SessionId = "S" + j,
                                            LaboratorioId = datosSedes.Code + "_" + i,
                                            SustentanteId = idsustentante.Id.Value
                                        });
                                        listanueva.RemoveAll(x => x.Id == idsustentante.Id);
                                    }
                                }
                            }

                            insertMasiveData(datosSedesAsignacions.ToList());
                            await db.SaveChangesAsync();
                        }
                        return Json(new { result = "", message = "Se creo con exito las sedes", status = "success" }, JsonRequestBehavior.AllowGet);

                    }
                    else if (Parametro1.HasValue && Parametro1.Value == 3)//CANTONES
                    {
                        List<PorCanton> datosporCantones = db.Database.SqlQuery<PorCanton>("exec sp_TipoAsignaciones @AsignacionId, @Param1, @Param2, @Param3, @Param4, @Param5", new SqlParameter("AsignacionId", Id),
                            new SqlParameter("Param1", Parametro1.Value), new SqlParameter("Param2", "0"), new SqlParameter("Param3", "0"), new SqlParameter("Param4", "0"), new SqlParameter("Param5", "0")).ToList<PorCanton>();

                        foreach (var item in datosporCantones)
                        {
                            int totalSuste = 0;
                            int totalLabo = 0;
                            int totalSession = 0;
                            int subtotalSession = 0;
                            int subtotalLabo = 0;
                            int rest = 0;
                            int xy = 0;
                            totalSuste = datosTemporalesDTO.Where(x => x.canton_id == item.canton_id).Count();
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


                            ApiPosicionGeografica.Root coordenadas = await ApiPosicionGeografica.GetByPosicionGeografica("Ecuador,'" + item.provincia + "'','" + item.canton + "'", usuario.APIKEY);

                            DatosSedes datosSedes = new DatosSedes
                            {
                                AsignacionId = Id,
                                NumeroSession = subtotalSession,
                                NumeroLaboratorio = subtotalLabo,
                                Code = item.canton_id,
                                Description = item.canton,
                                NumeroTotalSustentantes = totalSuste,
                                coordenada_lat = coordenadas.features.FirstOrDefault().center[0].ToString().Replace(',', '.'),
                                coordenada_lng = coordenadas.features.FirstOrDefault().center[1].ToString().Replace(',', '.')
                            };

                            db.DatosSedes.Add(datosSedes);

                            await db.SaveChangesAsync();

                            List<DatosSedesAsignacion> datosSedesAsignacions = new List<DatosSedesAsignacion>();
                            List<DatosTemporalesViewModel> listanueva = datosTemporalesDTO.Where(x => x.canton_id == datosSedes.Code).ToList();

                            for (int i = 1; i <= subtotalLabo; i++)
                            {
                                for (int j = 1; j <= subtotalSession; j++)
                                {
                                    int tomardatos = 1 * parametrosInicialesDTO.NumeroEquipos.Value;
                                    List<DatosTemporalesViewModel> listatem = listanueva.Take(tomardatos).ToList();
                                    foreach (var idsustentante in listatem)
                                    {
                                        datosSedesAsignacions.Add(new DatosSedesAsignacion
                                        {
                                            SedeId = datosSedes.Id,
                                            SessionId = "S" + j,
                                            LaboratorioId = datosSedes.Code + "_" + i,
                                            SustentanteId = idsustentante.Id.Value
                                        });
                                        listanueva.RemoveAll(x => x.Id == idsustentante.Id);
                                    }
                                }
                            }

                            insertMasiveData(datosSedesAsignacions.ToList());
                            await db.SaveChangesAsync();
                        }
                        return Json(new { result = "", message = "Se creo con exito las sedes", status = "success" }, JsonRequestBehavior.AllowGet);
                    }
                    else if (Parametro1.HasValue && Parametro1.Value == 4) //PARROQUIAS
                    {

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

            var cont = 0;
            var cont1 = 2;

            foreach (var item in listasedes.OrderBy(o => o.Code))
            {
                cont++;
                ws1.Cell(cont1, 1).Value = cont;
                ws1.Cell(cont1, 2).Value = item.FechaCreacion.ToString("dd/MM/yyyy");
                ws1.Cell(cont1, 3).Value = item.Asignacion.NombreProceso.Description ?? null;
                ws1.Cell(cont1, 4).Value = item.Code;
                ws1.Cell(cont1, 5).Value = item.Description;
                ws1.Cell(cont1, 6).Value = item.NumeroLaboratorio;
                ws1.Cell(cont1, 7).Value = item.NumeroSession;
                ws1.Cell(cont1, 8).Value = item.coordenada_lat;
                ws1.Cell(cont1, 9).Value = item.coordenada_lng;
                ws1.Cell(cont1, 10).Value = item.NumeroTotalSustentantes;
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
            public string DatosPersonales { get; set; }
            public string sexo { get; set; }
            public string grado { get; set; }
            public string paralelo { get; set; }
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
        }
        public ActionResult ExportarPorAsignacionExcel(Guid? Id)
        {
            string NombreDocumento = "ReportePorAsignacion" + DateTime.Now;
            XLWorkbook wb = new XLWorkbook();
            var ws1 = wb.Worksheets.Add("Reportes");

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

            ws1.Cell("I1").Value = "DatosPersonales";
            ws1.Cell("I1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("I1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("I1").Style.Font.Bold = true;

            ws1.Cell("J1").Value = "Sexo";
            ws1.Cell("J1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("J1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("J1").Style.Font.Bold = true;

            ws1.Cell("k1").Value = "Grado";
            ws1.Cell("k1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("k1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("k1").Style.Font.Bold = true;

            ws1.Cell("L1").Value = "Paralelo";
            ws1.Cell("L1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("L1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("L1").Style.Font.Bold = true;

            ws1.Cell("M1").Value = "Pais Nacimiento";
            ws1.Cell("M1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("M1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("M1").Style.Font.Bold = true;

            ws1.Cell("N1").Value = "Provincia Nacimiento";
            ws1.Cell("N1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("N1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("N1").Style.Font.Bold = true;

            ws1.Cell("O1").Value = "Discapacidad";
            ws1.Cell("O1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("O1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("O1").Style.Font.Bold = true;

            ws1.Cell("P1").Value = "Tipo Discapacidad";
            ws1.Cell("P1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("P1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("P1").Style.Font.Bold = true;

            ws1.Cell("Q1").Value = "Porcentaje de Discapacidad";
            ws1.Cell("Q1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("Q1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("Q1").Style.Font.Bold = true;

            ws1.Cell("R1").Value = "Correo Sustentante";
            ws1.Cell("R1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("R1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("R1").Style.Font.Bold = true;

            ws1.Cell("S1").Value = "Teléfono Sustentante";
            ws1.Cell("S1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("S1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("S1").Style.Font.Bold = true;

            ws1.Cell("T1").Value = "Teléfono Sustentante Secundario";
            ws1.Cell("T1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("T1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("T1").Style.Font.Bold = true;

            ws1.Cell("U1").Value = "Celular Sustentante";
            ws1.Cell("U1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("U1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("U1").Style.Font.Bold = true;

            ws1.Cell("V1").Value = "Jornada";
            ws1.Cell("V1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("V1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("V1").Style.Font.Bold = true;

            ws1.Cell("W1").Value = "Saber";
            ws1.Cell("W1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("W1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("W1").Style.Font.Bold = true;

            ws1.Cell("X1").Value = "Amie";
            ws1.Cell("X1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("X1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("X1").Style.Font.Bold = true;

            ws1.Cell("Y1").Value = "Nombre Institución";
            ws1.Cell("Y1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("Y1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("Y1").Style.Font.Bold = true;

            ws1.Cell("Z1").Value = "Provincia Id";
            ws1.Cell("Z1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("Z1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("Z1").Style.Font.Bold = true;

            ws1.Cell("AA1").Value = "Provincia";
            ws1.Cell("AA1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("AA1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("AA1").Style.Font.Bold = true;

            ws1.Cell("AB1").Value = "Cantón Id";
            ws1.Cell("AB1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("AB1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("AB1").Style.Font.Bold = true;

            ws1.Cell("AC1").Value = "Cantón";
            ws1.Cell("AC1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("AC1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("AC1").Style.Font.Bold = true;

            ws1.Cell("AD1").Value = "Parroquia Id";
            ws1.Cell("AD1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("AD1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("AD1").Style.Font.Bold = true;

            ws1.Cell("AE1").Value = "Parroquia";
            ws1.Cell("AE1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("AE1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("AE1").Style.Font.Bold = true;

            ws1.Cell("AF1").Value = "Cuircuito Id";
            ws1.Cell("AF1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("AF1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("AF1").Style.Font.Bold = true;

            ws1.Cell("AG1").Value = "Circuito";
            ws1.Cell("AG1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("AG1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("AG1").Style.Font.Bold = true;

            ws1.Cell("AH1").Value = "Distrito Id";
            ws1.Cell("AH1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("AH1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("AH1").Style.Font.Bold = true;

            ws1.Cell("AI1").Value = "Distrito";
            ws1.Cell("AI1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("AI1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("AI1").Style.Font.Bold = true;

            ws1.Cell("AJ1").Value = "Zona Id";
            ws1.Cell("AJ1").Style.Fill.BackgroundColor = XLColor.FromArgb(54, 127, 220);
            ws1.Cell("AJ1").Style.Font.FontColor = XLColor.FromArgb(255, 255, 255);
            ws1.Cell("AJ1").Style.Font.Bold = true;

            var cont = 0;
            var cont1 = 2;

            foreach (var item in porAsignaciones.OrderBy(o => o.Code))
            {
                cont++;
                ws1.Cell(cont1, 1).Value = cont;
                ws1.Cell(cont1, 2).Value = item.Code;
                ws1.Cell(cont1, 3).Value = item.Description;
                ws1.Cell(cont1, 4).Value = item.LaboratorioId;
                ws1.Cell(cont1, 5).Value = item.SessionId;
                ws1.Cell(cont1, 6).Value = item.usu_id;
                ws1.Cell(cont1, 7).Value = item.tipo_identificacion;
                ws1.Cell(cont1, 8).Value = item.identificacion;
                ws1.Cell(cont1, 9).Value = item.DatosPersonales;
                ws1.Cell(cont1, 10).Value = item.sexo;
                ws1.Cell(cont1, 11).Value = item.grado;
                ws1.Cell(cont1, 12).Value = item.paralelo;
                ws1.Cell(cont1, 13).Value = item.pais_nacimiento;
                ws1.Cell(cont1, 14).Value = item.provincia_nacimiento;
                ws1.Cell(cont1, 15).Value = item.discapacidad;
                ws1.Cell(cont1, 16).Value = item.tipo_discapacidad;
                ws1.Cell(cont1, 17).Value = item.porcentaje_discapacidad;
                ws1.Cell(cont1, 18).Value = item.correo_sustentante;
                ws1.Cell(cont1, 19).Value = item.telefono_sustentante;
                ws1.Cell(cont1, 20).Value = item.telefono_sustentante_secundario;
                ws1.Cell(cont1, 21).Value = item.celular_sustentante;
                ws1.Cell(cont1, 22).Value = item.jornada_sustentante;
                ws1.Cell(cont1, 23).Value = item.saber;
                ws1.Cell(cont1, 24).Value = item.amie;
                ws1.Cell(cont1, 25).Value = item.nombre_institucion;
                ws1.Cell(cont1, 26).Value = item.id_provincia;
                ws1.Cell(cont1, 27).Value = item.provincia;
                ws1.Cell(cont1, 28).Value = item.canton_id;
                ws1.Cell(cont1, 29).Value = item.canton;
                ws1.Cell(cont1, 30).Value = item.id_parroquia;
                ws1.Cell(cont1, 31).Value = item.parroquia;
                ws1.Cell(cont1, 32).Value = item.id_circuito;
                ws1.Cell(cont1, 33).Value = item.circuito;
                ws1.Cell(cont1, 34).Value = item.id_distrito;
                ws1.Cell(cont1, 35).Value = item.distrito;
                ws1.Cell(cont1, 36).Value = item.id_zona;
                cont1++;
            }

            ws1.Cell(1, 13).Style.Alignment.WrapText = true;

            ws1.Columns().AdjustToContents();

            return new ExcelResult(wb, NombreDocumento + DateTime.Now.ToString("dd/MM/yyyy"));
        }


    }
}