using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FootballClubsClient
{
    /// <summary>
    /// Класс позволяет преобразовать данные из одного типа в другой
    /// </summary>
    class DataConverter
    {
        /// <summary>
        /// Преобразует массив байт в Bitmap
        /// </summary>
        /// <param name="imageData"> Массив байт </param>
        /// <returns> Bitmap </returns>
        public static BitmapImage BitmapFromByteArray(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0)
                return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }

        /// <summary>
        /// Преобразует массив байт в SoundPlayer
        /// </summary>
        /// <param name="audioData"> Массив байт </param>
        /// <returns> SoundPlayer </returns>
        public static SoundPlayer AudioFromByteArray(byte[] audioData)
        {
            if (audioData == null || audioData.Length == 0)
                return null;

            var mediaStream = new MemoryStream(audioData);

            return new SoundPlayer(mediaStream); ;
        }
    }
}
