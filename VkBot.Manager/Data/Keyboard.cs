using System.Collections.Generic;

namespace VkBot.Manager.Data
{
    public class Keyboard
    {
        public int Id { get; set; }

        public KeyboardStatus KeyboardStatus { get; set; }
        public IEnumerable<KeyboardButton> Buttons { get; set; }
    }
}