using System;
using System.Linq;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

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

        
        public static List<string> ReadFile(string fileName) {
            List<string> lines = new List<string>();
            using (StreamReader sr = new StreamReader(fileName))
            {
                string line;
                while((line = sr.ReadLine()) != null) {
                    lines.Add(line);
                }
            }
            return lines;
        }
        
        public static void OutputFile(string fileName, List<string> fileLines)
        {
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                foreach (var line in fileLines)
                {
                    sw.WriteLine(line);
                }
            }
        }
        
        public static List<string> GenerateKmers(int k, string read)
        {
            //TODO: Implement function to create Kmers
            //and check comment
            return new List<string>();
        }

        public static T[] GetArrayRow<T>(this T[,] array, int row)
        {
            return Enumerable.Range(0,array.GetLength(1))
                            .Select(x => array[row,x]).ToArray();
        }

        public static T[] GetArrayCol<T>(this T[,] array, int col)
        {
            return Enumerable.Range(0,array.GetLength(0))
                            .Select(x => array[x,col]).ToArray();
        }

        public static void PrintArray(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i,j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}