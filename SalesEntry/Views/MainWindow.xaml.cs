using System;
using System.Windows;
using System.Data;
using MahApps.Metro.Controls;

namespace SalesEntry.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private DataTable CreateDataTable()
        {
            var dt = new DataTable();
            dt.Columns.Add("日付", typeof(DateTime));
            dt.Columns.Add("メーカー");
            dt.Columns.Add("商品コード");
            dt.Columns.Add("商品名");
            dt.Columns.Add("センサー");
            dt.Columns.Add("画素数");
            dt.Columns.Add("サイズ");
            dt.Columns.Add("単価", typeof(int));
            dt.Columns.Add("数量", typeof(int));
            dt.Columns.Add("合計", typeof(int));

            dt.Rows.Add(dt.NewRow().ItemArray = new object[] { "2020/10/11", "キャノン", "4147C001", "EOS R6000", "Full", "2400", "15x20x80", 150000, 3, 450000 });
            dt.Rows.Add(dt.NewRow().ItemArray = new object[] { "2020/10/12", "キャノン", "4147C012", "EOS R5000", "Full", "3000", "20x15x60", 160000, 6, 960000 });
            dt.Rows.Add(dt.NewRow().ItemArray = new object[] { "2020/11/15", "ニコン", "4960759127693", "COOLPIX S9999", "1/2.3型", "2400", "16x10x45", 20000, 1, 20000 });
            dt.Rows.Add(dt.NewRow().ItemArray = new object[] { "2020/11/23", "ニコン", "4960759127716", "COOLPIX S6666", "1/2.3型", "2400", "10x12x40", 20000, 4, 80000 });
            dt.Rows.Add(dt.NewRow().ItemArray = new object[] { "2020/11/09", "SONY", "S26347C001", "Cyber-shotHX444V", "1/2.3型", "2400", "20x15x30", 123000, 8, 984000 });
            dt.Rows.Add(dt.NewRow().ItemArray = new object[] { "2020/12/01", "SONY", "S30447C004", "Cyber-shotWX777", "1型", "2400", "20x15x30", 45000, 12, 540000 });
            dt.Rows.Add(dt.NewRow().ItemArray = new object[] { "2020/12/11", "富士フィルム", "FJ55505", "FUJIFILM X-S111", "Full", "2400", "18x20x30", 120000, 3, 360000 });

            return dt;
        }

    }
}
