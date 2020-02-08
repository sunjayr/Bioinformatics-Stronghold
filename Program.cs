using System;
using CommandLine;

namespace Rosalind
{
    [Verb("genomeAssembly", HelpText="Run a dynamic programming based genome reconstruction algorithm")]
    class GenomeAssemblyOptions
    {
        private string _inputFile;

        [Option('i', "inputFile", Required=true, HelpText="An input fasta file with reads")]
        public string InputFile 
        {   
            get{return this._inputFile;}
            set
            {
                if (value.EndsWith(".fa") || value.EndsWith(".fasta"))
                {
                    this._inputFile = value;
                }
                else {
                    throw new ArgumentException($"Fasta file expected, found {value}");
                }

            }
        }

        [Option('o', "outputFile", Required=false, HelpText="An output file to dump the reconstructed genome")]
        public string OutputFile {get; set;}
    }

    [Verb("orf", HelpText="Run a program to identify all possible open reading frames from a fasta sequence")]
    class OrfOptions
    {
        private string _inputFile;

        [Option('i', "inputFile", Required=true, HelpText="An input fasta file with reads")]
        public string InputFile 
        {   
            get{return this._inputFile;}
            set
            {
                if (value.EndsWith(".fa") || value.EndsWith(".fasta"))
                {
                    this._inputFile = value;
                }
                else {
                    throw new ArgumentException($"Fasta file expected, found {value}");
                }

            }
        }

        [Option('o', "outputFile", Required=false, HelpText="An output file to dump the reconstructed genome")]
        public string OutputFile {get; set;}
    }

    [Verb("ssMotif", HelpText="Run a program to identify a common shared spliced motif from two fasta sequences")]
    class SharedSplicedMotifOptions
    {
        private string _inputFile;

        [Option('i', "inputFile", Required=true, HelpText="An input fasta file with reads")]
        public string InputFile 
        {   
            get{return this._inputFile;}
            set
            {
                if (value.EndsWith(".fa") || value.EndsWith(".fasta"))
                {
                    this._inputFile = value;
                }
                else {
                    throw new ArgumentException($"Fasta file expected, found {value}");
                }

            }
        }

        [Option('o', "outputFile", Required=true, HelpText="An output file to dump the reconstructed genome")]
        public string OutputFile {get; set;}

    }



    class Program
    {
        
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<GenomeAssemblyOptions, OrfOptions, SharedSplicedMotifOptions>(args).MapResult(
                (GenomeAssemblyOptions opts) => {
                    GenomeAssembly genome = new GenomeAssembly(opts.InputFile, opts.OutputFile);
                    genome.Run();
                    return 0;
                },
                (OrfOptions opts) => {
                    OpenReadingFrame orf = new OpenReadingFrame(opts.InputFile);
                    orf.Execute();
                    return 0;
                },
                (SharedSplicedMotifOptions opts) => {
                    SharedSplicedMotif ssMotif = new SharedSplicedMotif(opts.InputFile, opts.OutputFile);
                    ssMotif.Run();
                    return 0;
                },
                errs => 1
            );
        }
    }
}
