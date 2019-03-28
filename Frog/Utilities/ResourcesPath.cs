using System.Windows.Media;
using System.Windows.Media.Imaging;
using System;

namespace Frog.Utilities
{
    static class ResourcesPath
    {
        public static readonly string Car = @"\Frog;component\resources\Car.png";
        public static readonly string Frog = @"\Frog;component\resources\FrogImg.png";
        public static readonly string Grass = @"\Frog;component\resources\Grass.png";
        public static readonly string PodEmpty = @"\Frog;component\resources\PodEmpty.png";
        public static readonly string Road = @"\Frog;component\resources\Road.png";
        public static readonly string Water = @"\Frog;component\resources\Water.bmp";
        public static readonly string Wood = @"\Frog;component\resources\Wood.png";

        public static readonly string CarUri = "pack://application:,,,/resources/Car.png";
        public static readonly string FrogUri = "pack://application:,,,/resources/Car.png";
        public static readonly string GrassUri = "pack://application:,,,/resources/Car.png";
        public static readonly string PodEmptyUri = "pack://application:,,,/resources/Car.png";
        public static readonly string RoadUri = "pack://application:,,,/resources/Car.png";
        public static readonly string WaterUri = "pack://application:,,,/resources/Car.png";
        public static readonly string WoodUri = "pack://application:,,,/resources/Car.png";

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
