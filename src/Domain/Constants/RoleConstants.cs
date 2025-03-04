using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Constants
{
    public static class RoleConstants
    {
        public const string Admin = "Admin";
        public const string Vendor = "Vendor";
        public const string Customer = "Customer";
        public const string Guest = "Guest";

        // Fixed GUIDs to ensure consistency
        public static readonly Guid AdminId = new("d396c1a1-3f4e-4e92-9316-ff1c32e68c98");
        public static readonly Guid VendorId = new("e7145f1a-6587-45f3-9d72-182f6b3c508b");
        public static readonly Guid CustomerId = new("99db4465-4b1a-4f1f-a5c3-0b2d32707f13");
        public static readonly Guid GuestId = new("a5c1ebde-7b77-42b6-b63b-931c0d7431f3");
    }
}
