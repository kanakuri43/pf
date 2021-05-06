using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesEntry.ViewModels
{
    public class SalesEntryViewModel : BindableBase
    {
        public SalesEntryViewModel()
        {
            PrintCommand = new DelegateCommand(PrintCommandExecute);
        }
        public DelegateCommand PrintCommand { get; }

        private void PrintCommandExecute()
        {
            // ボタンを押したときの処理

        }

    }

}
