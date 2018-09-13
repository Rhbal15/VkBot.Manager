using System.Collections.Generic;

namespace VkBot.Manager.Models
{
    public class Keyboard
    {
        public int Id { get; set; }

        public KeyboardStatus KeyboardStatus { get; set; }
        public IEnumerable<KeyboardButton> Buttons { get; set; }
    }

    public enum KeyboardStatus
    {
        Active,
        Inactive
    }
}