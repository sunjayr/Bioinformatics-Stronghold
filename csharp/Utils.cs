using System;
using System.Linq;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Rosalind 
{
    public static class Utils
    {
        
        /// <summary> ReadFastaFile is a static function to generate a Dictionary from fasta input</summary>
        public static Dictionary<string, string> ReadFastaFile(string fileName)
        {
            Dictionary<string, string> fastaDict = new Dictionary<string, string>();
            if (!(fileName.EndsWith(".fa") || fileName.EndsWith(".fasta")))
            {
                throw new ArgumentException($"File name {fileName} does not contain the appropriate fasta extension");
            }

            using (StreamReader sr = new StreamReader(fileName)) 
            {
                string line = "";
                string sequenceId = "";
                string sequenceBuffer = "";
                while ((line = sr.ReadLine()) != null) {
                    if (line.StartsWith(">"))
                    {
                        if (sequenceId.Length > 0 && sequenceBuffer.Length > 0)
                        {
                            fastaDict[sequenceId] = sequenceBuffer;
                            sequenceId = "";
                            sequenceBuffer = "";
                        }
                        sequenceId = line.Substring(1);
                    }
                    else
                    {
                        sequenceBuffer += line;
                    }
                }
                fastaDict[sequenceId] = sequenceBuffer;
            }
            return fastaDict;
        }
    }
}