using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty!;
        public string FirstName { get; set; } = string.Empty!;
        public string LastName { get; set; } = string.Empty!;
        public string Phone { get; set; } = string.Empty!;
        public string PasswordHash { get; set; } = string.Empty!;
    }
}
