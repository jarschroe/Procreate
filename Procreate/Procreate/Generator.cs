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

namespace Generation
{
    public class Generator
    {
        public ObservableCollection<Method> Methods { get; private set; }
        public List<GameObject> GameObjects { get; private set; }

        public Generator()
        {
            Methods = new ObservableCollection<Method>();
            GameObjects = new List<GameObject>();
        }

        public void Generate()
        {
            // generate each of the methods
            foreach (Method method in Methods)
            {
                method.Generate();
            }

            // TODO: generate each of the game objects
        }

        public void AddMethod(Method method)
        {
            // add a copy of the Method
            Methods.Add(method);
        }

        public void AddGameObject(GameObject gameObject, GameObject before, GameObject after) { }
        public void RemoveMethod(Method method) { }
        public void RemoveGameObject(GameObject gameObject) { }
    }
}
