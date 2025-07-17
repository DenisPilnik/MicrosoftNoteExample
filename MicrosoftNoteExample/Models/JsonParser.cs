using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Windows.Storage;
using static System.Net.Mime.MediaTypeNames;

namespace MicrosoftNoteExample.Models
{
    public static class JsonParser
    {
        private static StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
        public static ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();
        private static string Filename { get; set; } = "Note.json";

        public static async Task SaveToJson(Note note)
        {
            StorageFile noteFile = (StorageFile)await storageFolder.TryGetItemAsync(Filename);
            if (noteFile is null)
            {
                noteFile = await storageFolder.CreateFileAsync(Filename, CreationCollisionOption.ReplaceExisting);
            }
            LoadAllFromJson(noteFile);
            Notes.Add(note);
            File.WriteAllTextAsync(noteFile.Path, JsonConvert.SerializeObject(Notes));
        }

        public static async void LoadAllFromJson(StorageFile storageFile)
        {
            string json = File.ReadAllText(storageFile.Path);
            var notes = JsonConvert.DeserializeObject<List<Note>>(json);
            notes.ForEach( i  => Notes.Add(i));
        }
    }
}
