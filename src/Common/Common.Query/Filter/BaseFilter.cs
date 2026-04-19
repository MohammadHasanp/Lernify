using Common.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Query.Filter
{
    public class BaseFilter<TData,Tparam>:BasePaginate where Tparam :BaseFilterParam where TData :BaseDto
    {
        public List<TData>Datas{ get; set; }
        public Tparam FilterParams { get; set; }
    }

    public class BaseFilterParam
    {
        public int PageId { get; set; } = 1;
        public int Take { get; set; }=10;
    }
}
