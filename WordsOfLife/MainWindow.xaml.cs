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
using WordsOfLife;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameMechanics gameMechanics = new GameMechanics();

        public MainWindow()
        {
            InitializeComponent();

            wordLabel.Content = gameMechanics.getWord();
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
}
