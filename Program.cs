using System;

namespace Rosalind
{
    class Program
    {
        static void Main(string[] args)
        {
            OpenReadingFrame test = new OpenReadingFrame("./input/orf_input.fa");
            test.Execute();
        }
    }
}
