﻿using scrimp.Entities;
using System;
using System.Collections.Generic;

namespace scrimp.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public IEnumerable<Category> Children { get; set; }
        public int? ParentId { get; set; }
        public bool IsTransfer { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
