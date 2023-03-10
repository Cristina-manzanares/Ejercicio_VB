using System.Net;

namespace Ejercicio_VB
{
    public partial class Form1 : Form
    {
        public string ds;
        public Form1(string ip, IPAddress gateway, string username, string hostname, string ssid, string ssid_status, string mac, string int_conn, string vb, string vb_vers)
        {
            InitializeComponent();
            lbl_ip.Text = ip;
            lbl_gateway.Text = gateway.ToString();
            lbl_username.Text = username;
            lbl_hostname.Text = hostname;
            lbl_ssid.Text = ssid;
            lbl_ssid_status.Text = ssid_status;
            lbl_mac.Text = mac;
            lbl_int_conn.Text = int_conn;
            lbl_vb.Text = vb;
            lbl_vb_vers.Text = vb_vers;
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        //private void ip_Click(object sender, EventArgs e)
        //{
        //}



    }
}