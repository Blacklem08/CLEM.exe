using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;

namespace CLEM_exe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // P/Invoke pour changer le fond d'écran Windows
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        private const int SPI_SETDESKWALLPAPER = 0x0014;
        private const int SPIF_UPDATEINIFILE = 0x01;
        private const int SPIF_SENDCHANGE = 0x02;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        int NoClickCount = 0;
        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            NoClickCount = NoClickCount + 1;

            switch(NoClickCount)
            {
                case 1:
                    QuestionBlock.Text = "Are you sure about that?";
                    break;
                case 2:
                    QuestionBlock.Text = "Please reconsider.";
                    break;
                case 3:
                    QuestionBlock.Text = "This is getting annoying.";
                    break;
                case 4:
                    QuestionBlock.Text = "Last chance to say YES.";
                    break;
                case 5:
                    QuestionBlock.Text = "STOP IT !!!!!!!!";
                    // NoButton.IsEnabled = false;
                    break;
                case 6:
                    QuestionBlock.Text = "Why you hate me so much :'( ?!";
                    break;
                default:
                    YesButton.Visibility = Visibility.Hidden;
                    NoButton.Visibility = Visibility.Hidden;

                    Thread.Sleep(5 * 1000);

                    // Change le fond d'écran Windows
                    // string imagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "sad_face.jpg");
                    
                    string imagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sadface.jpg");

                    if (System.IO.File.Exists(imagePath))
                    {
                        SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, imagePath, SPIF_UPDATEINIFILE | SPIF_SENDCHANGE);
                    }
                    
                    Application.Current.Shutdown();
                    break;
            }
        }    
    }
}