using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MicrosoftNoteExample.Models
{
    public class AllNotes
    {
        public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();
         
        public AllNotes()
        {
            LoadNotes();
        }

        public async void LoadNotes()
        {
            Notes.Clear();
            // Get the file where the notes are stored.
            await GetFilesInFolderAsync();
        }

        private async Task GetFilesInFolderAsync()
        {
            // Get the notes form JSON file.
            (await JsonParser.GetAllNotesFromJson())?.ForEach(notes => Notes.Add(notes));
        }
    }
}
