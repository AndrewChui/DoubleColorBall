using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoubleColorBall
{
    public partial class Form1 : Form
    {
        private DataImport dataImport;
        private List<DCBall> dcBallList = new List<DCBall>();
        public Analysis analysis = new Analysis();
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "xlsx | *.xlsx";
            if(openFile.ShowDialog()==DialogResult.OK)
            {
                dataImport = new DoubleColorBall.DataImport(openFile.FileName);
                dcBallList = dataImport.ReadMyExcel();
                dataImport.Close();
            }
        }

        private void buttonAnslysis_Click(object sender, EventArgs e)
        {
            //总数
            analysis.Total = dcBallList.Count;
            foreach(var dc in dcBallList)
            {
                foreach(var r in dc.RedNum)
                {
                    analysis.RedNumCount[r - 1]++;
                }
                analysis.BlueNumCount[dc.BlueNum-1]++;
            }
            for(int i=0;i<33;i++)
            {
                analysis.RedRate[i] = analysis.RedNumCount[i] / (double)analysis.Total;
                listBox1.Items.Add(analysis.RedNumCount[i].ToString() + "\t" + (i + 1).ToString());
            }
            for(int i=0;i<16;i++)
            {
                analysis.BlueRate[i] = analysis.BlueNumCount[i] / (double)analysis.Total;
                listBox2.Items.Add(analysis.BlueNumCount[i] + "\t" + (i + 1).ToString());
            }
        }
    }

    public class Analysis
    {
        public int[] BlueNumCount { get; set; }
        public int Total { get; set; }
        public int[] RedNumCount { get; set; }
        public double[] BlueRate { get; set; }
        public double[] RedRate { get; set; }

        public Analysis()
        {
            BlueNumCount = new int[16];
            BlueRate = new double[16];
            RedNumCount = new int[33];
            RedRate = new double[33];
        }
    }
}
