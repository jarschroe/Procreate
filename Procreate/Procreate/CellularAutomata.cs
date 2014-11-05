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

using System.Linq;

namespace Generation
{
    class CellularAutomata : Algorithm
    {
        public int IterationCount;

        static int DefaultIterationCount = 1;
        // TEMP: floor/wall game objects
        static string WallDummy = "sand";
        static string FloorDummy = "grass";
        static int WallChance = 50;
        static int FloorChance = 50; 

        public CellularAutomata()
        {
            // default iteration count
            IterationCount = DefaultIterationCount;

            // add the randomise level pre-algorithm
            Generation.RandomiseLevel randomise = new RandomiseLevel();
            Generation.GameObject floor = new Generation.GameObject(FloorDummy, Level.Level.FloorImagePath, 50);
            Generation.GameObjectChancePair floorPair = new Generation.GameObjectChancePair(floor, FloorChance);
            Generation.GameObject wall = new Generation.GameObject(WallDummy, Level.Level.WallImagePath, 50);
            Generation.GameObjectChancePair wallPair = new Generation.GameObjectChancePair(wall, WallChance);
            randomise.GameObjectPool.Add(floorPair);
            randomise.GameObjectPool.Add(wallPair);
            PreAlgorithms.Add(randomise);
        }

        public override void Generate() 
        {
            // do all the pre-algorithms
            foreach (Algorithm algorithm in PreAlgorithms)
            {
                algorithm.Generate();
            }

            Level.Level level = Procreate.MainWindow.ControlPoint.Level;
            Generation.GameObject wall = new Generation.GameObject(WallDummy, Level.Level.WallImagePath, 50);
            Generation.GameObject floor = new Generation.GameObject(FloorDummy, Level.Level.FloorImagePath, 50);

            // iterate
            for (int iteration = 0; iteration < IterationCount; ++iteration)
            {
                for (int i = 0; i < level.Elements.Count(); ++i)
                {
                    for (int j = 0; j < level.Elements[i].Count(); ++j)
                    {
                        // set the element to a wall if most of its neighbours are walls
                        // otherwise it's in a grass area, so make it grass
                        int wallTally = 0;
                        int requiredWalls = 5;

                        for (int y = -1; y <= 1; ++y)
                        {
                            for (int x = -1; x <= 1; ++x)
                            {
                                int rowIndex = i + y;
                                int colIndex = j + x;

                                // require less neighbour walls if this element is a corner or border
                                if (rowIndex < 0 || rowIndex >= level.Elements.Count()
                                    || colIndex < 0 || colIndex >= level.Elements[i].Count())
                                {
                                    --requiredWalls;
                                }
                                else
                                {
                                    // the neighbour exists - so use it
                                    Level.LevelElement element = level.Elements[rowIndex][colIndex];

                                    if (element.Name == WallDummy)
                                    {
                                        wallTally++;
                                    }
                                }
                            }
                        }

                        if (wallTally >= requiredWalls)
                        {
                            level.Elements[i][j] = wall;
                        }
                        else
                        {
                            level.Elements[i][j] = floor;
                        }
                    }
                }
            }
        }
    }
}
