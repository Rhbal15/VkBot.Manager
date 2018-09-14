using System;

namespace VkBot.Manager.Data
{
    public class AdviceCondition
    {
        public int Id { get; set; }

        public int AdviceId { get; set; }
        public Advice Advice { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}