using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public class BaseEntity
    {
        public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? DateModified { get; set; }
        public DateTimeOffset? DateDeleted { get; set; }
        //public Guid? UserId { get; set; }
        public bool? IsDeleted { get; set; } = false;
    }
}
