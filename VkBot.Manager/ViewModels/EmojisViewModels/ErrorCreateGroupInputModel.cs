namespace VkBot.Manager.ViewModels.EmojisViewModels
{
    public class ErrorCreateGroupInputModel
    {
        public ErrorCreateGroupType ErrotType { get; set; } = ErrorCreateGroupType.None;
        public string Name { get; set; }
        public int Priority { get; set; }
        public string EmojiSequence { get; set; }
    }
}
