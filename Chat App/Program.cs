
using System;
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
            Application.SetCompatibleTextRenderingDefault(false);

            IPresenterServer presenter = new ProxyServer(ChatApp.Instance);
            ChatApp.Instance.SetPresenter(presenter);
            //presenter.Run();
            Application.EnableVisualStyles();
            Application.Run(ChatApp.Instance);
        }
    }
}
