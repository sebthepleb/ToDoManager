﻿using System;

namespace Models
{
    public class BaseModel
    {
        public int? Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string UpdateUsername { get; set; }
    }
}