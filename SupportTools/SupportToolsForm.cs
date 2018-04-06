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
using clib;

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
        SynchronizationContext WorkThreadContext;
        Thread th;

        public SupportToolsForm()
        {
            InitializeComponent();
            IsPageLoaded = false;
            lblVersion.Text = Application.ProductVersion;

        }
        
        private void btnClearCache_Click(object sender, EventArgs e)
        {
            btnClearCache.Enabled = false;
            pbrClearCache.Value = 0;
            
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
 
            var context = new SynchronizationContext();
            
            SynchronizationContext.SetSynchronizationContext(context);

            WorkThreadContext = SynchronizationContext.Current;

            RunBrowserThread(new Uri(txtClearpassStartPageUrl.Text));

            Invoke(new Action(() =>
            {
                lblStatusContent.Text = "Initial page loading";
            }));
            WriteLog("", true);
            WriteLog("Initial load", false);

            UpdateClearCacheProgressAndStatus("Proceeding logon");
            MakeAction("username", "value", txtUsername.Text, false);
            MakeAction("password", "value", txtPassword.Text, true);
            MakeAction("submit_btn", "Click", true);

            WriteLog("Creds load", false);
            
            UpdateClearCacheProgressAndStatus("Clicking configuration");
            MakeAction("menu_5_5_button", "Click", false);
            WriteLog("Menu 5.5", false);

            UpdateClearCacheProgressAndStatus("Clicking authentication");

            GetHtmlElementByTagSafe("span", "Authentication", false, true);
            
            if (element.GetAttribute("aria-expanded").Equals("false"))
            {
                MakeAction(element, "Click");
                WriteLog("Auth load", false);

            }
            UpdateClearCacheProgressAndStatus("Clicking sources");
            GetHtmlElementByTagSafe("span", "Sources", false, true);
            MakeAction(element, "Click");
            WriteLog("Source load", false);

            UpdateClearCacheProgressAndStatus("Clicking Kernel_AD");
            GetHtmlElementByTagSafe("span", "Kernel_AD", false, false);
            HtmlElement bottomEl = element;
            element = bottomEl.Parent;

            MakeAction(element, "Click");
            WriteLog("Kernel-AD load", false);

            UpdateClearCacheProgressAndStatus("Clearing cache");
            MakeAction("authSources-clearCache", "Click", false);
            WriteLog("Cache button click", false);
            
            GetHtmlElementDelegate("msgBarTxt");

            if (element != null)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (element.GetAttribute("InnerText") != "")
                    {
                        UpdateClearCacheProgressAndStatus(element.GetAttribute("InnerText"));
                        break;
                    }
                    else
                    {
                        if (i > 9)
                            UpdateClearCacheProgressAndStatus("Clearing cache failed");
                    }
                    Wait(10, true);

                }
                
            }

            Invoke(new Action(() =>
            {
               btnClearCache.Enabled = true;
            }));

            BrowserThreadContext.Send(delegate
            {
                Application.ExitThread();
            }, null);
            
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
            int nWinHandle2 = FindWindow(null, "Message From Webpage");
            if (nWinHandle != 0)
            {
                SendKeys.Send("Y%");
            }
            if (nWinHandle2 != 0)
            {
                SendKeys.Send("{ENTER}");
            }

            Activate();

        }

        public void MakeAction (string elementId, string AttributeName, string AttributeValue, bool DontWait)
        {
            element = null;
            int index = 0;
            if (!DontWait)
                Wait(10, false);

            while (element == null)
            {
                GetHtmlElementDelegate(elementId);
                if (element != null)
                {
                    element.SetAttribute(AttributeName, AttributeValue);
                }
                else 
                {
                    if (index > 10)
                    {
                        MessageBox.Show("Maximum attempts reached, Element is NULL - " + elementId);
                        break;
                    }
                    Wait(10, false);
                }
                index++;
            }
        }

        public void MakeAction(string elementId, string InvokeMethod, bool DontWait)
        {
            element = null;
            int index = 0;
            if (!DontWait)
                Wait(10, false);
            while (element == null)
            {
                GetHtmlElementDelegate(elementId);
                if (element != null)
                {
                    element.InvokeMember(InvokeMethod);
                }
                else
                {
                    if (index > 10)
                    {
                        MessageBox.Show("Maximum attempts reached, Element is NULL - " + elementId);
                        break;
                    }
                    Wait(10, false);
                }
                index++;
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

        public void GetHtmlElementByTagSafe(string tag, string tagName, bool DontWait, bool IsScriptInvoked)
        {
            element = null;
            int index = 0;
            if (!DontWait)
                Wait(10, IsScriptInvoked);
            while (element == null)
            {
                GetHtmlElementByTagDelegate(tag, tagName);
                if (element == null)
                {
                    if (index > 10)
                    {
                        MessageBox.Show("Maximum attempts reached, Element is NULL");
                        break;
                    }
                Wait(10, IsScriptInvoked);
                }
                index++;
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

            Thread.Sleep(100);

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
                th = new Thread(() => {        
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

        public void WriteLog(string Message, bool useDelimiter)
        {
            if (chkEnableLogging.Checked)
            {
                string CurrentTime = DateTime.Now.ToLongTimeString();
                string WriteLogPath = txtLogPath.Text;
            
                using (StreamWriter str_output = new StreamWriter(WriteLogPath + (WriteLogPath == "" ? "" : "\\") + "logSuppApp.txt", true))
                {
                    if (!useDelimiter)
                    {
                        str_output.WriteLine(CurrentTime + " -> " + Message);
                    }
                    else
                    {
                        str_output.WriteLine("--------------------");
                    }
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
                CryptoService.EncryptedBundle eb = new CryptoService.EncryptedBundle();
                eb = CryptoService.EncryptString(txtPassword.Text, 20, "s8Jkd74hHdyrO9h6");
                config.password = eb.EncryptedString;
                config.key = eb.EncryptedKey;
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
                CryptoService.EncryptedBundle eb = new CryptoService.EncryptedBundle();
                eb.EncryptedString = config.password;
                eb.EncryptedKey = config.key;
                txtPassword.Text = CryptoService.DecryptString(eb, "s8Jkd74hHdyrO9h6");
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
