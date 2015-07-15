// /* ============================================================================
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenStack.Common;

namespace OpenStack.Storage
{
    /// <summary>
    /// Represents the available parameters to list items in a container.
    /// </summary>
    public class ListStorageObjectsRequest
    {

        /// <summary>
        /// Gets the name of the container.
        /// </summary>
        public string ContainerName { get; set; }

        /// <summary>
        /// Gets the number of items to be returned.
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// Gets the name of an item to use as the floor of items returned.
        /// </summary>
        public string Marker { get; set; }

        /// <summary>
        /// Gets the name of an item to use as the ceiling of items returned.
        /// </summary>
        public string EndMarker { get; set; }

        /// <summary>
        /// Gets the prefix of an item to use as a limiter on items returned.
        /// </summary>
        public string FolderName { get; set; }

        /// <summary>
        /// Creates a new instance of the ListStorageObjectsRequest class.
        /// </summary>
        public ListStorageObjectsRequest()
        {
        }
    }
}
