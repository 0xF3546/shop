using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Core.Extensions
{
    public interface IPageable
    {
        public int Page {  get; }
        public int PageSize { get; }
    }
}
