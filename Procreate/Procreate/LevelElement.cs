﻿// The MIT License (MIT)

// Copyright (c) 2014 Jared Schroeder (jarschroe)

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Level
{
    public class LevelElement
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string ImagePath { get; set; }
        public Image Image { get; set; }

        static int ImageWidth = 32;

        public LevelElement(string path)
        {
            this.Name = "Name";
            this.ImagePath = path;
            this.Image = new Image();
            Image.Width = ImageWidth;
        }

        public void Init()
        {
            // load the image (code adapted from http://msdn.microsoft.com/en-us/library/ms748873(v=vs.110).aspx?cs-save-lang=1&cs-lang=csharp#_displayingimages)
            BitmapImage source = new BitmapImage();
            // load the image source
            source.BeginInit();
            source.UriSource = new Uri(ImagePath, UriKind.RelativeOrAbsolute);
            source.DecodePixelWidth = ImageWidth; 
            source.EndInit();
            // set the image source
            Image.Source = source;
        }

        public void Serialise() { }
        public void Load() { }
    }
}
