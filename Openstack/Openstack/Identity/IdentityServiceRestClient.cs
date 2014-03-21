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

using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Openstack.Common;
using Openstack.Common.Http;
using Openstack.Common.ServiceLocation;

namespace Openstack.Identity
{
    /// <inheritdoc/>
    internal class IdentityServiceRestClient : IIdentityServiceRestClient
    {
        internal IOpenstackCredential credential;
        internal CancellationToken cancellationToken;
        
        /// <summary>
        /// Creates a new instance of the IdentityServiceRestClient class.
        /// </summary>
        /// <param name="credential">The credential to be used by this client.</param>
        /// <param name="cancellationToken">The cancellation token to be used by this client.</param>
        public IdentityServiceRestClient(IOpenstackCredential credential, CancellationToken cancellationToken)
        {
            credential.AssertIsNotNull("credential");
            cancellationToken.AssertIsNotNull("cancellationToken");

            this.credential = credential;
            this.cancellationToken = cancellationToken;
        }

        /// <inheritdoc/>
        public async Task<IHttpResponseAbstraction> Authenticate()
        {
            var client = ServiceLocator.Instance.Locate<IHttpAbstractionClientFactory>().Create(this.cancellationToken);
            client.Headers.Add("Accept", "application/json");
            client.ContentType = "application/json";

            client.Uri = this.credential.AuthenticationEndpoint;
            client.Method = HttpMethod.Post;
            client.Content = CreateAuthenticationJsonPayload(this.credential).ConvertToStream();

            return await client.SendAsync();
        }

        /// <summary>
        /// Creates a Json payload that will be sent to the remote instance to authenticate.
        /// </summary>
        /// <param name="creds">The credentials used to authenticate.</param>
        /// <returns>A string that represents a Json payload.</returns>
        internal static string CreateAuthenticationJsonPayload(IOpenstackCredential creds)
        {
            var authPayload = new StringBuilder();
            authPayload.Append("{\"auth\":{\"passwordCredentials\":{\"username\":\"");
            authPayload.Append(creds.UserName);
            authPayload.Append("\",\"password\":\"");
            authPayload.Append(creds.Password.ConvertToUnsecureString());
            authPayload.Append("\"},\"tenantName\":\"");
            authPayload.Append(creds.TenantId);
            authPayload.Append("\"}}");
            return authPayload.ToString();
        }
    }
}
