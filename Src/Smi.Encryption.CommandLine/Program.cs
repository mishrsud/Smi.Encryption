using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGR.CommandLineParser;
using MGR.CommandLineParser.Command;

namespace Smi.Encryption.CommandLine
{
	class Program
	{
		private static ParserBuilder _parserBuilder = new ParserBuilder();

		static int Main(string[] args)
		{
			IParser parser = _parserBuilder.BuildParser();
			CommandResult<ICommand> commandResult = parser.Parse(args);

			if (commandResult.IsValid)
			{
				return commandResult.Execute();
			}

			return (int)commandResult.ReturnCode;
		}
	}
}
