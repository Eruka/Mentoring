using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncAwait
{
    public partial class Form1 : Form
    {
        readonly Dictionary<Control, CancellationTokenSource> controlsWithCts = new Dictionary<Control, CancellationTokenSource>() ;
        public Form1()
        {
            InitializeComponent();
        }

        private void cancelLoadingBtn_Click(object sender, EventArgs e)
        {
            var parent = ((Button)sender).Parent;
            var cts = controlsWithCts[parent];

            if (cts != null)
            {
                cts.Cancel();
            }
        }

        private async void loadPageBtn_Click(object sender, EventArgs e)
        {

            var parent = ((Button)sender).Parent;
            var urlTBox = (TextBox)parent.Controls[0];
            var pageContentTBox = (RichTextBox)parent.Controls[3];

            pageContentTBox.Clear();
            pageContentTBox.Text = "Loading";
            var cts = new CancellationTokenSource();
            var cancellationtoken = cts.Token;

            if (!controlsWithCts.ContainsKey(parent))
            {
                controlsWithCts.Add(parent, cts);
            } else
            {
                controlsWithCts[parent] = cts;
            }

            Task.Factory.StartNew(async () =>
             {
                 while (true)
                 {
                     cancellationtoken.ThrowIfCancellationRequested();
                     await Task.Delay(100);
                     pageContentTBox.Invoke(new MethodInvoker(() => pageContentTBox.Text += "."));
                 }
             }, cancellationtoken);

            try
            {
                string data = await GetDataAsync(urlTBox.Text.Trim(), cancellationtoken);
                cts.Cancel();
                pageContentTBox.Text += "\n" + data;
            }
            catch (OperationCanceledException)
            {
                pageContentTBox.Text += "download canceled";
            }
            catch (Exception ex)
            {
                cts.Cancel();
                pageContentTBox.Text += ex.Message;
            }
        }

        private async Task<string> GetDataAsync(string url, CancellationToken ct)
        {
            HttpClient client = new HttpClient();
            await Task.Delay(5000);
            HttpResponseMessage responce = await client.GetAsync(url, ct);
            return await responce.Content.ReadAsStringAsync();
        }

        private void addNewCtrlBtn_Click(object sender, EventArgs e)
        {
            int count = mainPnl.Controls.Count;
            var panel = new Panel() { Height = 140, Width = 1040, BackColor  = Color.Gray , Location = new Point(10, count * (140 + 10))};
            var textBox = new TextBox() { Text = "http://tut.by", Location = new Point(15,10)};
            var loadButton = new Button() { Text = "Load", Location = new Point(15, 45), BackColor = Color.LightGray };
            var cancelButton = new Button() { Text = "Cancel", Location = new Point(105, 45), BackColor = Color.LightGray };
            var contentTextBox = new RichTextBox() { Location = new Point(290, 10), Width = 730, Height = 115 };
            loadButton.Click += loadPageBtn_Click;
            cancelButton.Click += cancelLoadingBtn_Click;
            panel.Controls.AddRange(new Control[] { textBox, loadButton, cancelButton, contentTextBox });
            mainPnl.Controls.Add(panel);
        }
    }
}
