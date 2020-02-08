using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Rosalind {
    class SharedSplicedMotif {
        public List<string> reads;
        public Dictionary<string,string> readMap;
        public int[,] matchMatrix;
        public string FirstRead{get; set;}
        public string SecondRead{get; set;}
        public string OutputFile{get;set;}
        public List<Char> sequenceArray;
        public int matchValue;
        public SharedSplicedMotif(string fastaInput, string outputFile) {
            this.readMap = Utils.ReadFastaFile(fastaInput);
            this.reads = readMap.Values.ToList();
            if (this.reads.Count > 2) {
                throw new Exception("More than two fasta reads found");
            }
            this.FirstRead = this.reads[0];
            this.SecondRead = this.reads[1];
            this.matchMatrix = new int[this.FirstRead.Length + 1, this.SecondRead.Length + 1];
            this.matchValue = 1;
            this.sequenceArray = new List<char>();
            this.OutputFile = outputFile;

        }

        public void CreateMatchMatrix() {
            for (int i = 1; i < this.FirstRead.Length + 1; i++) {
                for (int j = 1; j < this.SecondRead.Length + 1; j++) {
                    if (this.FirstRead[i-1] == this.SecondRead[j-1]) {
                        this.matchMatrix[i,j] = Math.Max(this.matchMatrix[i-1,j-1] + this.matchValue, Math.Max(this.matchMatrix[i-1,j], this.matchMatrix[i,j-1]));
                    }
                    else {
                        this.matchMatrix[i,j] = Math.Max(this.matchMatrix[i-1,j], this.matchMatrix[i,j-1]);
                    }
                }
            }
        }

        public void PrintMatrix() {
            for (int i = 0; i < this.matchMatrix.GetLength(0); i++) {
                for (int j = 0; j < this.matchMatrix.GetLength(1); j++) {
                    Console.Write(this.matchMatrix[i,j] + " ");
                }
                Console.WriteLine();
            }
        }

        public void Backtrack(int i, int j) {
            if (i == 0 || j == 0) {
                return;
            }
            int score = this.matchMatrix[i,j];
            if (score == this.matchMatrix[i-1,j-1] + this.matchValue && this.FirstRead[i-1] == this.SecondRead[j-1]) {
                this.Backtrack(i-1,j-1);
                sequenceArray.Add(this.FirstRead[i-1]);
            }
            else if (score == this.matchMatrix[i-1,j]) {
                this.Backtrack(i-1,j);
            }
            else {
                this.Backtrack(i,j-1);
            }
        }
        
        public void Run(){
            this.CreateMatchMatrix();
            //this.PrintMatrix();
            this.Backtrack(this.matchMatrix.GetLength(0) - 1, this.matchMatrix.GetLength(1) - 1);
            string sequence = string.Join("", sequenceArray.ToArray());
            Utils.OutputFile(this.OutputFile, new List<string>(){sequence});
        }
    }
}