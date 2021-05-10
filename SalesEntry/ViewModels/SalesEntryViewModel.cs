using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Windows.Input;

namespace SalesEntry.ViewModels
{
    public class SalesEntryViewModel : BindableBase
    {

        public DataTable SalesTaxRates
        {
            get
            {
                var tr = new pf.Models.SalesTaxRate();
                //tr.TaxRatesList.AsEnumerable().Select(r => r["tax-rate"] = r["tax-rate"] + 100);

                return tr.TaxRatesList;
            }
        }

        public int SlipNo { get; set; }
        public DateTime SlipDate { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public int SalesTaxRateId { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public string Tel { get; set; }
        public string Subtotal { get; set; }
        public string SalesTax { get; set; }
        public int Total { get; set; }

        public DataTable SalesDataTable = new DataTable();

        public DelegateCommand PrintCommand { get; }
        public DelegateCommand EntryCommand { get; }
         

        public SalesEntryViewModel()
        {
            PrintCommand = new DelegateCommand(PrintCommandExecute);
            EntryCommand = new DelegateCommand(EntryCommandExecute);

            // 初期値表示
            var m = new Models.SalesEntryModel();
            m.SQL = "SELECT "
                  + "  h.id "
                  + "  , h.slip_no "
                  + "  , h.slip_date "
                  + "  , customers.customer_code "
                  + "  , customers.customer_name "
                  + "  , customers.zip_code "
                  + "  , customers.address1 +  customers.address1 address"
                  + "  , customers.tel "
                  + "  , staffs.staff_code "
                  + "  , staffs.staff_name "
                  + "FROM "
                  + "  sales_header h "
                  + "  LEFT JOIN customers "
                  + "    ON h.customer_id = customers.id "
                  + "    AND customers.state = 0 "
                  + "  LEFT JOIN staffs "
                  + "    ON h.staff_id = staffs.id "
                  + "   AND staffs.state = 0 "
                  + "  LEFT JOIN sales_detail d "
                  + "    ON h.slip_no = d.slip_no "
                  + "    AND d.state = 0 "
                  + "WHERE "
                  + "  h.state = 0 "
                  + "  AND h.slip_no = 2 ";
            SalesDataTable = m.ReadAsDataTable();

            SlipNo = (int)SalesDataTable.Rows[0]["slip_no"];
            SlipDate = (DateTime)SalesDataTable.Rows[0]["slip_date"];
            CustomerCode = SalesDataTable.Rows[0]["customer_code"].ToString();
            CustomerName = SalesDataTable.Rows[0]["customer_name"].ToString();
            SalesTaxRateId = 4;
            ZipCode = SalesDataTable.Rows[0]["zip_code"].ToString();
            Address = SalesDataTable.Rows[0]["address"].ToString();
            Tel = SalesDataTable.Rows[0]["tel"].ToString();
            Subtotal = "1,000";
            SalesTax = "100";
            Total = 1100;

        }

        private void PrintCommandExecute()
        {
            // 印刷ボタンを押したときの処理

        }
        private void EntryCommandExecute()
        {
            int slipNo = 0;
            // 登録ボタンを押したときの処理
            var m = new Models.SalesEntryModel();
            slipNo = m.RegistHeader(0, DateTime.Parse("2021/12/31"), 1, 1, 2);
            slipNo = m.RegistDetail(slipNo, 1, 1, 23, 1, 1000, 900, 500, 0, 0, 2);
            SlipNo = slipNo;
        }

    }

}
