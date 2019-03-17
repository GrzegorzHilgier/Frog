﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System.Drawing;



namespace Frog.Utilities
{
    class DrawableObject:ObservableObject
    {
        private String imagePath;
        public String ImagePath
        {
            get => imagePath;
            protected set
            {
                imagePath = value;
                RaisePropertyChangedEvent("ImagePath");
            }
        }

        private int xcoord;
        public virtual int Xcoord
        {
            get => xcoord;
            set
            {
                if(CheckIfXMovePossible(value))
                {
                    xcoord = value;
                    RaisePropertyChangedEvent("Xcoord");
                }
            }
        }
        private int ycoord;
        public virtual int Ycoord
        {
            get => ycoord;
            set
            {
                if (CheckIfYMovePossible(value))
                {
                    ycoord = value;
                    RaisePropertyChangedEvent("Ycoord");
                }
            }
        }
        public int Width { get; set; }
        public int Height { get; set; }
        //public BitmapImage Image { get; set; }

        public virtual bool CheckIfXMovePossible(int value)
        {
            return true;
        }
        public virtual bool CheckIfYMovePossible(int value)
        {
            return true;
        }
        public virtual void CheckIfOn(DrawableObject item) { }

    }
}
