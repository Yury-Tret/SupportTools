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
using System.IO;
using System.Xml.Serialization;

namespace SupportTools
{
    public partial class SupportToolsForm : Form
    {

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private static extern int FindWindow(string sClass, string sWindow);

        WebBrowser Browser;
        bool IsPageLoaded;
        bool IsBrowserBusy;
        HtmlElement element;
        HtmlElementCollection elc;
        SynchronizationContext BrowserThreadContext;
        

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
                WorkThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }


        public void WorkThreadJob()
        {
            RunBrowserThread(new Uri(txtClearpassStartPageUrl.Text));

            Invoke(new Action(() =>
            {
                lblStatusContent.Text = "Initial page loading";
            }));

            WriteLog("Initial load - BW");
            Wait(1000, false);
            WriteLog("Initial load - AW");
            UpdateClearCacheProgressAndStatus("Proceeding logon");
            MakeAction("username", "value", txtUsername.Text);
            MakeAction("password", "value", txtPassword.Text);
            MakeAction("submit_btn", "Click");
            WriteLog("Creds load - BW");
            Wait(1000, false);
            WriteLog("Creds load - AW");
            UpdateClearCacheProgressAndStatus("Clicking configuration");
            MakeAction("menu_5_5_button", "Click");
            WriteLog("Menu 5.5 - BW");
            Wait(1000, true);
            WriteLog("Menu 5.5 - AW");
            UpdateClearCacheProgressAndStatus("Clicking authentication");
            GetHtmlElementByTagDelegate("span", "Authentication");
            
            if (element.GetAttribute("aria-expanded").Equals("false"))
            {
                MakeAction(element, "Click");
                WriteLog("Auth load - BW");
                Wait(1000, true);
                WriteLog("Auth load - AW");
            }
            UpdateClearCacheProgressAndStatus("Clicking sources");
            GetHtmlElementByTagDelegate("span", "Sources");
            MakeAction(element, "Click");
            WriteLog("Source load - BW");
            Wait(1000, false);
            WriteLog("Source load - AW");
            UpdateClearCacheProgressAndStatus("Clicking Kernel_AD");
            GetHtmlElementByTagDelegate("span", "Kernel_AD");
            HtmlElement bottomEl = element;
            element = bottomEl.Parent;

            MakeAction(element, "Click");
            WriteLog("Kernel-AD load - BW");
            Wait(1000, false);
            WriteLog("Kernel-AD load - AW");
            UpdateClearCacheProgressAndStatus("Clearing cache");
            MakeAction("authSources-clearCache", "Click");
            WriteLog("Cache button click - BW");
            Wait(1000, true);
            WriteLog("Cache button click - AW");
            GetHtmlElementDelegate("msgBarTxt");
            if (element != null)
            {
                UpdateClearCacheProgressAndStatus(element.GetAttribute("InnerText"));
            }

        }
        public void UpdateClearCacheProgressAndStatus (string message)
        {
            Invoke(new Action(() =>
            {
                pbrClearCache.Value += 1;
                lblStatusContent.Text = message;
            }));
        }

        public void AcceptSslCertificate()
        {
            int nWinHandle = FindWindow(null, "Security Alert");
            if (nWinHandle != 0)
            {
                SendKeys.Send("Y%");
            }
        }

        public void MakeAction (string elementId, string AttributeName, string AttributeValue)
        {
            GetHtmlElementDelegate(elementId);
            if (element != null)
            {
                element.SetAttribute(AttributeName, AttributeValue);
            }
            else
            {
                MessageBox.Show("Element is NULL - " + elementId);
            }
        }

        public void MakeAction(string elementId, string InvokeMethod)
        {
            GetHtmlElementDelegate(elementId);
            if (element != null)
            {
                element.InvokeMember(InvokeMethod);
            }
            else
            {
                MessageBox.Show("Element is NULL - " + elementId);
            }
        }

        public void MakeAction(HtmlElement element, string InvokeMethod)
        {
            if (element != null)
            {
                element.InvokeMember(InvokeMethod);
            }
            else
            {
                MessageBox.Show("Element is NULL");
            }
        }

        public void Wait (int Interval, bool IsScriptInvoked)
        {
            do
            {
                BrIsBusyDelegate();
                if (Browser.InvokeRequired)
                {
                    Thread.Sleep(Interval);
                }
                else
                {
                    Application.DoEvents();
                }
            } while ((IsScriptInvoked ? false : !IsPageLoaded) || IsBrowserBusy);
            IsPageLoaded = false;

        }

        public void BrIsBusyDelegate ()
        {

            BrowserThreadContext.Send(delegate
            {
                IsBrowserBusy = Browser.IsBusy;
            }, null);
        }
        
        public void GetHtmlElementDelegate(string id)
        {
            if (Browser.InvokeRequired)
            {
                Browser.Invoke((MethodInvoker)delegate ()
                {
                    GetHtmlElementDelegate(id);
                });
            }
            else
            {
                element = Browser.Document.GetElementById(id);
            }
            
        }

        public void GetHtmlElementByTagDelegate(string tag, string param)
        {
            if (Browser.InvokeRequired)
            {
                Browser.Invoke((MethodInvoker)delegate ()
                {
                    GetHtmlElementByTagDelegate(tag, param);
                });
            }
            else
            {
                GetElementsByTagName(tag);
                foreach (HtmlElement el in elc)
                {
                    if (el.GetAttribute("InnerText").Equals(param))
                    {
                        element = el;
                    }
                 }
            }
        }

        public void GetElementsByTagName(string tag)
        {
            if (Browser.InvokeRequired)
            {
                Browser.Invoke((MethodInvoker)delegate ()
                {
                    GetElementsByTagName(tag);
                });

            }
            else
            {
                elc = Browser.Document.GetElementsByTagName(tag);
            }
        }


        
        private void RunBrowserThread(Uri url)
        {
            var th = new Thread(() => {        
                Browser = new WebBrowser();
                BrowserThreadContext = SynchronizationContext.Current;
                Browser.ScriptErrorsSuppressed = false;            
                Browser.DocumentCompleted += BrowserDocumentCompleted;            
                Browser.Navigate(url);            
                Application.Run();            
            });        
            th.SetApartmentState(ApartmentState.STA);        
            th.Start();       
         }                       
        
        void BrowserDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            var br = sender as WebBrowser;

            if (br.Url == e.Url)
            {
                IsPageLoaded = true;
            }
            
        }

        private void SupportToolsForm_Deactivate(object sender, EventArgs e)
        {
            AcceptSslCertificate();
        }

        public void WriteLog(string Message)
        {
            if (chkEnableLogging.Enabled)
            {
                string CurrentTime = DateTime.Now.ToLongTimeString();
                string WriteLogPath = txtLogPath.Text;
            
                using (StreamWriter str_output = new StreamWriter(WriteLogPath + (WriteLogPath == "" ? "" : "\\") + "logSuppApp.txt", true))
                {
                    str_output.WriteLine(CurrentTime + " -> " + Message);
                }
            }

        }

        private void txtLogPath_DoubleClick(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                txtLogPath.Text = folderBrowser.SelectedPath;
            }
        }

        public void SaveConfiguration()
        {
            try
            {
                ConfigurationData config = new ConfigurationData();
                config.username = txtUsername.Text;
                config.password = txtPassword.Text;
                config.startPage = txtClearpassStartPageUrl.Text;
                config.enableLogging = chkEnableLogging.Checked;
                config.logPath = txtLogPath.Text;
                WriteXMLData.SaveData(config, "config.xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void LoadConfiguration()
        {
            if (File.Exists("config.xml"))
            {
                XmlSerializer xmlSr = new XmlSerializer(typeof(ConfigurationData));
                FileStream readStream = new FileStream("config.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                ConfigurationData config = (ConfigurationData)xmlSr.Deserialize(readStream);
                txtUsername.Text = config.username;
                txtPassword.Text = config.password;
                txtClearpassStartPageUrl.Text = config.startPage;
                chkEnableLogging.Checked = config.enableLogging;
                txtLogPath.Text = config.logPath;
                readStream.Close();

            }

        }

        private void SupportToolsForm_Load(object sender, EventArgs e)
        {
            LoadConfiguration();
            if (!chkEnableLogging.Checked)
            {
                txtLogPath.Enabled = false;
            }
            else
            {
                txtLogPath.Enabled = true;
            }
        }

        private void SupportToolsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveConfiguration();
        }

        private void chkEnableLogging_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkEnableLogging.Checked)
            {
                txtLogPath.Enabled = false;
            }
            else
            {
                txtLogPath.Enabled = true;
            }
        }
    }
}
