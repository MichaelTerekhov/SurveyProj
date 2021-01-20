using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ManagerLib
{
    public class Survey
    {
        [JsonPropertyName("authorId")]
        public int AuthorId { get; set; }
        [JsonPropertyName("subject")]
        public string Subject { get; set; }
        [JsonPropertyName("kind")]
        public string Kind { get; set; }
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("containerId")]
        public static List<int> containerId = new List<int> { };
        [JsonPropertyName("questions")]
        public List<Question> QuestionContainer { get; set; } = new List<Question> { };
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        public override string ToString()
        {
            return $"[{Date}]\n" +
                $"Author: [{AuthorId}] Kind: {Kind} Subject: {Subject} SurveyId: [{Id}]";
        }
    }
}
