using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
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

namespace MahjongGame
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    

    public partial class MainWindow : Window
    {
        public void HideAllCanvases()
        {
            MainMenuCanvas.Visibility = Visibility.Hidden;
            LoginCanvas.Visibility = Visibility.Hidden;
            InstructionCanvas.Visibility = Visibility.Hidden;
            RankingCanvas.Visibility = Visibility.Hidden;
            MenuCanvas.Visibility = Visibility.Hidden;
            NewGameCanvas.Visibility = Visibility.Hidden;
            MainGameCanvas.Visibility = Visibility.Hidden;
            GameCanvas.Visibility = Visibility.Hidden;
            GameMenuCanvas.Visibility = Visibility.Hidden;
            GameInstructionCanvas.Visibility = Visibility.Hidden;
        }


        const int PICWIDTH = 70;
        const int PICHEIGHT = 56;
        static int PROPERTY;
        Game game = new Game();

        System.Windows.Threading.DispatcherTimer GameTimer = null;
        public void InitializeTimer()
        {
            GameTimer = new System.Windows.Threading.DispatcherTimer();
            GameTimer.Tick += dispatcherTimer_Tick;
            GameTimer.Interval = new TimeSpan(0, 0, 1);
            GameTimer.Start();
        }
        public void InitializeNewGame()
        {
            if (GameTimer == null) InitializeTimer();
            GameTimerLabel.Content = "00:00";
            GameTimer.Start();
            game.Counter = 0;
        }


        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            int min;
            int sec;
            game.Counter++;
            min = (int)(game.Counter / 60);
            sec = (int)(game.Counter % 60);
            
            GameTimerLabel.Content = (min < 10 ? "0" : "") + min + ":" + (sec < 10 ? "0" : "") + sec; 
        }

        public void DrawRectangleBoard(int x, int y, int zindex, Canvas parent)
        {
            int InitialPositionX = (int)(parent.ActualWidth / 2 - 0.5 * x * PICWIDTH);
            int InitialPositionY = (int)(parent.ActualHeight / 2 - 0.5 * y * PICHEIGHT);

            for (int ix = 0; ix < x; ix++)
            {
                for (int iy = 0; iy < y; iy++)
                {
                    GameElement el = new GameElement(ix * PICWIDTH + InitialPositionX, iy * PICHEIGHT + InitialPositionY, 1, zindex, parent);
                    el.GameButton.Click += new RoutedEventHandler(GameElementButton_Click);
                    game.AddElement(el);
                    
                }
            }
        }
      
        public void DrawBoard(int zindex, Canvas parent, int type)
        {
            const int x = 8;
            const int y = 7;
            int InitialPositionX = (int)(parent.ActualWidth / 2 - 0.5 * x * PICWIDTH);
            int InitialPositionY = (int)(parent.ActualHeight / 2 - 0.5 * y * PICHEIGHT);
            int[,] coordsHeart = new int[y, x] {{0,1,1,0,0,1,1,0},
                                                {1,0,0,1,1,0,0,1},
                                                {1,0,0,0,0,0,0,1},
                                                {1,0,0,1,1,0,0,1},
                                                {0,1,0,0,0,0,1,0},
                                                {0,0,1,0,0,1,0,0},
                                                {0,0,0,1,1,0,0,0}};

            int[,] coordsSnake = new int[y, x] {{1,1,1,1,1,1,1,1},
                                                {0,0,0,0,0,0,0,1},
                                                {1,1,1,1,1,1,0,1},
                                                {1,0,0,0,0,1,0,1},
                                                {1,0,1,1,1,1,0,1},
                                                {1,0,0,0,0,0,0,1},
                                                {1,1,1,1,1,1,1,1}};

            int[,] coordsHouse = new int[y, x] {{0,0,0,0,0,1,1,1},
                                                {0,1,0,0,0,1,1,1},
                                                {0,1,1,1,0,1,1,1},
                                                {1,1,1,1,1,0,1,0},
                                                {1,0,0,0,1,0,1,0},
                                                {1,0,1,0,1,0,1,0},
                                                {1,1,1,1,1,0,1,0}};

            int[,] coords = new int[y, x];
            if (type == 0) { coords = coordsHeart; }
            if (type == 1) { coords = coordsSnake; }
            if (type == 2) { coords = coordsHouse; }
            for (int ix = 0; ix < x; ix++)
            {
                for (int iy = 0; iy < y; iy++)
                {
                    
                    if (coords[iy, ix] == 1)
                    {
                        GameElement el = new GameElement(ix * PICWIDTH + InitialPositionX, iy * PICHEIGHT + InitialPositionY, 1, zindex, parent);
                        el.GameButton.Click += new RoutedEventHandler(GameElementButton_Click);
                        game.AddElement(el);
                    }
                  
                }
            }
        }

       

        public MainWindow()
        {
            InitializeComponent();
            HideAllCanvases();
            LoginCanvas.Visibility = Visibility.Visible;
            MainMenuCanvas.Visibility = Visibility.Hidden;
            ImageBrush myBrush = new ImageBrush();
            Image image = new Image();
            image.Source = new BitmapImage(new Uri(@"Resources/Background9.jpg", UriKind.RelativeOrAbsolute));
            myBrush.ImageSource = image.Source;
            this.Background = myBrush;
        }

      

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            HideAllCanvases();
            MainMenuCanvas.Visibility = Visibility.Visible;
            NewGameCanvas.Visibility = Visibility.Visible;
            MenuCanvas.Visibility = Visibility.Visible;
        }

        private void RankingButton_Click(object sender, RoutedEventArgs e)
        {
            HideAllCanvases();
            MainMenuCanvas.Visibility = Visibility.Visible;
            RankingCanvas.Visibility = Visibility.Visible;
            MenuCanvas.Visibility = Visibility.Visible;
        }

        private void InstructionButton_Click(object sender, RoutedEventArgs e)
        {
            HideAllCanvases();
            MainMenuCanvas.Visibility = Visibility.Visible;
            InstructionCanvas.Visibility = Visibility.Visible;
            MenuCanvas.Visibility = Visibility.Visible;
            InstructionTextBlock.Text = "Your task is to find couple of the same picture. Choose level of difficult and mode. If You want to check your place in ranking, press ''Ranking''.";
            //InstructionTextBlock.Text = "Find the same picture. For shuffling cards, press ''shuffle'' button. For help, press ''hint'' button";
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Mode1Button_Click(object sender, RoutedEventArgs e)
        {
            InitializeNewGame();
            HideAllCanvases();
            MainGameCanvas.Visibility = Visibility.Visible;
            GameCanvas.Visibility = Visibility.Visible;
            GameMenuCanvas.Visibility = Visibility.Visible;

            DrawRectangleBoard(5, 5, 1, GameCanvas);
            DrawRectangleBoard(4, 4, 2, GameCanvas);
            DrawRectangleBoard(3, 3, 3, GameCanvas);

            game.InitializeElements(4);
            game.ShuffleElements(100);
        }

        private void Mode2Button_Click(object sender, RoutedEventArgs e)
        {
            InitializeNewGame();
            HideAllCanvases();
            MainGameCanvas.Visibility = Visibility.Visible;
            GameCanvas.Visibility = Visibility.Visible;
            GameMenuCanvas.Visibility = Visibility.Visible;
            DrawBoard(1, GameCanvas,0);
            DrawBoard(2, GameCanvas,0);
            DrawBoard(3, GameCanvas,0);
            game.InitializeElements(4);
            game.ShuffleElements(100);
        }

        private void Mode3Button_Click(object sender, RoutedEventArgs e)
        {
            InitializeNewGame();
            HideAllCanvases();
            MainGameCanvas.Visibility = Visibility.Visible;
            GameCanvas.Visibility = Visibility.Visible;
            GameMenuCanvas.Visibility = Visibility.Visible;
            DrawBoard(1, GameCanvas, 1);
            DrawBoard(2, GameCanvas, 1);
            DrawBoard(3, GameCanvas, 1);
            game.InitializeElements(6);
            game.ShuffleElements(100);
        }

        private void Mode4Button_Click(object sender, RoutedEventArgs e)
        {
            InitializeNewGame();
            HideAllCanvases();
            MainGameCanvas.Visibility = Visibility.Visible;
            GameCanvas.Visibility = Visibility.Visible;
            GameMenuCanvas.Visibility = Visibility.Visible;

            DrawBoard(1, GameCanvas, 2);
            DrawBoard(2, GameCanvas, 2);
            DrawBoard(3, GameCanvas, 2);
            DrawBoard(4, GameCanvas, 2);
            DrawBoard(5, GameCanvas, 2);
            game.InitializeElements(6);
            game.ShuffleElements(100);
        }

        private void GameExitButton_Click(object sender, RoutedEventArgs e)
        {
            GameCanvas.Children.Clear();
            game = new Game();
            HideAllCanvases();
            MainMenuCanvas.Visibility = Visibility.Visible;
            NewGameCanvas.Visibility = Visibility.Visible;
            MenuCanvas.Visibility = Visibility.Visible;
        }

        private void GameInstructionButton_Click(object sender, RoutedEventArgs e)
        {
            GameTimer.Stop();
            HideAllCanvases();
            MainGameCanvas.Visibility = Visibility.Visible;
            GameInstructionCanvas.Visibility = Visibility.Visible;
            // InstructionTextBlock.Text = "Your task is to find couple of the same picture. Choose level of difficult and mode. If You want to check your place in ranking, press ''Ranking''.";
            GameInstructionTextBlock.Text = "Find the same picture. For shuffling cards, press ''shuffle'' button. For help, press ''hint'' button";
        }

        private void GameResumeButton_Click(object sender, RoutedEventArgs e)
        {
            HideAllCanvases();
            MainGameCanvas.Visibility = Visibility.Visible;
            GameCanvas.Visibility = Visibility.Visible;
            GameMenuCanvas.Visibility = Visibility.Visible;
            GameTimer.Start();
        }

        private void GameReshuffleButton_Click(object sender, RoutedEventArgs e)
        {
            game.ShuffleElements(100);
        }

       

        private void GameElementButton_Click(object sender, RoutedEventArgs e)
        {
            Button CurrentButton = sender as Button;
           foreach (GameElement el in game.Elements) // Check if button is enabled
            {
                if (Canvas.GetZIndex(CurrentButton) >= Canvas.GetZIndex(el.GameButton))
                    continue;
                if (Math.Abs(CurrentButton.Margin.Left - el.GameButton.Margin.Left) > CurrentButton.Width)
                   continue;
                if (Math.Abs(CurrentButton.Margin.Top - el.GameButton.Margin.Top) < CurrentButton.Height)
                 return;
               }
            var converter = new System.Windows.Media.BrushConverter();
            if (game.LastElement != null)
            {
                GameElement CurrentElement = null;
                foreach (GameElement el in game.Elements)
                {
                    if (sender as Button == el.GameButton)
                    {
                        CurrentElement = el;
                        break;
                    }
                }

                if (CurrentElement != null && CurrentElement != game.LastElement)
                {
                    if (CurrentElement.Index == game.LastElement.Index)
                    {
                        CurrentElement.GameButton.Visibility = Visibility.Hidden;
                        game.LastElement.GameButton.Visibility = Visibility.Hidden;
                        game.Elements.Remove(CurrentElement);
                        game.Elements.Remove(game.LastElement);

                        CurrentElement.RightRectangle.Visibility = Visibility.Hidden;
                        game.LastElement.RightRectangle.Visibility = Visibility.Hidden;
                        CurrentElement.BottomRectangle.Visibility = Visibility.Hidden;
                        game.LastElement.BottomRectangle.Visibility = Visibility.Hidden;
                    }
                    game.LastElement.GameButton.BorderBrush = (Brush)converter.ConvertFromString("#FF707070");
                    game.LastElement.GameButton.BorderThickness = new Thickness(1);
                    game.LastElement = null;
                    CurrentElement = null;
                    if(game.Elements.Count == 0)
                    {
                        GameEndLabel.Content = "Congratulations!";
                        GameEndLabel.Visibility = Visibility.Visible;

                    }
                }
            }
            else if (sender is Button)
            {
                foreach (GameElement el in game.Elements)
                {
                    if (sender as Button == el.GameButton)
                    {
                        game.LastElement = el;
                        game.LastElement.GameButton.BorderBrush = (Brush)converter.ConvertFromString("#FFDA35BE");
                        game.LastElement.GameButton.BorderThickness = new Thickness(3);
                        break;
                    }
                }
            }

        }

        private void GameBackgroundButton_Click(object sender, RoutedEventArgs e)
        {
            var rand = new Random();
            ImageBrush myBrush = new ImageBrush();
            Image image = new Image();
            image.Source = new BitmapImage(new Uri(@"Resources/Background" + rand.Next(10) + ".jpg", UriKind.RelativeOrAbsolute));
            myBrush.ImageSource = image.Source;
            this.Background = myBrush;
        }

        private void NewAccountButton_Click(object sender, RoutedEventArgs e)
        {
            string pathConnection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ewa_4\\Source\\Repos\\MahjongGame\\MahjongGame\\Users.mdf;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(pathConnection);
            string insertToDatabase = "INSERT INTO UserNameTable (Login, Password) VALUES ('" + this.UserBox.Text + "','" + this.PasswordBox.Text + "') ;";
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand(insertToDatabase, sqlConnection))
                {
                    if (sqlConnection.State == System.Data.ConnectionState.Closed)
                        sqlConnection.Open();
                    SqlDataReader myReader;
                    myReader = sqlCommand.ExecuteReader();
                    MessageBox.Show("Add new user with login: " + UserBox.Text);
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            UsernameLabel.Content = UserBox.Text;
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            string pathConnection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ewa_4\\Source\\Repos\\MahjongGame\\MahjongGame\\Users.mdf;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(pathConnection);
            try
            {
                if (sqlConnection.State == System.Data.ConnectionState.Closed)
                    sqlConnection.Open();
                string selectFromDatabase = "SELECT * FROM UserNameTable WHERE Login='" + this.UserBox.Text + "' AND Password='" + this.PasswordBox.Text + "' ;";
                SqlCommand sqlCommand = new SqlCommand(selectFromDatabase, sqlConnection);
                SqlDataReader dataReader;
                dataReader = sqlCommand.ExecuteReader();
                int count = 0;

                while (dataReader.Read())
                {
                    count = count + 1;
                }

                if (count == 1)
                {
                    HideAllCanvases();
                    MainMenuCanvas.Visibility = Visibility.Visible;
                    NewGameCanvas.Visibility = Visibility.Visible;
                    MenuCanvas.Visibility = Visibility.Visible;
                    UsernameLabel.Content = UserBox.Text;
                }
                else
                {
                    MessageBox.Show("Username or password is wrong!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            HideAllCanvases();
            MainMenuCanvas.Visibility = Visibility.Visible;
            NewGameCanvas.Visibility = Visibility.Visible;
            MenuCanvas.Visibility = Visibility.Visible;
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            HideAllCanvases();
            LoginCanvas.Visibility = Visibility.Visible;
        }

        private void GuestButton_Click(object sender, RoutedEventArgs e)
        {
            HideAllCanvases();
            MainMenuCanvas.Visibility = Visibility.Visible;
            NewGameCanvas.Visibility = Visibility.Visible;
            MenuCanvas.Visibility = Visibility.Visible;
            //LogoutButton.Visibility = Visibility.Hidden;
            UsernameLabel.Content = "Guest";
        }

        private void GamePauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (GameTimer.IsEnabled)
            {
                GameTimer.Stop();
                GamePauseButton.Content = "Resume";
            }
            else
            {
                GameTimer.Start();
                GamePauseButton.Content = "Pause";
            }
        }
    }
}
