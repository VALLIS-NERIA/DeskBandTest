using System;
using System.Collections.Generic;
using System.Linq;
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

namespace DiceDeskBand
{
    /// <summary>
    /// Interaction logic for DiceButton.xaml
    /// </summary>
    public sealed partial class DiceButton : Button
    {
        private static Random _rnd = new Random();
        private int _diceCount = 1;
        private int _diceD = 6;
        private string _description = null;

        public int DiceD {
            get => this._diceD;
            set {
                this._diceD = value;
                this._description = $"{this.DiceCount}D{this.DiceD}";
            }
        }

        public int DiceCount {
            get => this._diceCount;
            set {
                this._diceCount = value;
                this._description = $"{this.DiceCount}D{this.DiceD}";
            }
        }

        public string Description => this._description?? $"{this.DiceCount}D{this.DiceD}";

        public DiceButton()
        {
            InitializeComponent();
        }

        public DiceButton(int diceCount, int diceDimension) : this() {
            this.DiceCount = diceCount;
            this.DiceD = diceDimension;
        }

        public int Roll() {
            int sum = 0;
            for (int i = 0; i < this.DiceCount; i++) {
                sum += _rnd.Next(this.DiceD) + 1;
            }

            return sum;
        }
    }
}
