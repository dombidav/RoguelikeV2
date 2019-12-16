using RoguelikeV2.Controlers;
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

namespace RoguelikeV2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool ExitPossible { get; private set; } = false;

        public MainWindow() => this.InitializeComponent();

        private void MapGrid_Loaded(object sender, RoutedEventArgs e)
        {
            this.DrawMap();
            Map.Reset();
            this.RedrawEntities();
        }

        private void RedrawEntities()
        {
            this.ClearMap();

            lbl_PlayerHealth.Content = Map.Player.HP;
            lbl_PlayerSteps.Content = Map.Player.Steps;
            lbl_PlayerRuby.Content = Map.Player.Ruby;
            lbl_level.Content = Map.Lvl;

            #region Player Init
            //V3-ban nem lesz külön a bitmap és a UIElement.....
            Map.Player.UIElement = new Image() { Source = Map.Player.Image, MaxHeight = 64, MaxWidth = 64, Name=Map.Player.Name };
            _ = mapGrid.Children.Add(Map.Player.UIElement);
            Grid.SetRow(Map.Player.UIElement, Map.Player.Position.Row + 1); //A falak miatt kell +1
            Grid.SetColumn(Map.Player.UIElement, Map.Player.Position.Column + 1);
            Map.Player.DamageTaken += this.Player_DamageTaken;
            Map.Player.Died += this.Player_Died;
            #endregion

            #region Exit Init
            Map.Exit.UIElement = new Image() { Source = Map.Exit.Image, MaxHeight = 64, MaxWidth = 64, Name=Map.Exit.Name };
            _ = mapGrid.Children.Add(Map.Exit.UIElement);
            Grid.SetRow(Map.Exit.UIElement, Map.Exit.Position.Row + 1);
            Grid.SetColumn(Map.Exit.UIElement, Map.Exit.Position.Column + 1);
            Map.Exit.PlayerExited += this.Exit_PlayerExited;
            #endregion

            #region Other Init
            foreach (var item in Map.Entites)
            {
                item.UIElement = new Image() { Source = item.Image, MaxHeight = 64, MaxWidth = 64, Name = item.Name };
                _ = mapGrid.Children.Add(item.UIElement);
                Grid.SetRow(item.UIElement, item.Position.Row + 1);
                Grid.SetColumn(item.UIElement, item.Position.Column + 1);
            }
            #endregion
        }

        private void ClearMap()
        {
            var list = new List<UIElement>();
            foreach (UIElement item in mapGrid.Children)
            {
                if ((item is Image) && !string.IsNullOrWhiteSpace(((Image)item).Name))
                    list.Add(item);
                    
            }
            list.ForEach(x => mapGrid.Children.Remove(x));

        }

        private void Exit_PlayerExited(object sender, EventArgs e)
        {
            if (ExitPossible)
            {
                ExitPossible = false; //Szóval a static class Map másik szálon futott, szóval egy exit event volt hogy 400x futott le
                Map.Reset(true);
                this.RedrawEntities(); 
            }
        }

        private void Player_Died(object sender, bool e)
        {
            this.ClearMap();
            _ = MessageBox.Show(e ? "Died" : "Rip");
        }

        private void Player_DamageTaken(object sender, byte e) => throw new NotImplementedException();

        private void DrawMap()
        {
            for (var row = 1; row < 11; row++)
            {
                for (var col = 0; col < 12; col++)
                {
                    var tempBitmapImage = new BitmapImage(new Uri("Img/Floor.png", UriKind.Relative)); ;
                    if (col == 0)
                        tempBitmapImage = new BitmapImage(new Uri("Img/LeftWall.png", UriKind.Relative));
                    else if(col == 11)
                        tempBitmapImage = new BitmapImage(new Uri("Img/RightWall.png", UriKind.Relative));
                    var tempImage = new Image() { Source = tempBitmapImage, MaxHeight=64, MaxWidth=64 };
                    _ = mapGrid.Children.Add(tempImage);
                    Grid.SetRow(tempImage, row);
                    Grid.SetColumn(tempImage, col);
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W:
                case Key.Up:
                    if(Map.Player.MoveCol(-1)) Map.Tick();
                    break;
                case Key.S:
                case Key.Down:
                    if (Map.Player.MoveCol(1)) Map.Tick();
                    break;
                case Key.A:
                case Key.Left:
                    if (Map.Player.MoveRow(-1)) Map.Tick();
                    break;
                case Key.D:
                case Key.Right:
                    if (Map.Player.MoveRow(1)) Map.Tick();
                    break;
                default:
                    break;
            }
            ExitPossible = true;
            this.RedrawEntities();
        }
    }
}
