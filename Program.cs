using System;

namespace Rosalind
{
    class Program
    {
        static void Main(string[] args)
        {
            GenomeAssembly genome = new GenomeAssembly("./input/genome_assembly_input.fa");
            genome.PrintReads();
        }
    }
}
