using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeamViewerId
{
    public partial class Form1 : Form
    {
        Timer tmrBottomReset;
        public Form1()
        {
            InitializeComponent();
            tssBottom.Text = "";
            dgvResult.AutoGenerateColumns = false;
            tmrBottomReset = new Timer();
            tmrBottomReset.Interval = 5000;
            tmrBottomReset.Tick += TmrBottomReset_Tick;
        }

        private void TmrBottomReset_Tick(object sender, EventArgs e)
        {
            tssBottom.Text = "";
            tmrBottomReset.Stop();
        }

        private void SetMessage(string msg)
        {
            tmrBottomReset.Stop();
            tssBottom.Text = msg;
            tmrBottomReset.Start();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            DataAccessLayer dal = new DataAccessLayer();
            dal.AddParameter("@SearchTerm", txtSearch.Text, DbType.String);
            DataTable dt = dal.ExecuteStoredProcedure("GetTeamViewerId");
            dal.ClearParameters();

            List<TeamViewerInfo> list = new List<TeamViewerInfo>();
            foreach (DataRow row in dt.Rows)
                list.Add(new TeamViewerInfo { SerialNumber = row["SerialNumber"].ToString(), TimeStamp = (DateTime)row["TimeStamp"], TeamViewerId = row["TeamViewerId"].ToString() });

            dgvResult.DataSource = list;
        }

        private void dgvResult_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                e.SuppressKeyPress = true;
                Copy();
            }
        }

        private void dgvResult_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvResult.SelectedRows.Count == 1)
                Copy();
        }

        private void Copy()
        {
            TeamViewerInfo teamviewer = (TeamViewerInfo)dgvResult.SelectedRows[0].DataBoundItem;
            if (teamviewer.HasId)
            {
                Clipboard.SetData(DataFormats.Text, teamviewer.TeamViewerId);
                SetMessage($"{teamviewer.TeamViewerId} - Kopieret til clipboard");
            }
        }
    }

    class TeamViewerInfo
    {
        public string SerialNumber { get; set; }
        public DateTime TimeStamp { get; set; }
        public string TeamViewerId { get; set; }
        public bool HasId { get { return float.TryParse(TeamViewerId, out float f); } }
    }
    public class DataAccessLayer
    {
        private SqlConnection Conn;
        private List<SqlParameter> Parameters;

        public void AddParameter(string name, object value, DbType type)
        {
            SqlParameter p = new SqlParameter(name, type);
            p.Value = value;
            Parameters.Add(p);
        }
        public void ClearParameters()
        {
            Parameters.Clear();
        }
        public DataAccessLayer()
        {
            string connectionstring = @"Data Source=TRR-SRV1\SQLEXPRESS;Initial Catalog=DLDB;User Id=XXX;Password=XXXX;";
            Conn = new SqlConnection(connectionstring);
            Parameters = new List<SqlParameter>();
        }
        public DataTable ExecuteStoredProcedure(string SQL)
        {
            DataTable dt;

            using (SqlCommand Comm = Conn.CreateCommand())
            {
                Comm.CommandText = SQL;
                Comm.CommandType = CommandType.StoredProcedure;

                if (Parameters.Count > 0)
                    Comm.Parameters.AddRange(Parameters.ToArray());

                SqlDataAdapter da = new SqlDataAdapter(Comm);
                dt = new DataTable();
                da.Fill(dt);
            }

            return dt;
        }
    }
}
