using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

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

    public sealed class GameDictionary
    {
        // static initialisation by the CLR
        private static readonly GameDictionary instance = new GameDictionary();
        
        private List<Level> levels = new List<Level>();
        private Random randomNum = new Random();
        
        private GameDictionary() 
        {
            // Setup the data for the dictionary
            initializeGameData();
        }

        public static GameDictionary Instance
        {
            get
            {
                return instance;
            }
        }

        public string getWord(int lvl)
        {
            // Return a randomly chosen word from the requested level
            return levels[lvl].levelWords[randomNum.Next(levels[lvl].numOfWords)];
        }

        public int getScoreForLevelUp(int lvl)
        {
            // Return the score required to level up
            return levels[lvl].scoreForLevelUp;
        }

        public int getSpeedForLevel(int lvl)
        {
            // Return the speed of the level
            return levels[lvl].levelSpeed;
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
            levels.Add(new Level(levelWords[i], 3, 1500));

            i++;
            levelWords[i] = new string[] {  "a", "s", "d", "f", "g",
                                            "h", "j", "k", "l", ";"};
            levels.Add(new Level(levelWords[i], 3, 800));


            // qwerty row
            i++;
            levelWords[i] = new string[] {  "q", "w", "e", "r", "t",
                                            "y", "u", "i", "o", "p"};
            levels.Add(new Level(levelWords[i], 3, 1500));

            i++;
            levelWords[i] = new string[] {  "q", "w", "e", "r", "t",
                                            "y", "u", "i", "o", "p"};
            levels.Add(new Level(levelWords[i], 3, 800));



            // zxcvb row
            i++;
            levelWords[i] = new string[] {  "z", "x", "c", "v", "b",
                                            "n", "m", ",", ".", "/"};
            levels.Add(new Level(levelWords[i], 3, 1500));

            i++;
            levelWords[i] = new string[] {  "z", "x", "c", "v", "b",
                                            "n", "m", ",", ".", "/"};
            levels.Add(new Level(levelWords[i], 3, 800));


            // 12345 row
            i++;
            levelWords[i] = new string[] {  "1", "2", "3", "4", "5",
                                            "6", "7", "8", "9", "0"};
            levels.Add(new Level(levelWords[i], 1, 1500));

            i++;
            levelWords[i] = new string[] {  "1", "2", "3", "4", "5",
                                            "6", "7", "8", "9", "0"};
            levels.Add(new Level(levelWords[i], 1, 800));


            // home row words
            i++;
            levelWords[i] = new string[] {  "asdfg", "hjkl;", "gaff", "half",
                                            "has", "alas", "gad;", "kal",
                                            "lad", "jak;", ";;ll", ";hal",
                                            "had","fad", "kl;", "fjk",
                                            "sad", "glad", "fall", "gal"};
            levels.Add(new Level(levelWords[i], 3, 3000));

            i++;
            levelWords[i] = new string[] {  "asdfg", "hjkl;", "gaff", "half",
                                            "hass", "alas", "gads;", "kal",
                                            "lads", "jak;", ";;ll", ";hal",
                                            "had","fads", "kl;", "f;jk",
                                            "slad", "glad", "fall", "gals"};
            levels.Add(new Level(levelWords[i], 3, 1800));


            // qwert row words
            i++;
            levelWords[i] = new string[] {  "qwert", "yuiop", "putt", "tupp",
                                            "wet", "yet", "put", "erupt",
                                            "quit", "weep", "wipe", "rope",
                                            "tip","out", "turp", "poor",
                                            "err", "true", "ripe", "pope"};
            levels.Add(new Level(levelWords[i], 3, 3000));

            i++;
            levelWords[i] = new string[] {  "qwert", "yuiop", "putt", "tupp",
                                            "wete", "yett", "put", "erupt",
                                            "quit", "weep", "wipe", "rope",
                                            "tipt","out", "turp", "poor",
                                            "errt", "true", "ript", "pipe"};
            levels.Add(new Level(levelWords[i], 3, 1800));



            // zxcvb row words
            i++;
            levelWords[i] = new string[] {  "zxcvb", "nm,./", "bnm",
                                            "nxc", "vmn", "z,.", ".,c",
                                            "bv.n", "mx,", "/cv", "cc,",
                                            "bxm", "xxz", "zz.", "v,m"};
            levels.Add(new Level(levelWords[i], 3, 3000));

            i++;
            levelWords[i] = new string[] {  "zxcvb", "nm,./", "bnmv",
                                            "nx.c", "vmn", "z,.", ".,c",
                                            "bv.n", "mx,.", "/cv", "cc,/",
                                            "cbxm","xxz/", "/zz.", "v,m"};
            levels.Add(new Level(levelWords[i], 3, 1800));



            // 12345 row words
            i++;
            levelWords[i] = new string[] {  "12345", "67890", "929", "002",
                                            "876", "123", "934", "007", "653",
                                            "037", "892", "246", "768", "120",
                                            "029","715", "420"};
            levels.Add(new Level(levelWords[i], 3, 3000));

            i++;
            levelWords[i] = new string[] {  "12345", "67890", "929", "002",
                                            "876", "123", "934", "007", "653",
                                            "037", "892", "246", "768", "120",
                                            "029","715", "420"};
            levels.Add(new Level(levelWords[i], 3, 3000));


            // Ensure that the right number of levels have been initialised
            Debug.Assert(i == (numOfLevels - 1),
                "The level array index is not correct. Please check that the levels have been configure correctly.");

        }

    }
}
