namespace Forge.OpenAI.Models.FineTunes
{

    /// <summary>Represents the response of a fine tune job retrieve request</summary>
    public class FineTuneJobDataResponse : FineTuneResponseBase
    {

        /// <summary>Initializes a new instance of the <see cref="FineTuneJobDataResponse" /> class.</summary>
        public FineTuneJobDataResponse()
        {
        }

        /// <summary>Performs an implicit conversion from <see cref="FineTuneJobDataResponse" /> to <see cref="FineTuneJobData" />.</summary>
        /// <param name="jobResponse">The job response.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator FineTuneJobData(FineTuneJobDataResponse jobResponse)
            => new FineTuneJobData(
                jobResponse.Id,
                jobResponse.Object,
                jobResponse.Model,
                jobResponse.CreatedAtUnixTime,
                jobResponse.Events,
                jobResponse.FineTunedModel,
                jobResponse.HyperParams,
                jobResponse.OrganizationId,
                jobResponse.ResultFiles,
                jobResponse.Status,
                jobResponse.ValidationFiles,
                jobResponse.TrainingFiles,
                jobResponse.UpdatedAtUnixTime);

    }

}
