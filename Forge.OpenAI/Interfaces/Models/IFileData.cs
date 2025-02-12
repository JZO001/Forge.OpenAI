using System;

namespace Forge.OpenAI.Interfaces.Models
{

    public interface IFileData
    {

        /// <summary>Gets the identifier.</summary>
        /// <value>The identifier.</value>
        string Id { get; }

        /// <summary>Gets the object type.</summary>
        /// <value>The object.</value>
        string Object { get; }

        /// <summary>Gets the size of the file.</summary>
        /// <value>The size.</value>
        int FileSize { get; }

        /// <summary>Gets or sets the created unix time.</summary>
        /// <value>The created unix time.</value>
        int CreatedAtUnixTime { get; }

        /// <summary>Gets the created at.</summary>
        /// <value>The created at.</value>
        DateTime CreatedAt { get; }

        /// <summary>Gets the name of the file.</summary>
        /// <value>The name of the file.</value>
        string FileName { get; }

        /// <summary>Gets the purpose of the file existence.</summary>
        /// <value>The purpose.</value>
        string Purpose { get; }

    }

}
