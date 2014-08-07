﻿// /* ============================================================================
// Copyright 2014 Hewlett Packard
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ============================================================================ */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenStack.Common.Http;
using OpenStack.Common.ServiceLocation;
using OpenStack.Identity;
using OpenStack.Network;

namespace OpenStack.Test.Network
{
    [TestClass]
    public class NetworkServicePocoClientTests
    {
        internal TestNetworkServiceRestClient NetworkServiceRestClient;
        internal string authId = "12345";
        internal Uri endpoint = new Uri("http://testnetworkendpoint.com/v2.0/1234567890");
        internal IServiceLocator ServiceLocator;

        [TestInitialize]
        public void TestSetup()
        {
            this.NetworkServiceRestClient = new TestNetworkServiceRestClient();
            this.ServiceLocator = new ServiceLocator();

            var manager = this.ServiceLocator.Locate<IServiceLocationOverrideManager>();
            manager.RegisterServiceInstance(typeof(INetworkServiceRestClientFactory), new TestNetworkServiceRestClientFactory(this.NetworkServiceRestClient));
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.NetworkServiceRestClient = new TestNetworkServiceRestClient();
            this.ServiceLocator = new ServiceLocator();
        }

        ServiceClientContext GetValidContext()
        {
            var creds = new OpenStackCredential(this.endpoint, "SomeUser", "Password", "SomeTenant", "region-a.geo-1");
            creds.SetAccessTokenId(this.authId);

            return new ServiceClientContext(creds, CancellationToken.None, "Object Storage", endpoint);
        }

        #region Get Networks Tests

        [TestMethod]
        public async Task CanGetNetworksWithOkResponse()
        {
            var payload = @"{
                ""networks"": [
                    {
                        ""status"": ""ACTIVE"",
                        ""subnets"": [
                            ""d3839504-ec4c-47a4-b7c7-07af079a48bb""
                        ],
                        ""name"": ""myNetwork"",
                        ""router:external"": false,
                        ""tenant_id"": ""ffe683d1060449d09dac0bf9d7a371cd"",
                        ""admin_state_up"": true,
                        ""shared"": false,
                        ""id"": ""12345""
                    }
                ]
            }";

            var content = TestHelper.CreateStream(payload);

            var restResp = new HttpResponseAbstraction(content, new HttpHeadersAbstraction(), HttpStatusCode.OK);
            this.NetworkServiceRestClient.Responses.Enqueue(restResp);

            var client = new NetworkServicePocoClient(GetValidContext(), this.ServiceLocator);
            var result = await client.GetNetworks();

            Assert.IsNotNull(result);

            var networks = result.ToList();
            Assert.AreEqual(1, networks.Count());

            var network = networks.First();
            Assert.AreEqual("myNetwork", network.Name);
            Assert.AreEqual("12345", network.Id);
            Assert.AreEqual(NetworkStatus.Active, network.Status);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task CannotGetNetworksWithNoContent()
        {

            var restResp = new HttpResponseAbstraction(new MemoryStream(), new HttpHeadersAbstraction(), HttpStatusCode.NoContent);
            this.NetworkServiceRestClient.Responses.Enqueue(restResp);

            var client = new NetworkServicePocoClient(GetValidContext(), this.ServiceLocator);
            await client.GetNetworks();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task ExceptionthrownWhenGettingNetworksAndNotAuthed()
        {
            var restResp = new HttpResponseAbstraction(new MemoryStream(), new HttpHeadersAbstraction(), HttpStatusCode.Unauthorized);
            this.NetworkServiceRestClient.Responses.Enqueue(restResp);

            var client = new NetworkServicePocoClient(GetValidContext(), this.ServiceLocator);
            await client.GetNetworks();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task ExceptionthrownWhenGettingNetworksAndServerError()
        {
            var restResp = new HttpResponseAbstraction(new MemoryStream(), new HttpHeadersAbstraction(), HttpStatusCode.InternalServerError);
            this.NetworkServiceRestClient.Responses.Enqueue(restResp);

            var client = new NetworkServicePocoClient(GetValidContext(), this.ServiceLocator);
            await client.GetNetworks();
        }

        #endregion
    }
}
