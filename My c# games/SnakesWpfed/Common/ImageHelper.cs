using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SnakesWpfed.Common
{
    public static class ImageHelper
    {
        public static byte[] appleImageByteArray = File.ReadAllBytes(@"Pictures/apple.jpg");
        public static byte[] snakeHeadImageByteArray = File.ReadAllBytes(@"Pictures/snake_head.jpg");
        public static byte[] snakeBodyImageByteArray = File.ReadAllBytes(@"Pictures/snakeBody.jpg");
        public static byte[] grassImageByteArray = File.ReadAllBytes(@"Pictures/grass.jpg");

        public static void changeHeadImage(string newPath)
        {
            snakeHeadImageByteArray = File.ReadAllBytes(newPath);
        }
        public static void changeBodyImage(string newPath)
        {
            snakeBodyImageByteArray = File.ReadAllBytes(newPath);
        }
        public static void changeAppleImage(string newPath)
        {
            appleImageByteArray = File.ReadAllBytes(newPath);
        }
        public static void changeGrassImage(string newPath)
        {
            grassImageByteArray = File.ReadAllBytes(newPath);
        }
        public static void changeToDefaults()
        {
            appleImageByteArray = File.ReadAllBytes(@"Pictures/apple.jpg");
            snakeHeadImageByteArray = File.ReadAllBytes(@"Pictures/snake_head.jpg");
            snakeBodyImageByteArray = File.ReadAllBytes(@"Pictures/snakeBody.jpg");
            grassImageByteArray = File.ReadAllBytes(@"Pictures/grass.jpg");
        }
    }
}

