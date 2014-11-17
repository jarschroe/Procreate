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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

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
        static int DefaultParamDataLeftMargin = 110;
        const string OpenImageFilter = "Image Files (.jpg, .jpeg, .png, .bmp) | *.jpg; *.jpeg; *.png; *.bmp";

        // Context menu objects
        Generation.Method FocusedMethod;
        // Game object menu
        Generation.GameObject FocusedGameObject;

        public MainWindow()
        {
            InitializeComponent();

            // load Jared's Procreate software
            ControlPoint = new ControlPoint();

            // bind the level data to the application
            LevelElements.ItemsSource = ControlPoint.Level.Elements;
            // set the algorithm type values for the combo box
            MethodAlgorithm.ItemsSource = ControlPoint.Algorithms;

            // default focused method
            // bind the method name data to the application
            // default focused method and game object
            FocusedMethod = ControlPoint.MethodFactory.CreateMethod();
            MethodName.DataContext = FocusedMethod;
            FocusedGameObject = ControlPoint.GameObjectFactory.CreateGameObject();
            // bind game object attribute data to the application
            GameObjectName.DataContext = FocusedGameObject;
            GameObjectType.DataContext = FocusedGameObject;
            GameObjectImagePath.DataContext = FocusedGameObject;
            GameObjectAppearRate.DataContext = FocusedGameObject;
            GameObjectPreview.DataContext = FocusedGameObject;
            // bind the generator's lists
            GeneratorMethodList.ItemsSource = ControlPoint.Generator.Methods;
        }

        /// <summary>
        /// Forces an updated level to be redrawn
        /// </summary>
        private void UpdateLevelView()
        {
            LevelElements.ItemsSource = null;
            LevelElements.ItemsSource = ControlPoint.Level.Elements;
        }

        /// <summary>
        /// Updates the Procreate GUI to have the parameter labels relevant to the selected algorithm type
        /// </summary>
        private void UpdateAlgorithmParameterLabels()
        {
            // clear the previous parameter controls displayed to the user
            ParameterGrid.Children.Clear();

            switch (MethodAlgorithm.Text)
            {
                // adjust Method GUI to suit parameters for the chosen algorithm type
                // TODO: access algorithm names from a program-wide array rather than magic values
                // TODO: see if parameter labels can be templated in XAML, then loaded in code rather than created - maybe with data templates?
                case "Randomise Level":
                    {
                        // add Cellular Automata algorithm parameters to the GUI
                        // game object pool
                        Label pool = new Label();
                        pool.Content = "Game object pool:";
                        ParameterGrid.Children.Add(pool);
                        // TODO: figure out how to do game object pool data

                        break;
                    }
                case "Cellular Automata":
                    {
                        // add Cellular Automata algorithm parameters to the GUI
                        // iteration count
                        Label iterations = new Label();
                        iterations.Content = "Iterations:";
                        ParameterGrid.Children.Add(iterations);
                        TextBox iterationsData = new TextBox();
                        iterationsData.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        iterationsData.Margin = new Thickness(82, 0, 0, 0);
                        iterationsData.Width = TextBoxWidth;
                        iterationsData.Height = TextBoxHeight;
                        iterationsData.DataContext = FocusedMethod.Algorithm;
                        // binding code based on code at:
                        // http://msdn.microsoft.com/en-us/library/ms598270(v=vs.110).aspx &
                        // http://stackoverflow.com/questions/8415481/two-way-binding-for-textbox
                        Binding iterationsBind = new Binding("IterationCount");
                        iterationsBind.Mode = BindingMode.TwoWay;
                        iterationsData.SetBinding(TextBox.TextProperty, iterationsBind);
                        ParameterGrid.Children.Add(iterationsData);

                        break;
                    }
                case "Walkers":
                    {
                        // add Walkers algorithm parameters to the GUI
                        // minumum walkers
                        Label minWalkers = new Label();
                        minWalkers.Content = "Min walkers:";
                        ParameterGrid.Children.Add(minWalkers);
                        TextBox minWalkersData = new TextBox();
                        minWalkersData.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        minWalkersData.Margin = new Thickness(DefaultParamDataLeftMargin, 0, 0, 0);
                        minWalkersData.Width = TextBoxWidth;
                        minWalkersData.Height = TextBoxHeight;
                        minWalkersData.DataContext = FocusedMethod.Algorithm;
                        Binding minWalkersBind = new Binding("MinWalkers");
                        minWalkersBind.Mode = BindingMode.TwoWay;
                        minWalkersData.SetBinding(TextBox.TextProperty, minWalkersBind);
                        ParameterGrid.Children.Add(minWalkersData);

                        // maximum walkers
                        Label maxWalkers = new Label();
                        maxWalkers.Content = "Max walkers:";
                        maxWalkers.Margin = new Thickness(0, 35, 0, 0);
                        ParameterGrid.Children.Add(maxWalkers);
                        TextBox maxWalkersData = new TextBox();
                        maxWalkersData.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        maxWalkersData.Margin = new Thickness(DefaultParamDataLeftMargin, 35, 0, 0);
                        maxWalkersData.Width = TextBoxWidth;
                        maxWalkersData.Height = TextBoxHeight;
                        maxWalkersData.DataContext = FocusedMethod.Algorithm;
                        Binding maxWalkersBind = new Binding("MaxWalkers");
                        maxWalkersBind.Mode = BindingMode.TwoWay;
                        maxWalkersData.SetBinding(TextBox.TextProperty, maxWalkersBind);
                        ParameterGrid.Children.Add(maxWalkersData);

                        // minimum life
                        Label minLife = new Label();
                        minLife.Content = "Min Walker Life:";
                        minLife.Margin = new Thickness(0, 70, 0, 0);
                        ParameterGrid.Children.Add(minLife);
                        TextBox minLifeData = new TextBox();
                        minLifeData.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        minLifeData.Margin = new Thickness(DefaultParamDataLeftMargin, 70, 0, 0);
                        minLifeData.Width = TextBoxWidth;
                        minLifeData.Height = TextBoxHeight;
                        minLifeData.DataContext = FocusedMethod.Algorithm;
                        Binding minLifeBind = new Binding("MinLife");
                        minLifeBind.Mode = BindingMode.TwoWay;
                        minLifeData.SetBinding(TextBox.TextProperty, minLifeBind);
                        ParameterGrid.Children.Add(minLifeData);

                        // maximum life
                        Label maxLife = new Label();
                        maxLife.Content = "Max Walker Life:";
                        maxLife.Margin = new Thickness(0, 105, 0, 0);
                        ParameterGrid.Children.Add(maxLife);
                        TextBox maxLifeData = new TextBox();
                        maxLifeData.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        maxLifeData.Margin = new Thickness(DefaultParamDataLeftMargin, 105, 0, 0);
                        maxLifeData.Width = TextBoxWidth;
                        maxLifeData.Height = TextBoxHeight;
                        maxLifeData.DataContext = FocusedMethod.Algorithm;
                        Binding maxLifeBind = new Binding("MaxLife");
                        maxLifeBind.Mode = BindingMode.TwoWay;
                        maxLifeData.SetBinding(TextBox.TextProperty, maxLifeBind);
                        ParameterGrid.Children.Add(maxLifeData);

                        // minimum chance of changing direction
                        Label minChangeDirChance = new Label();
                        minChangeDirChance.Content = "Min Chance to\nChange Direction:";
                        minChangeDirChance.Margin = new Thickness(0, 140, 0, 0);
                        ParameterGrid.Children.Add(minChangeDirChance);
                        TextBox minChangeDirChanceData = new TextBox();
                        minChangeDirChanceData.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        minChangeDirChanceData.Margin = new Thickness(DefaultParamDataLeftMargin, 150, 0, 0);
                        minChangeDirChanceData.Width = TextBoxWidth;
                        minChangeDirChanceData.Height = TextBoxHeight;
                        minChangeDirChanceData.DataContext = FocusedMethod.Algorithm;
                        Binding minChangeDirChanceBind = new Binding("MinChangeDirectionChance");
                        minChangeDirChanceBind.Mode = BindingMode.TwoWay;
                        minChangeDirChanceData.SetBinding(TextBox.TextProperty, minChangeDirChanceBind);
                        ParameterGrid.Children.Add(minChangeDirChanceData);

                        // maxiumum chance of changing direction
                        Label maxChangeDirChance = new Label();
                        maxChangeDirChance.Content = "Max Chance to\nChange Direction:";
                        maxChangeDirChance.Margin = new Thickness(0, 185, 0, 0);
                        ParameterGrid.Children.Add(maxChangeDirChance);
                        TextBox maxChangeDirChanceData = new TextBox();
                        maxChangeDirChanceData.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        maxChangeDirChanceData.Margin = new Thickness(DefaultParamDataLeftMargin, 196, 0, 0);
                        maxChangeDirChanceData.Width = TextBoxWidth;
                        maxChangeDirChanceData.Height = TextBoxHeight;
                        maxChangeDirChanceData.DataContext = FocusedMethod.Algorithm;
                        Binding maxChangeDirChanceBind = new Binding("MaxChangeDirectionChance");
                        maxChangeDirChanceBind.Mode = BindingMode.TwoWay;
                        maxChangeDirChanceData.SetBinding(TextBox.TextProperty, maxChangeDirChanceBind);
                        ParameterGrid.Children.Add(maxChangeDirChanceData);

                        // TODO: walkers radius and death-related parameters
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void NewMethodButton_Click(object sender, RoutedEventArgs e)
        {
            // create a new method
            FocusedMethod = ControlPoint.MethodFactory.CreateMethod();
            MethodName.DataContext = FocusedMethod;
            MethodAlgorithm.Text = ControlPoint.Algorithms[(int)FocusedMethod.AlgorithmType];
            // update GUI labels for the method's parameters
            UpdateAlgorithmParameterLabels();
        }

        private void Exit_Click_1(object sender, RoutedEventArgs e)
        {
            // close the application (code from http://stackoverflow.com/questions/2820357/how-to-exit-a-wpf-app-programmatically)
            this.Close();
        }

        private void MethodAlgorithm_DropDownClosed_1(object sender, EventArgs e)
        {
            // update the method's algorithm
            switch (MethodAlgorithm.Text)
            {
                case "Randomise Level":
                    {
                        FocusedMethod.AlgorithmType = Generation.AlgorithmType.RANDOMISE_LEVEL;
                        break;
                    }
                case "Cellular Automata":
                    {
                        FocusedMethod.AlgorithmType = Generation.AlgorithmType.CELLULAR_AUTOMATA;
                        break;
                    }
                case "Walkers":
                    {
                        FocusedMethod.AlgorithmType = Generation.AlgorithmType.WALKERS;
                        break;
                    }
            }

            UpdateAlgorithmParameterLabels();
        }

        private void NewGameObjectButton_Click(object sender, RoutedEventArgs e)
        {
            // create a new game object
            FocusedGameObject = ControlPoint.GameObjectFactory.CreateGameObject();
        }

        private void OpenGameObjectImage_Click(object sender, RoutedEventArgs e)
        {
            // create a file dialog to view images in the explorer
            // code referenced from http://msdn.microsoft.com/en-us/library/microsoft.win32.openfiledialog.aspx
            Microsoft.Win32.OpenFileDialog openImage = new Microsoft.Win32.OpenFileDialog();
            // filter to use certain image files
            openImage.Filter = OpenImageFilter;

            // show open image dialog
            Nullable<bool> result = openImage.ShowDialog();

            if (result == true)
            {
                // load selected image
                FocusedGameObject.ImagePath = openImage.FileName;
            }
        }

        private void AddMethodToGenerator_Click(object sender, RoutedEventArgs e)
        {
            // add the method to the generator
            ControlPoint.Generator.AddMethod(FocusedMethod);
            // reset 
            FocusedMethod = ControlPoint.MethodFactory.CreateMethod(FocusedMethod);
            MethodName.DataContext = FocusedMethod;
        }

        private void GenerationMethod_Click(object sender, RoutedEventArgs e)
        {
            // TODO: focus on the clicked method
        }
    }
}
