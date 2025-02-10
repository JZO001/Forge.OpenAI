using Forge.OpenAI.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace Forge.OpenAI.Models.VectorStoreFileBatches
{

    /// <summary>
    /// https://platform.openai.com/docs/api-reference/vector-stores-file-batches/listBatchFiles
    /// </summary>
    /// <seealso cref="Forge.OpenAI.Models.Common.RequestBase" />
    public class VectorStoreFileBatchFileListRequest : RequestBase
    {

        /// <summary>
        /// The ID of the vector store for which to create a File Batch.
        /// </summary>
        [Required]
        public string VectorStoreId { get; set; }

        /// <summary>
        /// The ID of the file batch that the files belong to.
        /// </summary>
        /// <value>The batch identifier.</value>
        [Required]
        public string BatchId { get; set; }

        /// <summary>
        /// A limit on the number of objects to be returned. Limit can range between 1 and 100, and the default is 20.
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>Sort order by the created_at timestamp of the objects. asc for ascending order and desc for descending order.</summary>
        /// <value>The default is desc.</value>
        public string Order { get; set; }

        /// <summary>
        /// A cursor for use in pagination. after is an object ID that defines your place in the list. For instance, if you make a list request and receive 100 objects, ending with obj_foo, your subsequent call can include after=obj_foo in order to fetch the next page of the list.
        /// </summary>
        public string After { get; set; }

        /// <summary>
        /// A cursor for use in pagination. before is an object ID that defines your place in the list. For instance, if you make a list request and receive 100 objects, ending with obj_foo, your subsequent call can include before=obj_foo in order to fetch the previous page of the list.
        /// </summary>
        public string Before { get; set; }

        /// <summary>
        /// Filter by file status. One of in_progress, completed, failed, cancelled.
        /// </summary>
        public string Filter { get; set; }

    }

}
