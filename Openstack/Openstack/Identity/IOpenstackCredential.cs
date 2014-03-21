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

namespace Openstack.Identity
{
    using System.Security;

    /// <inheritdoc/>
    public interface IOpenstackCredential : ICredential
    {
        /// <summary>
        /// Gets the name of the user to use for the current instance of Openstack 
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// Gets the password to use for the current instance of Openstack 
        /// </summary>
        SecureString Password { get; }

        /// <summary>
        /// Gets the Id of the tenant to use for the current instance of Openstack 
        /// </summary>
        string TenantId { get; }

        /// <summary>
        /// Sets the access token to be used for the current instance of Openstack.
        /// </summary>
        /// <param name="accessTokenId">The access token id.</param>
        void SetAccessTokenId(string accessTokenId);


        /// <summary>
        /// Sets the service catalog to be used for the current instance of Openstack.
        /// </summary>
        /// <param name="catalog">The service catalog.</param>
        void SetServiceCatalog(OpenstackServiceCatalog catalog);
    }
}
