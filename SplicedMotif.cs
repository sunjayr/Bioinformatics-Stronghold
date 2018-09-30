using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Rosalind
{
    class SplicedMotif
    {
        public int[,] scoringArray;
        public List<int> indices;
        public string referenceSeq;
        public string querySeq;
        
        public SplicedMotif()
        {
            this.ReadFastaFile("spliced_motif_input.txt");
            scoringArray = new int[this.querySeq.Length, this.referenceSeq.Length];
            indices = new List<int>();

        }

        public void ReadFastaFile(string filename){
            ArrayList sequences = new ArrayList();
            StreamReader sr = new StreamReader(filename);
            string line;
            string sequenceBuffer = "";
            while((line = sr.ReadLine()) != null){
                if (line[0] == '>'){
                    if(sequenceBuffer != ""){
                        sequences.Add(sequenceBuffer);
                        sequenceBuffer = "";
                    }
                }
                else {
                    sequenceBuffer += line;
                }
            }
            sequences.Add(sequenceBuffer);
            referenceSeq = (string) sequences[0];
            querySeq = (string) sequences[1];

        }

        public void FindIndices(){
            for (int i = 0; i < scoringArray.GetLength(0); i++){
                for(int j = 0; j < scoringArray.GetLength(1); j++){
                    if (querySeq[i] == referenceSeq[j]){
                        scoringArray[i,j] = 1;
                    }
                }
            }

            for (int i = 0; i < scoringArray.GetLength(0); i++){
                for(int j = i; j < scoringArray.GetLength(1); j++){
                    if (scoringArray[i,j] == 1){
                        if (this.indices.Count == 0 || j > this.indices.Max()){
                            this.indices.Add(j + 1);
                            break;
                        }
                    }
                }
            }

            foreach(var element in this.indices){
                Console.Write(element + " ");
            }

        }

        public void Execute(){
            this.FindIndices();
        }

    }
}