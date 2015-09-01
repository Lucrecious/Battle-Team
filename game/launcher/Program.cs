/********************************
*			Lucrecious			*
*			Under MIT			*
*********************************/

using System;

namespace Launcher
{
	public static class Program
    {
        [STAThread]
        public static void Main()
        {
            using (App game = new App())
            {
                game.Run();
            }
        }
    }
}