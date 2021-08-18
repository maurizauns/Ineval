using System.Collections.Generic;
namespace Ineval.Models.Filters
{
    interface IFilter
    {
        IEnumerable<FieldFilter> Filters { get; }
    }
}
