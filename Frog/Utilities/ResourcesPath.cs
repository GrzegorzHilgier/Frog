using System.Windows.Media;
using System.Windows.Media.Imaging;
using System;

namespace Frog.Utilities
{
    static class ResourcesPath
    {

        public static readonly string CarPath = "pack://application:,,,/resources/Car.png";
        public static readonly string FrogPath = "pack://application:,,,/resources/FrogImg.png";
        public static readonly string GrassPath = "pack://application:,,,/resources/Grass.png";
        public static readonly string PodEmptyPath = "pack://application:,,,/resources/PodEmpty.png";
        public static readonly string RoadPath = "pack://application:,,,/resources/Road.png";
        public static readonly string WaterPath = "pack://application:,,,/resources/Water.bmp";
        public static readonly string WoodPath = "pack://application:,,,/resources/Wood.png";

        public static ImageSource BitmapFromPath(String path)
        {
            Uri uri = new Uri(path);
            return BitmapFromUri(uri);
        }

        public static ImageSource BitmapFromUri(Uri source)
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = source;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            return bitmap;
        }
    }
}
