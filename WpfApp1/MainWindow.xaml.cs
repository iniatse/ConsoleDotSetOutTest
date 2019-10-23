using System;
using System.Collections.Generic;
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
using System.IO;
using System.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string currentCliArgumentString;
        readonly string cliArgumentStringDevelopment = @"""..\..\..\..\..\SRC"" ""..\..\..\..\..\BUILD"" --excludedDirectories="".svn""|""bla""|""some"" --sass --database=""..\..\..\..\build.db"" --cache --excludedDirectoriesDatabaseException=""some"" --excludedDirectoriesCacheException=""some"" --excludedFileNames="".eslintignore""|"".eslintrc.json""|""eslintrc.base.json"" --browserify --recursiveBrowserifyCompilation --recursiveSassCompilation --html --recursiveHtmlCompilation --contextSpecificVariables=""..\..\..\..\..\SRC\TEMPLATES\contextSpecificVariables.xml"" --excludedFileNamesDatabaseException=""contextSpecificVariables.xml"" --excludedFileNamesCacheException=""contextSpecificVariables.xml""";
        readonly string cliArgumentStringProduction = @"""..\..\..\..\..\SRC"" ""..\..\..\..\..\BUILD"" --excludedDirectories="".svn""|""bla""|""some"" --sass --database=""..\..\..\..\build.db"" --cache --excludedDirectoriesDatabaseException=""some"" --excludedDirectoriesCacheException=""some"" --excludedFileNames="".eslintignore""|"".eslintrc.json""|""eslintrc.base.json""|""package.json""|""package-lock.json""|""contextSpecificVariables.xml"" --browserify --recursiveBrowserifyCompilation --recursiveSassCompilation --excludedFileTypes="".scss""|"".src.js""|"".src.html"" --excludedFileTypesDatabaseException="".scss""|"".src.js""|"".src.html"" --excludedFileTypesCacheException="".scss""|"".src.js""|"".src.html"" --production --html --recursiveHtmlCompilation --contextSpecificVariables=""..\..\..\..\..\SRC\TEMPLATES\contextSpecificVariables.xml"" --excludedFileNamesDatabaseException=""contextSpecificVariables.xml"" --excludedFileNamesCacheException=""contextSpecificVariables.xml""";
        readonly string demoOption = @"--demo";

        public MainWindow()
        {
            InitializeComponent();

            this.RedirectConsoleWriteLine();
        }

        void TestConsoleWriteLineRedirection()
        {
            Task.Run(() =>
            {
                for (int i = 0; i < 300000; i++)
                {
                    Console.WriteLine("testMessage testMessage testMessage testMessage testMessage testMessage testMessage testMessage testMessage testMessage testMessage testMessage testMessage testMessage testMessage testMessage testMessage testMessage testMessage " + i + Environment.NewLine);
                }
            });
        }

        void TestWithoutConsoleWriteLineRedirection()
        {
            Task.Run(() =>
            {
                for (int i = 0; i < 300000; i++)
                {
                    textBoxLog.Dispatcher.Invoke(() =>
                    {
                        textBoxLog.AppendText("testMessage testMessage testMessage testMessage testMessage testMessage testMessage testMessage testMessage testMessage testMessage testMessage testMessage testMessage testMessage testMessage testMessage testMessage testMessage " + i + Environment.NewLine);
                    }, System.Windows.Threading.DispatcherPriority.Background);
                }
            });
        }

        void TestConsoleWriteLineRedirectionWithThread()
        {
            //for (int i = 0; i < 300000; i++)
            //{
            //    textBoxLog.Dispatcher.Invoke(() =>
            //    {
            //        textBoxLog.AppendText("testMessage" + i + Environment.NewLine);
            //    }, System.Windows.Threading.DispatcherPriority.Background);
            //}

            for (int i = 0; i < 300000; i++)
            {
                Console.WriteLine("testMessage testMessage testMessage testMessage testMessage testMessage testMessage testMessage testMessage testMessage testMessage testMessage testMessage testMessage testMessage testMessage testMessage testMessage testMessage " + i + Environment.NewLine);
            }
        }

        void RedirectConsoleWriteLine()
        {
            Console.SetOut(new ControlWriter(textBoxLog, logSrollViewer));
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (buttonDemo.IsChecked == true)
            {
                this.currentCliArgumentString = $"{currentCliArgumentString} {demoOption}";
            }

            string[] tmpArgs = CommandLineArgumentsConverter.CommandLineToArgs(this.currentCliArgumentString);
            List<string> tmp = new List<string>();
            tmp.Add(null);
            tmp.AddRange(tmpArgs);
            string[] myArgs = tmp.ToArray();

            foreach (var current in myArgs)
            {
                Console.WriteLine(current);
            }

            myTabControl.SelectedIndex = 1;

            //this.TestWithoutConsoleWriteLineRedirection();

            this.TestConsoleWriteLineRedirection();

            //Thread t1 = new Thread(this.TestConsoleWriteLineRedirectionWithThread);
            //t1.Start();
        }

        private void radioButtonDevelopment_Checked(object sender, RoutedEventArgs e)
        {
            if (Convert.ToBoolean(radioButtonDevelopment.IsChecked))
            {
                this.currentCliArgumentString = this.cliArgumentStringDevelopment;
            }
        }

        private void radioButtonProduction_Checked(object sender, RoutedEventArgs e)
        {
            if (Convert.ToBoolean(radioButtonProduction.IsChecked))
            {
                this.currentCliArgumentString = this.cliArgumentStringProduction;

            }
        }
    }
}
