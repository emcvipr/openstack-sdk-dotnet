﻿// /* ============================================================================
// Copyright 2014 Hewlett Packard
//  
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
//  Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ============================================================================ */

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OpenStack.Common;
using OpenStack.Common.ServiceLocation;
using OpenStack.Identity;

namespace OpenStack.Network
{
    /// <inheritdoc/>
    internal class NetworkServiceClient : INetworkServiceClient
    {
        internal ServiceClientContext Context;
        internal IServiceLocator ServiceLocator;

        /// <summary>
        /// Creates a new instance of the NetworkServiceClient class.
        /// </summary>
        /// <param name="credentials">The credential to be used by this client.</param>
        /// <param name="token">The cancellation token to be used by this client.</param>
        /// <param name="serviceName">The name of the service to be used by this client.</param>
        /// <param name="serviceLocator">A service locator to be used to locate/inject dependent services.</param>
        public NetworkServiceClient(IOpenStackCredential credentials, string serviceName, CancellationToken token, IServiceLocator serviceLocator)
        {
            serviceLocator.AssertIsNotNull("serviceLocator", "Cannot create a network service client with a null service locator.");

            this.ServiceLocator = serviceLocator;
            var endpoint = new Uri(credentials.ServiceCatalog.GetPublicEndpoint(serviceName, credentials.Region));
            this.Context = new ServiceClientContext(credentials, token, serviceName, endpoint);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Network>> GetNetworks()
        {
            var client = this.GetPocoClient();
            return await client.GetNetworks();
        }

        /// <summary>
        /// Gets a client to interact with the remote OpenStack instance.
        /// </summary>
        /// <returns>A POCO client.</returns>
        internal INetworkServicePocoClient GetPocoClient()
        {
            return this.ServiceLocator.Locate<INetworkServicePocoClientFactory>().Create(this.Context, this.ServiceLocator);
        }
    }
}
