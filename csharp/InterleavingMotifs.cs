using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Rosalind
{
    class InterleavingMotifs
    {
        public List<string> sequences;
        public string firstSeq;
        public string secondSeq;
        public int[,] scoringMatrix;
        public List<char> supersequence;
        public readonly int matchingScore;
        public readonly int indelPenalty;

        public InterleavingMotifs(string inputFile, int matchScore, int indelPenalty) 
        {
            this.sequences = Utils.ReadFile(inputFile);
            if (this.sequences.Count != 2) {
                throw new Exception($"Invalid number of strings found: {this.sequences.Count}");
            }
            this.firstSeq = this.sequences[0];
            this.secondSeq = this.sequences[1];
            this.matchingScore = matchScore;
            this.indelPenalty = indelPenalty;
            this.supersequence = new List<char>();
        }

        public void InitScoringMatrix()
        {
            this.scoringMatrix = new int[this.firstSeq.Length + 1, this.secondSeq.Length + 1];
            for (int i = 1; i < this.scoringMatrix.GetLength(0); i++) {
                this.scoringMatrix[i,0] = (this.scoringMatrix[i-1,0] + this.indelPenalty);
            }
            for (int j = 1; j < this.scoringMatrix.GetLength(1); j++)
            {
                this.scoringMatrix[0,j] = (this.scoringMatrix[0,j-1] + this.indelPenalty);
            }
        }
        
        public void CreateScoringMatrix() 
        {
            for (int i = 1; i < this.scoringMatrix.GetLength(0); i++ )
            {
                for (int j = 1; j < this.scoringMatrix.GetLength(1); j++)
                {
                    if (this.firstSeq[i-1] == this.secondSeq[j-1])
                    {
                        this.scoringMatrix[i,j] = Math.Max(
                            this.scoringMatrix[i-1,j-1] + this.matchingScore,
                            Math.Max(
                                this.scoringMatrix[i-1,j] + this.indelPenalty,
                                this.scoringMatrix[i,j-1] + this.indelPenalty
                            )
                        );
                    }
                    else
                    {
                        this.scoringMatrix[i,j] = Math.Max(this.scoringMatrix[i-1,j] + this.indelPenalty,this.scoringMatrix[i,j-1] + this.indelPenalty);
                    }
                }
            }
        }

        public void Backtrack(int i, int j)
        {
            if (i == 0 && j == 0)
            {
                return;
            }

            if (i == 0)
            {
                this.Backtrack(i,j-1);
                this.supersequence.Add(this.secondSeq[j-1]);
            }
            else if(j == 0)
            {
                this.Backtrack(i-1,j);
                this.supersequence.Add(this.firstSeq[i-1]);
            }
            else
            {
                if (this.firstSeq[i-1] == this.secondSeq[j-1])
                {
                    if (this.scoringMatrix[i,j] == this.scoringMatrix[i-1,j-1] + this.matchingScore)
                    {
                        this.Backtrack(i-1,j-1);
                        this.supersequence.Add(this.firstSeq[i-1]);
                    }
                    else if (this.scoringMatrix[i,j] == this.scoringMatrix[i,j-1] + this.indelPenalty)
                    {
                        this.Backtrack(i,j-1);
                        this.supersequence.Add(this.secondSeq[j-1]);
                    }
                    else if (this.scoringMatrix[i,j] == this.scoringMatrix[i-1,j] + this.indelPenalty)
                    {
                        this.Backtrack(i-1,j);
                        this.supersequence.Add(this.firstSeq[i-1]);
                    }
                }
                else
                {
                    if(this.scoringMatrix[i,j] == this.scoringMatrix[i,j-1] + this.indelPenalty)
                    {
                        this.Backtrack(i,j-1);
                        this.supersequence.Add(this.secondSeq[j-1]);
                    }
                    else if(this.scoringMatrix[i,j] == this.scoringMatrix[i-1,j] + this.indelPenalty)
                    {
                        this.Backtrack(i-1,j);
                        this.supersequence.Add(this.firstSeq[i-1]);
                    }
                }
            }
        }


        public void Run()
        {
            this.InitScoringMatrix();
            this.CreateScoringMatrix();
            this.Backtrack(this.firstSeq.Length, this.secondSeq.Length);
            Console.WriteLine(string.Join("", supersequence.ToArray()));
        }
    }
}