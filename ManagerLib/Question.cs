using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ManagerLib
{
    public class Question
    {
        [JsonPropertyName("questionType")]
        public string QuestionType { get; set; }
        [JsonPropertyName("text")]
        public string Text { get; set; }
        [JsonPropertyName("AnswerOptions")]
        public List<string> AnswerOptions { get; set; }
        [JsonPropertyName("answers")]
        public List<string> Answers { get; set; }
        public Question()
        {
            AnswerOptions = new List<string>();
            Answers = new List<string>();
        }
        public override string ToString()
        {
            var s = new StringBuilder();
            s.AppendLine(Text);
            if (AnswerOptions != null)
                foreach (var m in AnswerOptions)
                    s.AppendLine(m);
            return s.ToString();
        }
    }
}
