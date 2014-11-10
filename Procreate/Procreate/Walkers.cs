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

        static Random random = new Random();

        public Walker(int life, int changeDirectionChance, int radius, int roomRadius)
        {
            Life = life;
            ChangeDirectionChance = changeDirectionChance;
            Radius = radius;
            RoomRadius = roomRadius;

            // place walker in random location in the level
            Level.Level level = Procreate.MainWindow.ControlPoint.Level;
            X = random.Next(0, level.Elements[0].Count - 1);
            Y = random.Next(0, level.Elements.Count - 1);

            // default trail type
            TrailType = Procreate.MainWindow.ControlPoint.GameObjectFactory.CreateGameObject();
            TrailType.Name = "Floor";
            TrailType.ImagePath = Level.Level.FloorImagePath;
            TrailType.AppearRate = 100;

            // random direction
            Direction = (Direction)random.Next(0, 3);
        }

        public void Step()
        {
            int chance = random.Next(1, 100);

            // randomally change direction
            // make sure the walker's chance is valid (between 0-100)
            if (ChangeDirectionChance >= 0 && ChangeDirectionChance <= 100)
            {
                if (chance <= ChangeDirectionChance)
                {
                    // change direction
                    Direction = (Direction)random.Next(0, 3);
                }

                Level.Level level = Procreate.MainWindow.ControlPoint.Level;

                // step once in direction, wrapping the level if required
                if (Direction == Generation.Direction.LEFT)
                {
                    X = (--X + level.Elements[0].Count) % (level.Elements[0].Count);
                }
                else if (Direction == Generation.Direction.RIGHT)
                {
                    X = ++X % (level.Elements[0].Count);

                }
                else if (Direction == Generation.Direction.UP)
                {
                    Y = (--Y + level.Elements.Count) % (level.Elements.Count);
                }
                else if (Direction == Generation.Direction.DOWN)
                {
                    Y = ++Y % (level.Elements.Count);
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

    public class Walkers : Algorithm
    {
        List<Walker> WalkerList;
        public int MinWalkers;
        public int MaxWalkers;
        // walker attributes
        public int MinLife;
        public int MaxLife;
        public int MinChangeDirectionChance;
        public int MaxChangeDirectionChance;
        public int WalkerRadius;
        // create rooms attributes
        public int ChanceOfDeath;
        public bool CreateRoomsUponDeath;
        public int MinRoomRadius;
        public int MaxRoomRadius;

        public Walkers()
        {
            // default values
            MinWalkers = 20;
            MaxWalkers = 40;
            MinLife = 500;
            MaxLife = 800;
            MinChangeDirectionChance = 30;
            MaxChangeDirectionChance = 80;
            WalkerRadius = 1;
            ChanceOfDeath = 1;
            CreateRoomsUponDeath = false;
            MinRoomRadius = 2;
            MaxRoomRadius = 5;

            Random random = new Random();
            // create the walkers
            int walkerCount = random.Next(MinWalkers, MaxWalkers);
            WalkerList = new List<Walker>(walkerCount);

            for (int i = 0; i < walkerCount; ++i)
            {
                WalkerList.Add(new Walker(random.Next(MinLife, MaxLife), random.Next(MinChangeDirectionChance, MaxChangeDirectionChance), WalkerRadius, random.Next(MinRoomRadius, MaxRoomRadius)));
            }
        }

        public override void Generate()
        {
            Random random = new Random();

            while (WalkerList.Count > 0)
            {
                for (int i = 0; i < WalkerList.Count; ++i)
                {
                    // update the walker
                    Walker walker = WalkerList[i];
                    walker.Step();

                    // see if the walker has died

                    if (walker.Life <= 0 || random.Next(1, 100) <= ChanceOfDeath)
                    {
                        // walker has died
                        if (CreateRoomsUponDeath)
                        {
                            walker.CreateRoom();
                        }

                        // remove the walker
                        WalkerList.RemoveAt(i);
                    }
                }
            }
        }
    }
}
