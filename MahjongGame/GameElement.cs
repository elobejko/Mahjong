using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MahjongGame
{
    class GameElement 
    {
        public Button GameButton { get; set; }
        public int Index { get; set; }
        public Button BottomRectangle;
        public Button RightRectangle;

        public  GameElement( int x, int y, int index, int zindex, Canvas parent)
        {
            GameButton = new System.Windows.Controls.Button();
            
            GameButton.Margin = new Thickness(x - (zindex-1)*15, y- (zindex - 1) * 15, 0,0);
            GameButton.Width = 70;
            GameButton.Height = 56;
            Index = index;              
            parent.Children.Add(GameButton);
            Canvas.SetZIndex(GameButton, 2 * zindex);

            BottomRectangle = new Button();
            RightRectangle = new Button();
            BottomRectangle.Margin = new Thickness(x - (zindex - 1) * 15, y - (zindex - 1) * 15 + 56, 0, 0);
            RightRectangle.Margin = new Thickness(x - (zindex - 1) * 15 + 70, y - (zindex - 1) * 15, 0, 0);
            BottomRectangle.Width = (int) GameButton.Width;
            BottomRectangle.Height = 15;
            RightRectangle.Height = (int)GameButton.Height;
            RightRectangle.Width = 15;

            SkewTransform mySkewX = new SkewTransform();
            mySkewX.AngleX = 45;
            mySkewX.AngleY = 0;

            SkewTransform mySkewY = new SkewTransform();
            mySkewY.AngleX = 0;
            mySkewY.AngleY = 45;

            
            Canvas.SetZIndex(BottomRectangle, 2*zindex - 1);
            Canvas.SetZIndex(RightRectangle, 2 * zindex - 1);
            BottomRectangle.RenderTransform = mySkewX;
            BottomRectangle.BorderBrush = Brushes.Black;
            RightRectangle.BorderBrush = Brushes.Black;
            RightRectangle.RenderTransform = mySkewY;
            parent.Children.Add(BottomRectangle);
            parent.Children.Add(RightRectangle);
            RightRectangle.IsEnabled = false;
            BottomRectangle.IsEnabled = false;

        }
        
    }
}
