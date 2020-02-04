using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace games
{
    public class Compiler
    {
        public YamlSequenceNode games;

        public Compiler()
        {
            using (var reader = new StreamReader(File.OpenRead("games.yml"))) {
                var yaml = new YamlStream();
                yaml.Load(new StringReader(reader.ReadToEnd()));
                var mapping = (YamlMappingNode)yaml.Documents[0].RootNode;
                games = (YamlSequenceNode)mapping.Children[new YamlScalarNode("gamelist")];
            }
        }

        public Task Build()
        {
            foreach (YamlMappingNode game in games) {
                var title = game.Children[new YamlScalarNode("title")].ToString();
                var exe = game.Children[new YamlScalarNode("exe")].ToString();

                Console.WriteLine("Compiling {0}", title);

                var lTitle = ToLiteral(title);
                var argsList = new string[]{
                    $"-O2 -o games/{exe}.exe src/game.c",
                    "-I/mingw64/include -L/mingw64/lib",
                    "-lglew32 -lglfw3 -lopengl32 -lgdi32 -mwindows",
                    $"-DWINDOW_TITLE=\\\"{lTitle}\\\""
                };

                var psi = new ProcessStartInfo();
                psi.FileName = @"C:\msys64\mingw64\bin\gcc.exe";
                psi.Arguments = string.Join(' ', argsList);
                psi.UseShellExecute = false;

                var proc = Process.Start(psi);
                proc.WaitForExit();
                Console.WriteLine("Build complete");
            }

            return Task.CompletedTask;
        }

        public string ToLiteral(string input)
        {
            using (var writer = new StringWriter()) {
                using (var provider = CodeDomProvider.CreateProvider("CSharp")) {
                    provider.GenerateCodeFromExpression(new CodePrimitiveExpression(input), writer, null);

                    return writer.ToString();
                }
            }
        }
    }
}
