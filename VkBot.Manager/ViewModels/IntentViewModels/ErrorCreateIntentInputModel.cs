namespace VkBot.Manager.ViewModels.IntentViewModels
{
    public class ErrorCreateIntentInputModel
    {
        public ErrorCreateIntentType ErrorType { get; set; } = ErrorCreateIntentType.None;
        public string Name { get; set; }
        public string Sentences { get; set; }
    }
}