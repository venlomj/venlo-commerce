﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Abstractions.Authentication
{
    public interface ITokenProvider
    {
        Task<string> Create(User user);
    }
}
