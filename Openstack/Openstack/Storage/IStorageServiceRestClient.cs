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
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using Openstack.Common.Http;

    /// <summary>
    /// Client that can connect to the REST endpoints of an Openstack storage service.
    /// </summary>
    public interface IStorageServiceRestClient
    {
        /// <summary>
        /// Creates a storage object on the remote Openstack instance.
        /// </summary>
        /// <param name="containerName">The name of the parent storage container.</param>
        /// <param name="objectName">The name of the storage object.</param>
        /// <param name="metadata">Metadata associated with the storage object.</param>
        /// <param name="content">The storage objects content.</param>
        /// <returns>The Http response from the remote service.</returns>
        Task<IHttpResponseAbstraction> CreateObject(string containerName, string objectName, IDictionary<string, string> metadata, Stream content);

        /// <summary>
        /// Creates a storage container on the remote Openstack instance.
        /// </summary>
        /// <param name="containerName">The name of the storage container.</param>
        /// <param name="metadata">Metadata associated with the storage container.</param>
        /// <returns>The Http response from the remote service.</returns>
        Task<IHttpResponseAbstraction> CreateContainer(string containerName, IDictionary<string, string> metadata);

        /// <summary>
        /// Gets a storage object from the remote Openstack instance.
        /// </summary>
        /// <param name="containerName">The name of the parent storage container.</param>
        /// <param name="objectName">The name of the object.</param>
        /// <returns>The Http response from the remote service.</returns>
        Task<IHttpResponseAbstraction> GetObject(string containerName, string objectName);

        /// <summary>
        /// Gets a storage container from the remote Openstack instance.
        /// </summary>
        /// <param name="containerName">The name of the storage container.</param>
        /// <returns>The Http response from the remote service.</returns>
        Task<IHttpResponseAbstraction> GetContainer(string containerName);

        /// <summary>
        /// Deletes a storage object from the remote Openstack instance.
        /// </summary>
        /// <param name="containerName">The name of the parent storage container.</param>
        /// <param name="objectName">The name of the storage object.</param>
        /// <returns>The Http response from the remote service.</returns>
        Task<IHttpResponseAbstraction> DeleteObject(string containerName, string objectName);

        /// <summary>
        /// Deletes a storage container from the remote Openstack instance.
        /// </summary>
        /// <param name="containerName">The name of the storage container.</param>
        /// <returns>The Http response from the remote service.</returns>
        Task<IHttpResponseAbstraction> DeleteContainer(string containerName);

        /// <summary>
        /// Updates a storage object on the remote Openstack instance.
        /// </summary>
        /// <param name="containerName">The name of the parent storage container.</param>
        /// <param name="objectName">The name of the storage object.</param>
        /// <param name="metadata">Metadata associated with the storage object.</param>
        /// <returns>The Http response from the remote service.</returns>
        Task<IHttpResponseAbstraction> UpdateObject(string containerName, string objectName, IDictionary<string, string> metadata);

        /// <summary>
        /// Updates a storage container on the remote Openstack instance.
        /// </summary>
        /// <param name="containerName">The name of the storage container.</param>
        /// <param name="metadata">Metadata associated with the storage container.</param>
        /// <returns>The Http response from the remote service.</returns>
        Task<IHttpResponseAbstraction> UpdateContainer(string containerName, IDictionary<string, string> metadata);

        /// <summary>
        /// Gets the metadata for a storage container from the remote Openstack instance.
        /// </summary>
        /// <param name="containerName">The name of the storage container.</param>
        /// <returns>The Http response from the remote service.</returns>
        Task<IHttpResponseAbstraction> GetContainerMetadata(string containerName);

        /// <summary>
        /// Gets the metadata for a storage object from the remote Openstack instance.
        /// </summary>
        /// <param name="containerName">The name of the parent storage container.</param>
        /// <param name="objectName">The name of the storage object.</param>
        /// <returns>The Http response from the remote service.</returns>
        Task<IHttpResponseAbstraction> GetObjectMetadata(string containerName, string objectName);

        /// <summary>
        /// Copies an object on the remote Openstack instance.
        /// </summary>
        /// <param name="sourceContainerName">The name of the parent storage container for the source storage object.</param>
        /// <param name="sourceObjectName">The name of the source storage object.</param>
        /// <param name="targetContainerName">The name of the parent storage container for the target storage object.</param>
        /// <param name="targetObjectName">The name of the target storage object.</param>
        /// <returns>The Http response from the remote service.</returns>
        Task<IHttpResponseAbstraction> CopyObject(string sourceContainerName, string sourceObjectName, string targetContainerName, string targetObjectName);

        /// <summary>
        /// Gets the details of the current storage account.
        /// </summary>
        /// <returns>The Http response from the remote service.</returns>
        Task<IHttpResponseAbstraction> GetAccount();
    }
}
