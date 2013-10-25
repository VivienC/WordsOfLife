using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;

namespace WordsOfLife
{
    // Data structure to hold the data for each level
    public struct Level
    {
        public Level(String[] stringWords, int score, int speed)
        {
            levelWords = stringWords;
            numOfWords = stringWords.Length;
            scoreForLevelUp = score;
            levelSpeed = speed;
        }

        public String[] levelWords;
        public int numOfWords;
        public int scoreForLevelUp;
        public int levelSpeed;
    }

    // Keeps control of the words in play and user data
    // such as the score and current level
    class GameMechanics : INotifyPropertyChanged
    {
        // Game mechanics
        List<Level> level = new List<Level>();
        List<string> currentGameWords = new List<string>();
        Random randomNum = new Random();

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
        public bool updateLevelAndRequiredScore { get; private set; }
        public int levelSpeed { get; private set; }

        // Declare an event
        public event PropertyChangedEventHandler PropertyChanged;

        public GameMechanics()
        {
            initializeGameData();
            userScore = 0;
            currentLevel = 0;
            requiredScore = level[currentLevel].scoreForLevelUp;
            updateLevelAndRequiredScore = true;
            levelSpeed = level[currentLevel].levelSpeed;

        }

        public string getWord()
        {
            // Random pick a word fromthe current level's list
            currentGameWords.Add(level[currentLevel].levelWords[randomNum.Next(level[currentLevel].numOfWords)]);
            return currentGameWords.ElementAt(currentGameWords.Count - 1);

        }

        public bool isAnswerRight(string userAnswer)
        {
            // Set these flags to false to control score and level updates
            updateLevelAndRequiredScore = false;

            // Check if the user has typed a word that matches the current level's word list
            if (currentGameWords.Contains(userAnswer))
            {
                currentGameWords.RemoveAt(currentGameWords.IndexOf(userAnswer));
                updateGameData(true);
                return true;
            }
            else
            {
                for (int i = 0; i < currentLevel; i++)
                {
                    // If the word is from previous levels then return true but do
                    // update the score.
                    if (level[i].levelWords.Contains(userAnswer))
                    {
                        return true;
                    }
                }
            }

            // The user got an answer wrong to update the score accordingly
            updateGameData(false);
            return false;
        }

        private void updateGameData(bool answerIsCorrect)
        {
            if (answerIsCorrect)
            {
                // increment their score and change flag
                userScore++;

                // Check for level up
                if (userScore >= level[currentLevel].scoreForLevelUp)
                {
                    // Reset the score to zero to start counting again
                    userScore = 0;
                    currentLevel++;
                    requiredScore = level[currentLevel].scoreForLevelUp;
                    updateLevelAndRequiredScore = true;
                    levelSpeed = level[currentLevel].levelSpeed;

                }
            }
            else
            {
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

        // Setup the words for each of the levels
        private void initializeGameData()
        {
            int numOfLevels = 16;
            string[][] levelWords = new string[numOfLevels][];

            /* To add a new level:
             * 1) Change the numOfLevels variable above.
             * 2) Add your level after the existing level, that you choose
             *      i++;
             *      level[i] = new string[] { insert your words } 
             *      level. Add(new Level(levelWords[i], numToGetCorrect, intervalBetweenNewWords)
             */

            int i = 0;
            // home row
            levelWords[i] = new string[] {"a", "s", "d", "f", "g",
                                            "h", "j", "k", "l", ";"};
            level.Add(new Level(levelWords[i], 3, 1500));

            i++;
            levelWords[i] = new string[] {  "a", "s", "d", "f", "g",
                                            "h", "j", "k", "l", ";"};
            level.Add(new Level(levelWords[i], 3, 800));


            // qwerty row
            i++;
            levelWords[i] = new string[] {  "q", "w", "e", "r", "t",
                                            "y", "u", "i", "o", "p"};
            level.Add(new Level(levelWords[i], 3, 1500));

            i++;
            levelWords[i] = new string[] {  "q", "w", "e", "r", "t",
                                            "y", "u", "i", "o", "p"};
            level.Add(new Level(levelWords[i], 3, 800));



            // zxcvb row
            i++;
            levelWords[i] = new string[] {  "z", "x", "c", "v", "b",
                                            "n", "m", ",", ".", "/"};
            level.Add(new Level(levelWords[i], 3, 1500));

            i++;
            levelWords[i] = new string[] {  "z", "x", "c", "v", "b",
                                            "n", "m", ",", ".", "/"};
            level.Add(new Level(levelWords[i], 3, 800));


            // 12345 row
            i++;
            levelWords[i] = new string[] {  "1", "2", "3", "4", "5",
                                            "6", "7", "8", "9", "0"};
            level.Add(new Level(levelWords[i], 1, 1500));

            i++;
            levelWords[i] = new string[] {  "1", "2", "3", "4", "5",
                                            "6", "7", "8", "9", "0"};
            level.Add(new Level(levelWords[i], 1, 800));


            // home row words
            i++;
            levelWords[i] = new string[] {  "asdfg", "hjkl;", "gaff", "half",
                                            "has", "alas", "gad;", "kal",
                                            "lad", "jak;", ";;ll", ";hal",
                                            "had","fad", "kl;", "fjk",
                                            "sad", "glad", "fall", "gal"};
            level.Add(new Level(levelWords[i], 3, 3000));

            i++;
            levelWords[i] = new string[] {  "asdfg", "hjkl;", "gaff", "half",
                                            "hass", "alas", "gads;", "kal",
                                            "lads", "jak;", ";;ll", ";hal",
                                            "had","fads", "kl;", "f;jk",
                                            "slad", "glad", "fall", "gals"};
            level.Add(new Level(levelWords[i], 3, 1800));


            // qwert row words
            i++;
            levelWords[i] = new string[] {  "qwert", "yuiop", "putt", "tupp",
                                            "wet", "yet", "put", "erupt",
                                            "quit", "weep", "wipe", "rope",
                                            "tip","out", "turp", "poor",
                                            "err", "true", "ripe", "pope"};
            level.Add(new Level(levelWords[i], 3, 3000));

            i++;
            levelWords[i] = new string[] {  "qwert", "yuiop", "putt", "tupp",
                                            "wete", "yett", "put", "erupt",
                                            "quit", "weep", "wipe", "rope",
                                            "tipt","out", "turp", "poor",
                                            "errt", "true", "ript", "pipe"};
            level.Add(new Level(levelWords[i], 3, 1800));



            // zxcvb row words
            i++;
            levelWords[i] = new string[] {  "zxcvb", "nm,./", "bnm",
                                            "nxc", "vmn", "z,.", ".,c",
                                            "bv.n", "mx,", "/cv", "cc,",
                                            "bxm", "xxz", "zz.", "v,m"};
            level.Add(new Level(levelWords[i], 3, 3000));

            i++;
            levelWords[i] = new string[] {  "zxcvb", "nm,./", "bnmv",
                                            "nx.c", "vmn", "z,.", ".,c",
                                            "bv.n", "mx,.", "/cv", "cc,/",
                                            "cbxm","xxz/", "/zz.", "v,m"};
            level.Add(new Level(levelWords[i], 3, 1800));



            // 12345 row words
            i++;
            levelWords[i] = new string[] {  "12345", "67890", "929", "002",
                                            "876", "123", "934", "007", "653",
                                            "037", "892", "246", "768", "120",
                                            "029","715", "420"};
            level.Add(new Level(levelWords[i], 3, 3000));

            i++;
            levelWords[i] = new string[] {  "12345", "67890", "929", "002",
                                            "876", "123", "934", "007", "653",
                                            "037", "892", "246", "768", "120",
                                            "029","715", "420"};
            level.Add(new Level(levelWords[i], 3, 3000));


            // Ensure that the right number of levels have been initialised
            Debug.Assert(i == (numOfLevels - 1),
                "The level array index is not correct. Please check that the levels have been configure correctly.");

        }
    }
}
