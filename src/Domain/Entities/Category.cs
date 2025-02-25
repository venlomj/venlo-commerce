﻿using Domain.Common;

namespace Domain.Entities
{
    public class Category : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public List<Product> Products { get; set; } = [];
    }
}
