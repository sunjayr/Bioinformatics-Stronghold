/*
Problem: Given a permutation pi of length n, find the longest increasing 
subsequence and the longest decreasing subsequence. 

Solution: Create an n x n scoring matrix for the permutation. Rows represent
the current index of the permutation, columns represent a possible previous 
position. Iterate over the scoring array and if a position is valid add one 
to the maximum value found at the previous position index in the maxValues
array. The maximum score in each row is stored in the maxValues array and the
index of the maximum value is stored in the backtrack array. Backtrack
starting at the index containing the maximum value in the maxValues array.
Keep iterating until reaching a value of 0 in the maxValues array.
 */
using System;
using System.IO;
using System.Linq;
using System.Collections;

namespace Rosalind
{
    class LongestSubsequence
    {
        public int[,] scoringArray;
        public int[] maxValues;
        public int[] backTrack;
        public int[] permutation;
        ArrayList finalSequence;
        public readonly int permutationLength;
        private bool increasing;
        
        public LongestSubsequence(bool increasing=true){
            const string filename = "longest_subsequence_input.txt";
            StreamReader fileReader = new StreamReader(filename);
            permutationLength = int.Parse(fileReader.ReadLine());
            
            permutation = new int[permutationLength];
            backTrack = new int[permutationLength];
            maxValues = new int[permutationLength];
            scoringArray = new int[permutationLength, permutationLength];
            finalSequence = new ArrayList();

            string[] permString = fileReader.ReadLine().Split(" ");
            for (int i = 0; i < permString.Length; i++){
                permutation[i] = int.Parse(permString[i]);
            }
            fileReader.Close();
            this.increasing = increasing;
        }
       
        public void ClearMatrices(){
            this.finalSequence.Clear();
            for(int i = 0; i < permutationLength; i++){
                this.backTrack[i] = 0;
                this.maxValues[i] = 0;
            }
        }
        
        public void CreateScoringMatrix(int direction){
            this.ClearMatrices();
            for (int i = 0; i < this.scoringArray.GetLength(0); i++){
                for (int j = 0; j <= i; j++){
                    if((permutation[i] * direction) > (permutation[j] * direction)){
                        scoringArray[i,j] = maxValues[j] + 1;
                        if(scoringArray[i,j] > maxValues[i]){
                            maxValues[i] = scoringArray[i,j];
                            backTrack[i] = j;
                        }
                    }
                    else
                    {
                        scoringArray[i,j] = 0;
                    }
                }

            }
        }

        public void Backtrack(){
            int maxValue = this.maxValues.Max();
            int startingIndex = Array.IndexOf(this.maxValues, maxValue);
            int currentIndex = startingIndex;
            finalSequence.Add(permutation[currentIndex]);
            do
            {
                currentIndex = backTrack[currentIndex];
                finalSequence.Add(permutation[currentIndex]);
            } while (maxValues[currentIndex] != 0);
            
            finalSequence.Reverse();
        }

        public void WriteOutput(){
            using (StreamWriter sr = new StreamWriter("longest_subsequence_output.txt", true)){
                foreach ( int element in finalSequence){
                    sr.Write(element + " ");
                }
                sr.WriteLine();
                sr.Close();
            }
        }

        public void Execute(){
            CreateScoringMatrix(1);
            this.Backtrack();
            this.WriteOutput();
            CreateScoringMatrix(-1);
            this.Backtrack();
            this.WriteOutput();
        }
    }
}