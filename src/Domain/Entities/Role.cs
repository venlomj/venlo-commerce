using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Constants;

namespace Domain.Entities
{
    public class Role : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; init; }

        // Navigation property
        public List<UserRole> UserRoles { get; set; } = new();

        // Predefined roles using static properties
        public static Role Admin => new() { Id = RoleConstants.AdminId, Name = RoleConstants.Admin, DateCreated = DateTimeOffset.UtcNow };
        public static Role Vendor => new() { Id = RoleConstants.VendorId, Name = RoleConstants.Vendor, DateCreated = DateTimeOffset.UtcNow };
        public static Role Customer => new() { Id = RoleConstants.CustomerId, Name = RoleConstants.Customer, DateCreated = DateTimeOffset.UtcNow };
        public static Role Guest => new() { Id = RoleConstants.GuestId, Name = RoleConstants.Guest, DateCreated = DateTimeOffset.UtcNow };
    }
}
