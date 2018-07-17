using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Runtime.InteropServices;
using CSDeskBand;
using CSDeskBand.Annotations;

namespace DiceDeskBand {
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    [ComVisible(true)]
    [Guid("66c84c57-b922-479f-889e-8b3fd3205911")]
    [CSDeskBandRegistration(Name = "Dice Deskband")]
    public partial class UserControl1 : INotifyPropertyChanged {
        public int LastResult {
            get => this.lastResult;
            set { this.lastResult = value; OnPropertyChanged();}
        }

        protected int DiceShape { get; set; } = 6;
        private Random rnd = new Random();
        private int lastResult = 0;

        public UserControl1() {
            InitializeComponent();
            this.Options.MinHorizontal.Width = 450;
        }
        private void Roll(object sender, RoutedEventArgs e) { this.LastResult = this.rnd.Next(this.DiceShape)+1; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }
    }
}
