using System.Collections.Generic;
using System.Text.Json.Serialization;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Lambdas.Shared;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace GetMessage
{
    public class Function
    {

        public APIGatewayProxyResponse FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var name = string.Empty;
            var nameExists = request.QueryStringParameters.TryGetValue("name", out name);

            APIGatewayProxyResponse response;
            if (nameExists)
            {
                response = new APIGatewayProxyResponse
                {
                    StatusCode = 200,
                    Body = $"Hello, {name}!",
                    Headers = new Dictionary<string, string>
                    {
                        { "Content-Type", "application/json" },
                        { "Access-Control-Allow-Origin", "*" }
                    }
                };
            }
            else
            {
                response = new APIGatewayProxyResponse
                {
                    StatusCode = 400,
                    Body = $"Missing 'name' parameter.",
                    Headers = new Dictionary<string, string>
                    {
                        { "Content-Type", "application/json" },
                        { "Access-Control-Allow-Origin", "*" }
                    }
                };
            }

            return response;
        }
    }
}
