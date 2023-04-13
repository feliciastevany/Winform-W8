using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Winform_W8
{
    public partial class Form1 : Form
    {
        MySqlConnection mysqlConnect;
        MySqlCommand mysqlCommand; //code yg ada di mysql (select*form)
        MySqlDataAdapter mySqlAdapter; // menerima hasil dari query// hasil dari selection
        string connectionString;
        string sqlQuery;
        DataTable dataPemain = new DataTable();
        DataTable dataNationality = new DataTable();
        DataTable dataTeamname = new DataTable();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connectionString = "server=localhost;uid=root;pwd=;database=premier_league";
            mysqlConnect = new MySqlConnection(connectionString);
            // mysqlConnect.Open(); // hanya digunakan ketika menggunakan DML (merubah data)

            sqlQuery = "SELECT player_id as 'Player ID', player_name as 'Player Name', height as 'Player Height', team_name as 'Team name'\r\nFROM player INNER JOIN team\r\nON player.team_id = team.team_id\r\n;";
            mysqlCommand = new MySqlCommand(sqlQuery, mysqlConnect);
            mySqlAdapter = new MySqlDataAdapter(mysqlCommand);
            mySqlAdapter.Fill(dataPemain);
            dgvPemain.DataSource = dataPemain;

            sqlQuery = "SELECT nationality_id as 'nationality ID', nation as 'nation' FROM nationality n";
            mysqlCommand = new MySqlCommand(sqlQuery, mysqlConnect);
            mySqlAdapter = new MySqlDataAdapter(mysqlCommand);
            mySqlAdapter.Fill(dataNationality);
            comboBox1.DataSource = dataNationality;
            comboBox1.ValueMember = "nationality ID";
            comboBox1.DisplayMember = "nation";

            sqlQuery = "SELECT team_name as 'Team Name' FROM team";
            mysqlCommand = new MySqlCommand(sqlQuery, mysqlConnect);
            mySqlAdapter = new MySqlDataAdapter(mysqlCommand);
            mySqlAdapter.Fill(dataTeamname);
            comboBox2.DataSource = dataTeamname;
            comboBox2.DisplayMember = "Team Name";
            comboBox2.ValueMember = "Team Name";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelValue.Text = comboBox1.SelectedValue.ToString();

            //dgvPemain = new DataTable();

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataTeamname = new DataTable();
            sqlQuery = "SELECT player_id as 'Player ID', player_name as 'Player Name', height as 'Player Height', team_name as 'Team name'\r\nFROM player INNER JOIN team\r\nON player.team_id = team.team_id\r\nWHERE team_name = '" + comboBox2.SelectedValue.ToString() + "';";
            mysqlCommand = new MySqlCommand(sqlQuery, mysqlConnect);
            mySqlAdapter = new MySqlDataAdapter(mysqlCommand);
            mySqlAdapter.Fill(dataTeamname);
            dgvPemain.DataSource = dataTeamname;
        }
    }
}
