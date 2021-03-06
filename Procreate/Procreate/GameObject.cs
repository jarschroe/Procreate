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
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Generation
{
    public class GameObject : INotifyPropertyChanged
    {
        // default values
        static string DefaultName = "New Game Object";
        static string DefaultType = "New Type";
        static string DefaultImagePath = Procreate.MainWindow.ControlPoint.ImagePathPrefix + "Images/Default.jpg";
        static int DefaultAppearRate = 50;

        public string Name { get; set; }
        public string Type { get; set; }
        public Image Image { get; set; }
        string imagePath;
        public string ImagePath
        {
            get
            {
                return imagePath;
            }
            set
            {
                if (imagePath != value)
                {
                    // load the new image
                    imagePath = value;
                    // notify application that the property has changed
                    OnPropertyChanged("ImagePath");
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(value);
                    bitmap.DecodePixelWidth = Procreate.ControlPoint.GridWidth / Procreate.ControlPoint.LevelWidth;
                    bitmap.EndInit();
                    Image.Source = bitmap;
                }
            }
        }
        public int AppearRate { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public GameObject()
        {
            Name = DefaultName;
            Type = DefaultType;
            Image = new Image();
            ImagePath = DefaultImagePath;
            AppearRate = DefaultAppearRate;
        }

        public GameObject(string name, string imagePath, int appearRate)
        {
            this.Name = name;
            Image = new Image();
            this.ImagePath = imagePath;
            AppearRate = DefaultAppearRate;
            // TODO: error if appear rate isn't between 0-100
            this.AppearRate = appearRate;
        }

        protected void OnPropertyChanged(string name)
        {
            // update the property's control to reflect the property change
            // code referenced from http://msdn.microsoft.com/en-us/library/ms743695(v=vs.110).aspx
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public void Generate() { }
        public void Serialise() { }
        public void Load() { }

        static public implicit operator Level.LevelElement(GameObject toConvert)
        {
            Level.LevelElement converted = new Level.LevelElement(toConvert);
            return converted;
        }
    }
}
