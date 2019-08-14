using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Rosalind
{
    class GenomeAssembly
    {
        private Dictionary<string, string> readMap;
        private List<string> reads;
        private int[,] overlapMatrix;


        public GenomeAssembly(string fastaInput) 
        {
            readMap = Utils.ReadFastaFile(fastaInput);
            reads = readMap.Values.ToList();
            overlapMatrix = new int [this.reads.Count,this.reads.Count];
        }

        public void PrintReads() 
        {
            foreach (var de in readMap)
            {
                Console.WriteLine($"Key: {de.Key} Value: {de.Value}");
            }
        }
        ///<summary>Calculates the overlap given a prefix and a suffix read.
        ///Assumes that a valid overlap must be at least half of the word length</summary> 
        public int CalculateOverlap(int prefix, int suffix)
        {
            string prefixRead = this.reads[prefix];
            string suffixRead = this.reads[suffix];
            //TODO: finish implementing overlap calc
            return 1;
            
        }

        public void BuildOverlapMatrix()
        {
            for (int prefixIndex = 0; prefixIndex < this.reads.Count; prefixIndex++)
            {
                for (int suffixIndex = 0; suffixIndex < this.reads.Count; suffixIndex++)
                {
                    int overlapScore = this.CalculateOverlap(prefixIndex, suffixIndex);
                    overlapMatrix[prefixIndex,suffixIndex] = overlapScore;

                }
            }
        }

        public void GenerateSuperString()
        {

        }
    }    
}