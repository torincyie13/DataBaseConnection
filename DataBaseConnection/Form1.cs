using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DataBaseConnection
{
    public partial class frmTitles : Form
    {
        public frmTitles()
        {
            InitializeComponent();
        }
        SqlConnection booksConnection;
        SqlCommand titlesCommand;
        SqlDataAdapter titlesAdapter;
        DataTable titlesTable;
        SQLDataAdapter myAdapter;
        myAdapter = new SqlDataAdapter();
        private void frmTitles_Load(object sender, EventArgs e)
        {
            // connect to books database
            booksConnection = new SqlConnection("Data Source=.\\SQLEXPRESS;" +
                "AttachDbFilename=C:\\VCSDB\\Working\\SQLBooksDB.mdf;" +
                "Integrated Security=True; Connect Timeout=30;" +
                "User Instance=True");
            // open the connection
            booksConnection.Open();
            // establish command object
            titlesCommand = new OleDBCommand("Select * from Titles", booksConnection);
            // establish data adapter/data table
            titlesAdapter = new SqlDataAdapter();
            titlesAdapter.SelectCommand = titlesCommand;
            titlesTable = new DataTable();
            titlesAdapter.Fill(titlesTable);
            // bind controls to data table
            txtTitle.DataBindings.Add("Text", titlesTable, "Year_Published");
            txtISBN.DataBindings.Add("Text", titlesTable, "ISBN");
            txtPubID.DataBindings.Add("Text", titlesTable, "PubID");
            // establish currency manager
            titlesManager = (CurrencyManager)BindingContext[titlesTable];
            // close connection
            booksConnection.Close();
            // displays state
            lblState.Text += booksConnection.State.ToString();
            // dispose of the connection object
            booksConnection.Dispose();
            titlesCommand.Dispose();
            titlesAdapter.Dispose();
            titlesTable.Dispose();
        }
    }
}
