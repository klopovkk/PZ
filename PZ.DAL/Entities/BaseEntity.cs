﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZ.DAL.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
