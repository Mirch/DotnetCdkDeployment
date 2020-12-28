using System.Collections.Generic;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Messages.Models;
using Newtonsoft.Json;


namespace Messages
{
    public class PostMessageFunction
    {

        public APIGatewayProxyResponse FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var message = JsonConvert.DeserializeObject<Message>(request.Body);

            var responseBody = new Message
            {
                From = "Post Lambda",
                Content = $"Hello, {message.From}!"
            };

            var response = new APIGatewayProxyResponse
            {
                StatusCode = 200,
                Body = JsonConvert.SerializeObject(responseBody),
                Headers = new Dictionary<string, string>
                {
                    { "Content-Type", "application/json" },
                    { "Access-Control-Allow-Origin", "*" }
                }
            };

            return response;
        }
    }
}
