using Orion.Bussines.Diger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Bussines
{
    public interface IBase<TDto, TSummary>
    {
        CommadResult Create(TDto model);
        CommadResult Update(TDto model);

        CommadResult Delete(TDto model);
        CommadResult Delete(int Id);
        TDto? GetById(int Id);
        IEnumerable<TDto> GetAll();
        TSummary? GetSummaryById(int id);
        IEnumerable<TSummary> GetSummary();
    }
}
