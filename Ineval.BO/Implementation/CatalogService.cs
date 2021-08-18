using RP.DAL.Repository;
using Ineval.DAL;
using System.Threading.Tasks;

namespace Ineval.BO
{
    public abstract class CatalogService<TEntity> : EntityService<TEntity> where TEntity : GeneralConfigurationBase
    {
        protected CatalogService()
        {

        }

        protected CatalogService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override async Task<SaveResult> SaveAsync(TEntity entity)
        {
            var result = FirstOrDefault(e => e.Id != entity.Id && e.Code == entity.Code);

            if (result != null)
            {
                return SaveResult.Failed(new[] { string.Format("Código: {0} ya asignado a {1} ", result.Code, result.Description) });
            }

            return await base.SaveAsync(entity);
        }
    }
}
