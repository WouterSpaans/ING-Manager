namespace AccountBrowser
{
    using System;
    using System.Data;
    using System.Windows.Forms;

    using ING_Manager.Data;

    public partial class AccountBrowser : Form
    {
        public AccountBrowser()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.DataSource = Xml.Read().Account;
            listBox1.DisplayMember = "ID";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var accountRow = (ING_Manager.Data.DataSet.AccountRow)((DataRowView)listBox1.SelectedItem).Row;
            dataGridView1.DataSource = accountRow.GetTransactionRows();
        }
    }
}
