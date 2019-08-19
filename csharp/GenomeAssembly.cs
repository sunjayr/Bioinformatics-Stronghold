using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using CommandLine;

/*
    Uses a dyanamic programming approach to generate a shortest common superstring
    1. Use the reads to generate a 2D matrix for prefix reads and suffix reads
    2. Calculate the maximum overlap between reads return score to an overlapMatrix
    3. Generate a path through the overlap matrix based on maximum overlaps
    4. Output the string
*/
namespace Rosalind
{
    class GenomeAssembly
    {
        private Dictionary<string, string> readMap;
        private List<string> reads;
        private int[,] overlapMatrix;
        public string Genome{get; set;}
        public string OutputLocation{get; set;}



        public GenomeAssembly(string fastaInput, string fastaOutput) 
        {
            this.readMap = Utils.ReadFastaFile(fastaInput);
            this.reads = readMap.Values.ToList();
            this.overlapMatrix = new int [this.reads.Count,this.reads.Count];
            this.OutputLocation = fastaOutput;

        }


        /*
            Calculate the overlap between two strings of any length using dynamic programming.
            Currently we iterate over the length of each string passed in using a 2D array. 
            Note: If we assume the strings are exactly equal length, we can cut down the iteration
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
                        Console.WriteLine($"Calculating overlap for ({prefixIndex},{suffixIndex})");
                        int overlapScore = this.CalculateOverlap(prefixRead, suffixRead);
                        this.overlapMatrix[prefixIndex,suffixIndex] = overlapScore;
                    }

                }
            }
        }

        
        private string FindSourceRead()
        {
            Console.WriteLine("Locating source read.");
            int minColSum = System.Int32.MaxValue;
            int minColIndex = -1;
            for (int col = 0; col < this.overlapMatrix.GetLength(1); col++)
            {
                var column = this.overlapMatrix.GetArrayCol(col);
                int colSum = column.Sum();
                if (colSum < minColSum)
                {
                    minColIndex = col;
                    minColSum = colSum;
                }
            }
            return this.reads[minColIndex];
        }

        private string FindSinkRead()
        {
            Console.WriteLine("Locating termination read.");
            int minRowSum = System.Int32.MaxValue;
            int minRowIndex = -1;
            for (int row = 0; row < this.overlapMatrix.GetLength(0); row++)
            {
                var rowArr = this.overlapMatrix.GetArrayRow(row);
                int rowSum = rowArr.Sum();
                if (rowSum < minRowSum)
                {
                    minRowIndex = row;
                    minRowSum = rowSum;
                }
            }

            return this.reads[minRowIndex];
        }
        
        private int AddOverlap(int prefixIndex, ref string genome)
        {
            var pRow = this.overlapMatrix.GetArrayRow(prefixIndex);
            int overlappingSymbols = pRow.Max();
            int overlapReadIndex = Array.IndexOf(pRow, pRow.Max());
            string matchingSuffix = this.reads[overlapReadIndex];
            
            genome += matchingSuffix.Substring(overlappingSymbols);
            return overlapReadIndex;

        }
        
        public void GenerateSuperString()
        {
            string genome = "";
            string source = this.FindSourceRead();
            string sink = this.FindSinkRead();
            genome += source;
            int sinkIndex = reads.IndexOf(sink);
            int currentIndex = reads.IndexOf(source);
            Console.WriteLine("Constructing genome.");
            while (currentIndex != sinkIndex)
            {
                int nextIndex = this.AddOverlap(currentIndex, ref genome);
                currentIndex = nextIndex;    
            }
            this.Genome = genome;

        }
        
        public void OutputGenome()
        {
            if (this.OutputLocation != null)
            {
                Utils.OutputFile(this.OutputLocation, new List<string>(){this.Genome});
            }
            else
            {
                Console.WriteLine(this.Genome);
            }
        }
        
        public void Run()
        {
            this.BuildOverlapMatrix();
            this.GenerateSuperString();
            this.OutputGenome();

        }
    }    
}