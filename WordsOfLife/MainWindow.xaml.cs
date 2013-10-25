using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;

namespace WordsOfLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameMechanics gameMechanics = new GameMechanics();
        IntToLevelStringConverter intToLevelStringConv = new IntToLevelStringConverter();
        Binding myBinding = new Binding();
        
        public MainWindow()
        {
            InitializeComponent();

            wordLabel.Content = gameMechanics.getWord();
            
            // User should be able to start typing straight away
            userInput.Focus();

            // Set up the binding for the level label
            myBinding.Source = gameMechanics;
            myBinding.Converter = intToLevelStringConv;
            myBinding.Path = new PropertyPath("currentLevel");
            levelLabel.SetBinding(Label.ContentProperty, myBinding);
            
        }

        private void userInputEvent(object sender, TextChangedEventArgs e)
        {
            // Until the user types a space they are not ready to submit 
            // their answer
            if (!userInput.Text.EndsWith(" "))
            {
                return;
            }

            // Check that the submitted asnwer is correct
            if (gameMechanics.isAnswerRight(userInput.Text.Replace(" ", "")))
            {
                wordLabel.Content = gameMechanics.getWord();                
            }

            userInput.Clear();

        }
    }

    // Class to convert from the level number to a string saying
    // "Level: " and the number. GameMechanics level is zero-based, 
    // so add or subtract 1 as appropriate.
    [ValueConversion(typeof(int), typeof(String))]
    public class IntToLevelStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "Level: " + ((int)value + 1);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Leave only the level number
            string valueString = ((string)value).Replace("Level: ", "");

            // Convert from string to integer
            int result = 0;
            int.TryParse(valueString, out result);
            return (result-1);
        }
    }
}
