using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace Rosalind
{
    class EditDistance
    {
        public string FirstString {get; set;}
        public string SecondString {get; set;}
        public int[,] scoringMatrix;
        public long[,] optimalAlignmentMatrix;
        public List<char> firstStringAlignment;
        public List<char> secondStringAlignment;
        public int optimalAlignmentCount;
        public EditDistance(string inputFile)
        {
            var fastaFile = Utils.ReadFastaFile(inputFile);
            var strings = fastaFile.Values.ToList();
            if (strings.Count != 2)
            {
                throw new Exception("List must have 2 fasta strings");
            }
            this.FirstString = strings[0];
            this.SecondString = strings[1];
            this.scoringMatrix = new int [this.FirstString.Length + 1, this.SecondString.Length + 1];
            this.optimalAlignmentMatrix = new long [this.FirstString.Length + 1, this.SecondString.Length + 1];
            this.firstStringAlignment = new List<char>();
            this.secondStringAlignment = new List<char>();
            this.optimalAlignmentCount = 0;
        }

        public void InitScoringMatrix()
        {
            for(int i = 0; i < this.scoringMatrix.GetLength(0); i++)
            {
                this.scoringMatrix[i,0] = i;
                this.optimalAlignmentMatrix[i,0] = 1;
            }
            for(int j = 0; j < this.scoringMatrix.GetLength(1); j++)
            {
                this.scoringMatrix[0,j] = j;
                this.optimalAlignmentMatrix[0,j] = 1;
            }
        }
        public void CreateScoringMatrix()
        {
            for (int i = 1; i < this.scoringMatrix.GetLength(0); i++)
            {
                for (int j = 1; j < this.scoringMatrix.GetLength(1); j++)
                {
                    if (this.FirstString[i-1] == this.SecondString[j-1])
                    {
                        this.scoringMatrix[i,j] = Math.Min(this.scoringMatrix[i-1,j-1], Math.Min(this.scoringMatrix[i,j-1] + 1, this.scoringMatrix[i-1,j] + 1));
                        if (this.scoringMatrix[i,j] == this.scoringMatrix[i-1,j-1])
                        {
                            this.optimalAlignmentMatrix[i,j] += this.optimalAlignmentMatrix[i-1,j-1];
                        }
                        
                    }
                    else
                    {
                        this.scoringMatrix[i,j] = Math.Min(this.scoringMatrix[i-1,j-1] + 1, Math.Min(this.scoringMatrix[i,j-1] + 1, this.scoringMatrix[i-1,j] + 1));
                        if (this.scoringMatrix[i,j] == this.scoringMatrix[i-1,j-1] + 1)
                        {
                            this.optimalAlignmentMatrix[i,j] += this.optimalAlignmentMatrix[i-1,j-1];
                        }
                    }

                    if (this.scoringMatrix[i,j] == this.scoringMatrix[i-1,j] + 1)
                    {
                        this.optimalAlignmentMatrix[i,j] += this.optimalAlignmentMatrix[i-1,j];
                    }
                    if (this.scoringMatrix[i,j] == this.scoringMatrix[i,j-1] + 1)
                    {
                        this.optimalAlignmentMatrix[i,j] += this.optimalAlignmentMatrix[i,j-1];
                    }
                }
            }
        }

        //TODO identify way to count optimal alignments at each cell as building the Backtrack matrix
        public void BackTrack(int i, int j)
        {
            if (i == 0 && j == 0)
            {
                return;
            }
            
            if (i == 0)
            {
                this.BackTrack(i,j-1);
                firstStringAlignment.Add('-');
                secondStringAlignment.Add(this.SecondString[j-1]);
                
            }
            else if (j == 0)
            {
                this.BackTrack(i-1,j);
                firstStringAlignment.Add(this.FirstString[i-1]);
                secondStringAlignment.Add('-');
            }
            else
            {
                int score = this.scoringMatrix[i,j];
                if (this.FirstString[i-1] == this.SecondString[j-1])
                {
                    if (score == this.scoringMatrix[i-1,j-1])
                    {
                        this.BackTrack(i-1,j-1);
                        this.firstStringAlignment.Add(this.FirstString[i-1]);
                        this.secondStringAlignment.Add(this.SecondString[j-1]);
                    }
                    else if (score == (this.scoringMatrix[i-1,j] + 1))
                    {
                        this.BackTrack(i-1,j);
                        this.firstStringAlignment.Add(this.FirstString[i-1]);
                        this.secondStringAlignment.Add('-');
                    }
                    else if (score == (this.scoringMatrix[i,j-1] + 1))
                    {
                        this.BackTrack(i,j-1);
                        this.firstStringAlignment.Add('-');
                        this.secondStringAlignment.Add(this.SecondString[j-1]);
                    }
                }
                else
                {
                    if (score == this.scoringMatrix[i-1,j-1] + 1)
                    {
                        this.BackTrack(i-1,j-1);
                        this.firstStringAlignment.Add(this.FirstString[i-1]);
                        this.secondStringAlignment.Add(this.SecondString[j-1]);
                    }
                    else if (score == (this.scoringMatrix[i-1,j] + 1))
                    {
                        this.BackTrack(i-1,j);
                        this.firstStringAlignment.Add(this.FirstString[i-1]);
                        this.secondStringAlignment.Add('-');
                    }
                    else if (score == (this.scoringMatrix[i,j-1] + 1))
                    {
                        this.BackTrack(i,j-1);
                        this.firstStringAlignment.Add('-');
                        this.secondStringAlignment.Add(this.SecondString[j-1]);
                    }
                }
            }
        }

        /*
        * A function to count all optimal alignments of two strings
        *
        */
        public void BackTrackAll(int i, int j)
        {
            if (i == 0 && j == 0)
            {
                return;
            }
            
            if (i == 0)
            {
                this.BackTrackAll(i,j-1);
                
            }
            else if (j == 0)
            {
                this.BackTrackAll(i-1,j);
            }
            else
            {
                int score = this.scoringMatrix[i,j];
                if (this.FirstString[i-1] == this.SecondString[j-1])
                {
                    if (score == this.scoringMatrix[i-1,j-1])
                    {
                        this.BackTrackAll(i-1,j-1);
                    }
                    
                    if (score == (this.scoringMatrix[i-1,j] + 1))
                    {
                        this.BackTrackAll(i-1,j);
                    }

                    if (score == (this.scoringMatrix[i,j-1] + 1))
                    {
                        this.BackTrackAll(i,j-1);
                    }
                }
                else
                {
                    if (score == this.scoringMatrix[i-1,j-1] + 1)
                    {
                        this.BackTrackAll(i-1,j-1);
                    }
                    
                    if (score == (this.scoringMatrix[i-1,j] + 1))
                    {
                        this.BackTrackAll(i-1,j);
                    }

                    if (score == (this.scoringMatrix[i,j-1] + 1))
                    {
                        this.BackTrackAll(i,j-1);
                    }
                }
            }
        }
        
        public void Run()
        {
            this.InitScoringMatrix();
            this.CreateScoringMatrix();
            Console.WriteLine(this.scoringMatrix[this.FirstString.Length, this.SecondString.Length]);
            this.BackTrack(this.FirstString.Length, this.SecondString.Length);
            Console.WriteLine(string.Join("", this.firstStringAlignment.ToArray()));
            Console.WriteLine(string.Join("", this.secondStringAlignment.ToArray()));
            //this.BackTrackAll(this.FirstString.Length, this.SecondString.Length);
            Console.WriteLine(this.optimalAlignmentMatrix[this.FirstString.Length,this.SecondString.Length] % 134217727);
        }
    }
}