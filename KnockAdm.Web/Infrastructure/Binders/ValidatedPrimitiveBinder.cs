using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;

namespace KnockAdm.Web
{
    public class ValidatedPrimitiveBinder<T> : HttpParameterBinding where T : struct
    {
        public ValidatedPrimitiveBinder(HttpParameterDescriptor descriptor)
            : base(descriptor)
        {
        }

        public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext actionContext,
            CancellationToken cancellationToken)
        {
            var value = actionContext.RequestContext.RouteData.Values[Descriptor.ParameterName];
            var validatedPrimitive = Activator.CreateInstance(typeof (Validated<>).MakeGenericType(typeof (T)), value);
            actionContext.ActionArguments[Descriptor.ParameterName] = validatedPrimitive;
            var tsc = new TaskCompletionSource<object>();
            tsc.SetResult(null);
            return tsc.Task;
        }
    }
}