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

namespace Openstack.Storage
{
    using System;
    using System.Linq;
    using System.Web;
    using Openstack.Common;
    using Openstack.Common.Http;
    using Openstack.Common.ServiceLocation;

    /// <inheritdoc/>
    internal class StorageAccountPayloadConverter : IStorageAccountPayloadConverter
    {
        /// <inheritdoc/>
        public StorageAccount Convert(string name, IHttpHeadersAbstraction headers, string payload)
        {
            name.AssertIsNotNullOrEmpty("name");
            headers.AssertIsNotNull("headers");
            payload.AssertIsNotNull("payload");

            var containerConverter = ServiceLocator.Instance.Locate<IStorageContainerPayloadConverter>();

            try
            {
                var totalBytes = long.Parse(headers["X-Account-Bytes-Used"].First());
                var totalObjects = int.Parse(headers["X-Account-Object-Count"].First());
                var totalContainers = int.Parse(headers["X-Account-Container-Count"].First());
                var containers = containerConverter.Convert(payload);
                var metadata = headers.Where(kvp => kvp.Key.StartsWith("X-Account-Meta")).ToDictionary(header => header.Key.Substring(15, header.Key.Length - 15), header => header.Value.First());

                return new StorageAccount(name, totalBytes, totalObjects, totalContainers, metadata, containers);
            }
            catch (Exception ex)
            {
                throw new HttpParseException(string.Format("Storage Account '{0}' payload could not be parsed.", name), ex);
            }
        }
    }
}
