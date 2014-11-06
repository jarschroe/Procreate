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

namespace Generation
{
    enum Direction
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }

    class Walker
    {
        public int Life;
        public int ChangeDirectionChance;
        public int Radius;
        public Direction Direction;
        public GameObject TrailType;
        // Position
        public int X;
        public int Y;
        // Members used if walkers create rooms upon death
        public int RoomRadius;

        public Walker()
        {
            // place walker in random location in the level
            Level.Level level = Procreate.MainWindow.ControlPoint.Level;
            Random random = new Random();
            X = random.Next(0, level.Elements[0].Count - 1);
            Y = random.Next(0, level.Elements.Count - 1);

            // default trail type
            TrailType = Procreate.MainWindow.ControlPoint.GameObjectFactory.CreateGameObject();
            TrailType.Name = "Floor";
            TrailType.ImagePath = Level.Level.FloorImagePath;
            TrailType.AppearRate = 100;

            // defaults
            Radius = 1;
            ChangeDirectionChance = 25;
            RoomRadius = 1;
        }

        public void Step()
        {
            Random random = new Random();
            int chance = random.Next(1, 100);

            // randomally change direction
            // make sure the walker's chance is valid (between 0-100)
            if (ChangeDirectionChance >= 0 && ChangeDirectionChance <= 100)
            {
                if (ChangeDirectionChance <= chance)
                {
                    // change direction
                    Direction = (Direction)random.Next(0, 3);
                }

                Level.Level level = Procreate.MainWindow.ControlPoint.Level;

                // step once in direction, wrapping the level if required
                if (Direction == Generation.Direction.LEFT)
                {
                    X = (--X + level.Elements[0].Count - 1) % (level.Elements[0].Count - 1);
                }
                else if (Direction == Generation.Direction.RIGHT)
                {
                    X = ++X % (level.Elements[0].Count - 1);

                }
                else if (Direction == Generation.Direction.UP)
                {
                    Y = (--Y + level.Elements.Count - 1) % (level.Elements.Count - 1);
                }
                else if (Direction == Generation.Direction.DOWN)
                {
                    Y = ++Y % (level.Elements.Count - 1);
                }

                // apply walker's trail to current location
                level.Elements[Y][X] = TrailType;

                // reduce life
                --Life;
            }
            else
            {
                // TODO: error for walker's change direction chance not being valid
            }
        }

        public void CreateRoom()
        {
            // create room within RoomRadius
        }
    }

    class Walkers : Algorithm
    {
        public bool CreateRoomsUponDeath;

        public Walkers()
        {
            CreateRoomsUponDeath = false;
        }

        public override void Generate()
        {

        }
    }
}
