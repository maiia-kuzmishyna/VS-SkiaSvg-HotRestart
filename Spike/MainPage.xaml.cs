using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Microsoft.Maui;
using System.IO;
using System.Linq;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Graphics;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using SKSvg = SkiaSharp.Extended.Svg.SKSvg;

namespace Spike;

public partial class MainPage : ContentPage
{
   private int count = 0;
   private ObservableCollection<string> tabItems;

   public MainPage()
   {
      InitializeComponent();
      tabItems = new ObservableCollection<string>();
      tabItems.Add("Tab 1");
      tabItems.Add("Tab 2");

      Debug.WriteLine("DeviceDisplayInfo on Button click");
      Debug.WriteLine("Density: " + DeviceDisplay.Current.MainDisplayInfo.Density);
      Debug.WriteLine("Width: " + DeviceDisplay.Current.MainDisplayInfo.Width);
      Debug.WriteLine("Height: " + DeviceDisplay.Current.MainDisplayInfo.Height);
      Debug.WriteLine("Orientation: " + DeviceDisplay.Current.MainDisplayInfo.Orientation);
      Debug.WriteLine("");
   }

   public ObservableCollection<string> TabItems
   {
      get => tabItems;
      set
      {
         tabItems = value;
         OnPropertyChanged("TabItems");
      }
   }

   internal static SKImage GetImageFromSvg(string name, string hexColor, int? width, int? height)
   {
      SKImage skImage;

      var realName = typeof(MainPage).Assembly.GetManifestResourceNames().FirstOrDefault(x => x.EndsWith(name, StringComparison.Ordinal));

      using (var stream = typeof(MainPage).Assembly.GetManifestResourceStream(realName))
      {
         if (stream == null)
            throw new FileNotFoundException(
               $"ImageHelper : could not load SVG file {name} in assembly {typeof(MainPage).Assembly}. Make sure the ID is correct, the file is there and it is set to Embedded Resource build action.");

         SKSvg svg = new SKSvg();
         svg.Load(stream);

         SKImageInfo imageInfo = new SKImageInfo(width ?? 24, height ?? 24);

         using (SKSurface surface = SKSurface.Create(imageInfo))
         {
            using (SKPaint paint = new SKPaint())
            {
               if (!string.IsNullOrWhiteSpace(hexColor))
               {
                  paint.ColorFilter = SKColorFilter.CreateBlendMode(Color.FromArgb(hexColor).ToSKColor(), SKBlendMode.SrcIn);
               }

               SKMatrix scaleMatrix = GetScaleMatrix(imageInfo, svg.Picture.CullRect);
               surface.Canvas.DrawPicture(svg.Picture, ref scaleMatrix, paint);
               skImage = surface.Snapshot();
            }
         }
      }

      return skImage;
   }

   internal static SKMatrix GetScaleMatrix(SKImageInfo canvasInfo, SKRect svgRect, Aspect aspect = Aspect.AspectFill)
   {
      if (svgRect.Width < 0.00001f || svgRect.Height < 0.0000001f)
         return SKMatrix.CreateIdentity();
      float widthRatio = canvasInfo.Width / svgRect.Width;
      float heightRatio = canvasInfo.Height / svgRect.Height;
      switch (aspect)
      {
         case Aspect.AspectFit:
            widthRatio = heightRatio = Math.Min(widthRatio, heightRatio);
            break;
         case Aspect.AspectFill:
            widthRatio = heightRatio = Math.Max(widthRatio, heightRatio);
            break;
         case Aspect.Fill:
            break;
         default:
            throw new ArgumentOutOfRangeException();
      }

      return SKMatrix.CreateScale(widthRatio, heightRatio);
   }
}