using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Windows.Input;
using System.Collections.ObjectModel;
using SalesEntry.Models;
using pf.Models;
using System.Windows;
using System.Windows.Controls;
using System.Printing;
using System.Windows.Documents;
using System.Windows.Markup;
using SalesEntry.Views;

namespace SalesEntry.ViewModels
{
    public class SalesEntryViewModel : BindableBase
    {
        //private DataTable _salesHeader = new DataTable();
        private Models.SalesEntryModel _salesEntryModel = new Models.SalesEntryModel();
        private int _slipNo = 0;
        private DateTime _slipDate;
        private string _customerCode;
        private string _customerName;
        private int _salesTaxRateId;
        private string _zipCode;
        private string _address;
        private string _tel;
        private string _messageString;
        private int _subtotal;
        private int _salesTax;
        private int _total;
        private ObservableCollection<SalesDetail> _salesDetails = new ObservableCollection<SalesDetail>();


        public SalesEntryViewModel()
        {
            SlipSearchCommand = new DelegateCommand<TextBox>(SlipSearchCommandExecute);
            CustomerSearchCommand = new DelegateCommand<TextBox>(CustomerSearchCommandExecute);
            PrevSlipCommand = new DelegateCommand(PrevSlipCommandExecute);
            NextSlipCommand = new DelegateCommand(NextSlipCommandExecute);
            PrintCommand = new DelegateCommand(PrintCommandExecute);
            RegistCommand = new DelegateCommand(RegistCommandExecute);
            DeleteCommand = new DelegateCommand(DeleteCommandExecute);
            CancelCommand = new DelegateCommand(CancelCommandExecute);

            MaxLineNum = 5;

            // 初期値表示
            InitScreen();


        }

        public DataTable SalesTaxRates
        {
            get
            {
                var tr = new pf.Models.SalesTaxRate();
                return tr.TaxRatesList;
            }
        }

        public int SlipNo
        {
            get { return _slipNo; }
            set { SetProperty(ref _slipNo, value); }
        }
        public DateTime SlipDate
        {
            get { return _slipDate; }
            set { SetProperty(ref _slipDate, value); }
        }
        public string CustomerCode
        {
            get { return _customerCode; }
            set { SetProperty(ref _customerCode, value); }
        }
        public string CustomerName
        {
            get { return _customerName; }
            set { SetProperty(ref _customerName, value); }
        }
        public int SalesTaxRateId
        {
            get { return _salesTaxRateId; }
            set { SetProperty(ref _salesTaxRateId, value); }
        }
        public string ZipCode
        {
            get { return _zipCode; }
            set { SetProperty(ref _zipCode, value); }
        }
        public string Address
        {
            get { return _address; }
            set { SetProperty(ref _address, value); }
        }
        public string Tel
        {
            get { return _tel; }
            set { SetProperty(ref _tel, value); }
        }
        public string MessageString
        {
            get { return _messageString; }
            set { SetProperty(ref _messageString, value); }
        }


        public int Subtotal
        {
            get => SalesDetails.Sum(x => x.Price);
        }

        public int SalesTax
        {
            get { return _salesTax; }
            set { SetProperty(ref _salesTax, value); }
        }
        public int Total
        {
            get => SalesDetails.Sum(x => x.Price);
        }

        public ObservableCollection<SalesDetail> SalesDetails
        {
            get { return _salesDetails; }
            set
            {
                SetProperty(ref _salesDetails, value);
            }
        }

        public int MaxLineNum { get; set; }


        public DelegateCommand<TextBox> SlipSearchCommand { get; }
        public DelegateCommand<TextBox> CustomerSearchCommand { get; }
        public DelegateCommand<DataGrid> ProductSearchCommand { get; }
        public DelegateCommand NextSlipCommand { get; }
        public DelegateCommand PrevSlipCommand { get; }
        public DelegateCommand PrintCommand { get; }
        public DelegateCommand RegistCommand { get; }
        public DelegateCommand DeleteCommand { get; }
        public DelegateCommand CancelCommand { get; }


        private void SlipSearchCommandExecute(TextBox slipNo)
        {
            if (slipNo.Text == "")
            {

            }
            else
            {
                CallSlip(int.Parse(slipNo.Text));
            }
        }
        private void CustomerSearchCommandExecute(TextBox customerCode)
        {
            MessageString = "";
            if (customerCode.Text == "")
            {
                // 空欄だったら検索ウインドウ表示

            }
            else
            {
                // コードが入力されていたらマスタを検索

                var cf = new CommonFunctions();
                var ci = cf.CustomerCodeToId(customerCode.Text);
                if (ci == -1)
                {
                    MessageString = "得意先が見つかりません";
                    return;
                }
                var c = new Customer(ci);

                CustomerName = c.CustomerName;
                Address = c.Address;
                Tel = c.Tel;

                //UIElement element =  as UIElement;
                customerCode.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));


            }
        }

        private void PrevSlipCommandExecute()
        {
            // 前伝票
            try
            {
                var dc = new DatabaseController();
                if (SlipNo == 0)
                {
                    dc.SQL = "SELECT "
                            + "  slip_no "
                            + " FROM "
                            + "   sales_header "
                            + " WHERE "
                            + "   state = 0 "
                            + " ORDER BY "
                            + "   slip_no DESC "
                            + " LIMIT 1";
                }
                else
                {
                    dc.SQL = "SELECT "
                            + "  slip_no "
                            + " FROM "
                            + "   sales_header "
                            + " WHERE "
                            + "   state = 0 "
                            + "   AND slip_no < " + SlipNo.ToString()
                            + " ORDER BY "
                            + "   slip_no DESC "
                            + " LIMIT 1";

                }

                IDataReader dr = dc.ReadAsDataReader();
                if (dr != null)
                {
                    dr.Read();
                    CallSlip((int)dr[0]);
                }

            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

        }

        private void NextSlipCommandExecute()
        {
            // 次伝票
            try
            {
                var dc = new DatabaseController();
                if (SlipNo == 0)
                {
                    return;
                }
                dc.SQL = "SELECT "
                        + "  slip_no "
                        + " FROM "
                        + "   sales_header "
                        + " WHERE "
                        + "   state = 0 "
                        + "   AND slip_no > " + SlipNo.ToString()
                        + " ORDER BY "
                        + "   slip_no "
                        + " LIMIT 1";

                IDataReader dr = dc.ReadAsDataReader();
                if (dr != null)
                {
                    dr.Read();
                    CallSlip((int)dr[0]);
                }

            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }

        private void PrintCommandExecute()
        {
            // 印刷
            try
            {
                SalesSlipViewModel ssvm = new SalesSlipViewModel();

                FixedDocument fixedDocument = new FixedDocument();

                SalesSlip page = new SalesSlip();
                page.DataContext = ssvm;
                FixedPage fixedPage = new FixedPage();
                fixedPage.Children.Add(page); //UserControlを追加
                PageContent pc = new PageContent();
                ((IAddChild)pc).AddChild(fixedPage);
                fixedDocument.Pages.Add(pc);

                var printServer = new LocalPrintServer();
                var queue = printServer.DefaultPrintQueue;
                var writer = PrintQueue.CreateXpsDocumentWriter(queue);
                writer.Write(fixedDocument);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "通知");
            }

        }

        private void RegistCommandExecute()
        {
            // 登録
            int slipNo = 0;
            slipNo = _salesEntryModel.RegistSalesHeader(SlipNo, SlipDate, CustomerCode, "0", "2");
            slipNo = _salesEntryModel.RegistSalesDetail(slipNo, _salesDetails, "2");
            SlipNo = slipNo;
            InitScreen();
        }

        private void DeleteCommandExecute()
        {
            // 削除
            _salesEntryModel.DeleteSales(SlipNo, "2");
            InitScreen();
        }
        private void CancelCommandExecute()
        {
            // キャンセル
            InitScreen();
        }
        private void InitScreen()
        {
            SlipNo = 0;
            SlipDate = DateTime.Today;
            CustomerCode = "";
            CustomerName = "";
            SalesTaxRateId = 0;
            ZipCode = "";
            Address = "";
            Tel = "";
            MessageString = "";

            // 明細データ作成
            SalesDetails.Clear();
            var list = new ObservableCollection<SalesDetail>();
            for (var i = 0; i < MaxLineNum; i++)
            {
                list.Add(new SalesDetail(i, () => RaisePropertyChanged(nameof(Subtotal))));
            }
            SalesDetails = list;

        }
        private void CallSlip(int slipNo)
        {
            var dc = new DatabaseController();
            dc.SQL = "SELECT "
                    + "  H.id h_id  "
                    + "  , H.slip_no "
                    + "  , H.slip_date "
                    + "  , H.customer_id "
                    + "  , H.staff_id "
                    + "  , D.id d_id "
                    + "  , D.line_no "
                    + "  , D.product_id "
                    + "  , D.qty "
                    + "  , D.catalog_price "
                    + "  , D.unit_price "
                    + "  , D.unit_cost "
                    + "  , D.line_sales_tax_price "
                    + "  , D.line_include_sales_tax_price "
                    + "  , customers.customer_code "
                    + "  , customers.customer_name "
                    + "  , customers.address1 "
                    + "  , customers.address2 "
                    + "  , products.product_code "
                    + "  , products.product_name "
                    + "FROM "
                    + "  sales_header H "
                    + "  LEFT JOIN sales_detail D "
                    + "    ON H.slip_no = D.slip_no "
                    + "    AND D.state = 0 "
                    + "  LEFT JOIN customers  "
                    + "    ON H.customer_id = customers.id "
                    + "  LEFT JOIN products  "
                    + "    ON D.product_id = products.id "
                    + "WHERE "
                    + "  H.state = 0 "
                    + "  AND H.slip_no = " + slipNo + " "
                    + "ORDER BY "
                    + "  line_no"
                    ;

            DataTable salesDataTable = dc.ReadAsDataTable();

            // HEADER
            SlipNo = (int)salesDataTable.Rows[0]["slip_no"];
            SlipDate = (DateTime)salesDataTable.Rows[0]["slip_date"];
            CustomerCode = salesDataTable.Rows[0]["customer_code"].ToString();
            CustomerName = salesDataTable.Rows[0]["customer_name"].ToString();
            SalesTaxRateId = 0;
            ZipCode = "";
            Address = salesDataTable.Rows[0]["address1"].ToString() + salesDataTable.Rows[0]["address2"].ToString();
            Tel = "";

            // DETAIL            
            SalesDetails.Clear();
            foreach (DataRow dr in salesDataTable.Rows)
            {
                var sd = new SalesDetail((int)dr["line_no"], () => RaisePropertyChanged(nameof(Subtotal)));
                sd.ProductCode = dr["product_code"].ToString();
                sd.ProductName = dr["product_name"].ToString();
                sd.Qty = (int)dr["qty"];
                sd.UnitCost = (int)dr["unit_cost"];
                sd.UnitPrice = (int)dr["unit_price"];

                SalesDetails.Add(sd);

            }

        }

    }

}
