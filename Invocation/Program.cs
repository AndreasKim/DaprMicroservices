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
using System;
using System.Threading;
using System.Threading.Tasks;
using GrpcServiceSample.Generated;

namespace Samples.Client
{
    class Program
    {

        static async Task<int> Main(string[] args)
        {
            using var client = new DaprClientBuilder().Build();
            var deposit = new GrpcServiceSample.Generated.ProductDto() { Description = "17" };
            var account = client.InvokeMethodGrpcAsync<GrpcServiceSample.Generated.ProductDto, ProductDto>("grpcsample", "createproduct", deposit, new CancellationToken()).Result;

            return 1;
        }
    }
}
