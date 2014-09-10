// The MIT License (MIT)

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

namespace Level
{
    public class Level
    {
        public List<List<LevelElement>> elements { get; set; }
        public int width { get; set; }
        public int height { get; set; }

        static string GrassImagePath = @"../../Images/Grass.jpg";

        public Level(int width, int height)
        {
            // create the level container
            this.elements = new List<List<LevelElement>>();

            for (int i = 0; i < height; ++i)
            {
                // create row containers
                this.elements.Add(new List<LevelElement>());
                
                for (int j = 0; j < height; ++j)
                {
                    // create Level Elements and fill the rows with them
                    this.elements[i].Add(new LevelElement(GrassImagePath));
                    // initialise the created Level Element
                    this.elements[i][j].Init();
                }
            }

            this.width = width;
            this.height = height;
        }

        public void Reset() { }
        public void Serialise() { }
        public void Load() { }
    }
}
