// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Google.Protobuf;

namespace Common.Tests
{
    public class AutoMockDataAttribute : AutoDataAttribute
    {
        public AutoMockDataAttribute()
            : base(() => new Fixture().Customize(new AutoMockDataConfiguration()))
        {
        }
    }

    public class AutoMockDataConfiguration : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<IMessage>(p => p.OmitAutoProperties());
            fixture.Customize(new AutoMoqCustomization() { ConfigureMembers = true });
        }
    }
}
