namespace Forge.OpenAI.Models.FineTunes
{

    /// <summary>Represents the response of a fine tune job cancel request</summary>
    public class FineTuneCancelResponse : FineTuneResponseBase
    {

        /// <summary>Initializes a new instance of the <see cref="FineTuneCancelResponse" /> class.</summary>
        public FineTuneCancelResponse()
        {
        }

        /// <summary>Performs an implicit conversion from <see cref="FineTuneCancelResponse" /> to <see cref="FineTuneJobData" />.</summary>
        /// <param name="jobResponse">The job response.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator FineTuneJobData(FineTuneCancelResponse jobResponse)
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
