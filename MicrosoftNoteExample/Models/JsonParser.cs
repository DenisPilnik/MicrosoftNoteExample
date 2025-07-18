using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

namespace MicrosoftNoteExample.Models
{
    public static class JsonParser
    {
        private static StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
        public static string Filename { get; set; } = "Note.json";

        public static async Task SaveToJsonAsync(Note note)
        {
            List<Note> notes = await GetAllNotesFromJson();
            if(notes != null)
            {
                if (!notes.Exists(n => n.Id == note.Id))
                {
                    notes.Add(note);
                }
                else
                {
                    notes.ForEach(n => { if (n.Id == note.Id) { n.Text = note.Text; } });
                }
                await File.WriteAllTextAsync((await GetStorageFile()).Path, JsonConvert.SerializeObject(notes));
            }
            else
            {
                List<Note> emptyList = new List<Note>();
                emptyList.Add(note);
                await File.WriteAllTextAsync((await GetStorageFile()).Path, JsonConvert.SerializeObject(emptyList));
            }     
        }

        public static async Task<int> GetIDAsync(Note note)
        {
            List<Note> notes = await GetAllNotesFromJson();
            if (notes == null)
            {
                return 1;
            }
            else
            {
                if(notes.Exists(x=> x.Id == note.Id))
                {
                    return note.Id;
                }
                else
                {
                    return (notes.MaxBy(x => x.Id).Id + 1);
                }
            }
        }

        public static async Task DeleteNoteAsync(Note note)
        {
            List<Note> notes = await GetAllNotesFromJson();
            if (notes != null)
            {
                notes.RemoveAll(x => x.Id == note.Id);
                await File.WriteAllTextAsync((await GetStorageFile()).Path, JsonConvert.SerializeObject(notes));
            }
        }

        private static async Task<StorageFile> GetStorageFile()
        {
            StorageFile noteFile = (StorageFile)await storageFolder.TryGetItemAsync(JsonParser.Filename);
            if (noteFile is null)
                noteFile = await storageFolder.CreateFileAsync(JsonParser.Filename, CreationCollisionOption.ReplaceExisting);
            return noteFile;
        }

        public static async Task<List<Note>> GetAllNotesFromJson()
        {
            return JsonConvert.DeserializeObject<List<Note>>((await File.ReadAllTextAsync((await GetStorageFile()).Path)));
        }
    }
}
