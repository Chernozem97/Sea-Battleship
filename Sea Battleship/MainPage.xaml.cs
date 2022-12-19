using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfAnimatedGif;

namespace Sea_Battleship
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            WindowConfig.GetCurrentAudioImg(AudioImg);
            WindowConfig.MainPage = this;
          //  WindowConfig.Player.Open(new Uri(Environment.CurrentDirectory+@"\pirat.wav"));
          //  WindowConfig.Player.Volume = 50;
          ////  WindowConfig.Player.Play();
            WindowConfig.game = null;
            WindowConfig.OnlineGame = null;
        }
        private void audioChanged(object sender, RoutedEventArgs e)//
        {
            WindowConfig.AudioChanged((Image)sender);
        }

        private void Loading_Click(object sender, RoutedEventArgs e)//
        {
            NavigationService.Navigate(new Uri("LoadingPage.xaml", UriKind.Relative));
        }

        private void NewGame_MouseLeftButtonDown(object sender, RoutedEventArgs e)//
        {
            if (TextBoxLogin.Text == "Введите логин.."|| TextBoxLogin.Text == "")
            {
                MessageBox.Show("Введите логин!");
            }
            else
            {
                WindowConfig.NavigationService = NavigationService;
                WindowConfig.GameState = WindowConfig.State.Offline;
                VybratRezhim pageRezhim = new VybratRezhim();
                NavigationService.Navigate(pageRezhim, UriKind.Relative);
            }
           // ConfigOfflineWindow window = new ConfigOfflineWindow();
           // if (window.ShowDialog() == true)
            //{
                //if (WindowConfig.game == null)
                //{
                //    PlacingPage placingPage = new PlacingPage(window.ArrangementClient, window.Game);
                //    NavigationService.Navigate(placingPage, UriKind.Relative);
                //}
                //else
                //{
                //    PlayPage playPage = new PlayPage(WindowConfig.game);
                //    NavigationService.Navigate(playPage, UriKind.Relative);
                //}
            //}
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)//
        {
            try
            {
                string path = System.IO.Directory.GetCurrentDirectory(); path = path.Replace(@"bin\Debug", ""); path = path.Replace(@"bin\Release", ""); System.Diagnostics.Process.Start(path+"Resources\\info\\info.html");
            }
            catch
            {
                MessageBox.Show("Справка отсутствует");
            }
        }

        private void CreateLobby_Click(object sender, RoutedEventArgs e)
        {
            ConfigOnlineHostWindow window = new ConfigOnlineHostWindow();
            WindowConfig.NavigationService = NavigationService;
            WindowConfig.GameState = WindowConfig.State.Online;
         
            window.ShowDialog();
        }



        private void ConnetToLobbyItem_Click(object sender, RoutedEventArgs e)
        {
            WindowConfig.NavigationService = NavigationService;
            WindowConfig.GameState = WindowConfig.State.Online;
            ConfigOnlineNotHostWindow window = new ConfigOnlineNotHostWindow();
            window.ShowDialog();
        }

        private void AboutItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("\t\t        Самарский университет\n\t\t     Кафедра программных систем\n\n        Курсовой проект по дисциплине \"Технология Программирования\"\n\n    Тема проекта: \"Автоматизированная система \"Игра Морской бой\"\n\nРазработчики: студенты группы 6402-090301D\n\n\t\tКазаченко Антон\n\t\tАнисимов Даниил\n\t\tМеламед Никита\n\n\t\t\t ", "О системе", MessageBoxButton.OK);
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {


        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
