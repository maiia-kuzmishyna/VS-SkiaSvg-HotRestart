using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using SkiaSharp;
using SkiaSharp.Views.iOS;
using UIKit;

namespace Spike.Platform.iOS
{
   public class CustomButtonHandler : ButtonHandler
   {

      protected override UIButton CreatePlatformView()
      {
         var button = base.CreatePlatformView();
         SKImage skImage = MainPage.GetImageFromSvg("info.svg", Colors.Red.ToArgbHex(), 48, 48);
         var image = skImage.ToUIImage();
         skImage.Dispose();
         button.SetImage(image, UIControlState.Normal);
         return button;
      }


   }
}

