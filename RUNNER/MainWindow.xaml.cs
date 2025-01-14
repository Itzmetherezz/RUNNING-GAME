using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input; 
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
namespace RUNNER
{ 
    /// <summary> 
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DispatcherTimer timer = new DispatcherTimer();

        Rect PlayerHitBox;
        Rect GroundHitBox;
        Rect ObstacleHitBox;

        bool jumping;

        int force = 20;
        int speed = 5;

        Random rand = new Random();

        bool GameOver;

        double SpriteIndex = 0;

        ImageBrush playerSprite = new ImageBrush();
        ImageBrush backgroundSprite = new ImageBrush();
        ImageBrush obstacleSprite = new ImageBrush();

        int[] ObstaclePosition = { 320, 310, 300, 305, 315 };

        int score = 0;


        public MainWindow()
        {
            InitializeComponent();

            MyCanvas.Focus();

            timer.Tick += GameEngine;
            timer.Interval = TimeSpan.FromMilliseconds(20);

            backgroundSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/ASSETS/background.gif"));

            background.Fill = backgroundSprite;
            background2.Fill = backgroundSprite;

            StartGame();

        }

        private void GameEngine(object? sender, EventArgs e)
        {
            Canvas.SetLeft(background, Canvas.GetLeft(background) - 3);
            Canvas.SetLeft(background2, Canvas.GetLeft(background2) - 3);

            if (Canvas.GetLeft(background) < -1262)
            {
                Canvas.SetLeft(background, Canvas.GetLeft(background2) + background2.Width);
            }

           

            if (Canvas.GetLeft(background2) < -1262) 

            {
                Canvas.SetLeft(background2, Canvas.GetLeft(background) + background.Width);

            }

            Canvas.SetTop(player, Canvas.GetTop(player)  + speed);
            Canvas.SetLeft(obstacle, Canvas.GetLeft(obstacle) - 12);
            scoreText.Content = "Score: " + score;

            PlayerHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width - 20, player.Height-5);
            ObstacleHitBox = new Rect(Canvas.GetLeft(obstacle), Canvas.GetTop(obstacle), obstacle.Width, obstacle.Height);
            GroundHitBox = new Rect(Canvas.GetLeft(ground), Canvas.GetTop(ground), ground.Width, ground.Height );

            if (PlayerHitBox.IntersectsWith(GroundHitBox))
            {
                speed = 0;

                Canvas.SetTop(player, Canvas.GetTop(ground) - player.Height);

                jumping = false;
                SpriteIndex += .5;
                if(SpriteIndex > 8)
                {
                    SpriteIndex = 1;
                }

                RunSprite(SpriteIndex);








            }

            if (jumping == true)
            {
                speed = -9;

                force -= 1;


            }
            else
            {

                speed = 12;
            }
            if (force<0)
            {
                jumping = false;
            }

            if (Canvas.GetLeft(obstacle)<50)   
            {
                Canvas.SetLeft(obstacle, 950);

                Canvas.SetTop(obstacle, ObstaclePosition[rand.Next(0, ObstaclePosition.Length)]);

                score += 1;

                

            }

            if(PlayerHitBox.IntersectsWith(ObstacleHitBox))
            {
                GameOver=true;
                timer.Stop();   
                GAMEOVERSCREEN.Visibility=Visibility.Visible;
                SCORE.Visibility=Visibility.Visible;
                scoreshower.Text = "Your Score: " + score;
                destro.Visibility=Visibility.Visible;

                Storyboard fadeInStoryBoard = (Storyboard)FindResource("FadeIn");
                fadeInStoryBoard.Begin();
                    
                    

            }

          












        }

      

        private void KeyisDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter && GameOver==true)
            {
                StartGame(); 
                   
            }
        }

        private void KeyisUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Space && jumping == false && Canvas.GetTop(player) > 260)
            {
                jumping = true;
                force = 15;
                speed = -12;

                playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/ASSETS/newRunner_02.gif"));
            }
        }

        private void StartGame() 
        {
            SCORE.Visibility=Visibility.Collapsed;
            GAMEOVERSCREEN.Visibility = Visibility.Collapsed;
            Canvas.SetLeft(background, 0);
            Canvas.SetLeft(background2, 1262);
            Canvas.SetLeft(player, 110);   
            Canvas.SetLeft(player, 140);
            Canvas.SetLeft(obstacle, 950);
            Canvas.SetTop(obstacle, 310);

            RunSprite(1);

            obstacleSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/ASSETS/obstacle.png"));
            obstacle.Fill= obstacleSprite;

            jumping = false;
            GameOver = false;
            score = 0;

            scoreText.Content = "Score: " + score;

            timer.Start();



        }

        private void RunSprite(double i)
        {

            switch (i)
            {
                case 1:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/ASSETS/newRunner_01.gif"));
                    break;
                    //it was vry tough to code this purely on c# without any engine like unity so please consider voting 
                case 2:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/ASSETS/newRunner_02.gif"));
                    break;

                case 3:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/ASSETS/newRunner_03.gif"));
                    break;

                case 4:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/ASSETS/newRunner_04.gif"));
                    break;

                case 5:

                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/ASSETS/newRunner_05.gif"));
                    break;

                case 6:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/ASSETS/newRunner_06.gif"));
                    break;

                case 7:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/ASSETS/newRunner_07.gif"));
                    break;

                case 8:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/ASSETS/newRunner_08.gif"));
                    break;

            }

            player.Fill = playerSprite;




        }
    }
}