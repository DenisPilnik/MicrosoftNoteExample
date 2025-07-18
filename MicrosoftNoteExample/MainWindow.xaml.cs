using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace MicrosoftNoteExample
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Hide the default system title bar.
            ExtendsContentIntoTitleBar = true;
            // Replace system title bar with the WinUI TitleBar.
            SetTitleBar(AppTitleBar);

            AppWindow.Resize(new Windows.Graphics.SizeInt32(604, 400));

            OverlappedPresenter presenter = OverlappedPresenter.Create();
            presenter.PreferredMinimumWidth = 500;
            presenter.PreferredMinimumHeight = 400;
            presenter.PreferredMaximumWidth = 800;
            presenter.PreferredMaximumHeight = 700;
            presenter.IsMaximizable = false;

            AppWindow.SetPresenter(presenter);
        }

        private void AppTitleBar_BackRequested(TitleBar sender, object args)
        {
            if (rootFrame.CanGoBack == true)
            {
                rootFrame.GoBack();
            }
        }
    }
}
