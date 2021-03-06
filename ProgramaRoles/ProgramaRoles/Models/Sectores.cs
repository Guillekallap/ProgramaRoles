﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProgramaRoles.Models
{
    public class Sectores
    {
        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string nombre { get; set; }

        [StringLength(30)]
        public string nombreAbreviado { get; set; }

        public int orden { get; set; }

        [StringLength(7)]
        public string tipo { get; set; }

        public bool vigente { get; set; }

    }
}