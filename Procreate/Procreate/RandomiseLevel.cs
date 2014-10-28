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

namespace Generation
{
    public class GameObjectChancePair
    {
        public GameObject GameObject;
        public int Chance;

        public GameObjectChancePair(GameObject gameObject, int chance)
        {
            this.GameObject = gameObject;
            // TODO: could I use the game object's chance, instead of using two separate chance values?
            this.Chance = chance;
        }
    }

    public class RandomiseLevel : Generation.Algorithm
    {
        public List<GameObjectChancePair> GameObjectPool;

        public RandomiseLevel()
        {
            GameObjectPool = new List<GameObjectChancePair>();
        }

        public override void Generate()
        {
            int poolCount = GameObjectPool.Count();

            if (poolCount > 0)
            {
                Random random = new Random();
                Level.Level level = Procreate.MainWindow.ControlPoint.Level;

                // set each level element to a chance-weighted game object from the pool
                foreach (List<Level.LevelElement> row in level.Elements)
                {
                    for (int i = 0; i < row.Count(); ++i)
                    {
                        Level.LevelElement element = row[i];

                        bool elementSet = false;

                        // select a game object from the pool randomally, and use it if its chance is high enough
                        // e.g. if a random value between 0-100 equals 60, the first game object with a chance of 60 or anything lesser (i.e. 60% of the possible values) will be used
                        // whereas any game object with a low chance, e.g. 1 is unlikely to be used because the random value will rarely equal 1 or less (only 1% of the time)
                        while (!elementSet)
                        {
                            int index = random.Next(poolCount);
                            int randomValue = random.Next(1, 100);

                            GameObjectChancePair candidate = GameObjectPool[index];

                            // make sure the game object's chance is valid (between 0-100)
                            if (candidate.Chance >= 0 && candidate.Chance <= 100)
                            {
                                if (randomValue <= candidate.Chance)
                                {
                                    row[i] = candidate.GameObject;
                                    elementSet = true;
                                }
                            }
                            else
                            {
                                // TODO: error for candidate game object's chance not being valid
                            }
                        }
                    }
                }
            }
            else
            {
                // TODO: error message for no items in the pool
            }
        }
    }
}
