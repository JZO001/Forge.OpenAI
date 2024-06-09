﻿using Forge.OpenAI.Models.Common;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.VectorStoreFileBatches
{

    /// <summary>
    /// https://platform.openai.com/docs/api-reference/vector-stores-file-batches/listBatchFiles
    /// </summary>
    /// <seealso cref="Forge.OpenAI.Models.Common.RequestBase" />
    public class VectorStoreFileBatchFileListRequest : RequestBase
    {

        /// <summary>
        /// A limit on the number of objects to be returned. Limit can range between 1 and 100, and the default is 20.
        /// </summary>
        [JsonPropertyName("limit")]
        public int? Limit { get; set; }

        /// <summary>Sort order by the created_at timestamp of the objects. asc for ascending order and desc for descending order.</summary>
        /// <value>The default is desc.</value>
        [JsonPropertyName("order")]
        public string Order { get; set; }

        /// <summary>
        /// A cursor for use in pagination. after is an object ID that defines your place in the list. For instance, if you make a list request and receive 100 objects, ending with obj_foo, your subsequent call can include after=obj_foo in order to fetch the next page of the list.
        /// </summary>
        [JsonPropertyName("after")]
        public string After { get; set; }

        /// <summary>
        /// A cursor for use in pagination. before is an object ID that defines your place in the list. For instance, if you make a list request and receive 100 objects, ending with obj_foo, your subsequent call can include before=obj_foo in order to fetch the previous page of the list.
        /// </summary>
        [JsonPropertyName("before")]
        public string Before { get; set; }

        /// <summary>
        /// Filter by file status. One of in_progress, completed, failed, cancelled.
        /// </summary>
        [JsonPropertyName("filter")]
        public string Filter { get; set; }

    }

}
