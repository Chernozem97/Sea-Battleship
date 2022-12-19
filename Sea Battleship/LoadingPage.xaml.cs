using Core;
using Sea_Battleship.Engine;
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

namespace Sea_Battleship
{
    /// <summary>
    /// Логика взаимодействия для LoadingPage.xaml
    /// </summary>
    public partial class LoadingPage : Page
    {
        public int z;
        public LoadingPage()
        {
            InitializeComponent();
            WindowConfig.GetCurrentAudioImg(AudioImg);
            Label label;
            Button button;
            int i = 1;
            List<string> list = FileSystem.SavedGameList();
            if (list != null)
            {
                foreach (string str in list)
                {
                    string[] tmp = str.Split('\\', '.');
                    string str1 = "";

                    str1 += tmp[1];

                    str1 = str1.TrimEnd('.');
                    label = new Label();
                    label.Content = str1;
                    button = new Button();
                    button.Content = str1;  
                    button.Click += Button_Click;
                    LoadGrid.Children.Add(label);
                    LoadGrid.Children.Add(button);
                    Grid.SetRow(label, i);
                    Grid.SetRow(button, i);
                    Grid.SetColumn(label, 0);
                    Grid.SetColumn(button, 0);
                    i++;
                }
            }
        }
        private void audioChanged(object sender, RoutedEventArgs e)
        {
            WindowConfig.AudioChanged((Image)sender);
        }

        private void BackClick_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("VybratRezhim.xaml", UriKind.Relative));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Game game = FileSystem.LoadGame(((Button)sender).Content.ToString());

                if (game.GameConfig.IsOnline)
                {
                    OnlineGame onlineGame = new OnlineGame(PlayerRole.Server, PlacementState.Loaded);
                    onlineGame.Game = game;
                    WindowConfig.IsLoaded = true;
                    WaitingWindow window = new WaitingWindow(onlineGame, null, PlacementState.Loaded);
                    window.SetNavigationService(NavigationService);
                    window.Show();
                    window.Wait();
                }
                else
                {
                    WindowConfig.game = game;
                    WindowConfig.IsLoaded = true;
                    PlayPage playPage = new PlayPage(WindowConfig.game);
                    NavigationService.Navigate(playPage, UriKind.Relative);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "Не удалось найти распознаваемые цифры."){
                    MessageBox.Show("Файл сохранения поврежден.");
                }
                else MessageBox.Show(ex.Message);
            }
        }

        private void RuleItem_Click(object sender, RoutedEventArgs e)
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

        private void AboutItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("\t\t        Самарский университет\n\t\t     Кафедра программных систем\n\n        Курсовой проект по дисциплине \"Программная инженерия\"\n\n    Тема проекта: \"Автоматизированная система \"Игра Морской бой\"\n\nРазработчики: студенты группы 6415-020302D\n\n\t\tБеляев Никита\n\t\tТарасов Вадим\n\t\tУгарин Алексей\n\n\t\t\t     Самара 2020", "О системе", MessageBoxButton.OK);
        }
    }
}
