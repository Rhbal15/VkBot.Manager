using System;
using System.Collections.Generic;

namespace VkBot.Manager.Data
{
    /// <summary>
    /// Будет использоваться для группировки стикеров при добавление в том виде, в котором они грубируются в вк.
    /// </summary>
    public class EmojiGroup
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int Priority { get; set; }
        public DateTime CreateDate { get; set; }

        public IEnumerable<Emoji> Emojis { get; set; }
    }
}