namespace games
{
    class Program
    {
        public async void Run()
        {
            Compiler c = new Compiler();
            await c.Build();
        }
        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run();
        }
    }
}
