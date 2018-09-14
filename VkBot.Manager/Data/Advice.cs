using System;
using System.Collections.Generic;

namespace VkBot.Manager.Data
{
    public class Advice
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public AdviceCondition Condition { get; set; }
        public DateTime CreatedDate { get; set; }

        public IEnumerable<AdviceResponse> Responses { get; set; }
    }
}