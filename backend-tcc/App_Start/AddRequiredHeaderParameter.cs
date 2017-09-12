using Swashbuckle.Swagger;
using System.Collections.Generic;
using System.Web.Http.Description;

namespace backend_tcc.api.App_Start
{
    public class AddRequiredHeaderParameter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters == null)
                operation.parameters = new List<Parameter>();

            if (operation.operationId != "Account_Authenticate")
            {
                operation.parameters.Add(new Parameter
                {
                    name = "Token",
                    @in = "header",
                    type = "string",
                    required = false
                });
            }
        }
    }
}