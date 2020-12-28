using System;
using System.Collections.Generic;
using Amazon.CDK;
using Amazon.CDK.AWS.APIGateway;
using Amazon.CDK.AWS.Lambda;

namespace Infrastructure
{
    public class CalculatorService : Construct
    {
        public CalculatorService(Construct scope, string id) : base(scope, id)
        {
            var additionHandler = new Function(this, "Addition", new FunctionProps
            {
                Runtime = Runtime.DOTNET_CORE_3_1,
                Code = Code.FromAsset("lambdas/Calculator/publish"),
                Handler = "Calculator::Calculator.AdditionFunction::FunctionHandler",
            });

            var api = new RestApi(this, "Calculator-API", new RestApiProps
            {
                RestApiName = "Calculator Service",
                Description = "This service calculates."
            });

            var getMessageIntegration = new LambdaIntegration(additionHandler, new LambdaIntegrationOptions
            {
                RequestTemplates = new Dictionary<string, string>
                {
                    ["application/json"] = "{ \"statusCode\": \"200\" }"
                },
            });

            api.Root.AddMethod("POST", getMessageIntegration);
        }
    }
}
