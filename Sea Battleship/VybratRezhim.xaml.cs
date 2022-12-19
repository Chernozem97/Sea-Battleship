using Core;
using System;
using System.IO;
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
using System.Windows.Shapes;

namespace Sea_Battleship
{
    /// <summary>
    /// Логика взаимодействия для VybratRezhim.xaml
    /// </summary>
    public partial class VybratRezhim : Page
    {
        private BotLevels _bot = BotLevels.Easy;
        private GameSpeed _gameSpeed = GameSpeed.Fast;
        private PlacementState _placement = PlacementState.Manualy;

        private ShipArrangement arrangementClient;
        private GameConfig game;

        public ShipArrangement ArrangementClient { get => arrangementClient; set => arrangementClient = value; }
        public GameConfig Game { get => game; set => game = value; }

        public VybratRezhim()
        {
            InitializeComponent();
            WindowConfig.VybratRezhim = this;

            WindowConfig.game = null;
            WindowConfig.OnlineGame = null;
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            Game = new GameConfig(_bot, _gameSpeed);
            //DialogResult = true;
            ShipArrangement arrangement;
            switch (_placement)
            {
                case PlacementState.Randomly:
                    arrangement = ShipArrangement.Random();
                    if (_bot == BotLevels.Easy || _bot == BotLevels.Medium)
                        ArrangementClient = ShipArrangement.Random();
                    else
                        ArrangementClient = ShipArrangement.Strategy();
                    WindowConfig.game = new Game(arrangement, ArrangementClient, Game);
                    //Close();
                    break;
                case PlacementState.Strategily:
                    arrangement = ShipArrangement.Strategy();
                    if (_bot == BotLevels.Easy || _bot == BotLevels.Medium)
                        ArrangementClient = ShipArrangement.Random();
                    else
                        ArrangementClient = ShipArrangement.Strategy();
                    WindowConfig.game = new Game(arrangement, ArrangementClient, Game);
                   // Close();
                    break;
                case PlacementState.Manualy:
                    if (_bot == BotLevels.Easy || _bot == BotLevels.Medium)
                        ArrangementClient = ShipArrangement.Random();
                    else
                        ArrangementClient = ShipArrangement.Strategy();
                   // Close();
                    break;
            }
            if (WindowConfig.game == null)
            {
                PlacingPage placingPage = new PlacingPage(ArrangementClient, Game);
                NavigationService.Navigate(placingPage, UriKind.Relative);
            }
            else
            {
                PlayPage playPage = new PlayPage(WindowConfig.game);
                NavigationService.Navigate(playPage, UriKind.Relative);
            }
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            //Close();
        }

        private void TimeLength_Click(object sender, RoutedEventArgs e)
        {
            RadioButton button = (RadioButton)sender;
            switch (button.Content)
            {
                case "30 секунд":
                    _gameSpeed = GameSpeed.Fast;
                    break;
                case "1 минута":
                    _gameSpeed = GameSpeed.Medium;
                    break;
                case "2 минуты":
                    _gameSpeed = GameSpeed.Slow;
                    break;
                case "5 минут":
                    _gameSpeed = GameSpeed.Turtle;
                    break;
            }
        }

        private void Complexity_Click(object sender, RoutedEventArgs e)
        {
            RadioButton button = (RadioButton)sender;
            switch (button.Content)
            {
                case "Лёгкая":
                    _bot = BotLevels.Easy;
                    break;
                case "Средняя":
                    _bot = BotLevels.Medium;
                    break;
                case "Сложная":
                    _bot = BotLevels.Hard;
                    break;
            }
        }

        private void Placement_Click(object sender, RoutedEventArgs e)
        {
            RadioButton button = (RadioButton)sender;
            switch (button.Content)
            {
                case "Ручной":
                    _placement = PlacementState.Manualy;
                    break;
                case "Случайный":
                    _placement = PlacementState.Randomly;
                    break;
                case "По стратегии":
                    _placement = PlacementState.Strategily;
                    break;
            }
        }

        private void audioChanged(object sender, RoutedEventArgs e)//
        {
            WindowConfig.AudioChanged((Image)sender);
        }

        private void Loading_Click(object sender, RoutedEventArgs e)//
        {
            NavigationService.Navigate(new Uri("LoadingPage.xaml", UriKind.Relative));
        }

        private void AboutItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("\t\t        Самарский университет\n\t\t     Кафедра программных систем\n\n        Курсовой проект по дисциплине \"Программная инженерия\"\n\n    Тема проекта: \"Автоматизированная система \"Игра Морской бой\"\n\nРазработчики: студенты группы 6415-020302D\n\n\t\tБеляев Никита\n\t\tТарасов Вадим\n\t\tУгарин Алексей\n\n\t\t\t     Самара 2020", "О системе", MessageBoxButton.OK);
        }
        private void RuleItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string path = System.IO.Directory.GetCurrentDirectory(); path = path.Replace(@"bin\Debug", ""); path = path.Replace(@"bin\Release", ""); System.Diagnostics.Process.Start(path+"Resources\\info\\info.html");
                //System.Diagnostics.Process.Start(path+"Resources\\info\\info.html");

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

            window.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            window.ShowDialog();
        }

        private void BackClick_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("MainPage.xaml", UriKind.Relative));
        }

        private void ConnetToLobbyItem_Click(object sender, RoutedEventArgs e)
        {
            WindowConfig.NavigationService = NavigationService;
            WindowConfig.GameState = WindowConfig.State.Online;
            ConfigOnlineNotHostWindow window = new ConfigOnlineNotHostWindow();

            window.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            window.ShowDialog();
        }

        private void ButtonNewGame_Click(object sender, RoutedEventArgs e)
        {
            this.GridPvC.Visibility = Visibility.Hidden;
            this.GridPvCNew.Visibility = Visibility.Visible;

        }
    }
}
