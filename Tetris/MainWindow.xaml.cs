using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tetris.Blocks;

namespace Tetris
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ImageSource[] tileImages = new ImageSource[]
            {
                new BitmapImage(new Uri("assets/TileEmpty.png", UriKind.Relative)),
                new BitmapImage(new Uri("assets/TileCyan.png", UriKind.Relative)),
                new BitmapImage(new Uri("assets/TileYellow.png", UriKind.Relative)),
                new BitmapImage(new Uri("assets/TileBlue.png", UriKind.Relative)),
                new BitmapImage(new Uri("assets/TileOrange.png", UriKind.Relative)),
                new BitmapImage(new Uri("assets/TilePurple.png", UriKind.Relative)),
                new BitmapImage(new Uri("assets/TileRed.png", UriKind.Relative)),
                new BitmapImage(new Uri("assets/TileGreen.png", UriKind.Relative)),

            };
        private readonly ImageSource[] blockImages = new ImageSource[]
            {
             new BitmapImage(new Uri("assets/Block-Empty.png", UriKind.Relative)),
             new BitmapImage(new Uri("assets/Block-I.png", UriKind.Relative)),
             new BitmapImage(new Uri("assets/Block-O.png", UriKind.Relative)),
             new BitmapImage(new Uri("assets/Block-J.png", UriKind.Relative)),
             new BitmapImage(new Uri("assets/Block-L.png", UriKind.Relative)),
             new BitmapImage(new Uri("assets/Block-S.png", UriKind.Relative)),
             new BitmapImage(new Uri("assets/Block-Z.png", UriKind.Relative)),
             new BitmapImage(new Uri("assets/Block-T.png", UriKind.Relative)),

            };

        private readonly Image[,] imageControls;
        private GameState gameState = new GameState();
        public MainWindow()
        {
            InitializeComponent();
            imageControls = SetupGameCanvas(gameState.GameGrid);
        }
        private Image[,] SetupGameCanvas(GameGrid grid)
        {
            Image[,] imageControls = new Image[grid.Rows, grid.Columns];
            int cellSize = 25;
            for (int r = 0; r < grid.Rows; r++)
            {
                for (int c = 0; c < grid.Columns; c++)
                {
                    Image imageControl = new Image
                    {
                        Width = cellSize,
                        Height = cellSize
                    };

                    // place image to canva
                    Canvas.SetTop(imageControl, (r - 2) * cellSize);
                    Canvas.SetLeft(imageControl, c * cellSize);
                    GameCanvas.Children.Add(imageControl);
                    imageControls[r, c] = imageControl;
                }

            }
            return imageControls;

        }

        private void DrawGrid(GameGrid grid)
        {
            for (int r = 0; r < grid.Rows; r++)
                for (int c = 0; c < grid.Columns; c++)
                {
                    int id = grid[r, c];
                    imageControls[r, c].Source = tileImages[id];
                }
        }
        private void DrawBlock(Block block)
        {
            foreach (Position p in block.TilePosition())
            {
                imageControls[p.Row, p.Column].Source = tileImages[block.Id];
            }
        }

        private void Draw(GameState gameState)
        {
            DrawGrid(gameState.GameGrid);
            DrawBlock(gameState.CurrentBlock);
            BlockNextImage.Source = blockImages[gameState.BlockQueue.NextBlock.Id];
            ScoreText.Text = "Score: " + gameState.Score.ToString();
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameState.GameOver)
                return;
            switch (e.Key)
            {
                case Key.Left:
                case Key.A:
                    gameState.MoveBlockLeft();
                    break;

                case Key.Right:
                case Key.D:
                    gameState.MoveBlockRight();
                    break;
                case Key.Down:
                case Key.S:
                    gameState.MoveBlockDown();
                    break;
                case Key.Up:
                case Key.W:
                    gameState.RotateBlockCW();
                    break;
            }
            Draw(gameState);

        }

        private async Task GameLoop()
        {
            Draw(gameState);

            while (!gameState.GameOver)
            {
                await Task.Delay(500);
                gameState.MoveBlockDown();
                Draw(gameState);
            }

            FinalScoreText.Text = "Score: " + gameState.Score.ToString();
            GameOverMenu.Visibility = Visibility.Visible;
        }

        private async void GameCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            await GameLoop();
        }


        private async void PlayAgainButton_Click(object sender, RoutedEventArgs e)
        {
            gameState.Reset();
            GameOverMenu.Visibility = Visibility.Hidden;
            await GameLoop();

        }
    }
}