using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CustomControls
{
    public class DataGridEx : Control
    {
        static DataGridEx()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DataGridEx), new FrameworkPropertyMetadata(typeof(DataGridEx)));
        }
        public DataGridEx()
        {
        }
    }
}
