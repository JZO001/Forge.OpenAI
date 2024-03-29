﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.ChatCompletions
{

    public class ChatCompletionStreamedResponse : ChatCompletionResponseBase
    {

        [JsonPropertyName("choices")]
        public IReadOnlyList<ChatChoiceStreamed> Choices { get; set; } = new List<ChatChoiceStreamed>();

    }

}
