using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Rosalind
{
    /*
    Uses an unconventional algorithm to generate a shortest common superstring
    1. Use the reads to generate a 2D matrix for prefix reads and suffix reads
    2. Calculate the maximum overlap between reads return score to an overlapMatrix
    3. Generate a path through the overlap matrix based on maximum overlaps
    4. Output the string
     */
    class GenomeAssembly
    {
        private Dictionary<string, string> readMap;
        private List<string> reads;
        private int[,] overlapMatrix;
        private readonly int readLengthCutoff;


        public GenomeAssembly(string fastaInput) 
        {
            this.readMap = Utils.ReadFastaFile(fastaInput);
            this.reads = readMap.Values.ToList();
            this.overlapMatrix = new int [this.reads.Count,this.reads.Count];
            this.readLengthCutoff = reads[0].Length / 2;
        }

        public void PrintReads() 
        {
            foreach (var de in readMap)
            {
                Console.WriteLine($"Key: {de.Key} Value: {de.Value}");
            }
        }
        /*
        Calculate the overlap between two strings of any length using dynamic programming.
        Currently we iterate over the length of each string passed in using a 2D array. 
        Note: If we assume the strings are of equal length, we can cut down the iteration
        in half by only iterating over half of the columns
         */ 
        public int CalculateOverlap(string prefixRead, string suffixRead)
        {
            const int match = 1; // use 1 to get the overlapping indices between prefix and suffix
            const int penalty = -4;
            int[,] score = new int[prefixRead.Length + 1, suffixRead.Length + 1];   
            
            for (int i = 1; i < prefixRead.Length + 1; i++)
            {
                for (int j = 1; j < suffixRead.Length + 1; j++)
                {
                    if (prefixRead[i-1] == suffixRead[j-1]){
                        score[i,j] += (match + score[i-1, j-1]);
                    }
                    else
                    {
                        score[i,j] += (penalty + score[i-1,j-1]);
                    }
                }
            }

            int maxOverlap = score.GetArrayRow(prefixRead.Length).Max();
            return maxOverlap;
        }

        public void BuildOverlapMatrix()
        {
            for (int prefixIndex = 0; prefixIndex < this.reads.Count; prefixIndex++)
            {
                for (int suffixIndex = 0; suffixIndex < this.reads.Count; suffixIndex++)
                {
                    if (prefixIndex != suffixIndex)
                    {
                        string prefixRead = this.reads[prefixIndex];
                        string suffixRead = this.reads[suffixIndex];
                        int overlapScore = this.CalculateOverlap(prefixRead, suffixRead);
                        int storedValue = overlapScore > readLengthCutoff ? overlapScore : 0; 
                        //Console.WriteLine($"Max overlap between {prefixRead} : {suffixRead} -> {overlapScore}");
                        this.overlapMatrix[prefixIndex,suffixIndex] = storedValue;
                    }

                }
            }
        }

        
        private string FindSourceRead()
        {
            for (int col = 0; col < this.overlapMatrix.GetLength(1); col++)
            {
                var column = this.overlapMatrix.GetArrayCol(col);
                if (column.Sum() == 0)
                {
                    return this.reads[col];
                }
            }
            return null;
        }

        private string FindSinkRead()
        {
            for (int row = 0; row < this.overlapMatrix.GetLength(0); row++)
            {
                var rowArr = this.overlapMatrix.GetArrayRow(row);
                if (rowArr.Sum() == 0)
                {
                    return this.reads[row];
                }
            }
            return null;
        }
        public void GenerateSuperString()
        {
            string genome = "";
            string source = this.FindSourceRead();
            string sink = this.FindSinkRead();
            
            int sinkIndex = reads.IndexOf(sink);
            int currentIndex = reads.IndexOf(source);
            while (currentIndex != sinkIndex)
            {
                //TODO finish this block
            }
            // Console.WriteLine($"Source read {source}");
            // Console.WriteLine($"Sink read {sink}");
        }

        public void Test()
        {
            // int overlap = this.CalculateOverlap("ATTAGACCTG", "AGACCTGCCG");
            // Console.WriteLine(overlap);
            for (int i = 0; i < overlapMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < overlapMatrix.GetLength(1); j++){
                    Console.WriteLine($"Overlap Calculation Row {reads[i]} Column {reads[j]} : {overlapMatrix[i,j]}");
                }
            }
        }

        public void Run()
        {

        }
    }    
}