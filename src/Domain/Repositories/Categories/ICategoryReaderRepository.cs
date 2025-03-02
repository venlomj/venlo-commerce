using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories.Base;

namespace Domain.Repositories.Categories
{
    public interface ICategoryReaderRepository : IBaseRead<Category, int>,
        IBaseVerify<Category, int>
    {
    }
}
