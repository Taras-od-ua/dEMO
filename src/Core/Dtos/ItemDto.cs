using System.Collections.Generic;

namespace Core.Dtos
{
    public class ItemDto
    {
        public long Id { get; set; }
        
        public string Title { get; set; }
        public long ListId { get; set; }
        public bool Done { get; set; }

    }
}
