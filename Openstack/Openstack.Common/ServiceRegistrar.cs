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

namespace Openstack.Common
{
    using Openstack.Common.Http;
    using Openstack.Common.ServiceLocation;

    /// <inheritdoc/>
    public class ServiceRegistrar : IServiceLocationRegistrar
    {
        /// <summary>
        /// Registers relevant services for the Openstack.Common namespace.
        /// </summary>
        /// <param name="manager">The service manager to use when registering the services.</param>
        /// <param name="locator">A reference to the service locator.</param>
        public void Register(IServiceLocationManager manager, IServiceLocator locator)
        {
            manager.RegisterServiceInstance(typeof(IHttpAbstractionClientFactory), new HttpAbstractionClientFactory());
        }
    }
}
