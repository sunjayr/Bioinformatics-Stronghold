/*
Given: A collection of up to 1000 reads of equal length (at most 50 bp) in FASTA format. Some of these reads were generated with a single-nucleotide error. For each read s in the dataset, one of the following applies:
s was correctly sequenced and appears in the dataset at least twice (possibly as a reverse complement);
s is incorrect, it appears in the dataset exactly once, and its Hamming distance is 1 with respect to exactly one correct read in the dataset (or its reverse complement).

Return: A list of all corrections in the form "[old read]->[new read]". (Each correction must be a single symbol substitution, and you may return the corrections in any order.)
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
namespace Rosalind
{
    class ReadCorrection
    {
        public List<string> reads;
        public Dictionary<string, string> readMap;
        public int[,] correctionMatrix;
        public Dictionary<string, string> reverseComplementMap;
        public List<string> correctReads;

        public ReadCorrection(string fastaInput) {
            this.readMap = Utils.ReadFastaFile(fastaInput);
            this.reads = this.readMap.Values.ToList();
            this.correctionMatrix = new int [this.reads.Count(), this.reads.Count()];
            this.reverseComplementMap = new Dictionary<string, string>();
            this.correctReads = new List<string>();
        }

        public string GetReverseComplement(string a) {
            var letterList = new List<char>();
            var complements = new Dictionary<char, char>(){
                {'A','T'},
                {'T','A'},
                {'C','G'},
                {'G','C'}
            };
            foreach(char letter in a.Reverse()) {
                letterList.Add(complements[letter]);
            }
            return String.Join("", letterList);
        }

        public int CalculateHammingDistance(string readA, string readB) {
            if (readA.Count() != readB.Count()) {
                throw new ArgumentException("Attempting to count hamming distance of unequal length strings");
            }
            
            var characters = readA.ToArray().Zip(readB.ToArray(), (x,y) => new Tuple<char, char>(x,y));
            var differences = from tup in characters where tup.Item1 != tup.Item2 select tup;
            return differences.Count(); 
        }
        
        public void IdentifyCorrectReads() {
            for (int i = 0; i < this.correctionMatrix.GetLength(0); i++) {
                string reverseComplement = this.GetReverseComplement(this.reads[i]);
                this.reverseComplementMap[this.reads[i]] = reverseComplement;
                for ( int j = 0; j < this.correctionMatrix.GetLength(1); j++) {
                    if (i == j) continue;
                    if (this.reads[i] == this.reads[j] || reverseComplement == this.reads[j]) {
                        this.correctionMatrix[i,j] = 1;
                        this.correctReads.Add(this.reads[i]);
                    }
                }

            }
        }
        
        public void CorrectReads() {
            for (int i = 0; i < this.correctionMatrix.GetLength(1); i++) {
                var arrayCol = this.correctionMatrix.GetArrayCol(i);
                if (arrayCol.Sum() == 0) {
                    foreach (string read in this.reads) {
                        if (this.correctReads.Contains(read) && this.CalculateHammingDistance(this.reads[i], read) == 1) {
                            Console.WriteLine(this.reads[i] + "->" + read);
                            break;
                        }
                        else if (this.correctReads.Contains(read) && this.CalculateHammingDistance(this.reads[i], this.reverseComplementMap[read]) == 1) {
                            Console.WriteLine(this.reads[i] + "->" + this.reverseComplementMap[read]);
                            break;
                        }
                    }        
                }
            }
        }
        
        public void Run() {
            this.IdentifyCorrectReads();
            this.CorrectReads();
        }
    }
}