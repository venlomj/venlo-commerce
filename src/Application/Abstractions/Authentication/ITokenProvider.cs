using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Abstractions.Authentication
{
    public interface ITokenProvider
    {
        string Create(User user);
    }
}
