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
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Procreate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ControlPoint ControlPoint { get; set; }

        static int TextBoxWidth = 82;
        static int TextBoxHeight = 23;

        public MainWindow()
        {
            InitializeComponent();

            // load Jared's Procreate software
            ControlPoint = new ControlPoint();

            LevelElements.ItemsSource = ControlPoint.Level.Elements;
            // set the algorithm type values for the combo box
            AlgorithmComboBox.ItemsSource = ControlPoint.Algorithms;
        }

        private void Exit_Click_1(object sender, RoutedEventArgs e)
        {
            // close the application (code from http://stackoverflow.com/questions/2820357/how-to-exit-a-wpf-app-programmatically)
            this.Close();
        }

        private void AlgorithmComboBox_DropDownClosed_1(object sender, EventArgs e)
        {
            // clear the previous parameter controls displayed to the user
            ParameterGrid.Children.Clear();

            switch (AlgorithmComboBox.Text)
            {
                // adjust Method GUI to suit parameters for the chosen algorithm type
                // TODO: access names from a program-wide array rather than magic values
                case "Randomise Level":
                    {
                        // debugging Randomise level algorithm
                        Generation.RandomiseLevel rl = new Generation.RandomiseLevel();
                        Generation.GameObject grass = new Generation.GameObject("grass", Level.Level.GrassImagePath, 50);
                        Generation.GameObjectChancePair grassPair = new Generation.GameObjectChancePair(grass, 75);
                        Generation.GameObject sand = new Generation.GameObject("sand", Level.Level.SandImagePath, 50);
                        Generation.GameObjectChancePair sandPair = new Generation.GameObjectChancePair(sand, 25);

                        rl.GameObjectPool.Add(grassPair);
                        rl.GameObjectPool.Add(sandPair);

                        rl.Generate();
                        UpdateLevelView();
                        break;
                    }
                case "Cellular Automata":
                    {
                        Label iterationsLabel = new Label();
                        iterationsLabel.Content = "Iterations:";
                        ParameterGrid.Children.Add(iterationsLabel);

                        // TODO: default iteration count
                        TextBox iterationsTextBox = new TextBox();
                        iterationsTextBox.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        iterationsTextBox.Margin = new Thickness(82, 0, 0, 0);
                        iterationsTextBox.Width = TextBoxWidth;
                        iterationsTextBox.Height = TextBoxHeight;
                        ParameterGrid.Children.Add(iterationsTextBox);

                        break;
                    }
                case "Walkers":
                    {
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        /// <summary>
        /// Forces an updated level to be redrawn
        /// </summary>
        private void UpdateLevelView()
        {
            LevelElements.ItemsSource = null;
            LevelElements.ItemsSource = ControlPoint.Level.Elements;
        }
    }
}
