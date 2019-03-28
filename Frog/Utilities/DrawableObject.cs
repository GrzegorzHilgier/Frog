
using System.Windows.Media;

namespace Frog.Utilities
{

    abstract class DrawableObject : ObservableObject
    {
        private bool isVisible = true;
        public bool IsVisible
        {
            get => isVisible;
            protected set
            {
                isVisible = value;
                RaisePropertyChangedEvent(nameof(IsVisible));
            }
        }

        private ImageSource image;
        public ImageSource Image
        {
            get => image;
            protected set
            {
                image = value;
                RaisePropertyChangedEvent(nameof(Image));
            }
        }

        private string imagePath;
        public string ImagePath
        {
            get => imagePath;
            protected set
            {
                imagePath = value;
                RaisePropertyChangedEvent(nameof(ImagePath));
            }
        }

        private int xcoord;
        public virtual int Xcoord
        {
            get => xcoord;
            set
            {
                    xcoord = value;
                    RaisePropertyChangedEvent(nameof(Xcoord));
            }
        }
        private int ycoord;
        public virtual int Ycoord
        {
            get => ycoord;
            set
            {
                ycoord = value;
                RaisePropertyChangedEvent(nameof(Ycoord));
            }
        }
        public int Width { get; set; }
        public int Height { get; set; }
        public int StartXcoord { get; set; } = 0;
        public int StartYcoord { get; set; } = 0;

        public DrawableObject( int x, int y, int width, int height)
        {           
            Width = width;
            Height = height;
            Xcoord = x;
            Ycoord = y;

        }

        public virtual bool CheckIfCollisionWith(DrawableObject item)
        {
            double opacity = item.Width*0.3;

            if (item.Xcoord + item.Width-opacity >= Xcoord && item.Xcoord + opacity <= Xcoord + Width && item.Ycoord + item.Height - opacity >= Ycoord && item.Ycoord <= Ycoord + Height - opacity)
            {
                return true;
            }
            else return false;
        }

        public abstract void Die();
       

    }
}
