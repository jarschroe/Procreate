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

namespace Generation
{
    public class Method
    {
        public string Name { get; set; }
        public Algorithm Algorithm { get; private set; }

        AlgorithmType algorithmType;
        private Method method;
        public AlgorithmType AlgorithmType
        {
            get
            {
                return algorithmType;
            }
            set
            {
                // adjust algorithm according to the chosen algorithm type
                if (algorithmType != value)
                {
                    switch (value)
                    {
                        case AlgorithmType.RANDOMISE_LEVEL:
                            {
                                Algorithm = new RandomiseLevel();
                                break;
                            }
                        case AlgorithmType.CELLULAR_AUTOMATA:
                            {
                                Algorithm = new CellularAutomata();
                                break;
                            }
                        case AlgorithmType.WALKERS:
                            {
                                Algorithm = new Walkers();
                                break;
                            }
                    }

                    algorithmType = value;
                }
            }
        }

        public Method()
        {
            // default empty name
            Name = "New Method";
            // default to Randomise Level algorithm
            AlgorithmType = Generation.AlgorithmType.RANDOMISE_LEVEL;
        }

        public Method(Method method)
        {
            Name = method.Name;
            AlgorithmType = method.AlgorithmType;
        }

        public void Generate()
        {
            // generate level with the algorithm
            Algorithm.Generate();
        }

        public void Serialise() { }
        public void Load() { }
    }
}
