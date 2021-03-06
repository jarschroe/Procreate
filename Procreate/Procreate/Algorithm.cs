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
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Generation
{
    public enum AlgorithmType
    {
        RANDOMISE_LEVEL,
        CELLULAR_AUTOMATA,
        WALKERS
    }

    // List of supported procedural generation algorithms
    public class AlgorithmComboBoxValues : ObservableCollection<string>
    {
        // code from http://msdn.microsoft.com/en-us/library/system.windows.controls.combobox(v=vs.110).aspx
        public AlgorithmComboBoxValues()
        {
            Add("Randomise Level");
            Add("Cellular Automata");
            Add("Walkers");
        }
    }

    public abstract class Algorithm
    {
        public List<Algorithm> PreAlgorithms { get; set; }

        public Algorithm()
        {
            PreAlgorithms = new List<Algorithm>();
        }

        public abstract void Generate();
        public void Serialise() { }
        public void Load() { }
    }
}
