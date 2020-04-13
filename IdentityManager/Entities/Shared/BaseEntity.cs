﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityManager.Entities.Shared
{
    /// <summary>
    /// Rather than adding date info on each entity, create a base class to use
    /// </summary>
    public class BaseEntity
    {
        public DateTime Created { get; set; }
    }
}