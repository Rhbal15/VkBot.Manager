using System.Collections.Generic;
using VkBot.Manager.Data;

namespace VkBot.Manager.Services.Models
{
    public interface IKeyboardService
    {
        void CreateAndActivateKeyboard(string emojiSequence);
        void DeactivateAllKeyboards();
        Keyboard GetActiveKeyboard();
        IEnumerable<Emoji> GetActiveKeyboardEmojis();
    }
}
