using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MahjongGame
{
    class Game
    {
        public List<GameElement> Elements { set; get; }
        public int Counter { set; get; }
        public GameElement LastElement { set; get; }

        public Game()
        {
            Elements = new List<GameElement>();
            LastElement = null;
            Counter = 0;
        }

        public void AddElement(GameElement e)
        {
            if (Elements == null)
            {
                Elements = new List<GameElement>();
            }
            Elements.Add(e);
        }

        public void InitializeElements(int n)
        {
            int ElementsNumber = Elements.Count;
           
            for( int ii=0; ii<ElementsNumber; ii++)
            {
                Elements[ii].Index = (int)((double)(ii) / n);
            }

        }
        public void ShuffleElements(int mixNumber)
        {
            for(int ii=0; ii < mixNumber; ii++)
            {
                Random rnd = new Random(Guid.NewGuid().GetHashCode());
                int index1 = rnd.Next(Elements.Count);
                int index2 = rnd.Next(Elements.Count);
               
                if(index1 != index2)
                {
                    int tmp = Elements[index1].Index;
                    Elements[index1].Index = Elements[index2].Index;
                    Elements[index2].Index = tmp;
                }
            }
            UpdateElements();
        }

        public void UpdateElements()
        {
            foreach(GameElement e in Elements)
            {
                e.GameButton.Content = e.Index;
                BitmapImage bitimg = new BitmapImage();
                bitimg.BeginInit();
                bitimg.UriSource = new Uri(@"Resources/images" + e.Index + ".jpeg", UriKind.RelativeOrAbsolute);
                bitimg.EndInit();
                e.GameButton.Background = new ImageBrush(bitimg);
               // e.GameButton.BorderThickness = new System.Windows.Thickness(0,0, 5,5);

            }
        }
        //private DateTime Time { set; get; }
      public void DeletePairOfElements()
        {
            foreach(GameElement e in Elements)
            {
                if (LastElement.GameButton == e.GameButton) ;
            }

        }
       
    }
}
