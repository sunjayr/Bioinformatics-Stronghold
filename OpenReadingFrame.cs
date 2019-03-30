using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Rosalind
{
    class OpenReadingFrame
    {
        private string filename;
        private string codonInput = "";
        private Dictionary<string, string> fastaSequences;
        private Dictionary<string, string> codonHashTable;
        private Dictionary<string, List<string>> readingFrames;
        
        //Setting the file name using C# properties
        public string FastaFileName{
            set{
                if(value.EndsWith(".fasta") || value.EndsWith(".fa")){
                    this.filename = value;
                }
                else{
                    throw new ArgumentException("Value not of type .fasta!");
                }
            }
            get{
                return this.filename;
            }
        }

        public string CodonFileName{
            set{
                if(File.Exists(value)){
                    this.codonInput = value;
                }
                else{
                    throw new FileNotFoundException($"File path {value} not found.");
                }
            }
            get{
                return this.codonInput;
            }
        }
        public OpenReadingFrame(string fastaFileName){
            this.FastaFileName = fastaFileName;
            this.fastaSequences = new Dictionary<string, string>();
            this.CodonFileName = "./input/RNA_table.txt";
            this.codonHashTable = new Dictionary<string, string>();
            this.readingFrames = new Dictionary<string, List<string>>();
        }
        
        public void CreateFastaHashtable(){
            List<string> fastaIds = new List<string>();
            List<string> sequences = new List<string>();
            StreamReader sr = new StreamReader(this.FastaFileName);
            string line;
            string sequenceBuffer = "";
            while((line = sr.ReadLine()) != null){
                if(line.StartsWith(">")){
                    if( sequenceBuffer.Length > 0 ){
                        sequences.Add(sequenceBuffer);
                    }
                    fastaIds.Add(line.Substring(1));
                }
                else{
                    sequenceBuffer += line;
                }
            }
            sequences.Add(sequenceBuffer);
            
            //Using anonymous types and Lambda expressions to create dictionary from two lists
            this.fastaSequences = fastaIds.Zip(sequences, (key, value) => new {Id = key, Seq= value})
                                        .ToDictionary(x => (string) x.Id, x => (string) x.Seq);

        }

        public void CreateCodonTable(){
            StreamReader codonReader = new StreamReader(this.CodonFileName);
            string line;
            while((line = codonReader.ReadLine()) != null){
                var lineArray = line.Split(" ");
                this.codonHashTable[(string) lineArray[0]] = (string) lineArray[1];
            }

        }

        public static string ReverseString(string s){
            char[] chars = s.ToCharArray();
            Array.Reverse(chars);
            string reversedString = new string(chars);
            string reverseComplement = String.Join("", reversedString
                                            .Select(x => x == 'A' ? 'T' :
                                                    x == 'T' ? 'A' :
                                                    x == 'C' ? 'G' :
                                                    x == 'G' ? 'C' : x));
            return reverseComplement;
        }
        
        private void CreateSubstrings(string idname, string sequence){
            for(int i = 0; i < 3; i++){
                this.readingFrames[idname].Add(sequence.Substring(i));
            }
        }
        
        public void FindReadingFrames(){
            foreach(var idname in this.fastaSequences.Keys){
                string sequence = (string) this.fastaSequences[(string) idname];
                string reverse = ReverseString(sequence);
                this.readingFrames[idname] = new List<string>();
                CreateSubstrings((string) idname, sequence);
                CreateSubstrings((string) idname, reverse);
            }

        }

        public string TranslateDna(string codingStrand) {
            string proteinSequence = "";
            for (int i = 0; i < codingStrand.Length-3; i+= 3) {
                proteinSequence += this.codonHashTable[codingStrand.Substring(i,3)];
            }
            return proteinSequence;
        }
        public void FindProteinString(){
            List<string> proteinStrings = new List<string>();
            foreach(var idname in this.readingFrames.Keys){
                //Multiple starting points per reading frame
                // Ex: SHVANSGYMGMTPRLGLESLLE$A$MIRVAS
                // Ans:        MGMTPRLGLESLLE
                //               MTPRLGLESLLE
                
                //transcribe and translate each reading frame
                foreach(var seq in this.readingFrames[(string) idname]) {
                    string sequence = seq.Replace('T', 'U');
                    string translatedSeq = this.TranslateDna(sequence);
                    
                    var startingIndices = translatedSeq.Select((x, index) => new {Letter = x, Index = index})
                                                       .Where( x => x.Letter == 'M')
                                                       .Select(x => x.Index);
                    var endingIndices = translatedSeq.Select((x, index) => new {Letter = x, Index = index})
                                                       .Where( x => x.Letter == '$')
                                                       .Select(x => x.Index);
                    
                    
                    if (endingIndices != null){
                        foreach (var startIndex in startingIndices) {
                            var stoppingIndices = endingIndices.Where(x => x > startIndex);
                            if (stoppingIndices.Any()) {
                                int endIndex = stoppingIndices.Min();
                                proteinStrings.Add(translatedSeq.Substring(startIndex, endIndex - startIndex));
                            }
                        }
                    }

                }
                var distinctProteins = proteinStrings.Distinct().ToList();
                foreach (var element in distinctProteins) {
                    Console.WriteLine((string) element);
                }
                
            }
        }
        
        public void Execute(){
            this.CreateFastaHashtable();
            this.CreateCodonTable();
            this.FindReadingFrames();
            this.FindProteinString();
        }

    }
}