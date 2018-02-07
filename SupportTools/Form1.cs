using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;

namespace SupportTools
{
    public partial class SupportToolsForm : Form
    {

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private static extern int FindWindow(string sClass, string sWindow);

        WebBrowser Browser;
        bool IsPageLoaded;


        public SupportToolsForm()
        {
            InitializeComponent();
            IsPageLoaded = false;

        }
        
        private void btnClearCache_Click(object sender, EventArgs e)
        {
            try
            {
                Thread WorkThread = new Thread(new ThreadStart(WorkThreadJob));
                WorkThread.SetApartmentState(ApartmentState.STA);
                WorkThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }


        public void WorkThreadJob()
        {
        //    RunBrowserThread(new Uri(txtClearpassStartPageUrl.Text));

            Browser = new WebBrowser();
            Browser.ScriptErrorsSuppressed = false;
            Browser.DocumentCompleted += BrowserDocumentCompleted;
            Browser.Navigate(txtClearpassStartPageUrl.Text);

            

            Wait(10);

            MakeActionById("username", "SetAttribute", "value", txtUsername.Text, null);
            MakeActionById("password", "SetAttribute", "value", txtPassword.Text, null);
            MakeActionById("submit_btn", "InvokeMember", null, null, "Click");

            Wait(10);

            HtmlDocument doc = Browser.Document;



        }

        public void AcceptSslCertificate()
        {
            int nWinHandle = FindWindow(null, "Security Alert");
            if (nWinHandle != 0)
            {
                SendKeys.Send("Y%");
            }
        }

        public void MakeActionById (string elementId, string Action, string AttributeName, string AttributeValue, string InvokeMethod)
        {
            HtmlElement element = Browser.Document.GetElementById(elementId);
            if (element != null)
            {
                switch (Action)
                {
                    case "SetAttribute":
                        element.SetAttribute(AttributeName, AttributeValue);
                        break;
                    case "InvokeMember":
                        element.InvokeMember(InvokeMethod);
                        break;
                    default:
                        MessageBox.Show("Undefined Actopn: " + Action);
                        break;
                }
            }
        }

        public void Wait (int Interval)
        {
            while (!IsPageLoaded)
            {
                Application.DoEvents();
                Thread.Sleep(Interval);
            }
            IsPageLoaded = false;
        }

/*
        private void RunBrowserThread(Uri url)
        {
            var th = new Thread(() => {
                Browser = new WebBrowser();
                Browser.ScriptErrorsSuppressed = true;
                Browser.DocumentCompleted += BrowserDocumentCompleted;
                Browser.Navigate(url);
                Application.Run();
            });
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }
*/
        void BrowserDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            var br = sender as WebBrowser;
            if (br.Url == e.Url)
            {
                IsPageLoaded = true;
  
    //          Application.ExitThread();   // Stops the thread
            }
        }

        private void SupportToolsForm_Deactivate(object sender, EventArgs e)
        {
            AcceptSslCertificate();
        }
    }
}
