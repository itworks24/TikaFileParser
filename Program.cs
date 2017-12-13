using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;
using TikaOnDotNet.TextExtraction;

namespace TikaFileParser
{

    class Options
    {
        [Option('f', "file", Required = true,
          HelpText = "Input file to be processed.")]
        public string InputFile { get; set; }

        [Option('r', "redirect", DefaultValue = "",
          HelpText = "Redirects output to provided file name.")]
        public string Redirect { get; set; }

        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
              current => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }

    class Program
    {
        private static int Main(string[] args)
        {
            var options = new Options();
            if (!Parser.Default.ParseArguments(args, options)) return -1;
            var textExtractor = new TextExtractor();
            var docContent = textExtractor.Extract(options.InputFile);
            if(options.Redirect != "") 
                System.IO.File.WriteAllText(options.Redirect, docContent.Text);
            else 
                Console.Write(docContent.Text);
            return 0;
        }
    }
}
