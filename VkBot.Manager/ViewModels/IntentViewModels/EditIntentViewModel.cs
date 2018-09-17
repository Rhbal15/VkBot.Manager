namespace VkBot.Manager.ViewModels.IntentViewModels
{
    public class EditIntentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreatedDate { get; set; }
        public string Sentences { get; set; }
        public ErrorCreateIntentType ErrorType { get; set; } = ErrorCreateIntentType.None;
    }
}