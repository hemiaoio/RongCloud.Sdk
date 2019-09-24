using System;
using Xunit;
using Shouldly;
using Xunit.Abstractions;
using RongCloud.Server.models.user;
using System.Threading.Tasks;

namespace RongCloud.Server.Sdk.Tests
{
    public class RongCloud_Tests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private RongCloud _rongCloud;
        public RongCloud_Tests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
        [Fact]
        public void Test_GetInstance()
        {
            _rongCloud = RongCloud.GetInstance("mgb7ka1nmdhag", "lYxcetoUuCZ");
            _rongCloud.ShouldNotBeNull();


        }
        [Fact]
        public async Task Test_RegisterAsync()
        {
            Test_GetInstance();
            var user = new UserModel();
            user.id = "2";
            user.name = "admin";
            user.SetPortrait("https://pics4.baidu.com/feed/6159252dd42a28345654665054d8b4ef14cebf69.jpeg?token=130ba2cfac0e627eaf6bbb6911988896&s=A9F1EB069FE81E8C729E259A0300D09D");
            var tokenModel = await _rongCloud.User.Register(user);
            tokenModel.Token.ShouldNotBeNullOrWhiteSpace();
        }
    }
}
