using System;
using System.Collections.Generic;

namespace LucAdm.DataGen
{
    public class EnvironmentAttribute : Attribute
    {
        public EnvironmentAttribute(params EnvironmentEnum[] environments)
        {
            Environments = environments;
        }

        public IList<EnvironmentEnum> Environments { get; private set; }
    }
}