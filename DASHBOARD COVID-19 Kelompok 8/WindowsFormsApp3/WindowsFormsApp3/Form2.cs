using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;
using System.Data.OleDb;

namespace WindowsFormsApp3
{
    public partial class Form2 : Form
    {
        // deklarasi variabel dan db access
        OleDbConnection koneksi;
        OleDbCommand oleDbCmd = new OleDbCommand();
        String connParam = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=E:\Dashboard Covid-19\DASHBOARD COVID-19 new\WindowsFormsApp3\db\db.mdb;Persist Security Info=False";

        public Form2()
        {
            InitializeComponent();

            Func<ChartPoint, string> labelPoint = chartPoint =>
               string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            // prepare variabel
            int mati = 0;
            int selamat = 0;
            int dirawat = 0;
            int total = 0;

            // load data dari database 
            using (OleDbConnection connection = new OleDbConnection(connParam))
            {
                // buat query dan koneksikan ke db
                OleDbCommand command = new OleDbCommand("select * from covid", connection);
                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();

                // fetch data
                while (reader.Read())
                {
                    selamat = reader.GetInt32(2);
                    mati = reader.GetInt32(3);
                    dirawat = reader.GetInt32(4);
                    total = reader.GetInt32(5);
                }
                // always call Close when done reading.
                reader.Close();
            }

            // inject data ke chart
            pieChart2.Series = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Sembuh",
                    Values = new ChartValues<double> {selamat},
                    PushOut = 15,
                    DataLabels = true,
                    LabelPoint = labelPoint
                },
                new PieSeries
                {
                    Title = "Dalam masa Perawatan",
                    Values = new ChartValues<double> {dirawat},
                    DataLabels = true,
                    LabelPoint = labelPoint
                },
                new PieSeries
                {
                    Title = "Meninggal",
                    Values = new ChartValues<double> {mati},
                    DataLabels = true,
                    LabelPoint = labelPoint
                }
            };

            pieChart2.LegendLocation = LegendLocation.Bottom;


            cartesianChart1.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Sembuh",
                    Values = new ChartValues<double> { 231, 285, 108, 218 }
                }
            };

            //adding series will update and animate the chart automatically
            cartesianChart1.Series.Add(new ColumnSeries
            {
                Title = "Positif",
                Values = new ChartValues<double> { 568, 490, 529, 489 }
            });

            cartesianChart1.Series.Add(new ColumnSeries
            {
                Title = "Meningal ",
                Values = new ChartValues<double> { 15, 33, 13, 59 }
            });

            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Tanggal",
                Labels = new[] { "14 Mei", "15 Mei", "16 Mei", "17 Mei" }
            });

            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Jumlah",
                LabelFormatter = value => value.ToString("N")
            });

        }

    


    private void pieChart2_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {
            
        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }

        private void cartesianChart1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }
    }
}
