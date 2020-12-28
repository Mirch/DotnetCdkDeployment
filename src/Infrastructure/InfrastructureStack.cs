using Amazon.CDK;

namespace Infrastructure
{
    public class InfrastructureStack : Stack
    {
        internal InfrastructureStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            new MessagesService(this, "MessagesService");
            new CalculatorService(this, "CalculatorService");
        }
    }
}
