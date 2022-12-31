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
using Services.ProductsService.Generated;
namespace Samples.Client
{
    class Program
    {

        static async Task<int> Main(string[] args)
        {
            await Task.Delay(10000);
            for (int i = 0; i < 10; i++)
            {
                await TestCrud();
            }

            return 1;
        }

        private static async Task TestCrud()
        {
            var client = new DaprClientBuilder().Build();

            var deposit = new CreateProductRequest() { Name = "TestName", Description = "TestDescription" };
            var productId = await client.InvokeMethodGrpcAsync<CreateProductRequest, Int32Value>("productsservice", "createproduct", deposit, new CancellationToken());
            Console.WriteLine("Created Product:");
            Console.WriteLine(productId.Value);
            Console.WriteLine();

            var product = await client.InvokeMethodGrpcAsync<GetProductByIdRequest, GetProductByIdResponse>
                ("productsservice", "getproduct", new GetProductByIdRequest() { Id = productId.Value, IncludeRatings = false, IncludeSalesInfo = false }, new CancellationToken());
            Console.WriteLine("Read Product:");
            Console.WriteLine(product.Id);
            Console.WriteLine(product.Name);
            Console.WriteLine(product.Description);
            Console.WriteLine();

            await client.PublishEventAsync("pubsub", "updateproduct",
                new UpdateProductRequest() { Id = product.Id, Description = "NewDescription", Name = "NewName" });

            await Task.Delay(1000);

            var updatedProd = await client.InvokeMethodGrpcAsync<GetProductByIdRequest, GetProductByIdResponse>
                ("productsservice", "getproduct", new GetProductByIdRequest() { Id = product.Id, IncludeRatings = false, IncludeSalesInfo = false }, new CancellationToken());

            Console.WriteLine("Update Product by PubSub:");
            Console.WriteLine(updatedProd.Id);
            Console.WriteLine(updatedProd.Name);
            Console.WriteLine(updatedProd.Description);
            Console.WriteLine();

            await client.PublishEventAsync("pubsub", "deleteproduct", new DeleteProductRequest() { Id = product.Id });
            await Task.Delay(1000);
            Console.WriteLine("Delete Product by PubSub:");
            var deletedProd = await client.InvokeMethodGrpcAsync<GetProductByIdRequest, GetProductByIdResponse>
                ("productsservice", "getproduct", new GetProductByIdRequest() { Id = product.Id, IncludeRatings = false, IncludeSalesInfo = false }, new CancellationToken());
            Console.WriteLine(deletedProd.Id);
            Console.WriteLine();
        }
    }
}
