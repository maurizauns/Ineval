using AutoMapper;
using Ineval.BO;
using Ineval.Controllers;
using Ineval.DAL;
using Ineval.Dto;
using Ineval.Dto.Dto.Procesos;
using Ineval.Models.Filters;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using RP.Website.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static Ineval.Dto.ApiDriving;
using static Ineval.Dto.ApiPosicionGeografica;

namespace Ineval.Controllers
{
    [Authorize(Roles = "Administrador, Responsable_Unidad, Analista, Visitante")]
    public class AsignacionController : BaseController<Guid, Asignacion, AsignacionViewModel>
    {
        SwmContext db = new SwmContext();
        public AsignacionController()
        {
            EntityService = new AsignacionService();

            Title = "Asignación";
        }

        public override void OnBeginIndex()
        {
            List<NombreProceso> nombreProcesos = null;
            using (var nombreProcesosService = new NombreProcesoService())
            {
                nombreProcesos = nombreProcesosService.GetAll().ToList();
                ViewBag.NombreProcesoId =
                new SelectList((from s in nombreProcesos.ToList() select new { Id = s.Id, Description = "(" + s.Code + ") " + s.Description }), "Id", "Description", null);
            }
        }

        protected override IQueryable<Asignacion> ApplyFilters(IQueryable<Asignacion> generalQuery, MvcJqGrid.Rule[] filters)
        {
            if (filters == null)
            {
                return generalQuery;
            }

            foreach (var item in filters)
            {
                var term = item.data.Trim().ToUpper();

                if (String.Equals(item.field, "codigo", StringComparison.OrdinalIgnoreCase))
                {
                    generalQuery = generalQuery.Where(x => x.Code.Trim().ToUpper().Contains(term));
                }
                else if (String.Equals(item.field, "descripcion", StringComparison.OrdinalIgnoreCase))
                {
                    generalQuery = generalQuery.Where(x => x.Description.Trim().ToLower().Contains(term));
                }
            }
            return generalQuery;
        }

        protected override string[] GetRow(Asignacion item)
        {

            if (User.IsInRole("Administrador") || User.IsInRole("Responsable_Unidad"))
            {
                return new[] {
                HttpUtility.HtmlEncode(item.Code),
                HttpUtility.HtmlEncode(item.Description),
                HttpUtility.HtmlEncode(item.NombreProceso != null ? "(" + item.NombreProceso.Code + ") " + item.NombreProceso.Description : ""),
                HttpUtility.HtmlEncode(item.EstadoProceso.HasValue ? item.EstadoProceso.Value == 1 ? "Activo" : "Finalizado" : "Activo"),
                HttpUtility.HtmlEncode(GridHelperExts.ActionsList("asignacion-modal")
                        .Add(GridHelperExts.EditAction(Url.Action("GetEntity"), item.Id, "asignacionCallback"))
                        .Add(GridHelperExts.DeleteAction(Url.Action("Delete"), "asignacion-grid", item.Id))
                        .Add(ConfiguracionAction(item.Id))
                        .End())
                };

            }
            else
            {
                return new[] {
                HttpUtility.HtmlEncode(item.Code),
                HttpUtility.HtmlEncode(item.Description),
                HttpUtility.HtmlEncode(item.NombreProceso != null ? "(" + item.NombreProceso.Code + ") " + item.NombreProceso.Description : ""),
                HttpUtility.HtmlEncode(item.EstadoProceso.HasValue ? item.EstadoProceso.Value == 1 ? "Activo" : "Finalizado" : "Activo"),
                HttpUtility.HtmlEncode(GridHelperExts.ActionsList("asignacion-modal")
                        .Add(ConfiguracionAction(item.Id))
                        .End())
                };
            }


        }
        public IHtmlString ConfiguracionAction(object id = null)
        {
            var button = string.Format(@"<li class=""""><a title=""Proceso inicial"" data-toggle=""tooltip"" class=""btn btn-info btn-xs"" href=""{0}""><i class=""bx bxs-cog""></i></a></li>",
                            Url.Action("Record", "Proceso", new { id }));
            return MvcHtmlString.Create(button);
        }
        protected override AsignacionViewModel MapperEntityToModel(Asignacion entity)
        {
            return Mapper.Map<Asignacion, AsignacionViewModel>(entity);
        }

        protected override Asignacion MapperModelToEntity(AsignacionViewModel viewModel)
        {
            var asignacion = new Asignacion();
            if (viewModel.Id != null && viewModel.Id != Guid.Empty)
            {
                asignacion = EntityService.GetById(viewModel.Id.Value);
            }
            return Mapper.Map(viewModel, asignacion);
        }

        public async Task<ActionResult> GetFormulario(Guid? id)
        {
            //var procesoService = new NombreProcesoService();
            List<NombreProceso> nombreProceso = new List<NombreProceso>();
            using (var procesoService = new NombreProcesoService())
            {
                nombreProceso = procesoService.GetAll().OrderBy(o => o.Description).ToList();
            }
            return Json(new { procesosList = nombreProceso }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> UpdateEstadoProceso(Guid? Id)
        {
            bool estado = false;
            Asignacion asignacions = new Asignacion();

            asignacions = await EntityService.FirstOrDefaultAsync(x => x.Id == Id);

            asignacions.EstadoProceso = 3;

            var result = await EntityService.UpdateAsync(asignacions);
            if (result.Succeeded)
            {
                estado = true;
            }

            return Json(new { message = "Asignación Finalizada Correctamente", status = estado }, JsonRequestBehavior.AllowGet);

        }

        public override IEnumerable<FieldFilter> Filters
        {
            get
            {
                var filters = new List<FieldFilter>
                {
                    new FieldFilter
                    {
                        Description = "Descripción",
                        Name = "descripcion",
                        Type = FilterType.Textbox
                    },
                    new FieldFilter
                    {
                        Description = "Código",
                        Name = "codigo",
                        Type = FilterType.Textbox
                    }
                };
                return filters;
            }
        }

        [AllowAnonymous]
        public async Task<JsonResult> GetValues(Guid? id)
        {
            try
            {
                var elements = await EntityService.GetAllAsync();

                var result = await elements.Where(q => q.Id == id).Select(q => new
                {
                    value = q.Id,
                    text = q.Description
                }).ToListAsync();

                return Json(new
                {
                    success = true,
                    values = (object)result
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new
                {
                    success = false,
                    values = default(object)
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> TestApi()
        {
            UsuarioService usuarioService = new UsuarioService();
            try
            {
                var userId = User.Identity.GetUserId();
                var usuario = usuarioService.ObtenerPorApplicationUserId(userId);

                ApiCycling.Root weatherForecast = await ApiCycling.GetByCycling("-122.42,37.78", "-77.03,38.91", usuario.APIKEY);

                return Json(new { result = weatherForecast, state = true }, JsonRequestBehavior.AllowGet);
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

        public async Task<ActionResult> SaveAsignacion(AsignacionViewModel model)
        {
            var userId = User.Identity.GetUserId();

            int edit = 0;


            OnBeginCrudAction();

            if (!ModelState.IsValid)
            {
                return await Task.Run(() => Json(new { success = false, message = GetValidationMessages() }, JsonRequestBehavior.AllowGet));
            }

            try
            {
                if (model.Id != null)
                {
                    edit = 1;
                }
                else
                {
                    model.EstadoProceso = 1;
                }

                var entity = MapperModelToEntity(model);

                var saveResult = await EntityService.SaveAsync(entity);

                if (saveResult.Succeeded)
                {
                    ParametrosInicialesService entityparam = new ParametrosInicialesService();
                    var paraexiste = await entityparam.GetAll().Where(x => x.AsignacionId == entity.Id).SingleOrDefaultAsync();
                    if (paraexiste != null)
                    {

                    }
                    else
                    {
                        TimeSpan HoraMaxima = TimeSpan.Parse("19:00");
                        TimeSpan HoraInicio = TimeSpan.Parse("08:00");
                        TimeSpan HoraFin = new TimeSpan();
                        TimeSpan TiempoEvaluacion = TimeSpan.Parse("02:00");
                        TimeSpan TiempoReceso = TimeSpan.Parse("01:00");
                        TimeSpan TiempoReal = TiempoEvaluacion + TiempoReceso;

                        int NumeroSessiones = 1;

                        DateTime HOY = DateTime.Now;

                        TimeSpan HoraSession = HoraInicio + TiempoReal;

                        while (HoraSession <= HoraMaxima)
                        {

                            NumeroSessiones += 1;
                            HoraSession += TiempoReal;
                            HoraFin = HoraSession - TiempoReceso;
                        };

                        ParametrosIniciales result = new ParametrosIniciales
                        {
                            AsignacionId = entity.Id,
                            HoraMaxima = HoraMaxima.ToString(),
                            HoraInicio = HoraInicio.ToString(),
                            HoraFin = HoraFin.ToString(),
                            TiempoEvaluacion = TiempoEvaluacion.ToString(),
                            TiempoReceso = TiempoReceso.ToString(),
                            TiempoReal = TiempoReal.ToString(),
                            SiNoNumerosSesiones = true,
                            NumerosSesiones = NumeroSessiones,
                            SiNoNumeroEquipos = true,
                            NumeroEquipos = 20,
                            SiNoNumeroDiasEvaluar = true,
                            NumeroDiasEvaluar = 1,
                            SiNoNumeroLaboratorios = true,
                            NumeroLaboratorios = 5,
                            SiNoTiempoViaje = true,
                            TiempoViaje = 60,
                            Tipo = 1,
                            FechaSesion = DateTime.Today,
                            HorariosSesion = "[]"
                        };

                        var saveresultparam = await entityparam.SaveAsync(result);
                    }

                    if (edit == 1)
                    {
                        bool status = await EnvioCorreos.SendAsync(userId, "Se modifico un proceso de Asignación.");
                    }
                    else
                    {
                        bool status = await EnvioCorreos.SendAsync(userId, "Se creo un nuevo proceso de Asignación.");
                    }

                    return await Task.Run(() => Json(new { success = true, message = string.Empty }, JsonRequestBehavior.AllowGet));


                }

                return await Task.Run(() => Json(new { success = false, message = saveResult.GetErrorsString() }, JsonRequestBehavior.AllowGet));
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        public async Task<ActionResult> TestApi2()
        {
            UsuarioService usuarioService = new UsuarioService();
            try
            {
                var userId = User.Identity.GetUserId();
                var datosMapboxAPIKEYs = await db.DatosMapboxAPIKEY.ToListAsync();

                ApiDriving.Root weatherForecast = new ApiDriving.Root();
                foreach (var item in datosMapboxAPIKEYs)
                {
                    weatherForecast = await ApiDriving.GetByDriving("-78.29870190300,-0.11483729100", "-78.34341925700,-0.05793245000", item.APIKEY.Trim());
                }


                return Json(new { result = weatherForecast, state = true }, JsonRequestBehavior.AllowGet);
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

        public async Task<ActionResult> TestApi3()
        {
            UsuarioService usuarioService = new UsuarioService();
            try
            {
                var userId = User.Identity.GetUserId();
                var datosMapboxAPIKEYs = await db.DatosMapboxAPIKEY.ToListAsync();

                ApiPosicionGeografica.Root weatherForecast = new ApiPosicionGeografica.Root();

                foreach (var item in datosMapboxAPIKEYs)
                {
                    weatherForecast = await ApiPosicionGeografica.GetByPosicionGeografica("Yantzaza, Zamora-Chinchipe,Ecuador", item.APIKEY);
                }

                return Json(new { result = weatherForecast, state = true }, JsonRequestBehavior.AllowGet);
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

        public class Posicion
        {
            public string Lng { get; set; }
            public string Lat { get; set; }
        }
        public async Task<ActionResult> Test(Guid? Id)
        {
            List<DatosInstituciones> lista = new List<DatosInstituciones>();
            //Guid? id = "ef7fe99a-0f23-ec11-a5dc-50e0857d5969";
            lista = await db.DatosInstituciones.Where(x => x.AsignacionId == Id && x.provincia == "Pichincha").ToListAsync();
            var jsonResult = Json(new { result = lista }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public async Task<ActionResult> prueba()
        {
            TimeSpan HoraMaxima = TimeSpan.Parse("19:00:00");
            TimeSpan HoraInicio = TimeSpan.Parse("08:00:00");
            TimeSpan HoraFin = new TimeSpan();
            TimeSpan TiempoEvaluacion = TimeSpan.Parse("02:00:00");
            TimeSpan TiempoReceso = TimeSpan.Parse("01:00:00");
            TimeSpan TiempoReal = TiempoEvaluacion + TiempoReceso;

            int NumeroSessiones = 1;

            TimeSpan HoraSession = HoraInicio + TiempoReal;

            while (HoraSession <= HoraMaxima)
            {

                NumeroSessiones += 1;
                HoraSession += TiempoReal;
                HoraFin = HoraSession - TiempoReceso;
            }



            string ss = TiempoReal.ToString();


            int toalasu = 150;
            int subtotal = 0;



            return null;
        }


        public async Task<ActionResult> EnviarCorreo()
        {
            MailMessage msg = new MailMessage();

            msg.To.Add("r.caiza@reliv.la");
            msg.Subject = "test de prueba";
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            //msg.Bcc.add

            msg.Body = "Hola";
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = true;
            msg.From = new System.Net.Mail.MailAddress("crissrobert0984@gmail.com");
            System.Net.Mail.SmtpClient cliete = new System.Net.Mail.SmtpClient();

            cliete.Port = 587;
            cliete.EnableSsl = true;

            cliete.Host = "smtp.gmail.com";
            cliete.Credentials = new NetworkCredential("crissrobert0984@gmail.com", "r1o2b3e4r5t6o7");


            try
            {
                cliete.Send(msg);
                return Json("Hola", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);

            }
        }


        //public static List<double> GetListOfRandomDoubles(int countOfNumbers, double totalSum, int digits)
        //{
        //    Random r = new Random();

        //    List<double> randomDoubles = new List<double>();
        //    double totalRandomSum = 0;

        //    for (int i = 0; i < countOfNumbers; i++)
        //    {
        //        double nextDouble = r.NextDouble();
        //        randomDoubles.Add(nextDouble);
        //        totalRandomSum += nextDouble;
        //    }

        //    double totalFactor = 1 / totalRandomSum;
        //    totalFactor = totalFactor * totalSum;

        //    for (int i = 0; i < randomDoubles.Count; i++)
        //    {
        //        randomDoubles[i] = randomDoubles[i] * totalFactor;
        //        randomDoubles[i] = Math.Round(randomDoubles[i], digits);
        //    }

        //    double currentRandomSum = 0;
        //    randomDoubles.ForEach(x => currentRandomSum += x);
        //    randomDoubles[0] += totalSum - currentRandomSum;

        //    return randomDoubles;
        //}

        //List<double> randomDoubles = GetListOfRandomDoubles(7, 100, 2);

        //public static List<double> GetListOfRandomDoubles(int countOfNumbers, double totalSum, int digits, int limite)
        //{
        //    Random r = new Random();

        //    List<double> randomDoubles = new List<double>();
        //    double suma = 0;
        //    //do
        //    //{

        //    double totalRandomSum = 0;
        //    randomDoubles = new List<double>();
        //    suma = 0;
        //    for (int i = 0; i < countOfNumbers; i++)
        //    {
        //        double nextDouble = r.NextDouble();
        //        randomDoubles.Add(nextDouble);
        //        totalRandomSum += nextDouble;
        //    }

        //    double totalFactor = 1 / totalRandomSum;
        //    totalFactor = totalFactor * totalSum;

        //    for (int i = 0; i < randomDoubles.Count; i++)
        //    {
        //        randomDoubles[i] = randomDoubles[i] * totalFactor;
        //        randomDoubles[i] = Math.Round(randomDoubles[i], digits);

        //        if (randomDoubles[i] > limite)
        //        {
        //            randomDoubles[i] = limite;
        //        }
        //    }

        //    double currentRandomSum = 0;
        //    randomDoubles.ForEach(x => currentRandomSum += x);

        //    double faltanteTotal = totalSum - currentRandomSum;
        //    int div = 0;
        //    double promedioTotal = 0;
        //    int sumaFinal = 0;
        //    double algo = randomDoubles.Min() + faltanteTotal;
        //    if (algo > limite)
        //    {
        //        div = ((int)(faltanteTotal / limite));
        //        div++;
        //        promedioTotal = faltanteTotal / div;
        //        double promedioTruncado = Math.Truncate(promedioTotal);
        //        int productoFinal = (int)(promedioTruncado * div);
        //        sumaFinal = (int)(faltanteTotal - productoFinal);
        //    }
        //    else
        //    {
        //        div = 1;
        //        promedioTotal = faltanteTotal;
        //    }

        //    for (int j = 0; j < div; j++)
        //    {
        //        bool conf = true;
        //        for (int i = 0; i < randomDoubles.Count; i++)
        //        {
        //            if (conf)
        //            {
        //                //double faltante = totalSum - currentRandomSum;
        //                //double sumaFaltante = randomDoubles[i] + faltante;

        //                double sumaFaltante = randomDoubles[i] + promedioTotal;
        //                if (sumaFaltante <= limite)
        //                {
        //                    randomDoubles[i] = Math.Truncate(sumaFaltante);
        //                    conf = false;
        //                    break;
        //                }
        //            }

        //        }
        //    }

        //    if (sumaFinal > 0)
        //    {
        //        int div1 = ((int)(sumaFinal / limite));
        //        div1++;
        //        double promedioTotal1 = sumaFinal / div1;
        //        double promedioTruncado1 = Math.Truncate(promedioTotal1);
        //        int productoFinal1 = (int)(promedioTruncado1 * div1);
        //        int sumaFinal1 = (int)(sumaFinal - productoFinal1);

        //        for (int j = 0; j < div1; j++)
        //        {
        //            for (int i = 0; i < randomDoubles.Count; i++)
        //            {
        //                double sumaFaltante = randomDoubles[i] + promedioTotal1;
        //                if (sumaFaltante <= limite)
        //                {
        //                    randomDoubles[i] = Math.Truncate(sumaFaltante);
        //                    break;
        //                }
        //            }
        //        }

        //        if (sumaFinal1 > 0)
        //        {
        //            for (int j = 0; j < div1; j++)
        //            {
        //                for (int i = 0; i < randomDoubles.Count; i++)
        //                {
        //                    double sumaFaltante = randomDoubles[i] + sumaFinal1;
        //                    if (sumaFaltante <= limite)
        //                    {
        //                        randomDoubles[i] = Math.Truncate(sumaFaltante);
        //                        break;
        //                    }
        //                }
        //            }
        //        }

        //    }


        //    suma = randomDoubles.Sum();
        //    //} while (suma != totalSum);




        //    return randomDoubles;
        //}

        //public static List<Double> testpryeba(int countOfNumbers, double totalSum, int digits, int limite)
        //{
        //    Random rnd = new Random();
        //    List<Double> x = new List<double>();

        //    for (int i = 0; i < countOfNumbers; i++)
        //    {
        //        double r = rnd.NextDouble() * 100;
        //        if (r > limite)
        //        {
        //            r = limite;

        //        }
        //        x.Add(r);
        //    }

        //    double v = x.Sum();
        //    if (v < totalSum)
        //    {
        //        double sumaMenores = 0;
        //        int contadorMenor = 0;
        //        double sumaLimites = 0;
        //        List<int> listaMenores = new List<int>();
        //        for (int i = 0; i < x.Count; i++)
        //        {

        //            if (x[i] != limite)
        //            {
        //                sumaMenores += x[i];
        //                listaMenores.Add(i);
        //                contadorMenor++;
        //            }
        //            else
        //            {
        //                sumaLimites += x[i];
        //            }
        //        }
        //        double faltanteMenores = totalSum - sumaLimites;
        //        double promedioMenor = faltanteMenores / contadorMenor;

        //        for (int i = 0; i < listaMenores.Count; i++)
        //        {
        //            x[listaMenores[i]] = Math.Round(promedioMenor, 0);
        //        }
        //    }
        //    else if (v > totalSum)
        //    {
        //        double sobrante = v - totalSum;
        //        x[countOfNumbers - 1] = x[countOfNumbers - 1] - sobrante;
        //    }
        //    return x;
        //}

        //public async Task<ActionResult> pruebass()
        //{            
        //    List<double> randomDoubles = GetListOfRandomDoubles2(3, 50, 0, 20);

        //    randomDoubles.Sort();
        //    return Json(new { p = randomDoubles, sum = randomDoubles.Sum() }, JsonRequestBehavior.AllowGet);
        //}       


    }
}