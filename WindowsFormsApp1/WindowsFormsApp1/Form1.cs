using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        ServiceEntities db;
        public Form1()
        {
            db = new ServiceEntities();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowClients();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            string name = ClientNameTextBox.Text;
            string telephone = ClientTelephoneTextBox.Text;
            string email = ClientEmailTextBox.Text;
            var client = new Clients
            {
                Id = db.Clients.ToList().Count+1,
                Name = name,
                Telephone = telephone,
                Email = email,

            };
            ShowClients();
            db.Clients.Add(client);
            db.SaveChanges();
        }
        private void ShowClients()
        {
            dataGridView1.DataSource = db.Clients
    .Select(c => new { c.Id, c.Name, c.Telephone, c.Email })
    .ToList();
        }
    }
}
