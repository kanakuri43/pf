using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesEntry.ViewModels
{
    public class SalesSlipViewModel : BindableBase
    {
        public SalesSlipViewModel()
        {
            SlipNo = 12345;
            SlipDate = DateTime.Today;
        }
        public int SlipNo { get; set; }
        public DateTime SlipDate { get; set; }
    }
}
