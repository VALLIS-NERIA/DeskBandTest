using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using CSDeskBand;
using CSDeskBand.Annotations;
using CSDeskBand.Wpf;


namespace DiceDeskBand {
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    [ComVisible(true)]
    [Guid("89BF6B36-A0B0-4C95-A666-87A55C226986")]
    [CSDeskBandRegistration(Name = "Sample WPF Deskband")]
    public partial class UserControl1 : INotifyPropertyChanged {
        protected int LastResult { get; set; } = 0;
        protected int DiceShape { get; set; } = 6;
        private Random rnd = new Random();
        public UserControl1() {
            InitializeComponent();
        }
        private void Roll(object sender, RoutedEventArgs e) { this.LastResult = this.rnd.Next(this.DiceShape)+1; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }
    }
}
