using System;
using Newtonsoft.Json;
using System.Drawing.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Windows.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Identity.Client;

namespace streamingfromSignalR
{
    public partial class Form1 : Form
    {
        HubConnection connection;
        public Form1()
        {
            InitializeComponent();
            InitializeSignalR();
            
        }
        private async void InitializeSignalR()
        {
            connection = new HubConnectionBuilder()
                    .WithUrl("https://localhost:7224/test")
                    .WithAutomaticReconnect()
                    .Build();
            connection.On<ProductClass>("ReceiveData", (message) => {
                UpdateDataGrid(message);
            });
            try
            {
                await connection.StartAsync();
                MessageBox.Show("Connected to SignalR Hub!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to connect: {ex.Message}");
            }
        }
        private void UpdateDataGrid(ProductClass data)
        {
            // Ensure updates are done on the UI thread
            if (InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateDataGrid(data)));
            }
            else
            {
                // Bind data to the DataGridView
                dataGridView1.DataSource = data;
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
        }
    }
}
