using System.Collections.Generic;

namespace Core.Dtos
{
    public class ListDto
    {
        public long Id { get; set; }
        public string Color { get; set; }
        public string Title { get; set; }
        public long CategoryId { get; set; }
        
        public IEnumerable<ItemDto> Items { get; set; }
    }
}
