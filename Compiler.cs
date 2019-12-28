using Newtonsoft.Json;
using System;
using System.IO;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Threading.Tasks;

namespace games
{
    public class Compiler
    {
        public GamesList games;

        public Compiler()
        {
            using (var reader = new StreamReader(File.OpenRead("games.json"))) {
                games = JsonConvert.DeserializeObject<GamesList>(reader.ReadToEnd());
            }
        }

        public Task Build()
        {
            foreach (var game in games.Games) {
                Console.WriteLine($"Compiling {game.Title}");

                var title = ToLiteral(game.Title);
                var argsList = new string[]{
                    $"-o games/{game.Exe}.exe src/game.c",
                    "-I/mingw64/include -L/mingw64/lib",
                    "-lglew32 -lglfw3 -lopengl32 -mwindows",
                    $"-DWINDOW_TITLE=\\\"{title}\\\""
                };

                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = @"C:\msys64\mingw64\bin\gcc.exe";
                psi.Arguments = string.Join(' ', argsList);
                psi.UseShellExecute = false;

                Process p = Process.Start(psi);
                p.WaitForExit();
                Console.WriteLine($"Build complete");
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

        [JsonObject]
        public class GamesList
        {
            [JsonProperty("gamelist")]
            public Game[] Games { get; set; }
        }

        [JsonObject]
        public class Game
        {
            [JsonProperty("title")]
            public string Title { get; set; }
            [JsonProperty("exe")]
            public string Exe { get; set; }
        }
    }
}
