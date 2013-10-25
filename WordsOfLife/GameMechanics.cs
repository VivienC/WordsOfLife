using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace WordsOfLife
{
    // Keeps control of the words in play and user data
    // such as the score and current level
    class GameMechanics : INotifyPropertyChanged
    {
        // Game mechanics
        List<string> currentGameWords = new List<string>();
        private int _currentLevel;
        public int currentLevel 
        {
            get
            {
                return _currentLevel;
            }
            private set
            {
                _currentLevel = value;
                OnPropertyChanged("currentLevel");
            }
        }
        
        public int userScore { get; private set; }
        public int requiredScore { get; private set; }
        // TODO: delete this and use binding instead
        public bool updateLevelAndRequiredScore { get; private set; }
        public int levelSpeed { get; private set; }

        // Declare an event
        public event PropertyChangedEventHandler PropertyChanged;

        public GameMechanics()
        {
            userScore = 0;
            currentLevel = 0;
            requiredScore = GameDictionary.Instance.getScoreForLevelUp(currentLevel);
            updateLevelAndRequiredScore = true;
            levelSpeed = GameDictionary.Instance.getSpeedForLevel(currentLevel);

        }

        public string getWord()
        {
            // Get a word from the the game dictionary            
            currentGameWords.Add(GameDictionary.Instance.getWord(currentLevel));
            return currentGameWords.ElementAt(currentGameWords.Count - 1);

        }

        public bool isAnswerRight(string userAnswer)
        {
            // Set these flags to false to control score and level updates
            updateLevelAndRequiredScore = false;

            // Check if the user has typed a word that matches the current level's word list
            bool answerIsRight = currentGameWords.Contains(userAnswer);

            // If answer is right then remove the word from the list
            if (answerIsRight)
            {
                currentGameWords.RemoveAt(currentGameWords.IndexOf(userAnswer));
            }

            // Update score and return result to the caller
            updateGameData(answerIsRight);
            return answerIsRight;
        }

        private void updateGameData(bool answerIsCorrect)
        {
            if (answerIsCorrect)
            {
                // increment their score and change flag
                userScore++;

                // Check for level up
                if (userScore >= GameDictionary.Instance.getScoreForLevelUp(currentLevel))
                {
                    // Reset the score to zero to start counting again
                    userScore = 0;
                    currentLevel++;
                    requiredScore = GameDictionary.Instance.getScoreForLevelUp(currentLevel);
                    updateLevelAndRequiredScore = true;
                    levelSpeed = GameDictionary.Instance.getSpeedForLevel(currentLevel);
                }
            }
            else
            {
                // TODO: Half the score instead of setting it to zero
                userScore = 0;
            }

        }

        // Create the OnPropertyChanged method to raise the event 
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        
    }
}
