﻿using System.Collections.Generic;

namespace KnockAdm.DataGen
{
    public class Data<T> where T : Entity, new()
    {
        public virtual IEnumerable<T> GetGeneratedData(EnvironmentEnum environment)
        {
            return null;
        }
    }
}