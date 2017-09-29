using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.ComponentModel;

namespace DoubleColorBall
{
    public class DCBall
    {
        public DateTime Datetime { get; set; }
        public int Sequese { get; set; }
        public int BlueNum { get; set; }
        public int[] RedNum { get; set; }
        public DCBall()
        {
            RedNum = new int[6];
        }
    }
    public class DataImport
    {
        private string fileName;
        private List<DCBall> dcBall = new List<DCBall>();
        private  Excel.Workbook MyBook = null;
        private  Excel.Application MyApp = null;
        private  Excel.Worksheet MySheet = null;
        private  int lastRow = 0;

        public DataImport(string fn)
        {
            fileName = fn;
            InitializeExcel();
        }
        public  void InitializeExcel()
        {
            MyApp = new Excel.Application();
            MyApp.Visible = false;
            MyBook = MyApp.Workbooks.Open(fileName);
            MySheet = (Excel.Worksheet)MyBook.Sheets[1]; // Explict cast is not required here
            lastRow = MySheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell).Row;
        }
        public  List<DCBall> ReadMyExcel()
        {
            dcBall.Clear();
            for (int index = 1; index <= lastRow; index++)
            {
                var MyValues = (System.Array)MySheet.get_Range("B" + index.ToString(), "J" + index.ToString()).Cells.Value;
                int[] redNum = new int[6];
                for(int i=0;i<6;i++)
                {
                    redNum[i] = Convert.ToInt32(MyValues.GetValue(1, 3 + i));
                }
                dcBall.Add(new DCBall
                {
                    Sequese = Convert.ToInt32(MyValues.GetValue(1, 1)),
                    Datetime = Convert.ToDateTime(MyValues.GetValue(1, 2).ToString()),
                    RedNum = redNum,
                    BlueNum = Convert.ToInt32(MyValues.GetValue(1, 9))
                });
            }
            return dcBall;
        }

        public void Close()
        {
            MyBook.Saved = true;
            MyApp.Quit();
        }

    }
}
