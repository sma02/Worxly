﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worxly.DTOs.Interfaces
{
    public interface IUserIdentity
    {
        public int Id { get; set; }
        public string Username { get; set; }
    }
}
