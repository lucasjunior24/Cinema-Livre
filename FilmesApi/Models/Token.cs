﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesApi.Data.Dtos.Usuario
{
    public class Token
    {
        public Token(string value)
        {
            Value = value;
        }
        public string Value { get; set; }
    }
}
