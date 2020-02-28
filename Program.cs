using System;
using CommandLine;

namespace Rosalind
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<GenomeAssemblyOptions, OrfOptions, SharedSplicedMotifOptions, ReadCorrectionOptions, InterleavingMotifsOptions>(args).MapResult(
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
                (ReadCorrectionOptions opts) => {
                    ReadCorrection rc = new ReadCorrection(opts.InputFile);
                    rc.Run();
                    return 0;
                },
                (InterleavingMotifsOptions opts) => {
                    InterleavingMotifs im = new InterleavingMotifs(opts.InputFile, opts.MatchScore, opts.IndelPenalty);
                    im.Run();
                    return 0; 
                },
                errs => 1
            );
        }
    }
}
