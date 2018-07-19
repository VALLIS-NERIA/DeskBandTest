using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace DiceDeskBand {
    public struct DiceInfo {
        public int DiceCount;
        public int DiceDimension;
    }
    internal static class Settings {
        public static void SaveDices(params DiceInfo[] dices) {
            
            var bandReg = Registry.ClassesRoot.OpenSubKey("CLSID\\{66c84c57-b922-479f-889e-8b3fd3205911}", true);

            var diceKey = bandReg.OpenSubKey("Dices", true) ?? bandReg.CreateSubKey("Dices", true);

            var storedKey = diceKey.GetValue("Stored");
            if (storedKey == null) {
                diceKey.SetValue("Stored", new string[0], RegistryValueKind.MultiString);
                storedKey = diceKey.GetValue("Stored");
            }

            var stored = (storedKey is string[] strings) ? new HashSet<string>(strings) : null;
            if (stored == null) {
                throw new InvalidCastException("The value named \"Stored\" in HKEY_CLASSES_ROOT\\CLSID\\{66c84c57-b922-479f-889e-8b3fd3205911} is not MultiString!");
            }

            var list = new List<string>(stored);
            foreach (DiceInfo dice in dices) {
                string value = $"{dice.DiceCount}D{dice.DiceDimension}";
                if (!stored.Contains(value)) {
                    list.Add(value);
                }
            }


            diceKey.SetValue("Stored",list.ToArray(), RegistryValueKind.MultiString);
        }

        public static IEnumerable<DiceInfo> LoadDices() {
            var bandReg = Registry.ClassesRoot.OpenSubKey("CLSID\\{66c84c57-b922-479f-889e-8b3fd3205911}", true);

            var diceKey = bandReg.OpenSubKey("Dices", true) ?? bandReg.CreateSubKey("Dices", true);

            var storedKey = diceKey.GetValue("Stored");
            if (storedKey == null) {
                diceKey.SetValue("Stored", new string[0], RegistryValueKind.MultiString);
                storedKey = diceKey.GetValue("Stored");
            }

            var stored = storedKey as string[];
            if (stored == null) {
                throw new InvalidCastException("The value named \"Stored\" in HKEY_CLASSES_ROOT\\CLSID\\{66c84c57-b922-479f-889e-8b3fd3205911} is not MultiString!");
            }

            var list = new List<DiceInfo>();
            foreach (var diceStr in stored) {
                var sp = diceStr.Split('D');
                try {
                    int x = int.Parse(sp[0]);
                    int d = int.Parse(sp[1]);
                    list.Add(new DiceInfo { DiceCount = x, DiceDimension = d });
                }
                catch (ArgumentOutOfRangeException) { }
                catch (FormatException) { }
            }

            return list;
        }
    }



}
