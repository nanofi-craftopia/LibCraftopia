using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.IO;

//Depends on UnityEngine.ImageConversionModule
namespace LibCraftopia.Utils
{
    public static class ResourceLoader
    {

        public static Sprite ReadSprite(string path)
        {
            var png = ReadPng(path);
            return Sprite.Create(png, new Rect(0, 0, png.width, png.height), new Vector2(0, 1), 1);
        }

        private static byte[] ReadPngFile(string path)
        {
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            BinaryReader bin = new BinaryReader(fileStream);
            byte[] values = bin.ReadBytes((int)bin.BaseStream.Length);

            bin.Close();

            return values;
        }

        private static Texture2D ReadPng(string path)
        {
            byte[] readBinary = ReadPngFile(path);

            int pos = 16;

            int width = 0;
            for (int i = 0; i < 4; i++)
            {
                width = width * 256 + readBinary[pos++];
            }

            int height = 0;
            for (int i = 0; i < 4; i++)
            {
                height = height * 256 + readBinary[pos++];
            }

            Texture2D texture = new Texture2D(width, height);
            ImageConversion.LoadImage(texture, readBinary);
            return texture;
        }


    }
}
