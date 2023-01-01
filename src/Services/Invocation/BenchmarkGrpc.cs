using BenchmarkDotNet.Attributes;
using Dapr.Client;
using Services.ProductsService.Generated;

namespace Invocation
{
    [Config(typeof(AntiVirusFriendlyConfig))]
    public class BenchmarkGrpc
    {
        private  readonly DaprClient _client;

        public BenchmarkGrpc()
        {
            _client = new DaprClientBuilder().Build();
        }

        [Benchmark]
        public async Task SendHugeRequestToServer()
        {
            var request = new PerformanceTestRequest();
            request.IntList.AddRange(CreateIntList());
            request.StringList.AddRange(CreateStrList());
            var response = await _client.InvokeMethodGrpcAsync<PerformanceTestRequest, PerformanceTestResponse>
                ("productsservice", "performancetesthuge", request, new CancellationToken());
        }


        private static List<int> CreateIntList()
        {
            var intList = new List<int>();
            for (int i = 0; i < 150; i++)
            {
                intList.Add(123);
            }

            return intList;
        }

        private static List<string> CreateStrList()
        {
            var stringList = new List<string>();
            for (int i = 0; i < 180; i++)
            {
                stringList.Add("This is a test string. ");
            }

            return stringList;
        }
    }
}

