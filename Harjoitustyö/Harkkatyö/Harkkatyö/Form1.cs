using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace ListViewExample
{
    public partial class ListViewFormExample : Form
    {
        private static readonly string urlIosystemGripper = "http://127.0.0.1:8081/rw/iosystem/signals/DI_Gripper1_Closed";
        private static readonly string urlIosystemTcpspeed = "http://127.0.0.1:8081/rw/iosystem/signals/AO_TCP_SPEED";
        private static readonly string username = "Default User";
        private static readonly string password = "robotics";

        private delegate void MessageUpdate2(int time, string gripperData, string speedData);
        private MessageUpdate2 deleg2;
        private volatile bool bContinue = false;
        private CancellationTokenSource cancellationTokenSource;

        public ListViewFormExample()
        {
            InitializeComponent();
            InitializeListView();
        }

        private void InitializeListView()
        {
            listView1.Columns.Clear();
            listView1.View = View.Details;
            listView1.Columns.Add("Time", 100);
            listView1.Columns.Add("Gripper State", 150);
            listView1.Columns.Add("TCP Speed", 150);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            buttonStart.Enabled = false;
            buttonStop.Enabled = true;
            cancellationTokenSource = new CancellationTokenSource();
            this.deleg2 = new MessageUpdate2(UpdateListView2);
            Task.Run(() => BackgroundThread(cancellationTokenSource.Token));
        }

        private void UpdateListView2(int time, string gripperData, string speedData)
        {
            if (listView1.InvokeRequired)
            {
                listView1.Invoke(new MethodInvoker(() => UpdateListView2(time, gripperData, speedData)));
                return;
            }
            int n = listView1.Items.Count;
            var item = new ListViewItem(time.ToString());
            item.SubItems.Add(gripperData);
            item.SubItems.Add(speedData);
            listView1.Items.Add(item);
        }

        public async Task BackgroundThread(CancellationToken token)
        {
            int time = 0;
            var handler = new HttpClientHandler
            {
                Credentials = new System.Net.NetworkCredential(username, password)
            };

            using (var client = new HttpClient(handler))
            {
                while (!token.IsCancellationRequested)
                {
                    try
                    {
                        var responseGripper = await client.GetAsync(urlIosystemGripper + "?json=1", token);
                        var responseSpeed = await client.GetAsync(urlIosystemTcpspeed + "?json=1", token);

                        string contentGripper = await responseGripper.Content.ReadAsStringAsync();
                        string contentSpeed = await responseSpeed.Content.ReadAsStringAsync();

                        Console.WriteLine("Full JSON Response (Gripper): " + contentGripper);
                        Console.WriteLine("Full JSON Response (TCP Speed): " + contentSpeed);

                        string gripperState = ParseJsonValue(contentGripper, "lvalue");  // K‰den tila
                        string speedValue = ParseJsonValue(contentSpeed, "pvalue");      // TCP Speed -arvo

                        Console.WriteLine("Gripper: " + gripperState);
                        Console.WriteLine("TCP Speed: " + speedValue);

                        time++;
                        this.BeginInvoke(deleg2, new object[] { time, gripperState, speedValue });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }

                    await Task.Delay(1000, token);
                }
            }
        }

        private string ParseJsonValue(string json, string key)
        {
            try
            {
                var jObject = JObject.Parse(json);
                var token = jObject.SelectToken($"_embedded._state[0].{key}");

                string value = token?.ToString() ?? "N/A";

                // lvalue n‰ytt‰‰ onko k‰si kiinni vai auki
                if (key == "lvalue")
                    return value == "1" ? "Closed (Kiinni)" : value == "0" ? "Open (Auki)" : "Unknown";

                return value;
            }
            catch (Exception ex)
            {
                Console.WriteLine("JSON Parse Error: " + ex.Message);
                return "Invalid Data";
            }
        }



        private void buttonStop_Click(object sender, EventArgs e)
        {
            buttonStop.Enabled = false;
            buttonStart.Enabled = true;
            cancellationTokenSource?.Cancel();
        }

        private void ListViewFormExample_Load(object sender, EventArgs e)
        {
            buttonStart.Enabled = true;
            buttonStop.Enabled = false;
        }

        private void ListViewFormExample_FormClosing(object sender, FormClosingEventArgs e)
        {
            cancellationTokenSource?.Cancel();
        }
    }
}
