using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DomainLoggingView
{
    public partial class Form1 : Form
    {
        List<Preset> presets = new List<Preset>();

        public Form1()
        {
            InitializeComponent();

            presets.Add(new Preset { Name = "Select fra Log", Text = "SELECT * FROM [CompleteLog] ORDER BY [TimeStamp] DESC" });
            presets.Add(new Preset { Name = "Select fra CustomLog", Text = "SELECT * FROM [CompleteCustomLog] ORDER BY [TimeStamp] DESC" });
            presets.Add(new Preset { Name = "Find alt om MANR", Text = "SELECT * FROM [CompleteLog] WHERE [Username] = '123456'" });
            presets.Add(new Preset { Name = "Find alt om Computer", Text = "SELECT * FROM [CompleteLog] WHERE [ComputerName] = 'TRR-CZC1234'" });
            presets.Add(new Preset { Name = "Find alle logins", Text = "SELECT * FROM [CompleteLog] WHERE [Action] = 'Logon'" });
            presets.Add(new Preset { Name = "Få onlinetid af computer på dato", Text = "SELECT dbo.GetOnlineMinutes('TRR-CZC1234', '2017-01-01')" });
            presets.Add(new Preset { Name = "Få onlinetid af computere i dag", Text = "SELECT DISTINCT [ComputerName], dbo.GetOnlineMinutes([ComputerName], GETDATE()) AS [Minutes] FROM [CompleteLog] WHERE CONVERT(DATE, [TimeStamp]) = CONVERT(DATE, GETDATE())" });
            presets.Add(new Preset { Name = "Hent log for flere computere i en datoperiode", Text = 
@"SELECT
    [ComputerName], [TimeStamp], [Action]
FROM
    [CompleteLog]
WHERE 
    ([ComputerName] = 'TRR-CZC238BVQN' OR
    [ComputerName] = 'TRR-CZC23864BB' OR
    [ComputerName] = 'TRR-CZC23864BF' OR
    [ComputerName] = 'TRR-CZC23864CB' OR
    [ComputerName] = 'TRR-CZC238BVP8' OR
    [ComputerName] = 'TRR-CZC238BVPM' OR
    [ComputerName] = 'TRR-CZC23864BK' OR
    [ComputerName] = 'TRR-CZC23864JY' OR
    [ComputerName] = 'TRR-CZC23864DD' OR
    [ComputerName] = 'TRR-CZC238BVP3') AND
    Convert(date,[TimeStamp]) >= '2017-02-20'
    AND
    Convert(date,[TimeStamp]) <= '2017-02-24'" });
            presets.Add(new Preset { Name = "Hent minutter for flere computere i en uge", Text =
@"DECLARE @MandagIUgen DATE = '2017-02-20'
DECLARE @coms VARCHAR(250) = 'TRR-CZC238BVQN,TRR-CZC23864BB,TRR-CZC23864BF,TRR-CZC23864CB,TRR-CZC238BVP8,TRR-CZC238BVPM,TRR-CZC23864BK,TRR-CZC23864JY,TRR-CZC23864DD,TRR-CZC238BVP3'
DECLARE @dt1 DATE = @MandagIUgen;DECLARE @dt2 DATE = DATEADD(DAY, 1, @dt1);DECLARE @dt3 DATE = DATEADD(DAY, 1, @dt2);DECLARE @dt4 DATE = DATEADD(DAY, 1, @dt3);DECLARE @dt5 DATE = DATEADD(DAY, 1, @dt4)
SELECT DISTINCT [ComputerName], dbo.GetOnlineMinutes([ComputerName], @dt1) AS [Minutes], @dt1 AS [Date] FROM [CompleteLog] WHERE CONVERT(DATE, [TimeStamp]) = CONVERT(DATE, @dt1) AND @coms LIKE ('%' + [ComputerName] + '%')
UNION
SELECT DISTINCT [ComputerName], dbo.GetOnlineMinutes([ComputerName], @dt2) AS [Minutes], @dt2 AS [Date] FROM [CompleteLog] WHERE CONVERT(DATE, [TimeStamp]) = CONVERT(DATE, @dt2) AND @coms LIKE ('%' + [ComputerName] + '%')
UNION
SELECT DISTINCT [ComputerName], dbo.GetOnlineMinutes([ComputerName], @dt3) AS [Minutes], @dt3 AS [Date] FROM [CompleteLog] WHERE CONVERT(DATE, [TimeStamp]) = CONVERT(DATE, @dt3) AND @coms LIKE ('%' + [ComputerName] + '%')
UNION
SELECT DISTINCT [ComputerName], dbo.GetOnlineMinutes([ComputerName], @dt4) AS [Minutes], @dt4 AS [Date] FROM [CompleteLog] WHERE CONVERT(DATE, [TimeStamp]) = CONVERT(DATE, @dt4) AND @coms LIKE ('%' + [ComputerName] + '%')
UNION
SELECT DISTINCT [ComputerName], dbo.GetOnlineMinutes([ComputerName], @dt5) AS [Minutes], @dt5 AS [Date] FROM [CompleteLog] WHERE CONVERT(DATE, [TimeStamp]) = CONVERT(DATE, @dt5) AND @coms LIKE ('%' + [ComputerName] + '%')
ORDER BY [Date], [Minutes]"});

            cbbPresets.DataSource = presets;

            tssBottom.Text = "";
        }

        private void btnFillPreset_Click(object sender, EventArgs e)
        {
            txtSQL.Text = ((Preset)cbbPresets.SelectedItem).Text;
        }

        private void btnRunSQL_Click(object sender, EventArgs e)
        {
            RunCode();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                RunCode();
                e.Handled = true;
            }
        }

        private void RunCode()
        {
            DataAccessLayer dal = new DataAccessLayer();

            try
            {
                DataTable dt = dal.ExecuteDataTable(txtSQL.Text);
                dgvResult.DataSource = null;
                dgvResult.DataSource = dt;
                tssBottom.Text = $"{dt.Rows.Count} rækker hentet!";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                tssBottom.Text = "Fejl";
            }
        }
    }

    class Preset
    {
        public string Name { get; set; }
        public string Text { get; set; }
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
        public DataTable ExecuteDataTable(string SQL)
        {
            DataTable dt;

            using (SqlCommand Comm = Conn.CreateCommand())
            {
                Comm.CommandText = SQL;

                if (Parameters.Count > 0)
                    Comm.Parameters.AddRange(Parameters.ToArray());

                SqlDataAdapter da = new SqlDataAdapter(Comm);
                dt = new DataTable();
                da.Fill(dt);
            }

            return dt;
        }
        public SqlDataReader ExecuteReader(string SQL)
        {
            SqlCommand Comm = Conn.CreateCommand();
            Comm.CommandText = SQL;
            Comm.CommandType = CommandType.Text;

            if (Parameters.Count > 0)
                Comm.Parameters.AddRange(Parameters.ToArray());

            Conn.Open();

            SqlDataReader Reader = Comm.ExecuteReader(CommandBehavior.CloseConnection);

            return Reader;
        }
        public int ExecuteNonQuery(string SQL)
        {
            using (SqlCommand Comm = Conn.CreateCommand())
            {
                Comm.CommandText = SQL;

                if (Parameters.Count > 0)
                    Comm.Parameters.AddRange(Parameters.ToArray());

                Conn.Open();
                int i = Comm.ExecuteNonQuery();
                Conn.Close();

                return i;
            }
        }
        public object ExecuteScalar(string SQL)
        {
            object result;
            using (SqlCommand Comm = Conn.CreateCommand())
            {
                Comm.CommandText = SQL;
                if (Parameters.Count > 0)
                    Comm.Parameters.AddRange(Parameters.ToArray());

                Conn.Open();
                result = Comm.ExecuteScalar();
                Conn.Close();
            }
            return result;
        }
    }
}