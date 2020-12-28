using System.Collections.Generic;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Messages.Models;
using Newtonsoft.Json;

namespace Messages
{
    public class GetMessageFunction
    {
        public APIGatewayProxyResponse FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var name = string.Empty;
            APIGatewayProxyResponse response = null;
            try
            {
                var nameExists = request.QueryStringParameters.TryGetValue("name", out name);

                if (nameExists)
                {
                    var responseBody = new Message
                    {
                        From = "Get Lambda",
                        Content = $"Hello, {name}!"
                    };

                    response = new APIGatewayProxyResponse
                    {
                        StatusCode = 200,
                        Body = JsonConvert.SerializeObject(responseBody),
                        Headers = new Dictionary<string, string>
                    {
                        { "Content-Type", "application/json" },
                        { "Access-Control-Allow-Origin", "*" }
                    }
                    };
                }
            }
            catch
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
