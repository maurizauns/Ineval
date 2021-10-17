using AutoMapper;
using Ineval.BO;
using Ineval.DAL;
using Ineval.Dto;
using Ineval.Dto.Dto.Procesos;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        #region ListasGenericas
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

            if (Id.HasValue)
            {
                ParametrosIniciales parametrosIniciales = await db.ParametrosIniciales.Where(x => x.AsignacionId == Id).FirstOrDefaultAsync();
                ParametrosInicialesViewModel parametrosInicialesDTO = Mapper.Map<ParametrosInicialesViewModel>(parametrosIniciales);

                List<DatosInstituciones> datosInstituciones = new List<DatosInstituciones>();
                List<DatosInstitucionesViewModel> resultDTO = new List<DatosInstitucionesViewModel>();
                datosInstituciones = await db.DatosInstituciones.Where(x => x.AsignacionId == Id).ToListAsync();
                resultDTO = Mapper.Map<List<DatosInstitucionesViewModel>>(datosInstituciones);

                List<DatosTemporales> resultTemporales = new List<DatosTemporales>();
                List<DatosTemporalesViewModel> datosTemporalesDTO = new List<DatosTemporalesViewModel>();
                resultTemporales = await db.DatosTemporales.Where(x => x.AsignacionId == Id).ToListAsync();
                datosTemporalesDTO = Mapper.Map<List<DatosTemporalesViewModel>>(resultTemporales);

                List<Posiciones> pos = new List<Posiciones>();

                if (Parametro1.HasValue && Parametro1.Value == 1)
                {
                }
                else if (Parametro1.HasValue && Parametro1.Value == 2)
                {
                    var gruopbyprovince = from d in db.DatosTemporales
                                          where d.AsignacionId == Id
                                          group d by new { d.id_provincia, d.provincia } into Provincias
                                          select new
                                          {
                                              id_Provincias = Provincias.Key.id_provincia,
                                              Provincia = Provincias.Key.provincia
                                          };

                    foreach (var item in gruopbyprovince)
                    {

                        try
                        {
                            ApiPosicionGeografica.Root weatherForecast = await ApiPosicionGeografica.GetByPosicionGeografica(item.Provincia, usuario.APIKEY);
                            //List< ApiPosicionGeografica.Root > ss=

                            for (int i = 0; i < weatherForecast.features.Count; i++)
                            {
                                for (int j = 0; j < weatherForecast.features[i].center.Count; j++)
                                {
                                    pos.Add(new Posiciones
                                    {
                                        NombreProvincia = item.Provincia.ToString(),
                                        lat = weatherForecast.features[i].center[j],
                                        lng = weatherForecast.features[i].center[j + 1]
                                    });
                                    break;
                                }
                                break;
                            }
                        }
                        catch (Exception)
                        {
                            return Json(new { result = "", state = false }, JsonRequestBehavior.AllowGet);
                        }
                        finally
                        {
                            usuarioService.Dispose();
                        }
                    }

                }





                //List<ConteoDatos> conteoDatos = new List<ConteoDatos>();

                //if (resultDTO.Count() > 0)
                //{
                //    if (Parametro1.Value == 1)  
                //    {
                //        foreach (var item in resultDTO)
                //        {
                //            var resultbyAmie = datosTemporalesDTO.Where(x => x.amie == item.Amie).ToList();
                //            if (resultbyAmie.Count() > 0)
                //            {
                //                int numlab = 1;
                //                int sessiones = 1;
                //                if (parametrosInicialesDTO != null)///CONFIGURACION DE PARAMETROS INICIALES
                //                {
                //                    if (resultbyAmie.Count() == 0)
                //                    {

                //                    }
                //                    else { }
                //                }
                //                else
                //                {

                //                }                                
                //            }
                //            else
                //            {
                //                conteoDatos.Add(new ConteoDatos
                //                {
                //                    Amie = item.Amie,
                //                    NombreInstitucion = item.NombreInstitucion,
                //                    NumeroSustentates = resultbyAmie.Count(),
                //                    NumeroLaboratorios = resultbyAmie.Count(),
                //                    NumeroSessiones = resultbyAmie.Count()
                //                });
                //            }
                //        }
                //    }else if (Parametro2.HasValue)
                //    {
                //        if (Parametro2.Value == 1)
                //        {

                //        }
                //    }

                //    return Json(new { conteoDatos }, JsonRequestBehavior.AllowGet);

                //}
                //else
                //{

                //}
            }
            else
            {

            }

            return Json(new { }, JsonRequestBehavior.AllowGet);
        }
    }
}