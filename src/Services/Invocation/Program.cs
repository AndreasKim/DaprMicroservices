// ------------------------------------------------------------------------
// Copyright 2021 The Dapr Authors
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//     http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ------------------------------------------------------------------------

using Dapr.Client;
using Google.Protobuf.WellKnownTypes;
using Services.ProductsService;

namespace Samples.Client
{
    class Program
    {

        static async Task<int> Main(string[] args)
        {
            while (true)
            {
                await Task.Delay(10000);
                using var client = new DaprClientBuilder().Build();
                var deposit = new CreateProductCommandDto() { Name = "Test" };
                var account = await client.InvokeMethodGrpcAsync<CreateProductCommandDto, Int32Value>("productsservice", "createproduct", deposit, new CancellationToken());
                Console.WriteLine(account.Value);
                Console.WriteLine("!!!!!");
            }


            return 1;
        }
    }
}
