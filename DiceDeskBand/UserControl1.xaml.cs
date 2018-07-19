using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using CSDeskBand;
using CSDeskBand.Annotations;
using System.Linq;

namespace DiceDeskBand {
    using System.Windows.Data;

    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    [ComVisible(true)]
    [Guid("66c84c57-b922-479f-889e-8b3fd3205911")]
    [CSDeskBandRegistration(Name = "Dice Deskband")]
    public partial class UserControl1 : INotifyPropertyChanged {
        private Orientation _taskbarOrientation;
        private int _taskbarWidth;
        private int _taskbarHeight;
        private Edge _taskbarEdge;

        public Orientation TaskbarOrientation {
            get => _taskbarOrientation;
            set {
                if (value == _taskbarOrientation) return;
                _taskbarOrientation = value;
                OnPropertyChanged();
            }
        }

        public int TaskbarWidth {
            get => _taskbarWidth;
            set {
                if (value == _taskbarWidth) return;
                _taskbarWidth = value;
                OnPropertyChanged();
            }
        }

        public int TaskbarHeight {
            get => _taskbarHeight;
            set {
                if (value == _taskbarHeight) return;
                _taskbarHeight = value;
                OnPropertyChanged();
            }
        }

        public Edge TaskbarEdge {
            get => _taskbarEdge;
            set {
                if (value == _taskbarEdge) return;
                _taskbarEdge = value;
                OnPropertyChanged();
            }
        }

        public int LastResult {
            get => this.lastResult;
            set {
                this.lastResult = value;
                OnPropertyChanged();
            }
        }


        private int lastResult = 0;

        public UserControl1() {
            InitializeComponent();
            this.Options.MinHorizontal.Width = 150;
            var dices = Settings.LoadDices();
            foreach (var info in dices) {
                this.DicePanel.Children.Add(
                    new DiceButton(info)
                    {
                        Content = new Binding
                        {
                            RelativeSource = RelativeSource.Self,
                            Path = new PropertyPath("Description")
                        }
                    });
            }
        }

        private void AddMoreDiceButton(DiceInfo info) {
            this.DicePanel.Children.Add(
                new DiceButton(info)
                {
                    Content = new Binding
                    {
                        RelativeSource = RelativeSource.Self,
                        Path = new PropertyPath("Description")
                    }
                });
            Settings.SaveDices(info);
        }

        private void Roll(object sender, RoutedEventArgs e) {
            if (sender is DiceButton b) {
                this.LastResult = b.Roll();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }
    }
}
