using System.Collections.Generic;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Calculator.Models;
using Newtonsoft.Json;
using System.Linq;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Calculator
{
    public class AdditionFunction
    {
        public APIGatewayProxyResponse FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var numberList = JsonConvert.DeserializeObject<NumberList>(request.Body);

            var result = new
            {
                sum = numberList.Numbers.Sum()
            };

            var response = new APIGatewayProxyResponse
            {
                StatusCode = 400,
                Body = JsonConvert.SerializeObject(result),
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
