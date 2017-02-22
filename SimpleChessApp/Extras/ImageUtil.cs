﻿using System;
using System.Drawing;
using System.Drawing.Imaging;

public static class ImageUtil
{
    private const int bytesPerPixel = 4;

    /// <summary>
    /// Change the opacity of an image
    /// </summary>
    /// <param name="originalImage">The original image</param>
    /// <param name="opacity">Opacity, where 1.0 is no opacity, 0.0 is full transparency</param>
    /// <returns>The changed image</returns>
    public static Image Opacity(this Image originalImage, double opacity)
    {
        if ((originalImage.PixelFormat & PixelFormat.Indexed) == PixelFormat.Indexed)
        {
            // Cannot modify an image with indexed colors
            return originalImage;
        }

        Bitmap bmp = (Bitmap)originalImage.Clone();

        // Specify a pixel format.
        PixelFormat pxf = PixelFormat.Format32bppArgb;

        // Lock the bitmap's bits.
        Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
        BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, pxf);

        // Get the address of the first line.
        IntPtr ptr = bmpData.Scan0;

        // Declare an array to hold the bytes of the bitmap.
        // This code is specific to a bitmap with 32 bits per pixels 
        // (32 bits = 4 bytes, 3 for RGB and 1 byte for alpha).
        int numBytes = bmp.Width * bmp.Height * bytesPerPixel;
        byte[] argbValues = new byte[numBytes];

        // Copy the ARGB values into the array.
        System.Runtime.InteropServices.Marshal.Copy(ptr, argbValues, 0, numBytes);

        // Manipulate the bitmap, such as changing the
        // RGB values for all pixels in the the bitmap.
        for (int counter = 0; counter < argbValues.Length; counter += bytesPerPixel)
        {
            // argbValues is in format BGRA (Blue, Green, Red, Alpha)

            // If 100% transparent, skip pixel
            if (argbValues[counter + bytesPerPixel - 1] == 0)
                continue;

            int pos = 0;
            pos++; // B value
            pos++; // G value
            pos++; // R value

            argbValues[counter + pos] = (byte)(argbValues[counter + pos] * opacity);
        }

        // Copy the ARGB values back to the bitmap
        System.Runtime.InteropServices.Marshal.Copy(argbValues, 0, ptr, numBytes);

        // Unlock the bits.
        bmp.UnlockBits(bmpData);

        return bmp;
    }
}