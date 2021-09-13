using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;

namespace PaymentService.API.Configurations
{
    /// <summary>
    /// The class to add the authorization header.
    /// </summary>
    public class AddAuthHeaderParamOpFilter : IOperationFilter
    {
        /// <summary>
        /// Applies the operation filter.
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="schemaRegistry"></param>
        /// <param name="apiDescription"></param>
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters != null)
            {
                operation.parameters.Add(new Parameter
                {
                    name = "Authorization",
                    @in = "header",
                    description = "access token",
                    required = false,
                    type = "string"
                });
            }
        }
    }
}