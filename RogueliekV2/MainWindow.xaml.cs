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
        public MainWindow() => this.InitializeComponent();

        private void MapGrid_Loaded(object sender, RoutedEventArgs e) => this.DrawMap();

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
    }
}
