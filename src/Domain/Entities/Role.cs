﻿using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Role : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        // Navigation property
        public List<UserRole> UserRoles { get; set; }
    }
}
