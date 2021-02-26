using Microsoft.AspNetCore.Http;
using MyLab.Log.Dsl;

namespace MyLab.Log.Ctx.Trace
{
    /// <summary>
    /// Adds to log trace id from http context
    /// </summary>
    public class HttpTraceIdLogCtx : ILogContextExtension
    {
        private readonly IHttpContextAccessor _httpContextAccessor;


        /// <summary>
        /// Initializes a new instance of <see cref="HttpTraceIdLogCtx"/>
        /// </summary>
        public HttpTraceIdLogCtx(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <inheritdoc />
        public DslExpression Apply(DslExpression dslExpression)
        {
            return dslExpression.AndLabel(TraceLogContextExtensionConstants.TraceIdLabelName,
                _httpContextAccessor.HttpContext.TraceIdentifier);
        }
    }
}