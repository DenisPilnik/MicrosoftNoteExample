using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.Windows.AppNotifications;
using Microsoft.Windows.AppNotifications.Builder;
using MicrosoftNoteExample.Models;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MicrosoftNoteExample.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NotePage : Page
    {
        private Note? NoteModel;

        public NotePage()
        {
            InitializeComponent();
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            await NoteModel.SaveAsync();
            AppNotification notification = new AppNotificationBuilder()
                .AddText("Simple note")
                .AddText($"Your note '{NoteModel.Text}' saved!")
                .BuildNotification();

            AppNotificationManager.Default.Show(notification);
        }
        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (NoteModel is not null)
            {
                await NoteModel.DeleteAsync();
            }
            if (Frame.CanGoBack == true)
            {
                Frame.GoBack();
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is Note note)
            {
                NoteModel = note;
            }
            else
            {
                NoteModel = new Note();
            }
        }
    }
}
