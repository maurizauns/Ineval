using Ineval.BO;
using Ineval.DAL;

namespace Ineval.Controllers
{
    public class CountryController : BaseConfiguracionGeneralController<Country>
    {
        public CountryController()
        {
            EntityService = new CountryService();
            Title = "Paises";
        }
    }
}