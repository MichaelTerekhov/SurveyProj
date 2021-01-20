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
        [JsonPropertyName("answerOptions")]
        public List<string> answerOptions { get; set; }

        public Question()
        {
            answerOptions = new List<string>();
        }
        public override string ToString()
        {
            var s = new StringBuilder();
            s.AppendLine(Text);
            if (answerOptions != null)
                foreach (var m in answerOptions)
                    s.AppendLine(m);
            return s.ToString();
        }
    }
}
