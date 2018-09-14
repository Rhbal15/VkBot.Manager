namespace VkBot.Manager.Data
{
    public class AdviceResponse
    {
        public int Id { get; set; }

        public int Priority { get; set; }
        public string Text { get; set; }
        public Advice Advice { get; set; }
    }
}