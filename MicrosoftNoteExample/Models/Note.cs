using System;
using System.Threading.Tasks;

namespace MicrosoftNoteExample.Models
{
    public class Note
    {
        public int Id { get; set; } = 0;
        public string Text { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;

        public Note() 
        {
            
        }

        public async Task SaveAsync()
        {
            Id = await JsonParser.GetIDAsync(this);
            await JsonParser.SaveToJsonAsync(this);
        }

        public async Task DeleteAsync()
        {
            await JsonParser.DeleteNoteAsync(this);
        }
    }
}
