using System;
using System.Collections.Generic;
using Amazon.CDK;
using Amazon.CDK.AWS.APIGateway;
using Amazon.CDK.AWS.Lambda;

namespace Infrastructure
{
    public class MessagesServices : Construct
    {
        public MessagesServices(Construct scope, string id) : base(scope, id)
        {
            var getMessageHandler = new Function(this, "GetMessage", new FunctionProps
            {
                Runtime = Runtime.DOTNET_CORE_3_1,
                Code = Code.FromAsset("lambdas/GetMessage/publish"),
                Handler = "GetMessage::GetMessage.Function::FunctionHandler",
            });

            var postMessageHandler = new Function(this, "PostMessage", new FunctionProps
            {
                Runtime = Runtime.DOTNET_CORE_3_1,
                Code = Code.FromAsset("lambdas/PostMessage/publish"),
                Handler = "PostMessage::PostMessage.Function::FunctionHandler",
            });

            var api = new RestApi(this, "Messages-API", new RestApiProps
            {
                RestApiName = "Messages Service",
                Description = "This service services messages."
            });

            var getMessageIntegration = new LambdaIntegration(getMessageHandler, new LambdaIntegrationOptions
            {
                RequestTemplates = new Dictionary<string, string>
                {
                    ["application/json"] = "{ \"statusCode\": \"200\" }"
                },
            });

            var postMessageIntegration = new LambdaIntegration(postMessageHandler, new LambdaIntegrationOptions
            {
                RequestTemplates = new Dictionary<string, string>
                {
                    ["application/json"] = "{ \"statusCode\": \"200\" }"
                }
            });

            api.Root.AddMethod("GET", getMessageIntegration);
            api.Root.AddMethod("POST", postMessageIntegration);
        }
    }
}
