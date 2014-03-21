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
    using System;
    using System.Linq;
    using System.Web;
    using Newtonsoft.Json.Linq;
    using Openstack.Common;
    using Openstack.Common.ServiceLocation;

    /// <inheritdoc/>
    internal class OpenstackServiceCatalogPayloadConverter : IOpenstackServiceCatalogPayloadConverter
    {
        /// <inheritdoc/>
        public OpenstackServiceCatalog Convert(string payload)
        {
            payload.AssertIsNotNull("payload", "A null service catalog payload cannot be converted.");

            var catalog = new OpenstackServiceCatalog();

            if (String.IsNullOrEmpty(payload))
            {
                return catalog;
            }

            try
            {
                var obj = JObject.Parse(payload);
                var defArray = obj["access"]["serviceCatalog"];
                catalog.AddRange(defArray.Select(ConvertServiceDefinition));
            }
            catch (HttpParseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new HttpParseException(string.Format("Service catalog payload could not be parsed. Payload: '{0}'", payload), ex);
            }

            return catalog;
        }

        /// <summary>
        /// Converts a Json token that represents a service definition into a POCO object.
        /// </summary>
        /// <param name="serviceDef">The token.</param>
        /// <returns>The service definition.</returns>
        internal OpenstackServiceDefinition ConvertServiceDefinition(JToken serviceDef)
        {
            var converter = ServiceLocator.Instance.Locate<IOpenstackServiceDefinitionPayloadConverter>();
            return converter.Convert(serviceDef.ToString());
        }
    }
}
