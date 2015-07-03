using System;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace KnockAdm.Web
{
    public static class BindingConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.ParameterBindingRules.Add(FindDescriptor);
        }

        private static HttpParameterBinding FindDescriptor(HttpParameterDescriptor descriptor)
        {
            if (descriptor.ParameterType.IsGenericType &&
                descriptor.ParameterType.GetGenericTypeDefinition() == typeof (Validated<>))
            {
                return
                    (HttpParameterBinding)
                        Activator.CreateInstance(
                            typeof (ValidatedPrimitiveBinder<>).MakeGenericType(descriptor.ParameterType.GetGenericArguments()[0]),
                            descriptor);
            }
            return null;
        }
    }
}