/***************************************************************************
 *                                                                         *
 *  Autor:  Gafencu Gabriel                                                *
 *  Grupa:  1309A                                                          *
 *  Fisier: Program.cs                                                     *
 *                                                                         *
 *  Descriere: Punctul de intrare al aplicației                            *
 *  ***********************************************************************/

using System;
using System.Threading;
using System.Windows.Forms;

using ChatAppClient;
using MainServerNs;

namespace Chat_App
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {


            bool result;
            var mutex = new Mutex(true, "Chat App", out result);

            if (!result)
                return;

            Application.SetCompatibleTextRenderingDefault(false);

            IPresenterServer presenter = new ProxyServer(ChatApp.Instance);
            ChatApp.Instance.SetPresenter(presenter);
            ((ProxyServer)presenter).Run();
            Application.EnableVisualStyles();
            Application.Run(ChatApp.Instance);

            GC.KeepAlive(mutex);
        }
    }
}
