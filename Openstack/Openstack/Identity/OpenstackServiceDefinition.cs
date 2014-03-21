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
    using System.Collections.Generic;
    using Openstack.Common;

    /// <summary>
    /// Represents the definition of an Openstack service.
    /// </summary>
    public class OpenstackServiceDefinition
    {
        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the type of the service.
        /// </summary>
        public string Type { get; private set; }

        /// <summary>
        /// Gets a list of endpoints for the service.
        /// </summary>
        public IEnumerable<OpenstackServiceEndpoint> Endpoints { get; private set; }

        /// <summary>
        /// Creates a new instance of the OpenstackServiceDefinition class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="endpoints"></param>
        internal OpenstackServiceDefinition(string name, string type, IEnumerable<OpenstackServiceEndpoint> endpoints)
        {
            name.AssertIsNotNullOrEmpty("name","Cannot create a service definition with a name that is null or empty.");
            type.AssertIsNotNullOrEmpty("type", "Cannot create a service definition with a type that is null or empty.");
            endpoints.AssertIsNotNull("endpoints", "Cannot create a service definition with a null endpoint collection.");

            this.Name = name;
            this.Type = type;
            this.Endpoints = endpoints;
        }
    }
}
