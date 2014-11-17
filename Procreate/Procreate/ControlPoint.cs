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

namespace Procreate
{
    public class ControlPoint
    {
        public Level.Level Level { get; set; }
        public Generation.AlgorithmComboBoxValues Algorithms { get; set; }
        public Generation.GameObjectFactory GameObjectFactory { get; private set; }
        public Generation.MethodFactory MethodFactory { get; private set; }
        public Generation.Generator Generator { get; private set; }
        //TEMP
        public static int GridWidth = 620;
        public static int GridHeight = 620;
        public static int LevelWidth = 64;
        public static int LevelHeight = 64;
        //TEMP
        public string ImagePathPrefix = "pack://siteoforigin:,,,/";

        public ControlPoint()
        {
            Level = new Level.Level(LevelWidth, LevelHeight);
            Algorithms = new Generation.AlgorithmComboBoxValues();
            GameObjectFactory = new Generation.GameObjectFactory();
            MethodFactory = new Generation.MethodFactory();
            Generator = new Generation.Generator();
        }
    }
}
