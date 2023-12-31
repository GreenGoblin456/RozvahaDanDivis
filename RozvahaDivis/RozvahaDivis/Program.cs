﻿using System.Text;
using static RozvahaDanielDivis.Program;

namespace RozvahaDanielDivis
{
    internal class Program
    {
        public enum FinancialItemType
        {
            Asset,
            Equity,
            PL,
            Liability
        }
        public class Item
        {
            public string Nazev { get; set; }
            public decimal Hodnota { get; set; }
            public FinancialItemType Typ { get; set; }
        }

        static public decimal CZKToEURRate = 24.29M;
        static void Main(string[] args)
        {
            decimal rozvaha = 0;
            decimal vysA = 0;
            decimal vysP = 0;
            decimal vysAe = 0;
            decimal vysPe = 0;
            Item[] item = new Item[4];
            FinancialItemType financialItemType;
            for (int i = 0; i < 4; i++)
            {
                cara();
                centerText("V Hodnotě Aktivi kladně");
                centerText("V Hodnotě Pasivi záporně");
                cara();
                Console.WriteLine("Zadej název pro Item{0}", i + 1);
                string nazev = "";
                nazev = Console.ReadLine();
                while (string.IsNullOrEmpty(nazev))
                {
                    Console.WriteLine("Zadejte Platnej Název");
                    nazev = Console.ReadLine();
                }
                Console.WriteLine("Zadej hodnotu pro Item{0}", i + 1);
                string Shodn = "";
                decimal hodn;
                Shodn = Console.ReadLine();
                bool test = decimal.TryParse(Shodn, out hodn);
                while (string.IsNullOrEmpty(Shodn) || test == false)
                {
                    Console.WriteLine("Zadejte Platnou Hodnotu");
                    Shodn = Console.ReadLine();
                    test = decimal.TryParse(Shodn, out hodn);

                }                
                hodn = decimal.Parse(Shodn);
                Console.WriteLine("Zadej typ pro Item{0} (Platné typy: Asset, Equity, PL, Liability)", i + 1);
                string typ = Console.ReadLine();
                while(string.IsNullOrEmpty(typ) ||
                    !Enum.TryParse(typ, true, out financialItemType) ||
                    !Enum.IsDefined(typeof(FinancialItemType), financialItemType))
                {
                    Console.WriteLine("Zadejte Platnej Typ (Asset, Equity, PL, Liability)");
                    typ = Console.ReadLine();
                }
                item[i] = new Item { Nazev = nazev, Hodnota = hodn, Typ = type(typ) };
                rozvaha += item[i].Hodnota;
                if (item[i].Hodnota < 0)
                {
                    vysP += item[i].Hodnota;
                    vysPe += Prevod(item[i].Hodnota, item[i].Typ);
                }
                else
                { 
                    vysA += item[i].Hodnota;
                    vysAe += Prevod(item[i].Hodnota, item[i].Typ);  
                }
                Console.Clear();
            }
            cara();
            centerText("Firma A");
            centerText("Datum: 31.12.2023");

            centerText("| Aktiv           | Pasiv            |");
            centerText($"| {"Název",-16}| {"Typ",-16} | {"Hodnota",-15:C} | {"Název",-16} | {"Typ",-16} | {"Hodnota",-15} |");
            cara();
            for (int i = 0; i < 4; i++)
            {
                string Aktiv = $"|{"",-16} | {"",-16} | {"",-15:C} |";
                string Pasiv = $" {"",-16} | {"",-16} | {"",-15:C} |";
                if (item[i].Hodnota > 0)
                {
                    Aktiv = $"| {item[i].Nazev,-16}| {item[i].Typ,-16} | {item[i].Hodnota,-15:C} |";
                    if (i + 1 != 4)
                    {
                        if (item[i + 1].Hodnota < 0)
                        {
                            Pasiv = $" {item[i + 1].Nazev,-16} | {item[i + 1].Typ,-16} | {item[i + 1].Hodnota,-15:C} |";
                            i++;
                        }
                    }
                }
                else
                {
                    Pasiv = $" {item[i].Nazev,-16} | {item[i].Typ,-16} | {item[i].Hodnota,-15:C} |";
                    if (i + 1 != 4)
                    {
                        if (item[i + 1].Hodnota > 0)
                        {
                            Aktiv = $"| {item[i + 1].Nazev,-16}| {item[i + 1].Typ,-16} | {item[i + 1].Hodnota,-15:C} |";
                            i++;
                        }
                    }
                }
                centerText(Aktiv + Pasiv);
            }

            centerText("Celkem (zaokrouhleno na 3 místa)");
            centerText($"| Czk {vysA,-12}| Czk {vysP,-12} |");
            cara();

            if (rozvaha == 0)
            {
                centerText("Rozvaha je platná");
            }
            else
            {
                centerText("Rozvaha není platná");
            }
            Console.WriteLine();
            centerText("Pro verzi anglickou s Eury stiskněte Enter");
            Console.ReadLine();

            centerText("Firm A");
            centerText("Date: 12.31.2023");

            centerText("| Activum         | Passivum         |");
            centerText($"| {"Name",-16}| {"Type",-16} | {"Value",-15:C} | {"Name",-16} | {"Type",-16} | {"Value",-15} |");
            cara();
            decimal multiplier = (decimal)Math.Pow(10, 3);
            for (int i = 0; i < 4; i++)
            {
                string Aktiv = $"|{"",-16} | {"",-16} | {"",-15:C} |";
                string Pasiv = $" {"",-16} | {"",-16} | {"",-15:C} |";
                if (item[i].Hodnota > 0)
                {
                    Aktiv = $"| {item[i].Nazev,-16}| {item[i].Typ,-16} | {Math.Ceiling(Prevod(item[i].Hodnota, item[i].Typ) * multiplier) / multiplier,-12}Eur |";
                    if (i + 1 != 4)
                    {
                        if (item[i + 1].Hodnota < 0)
                        {
                            Pasiv = $" {item[i + 1].Nazev,-16} | {item[i + 1].Typ,-16} | {Math.Ceiling(Prevod(item[i + 1].Hodnota, item[i + 1].Typ) * multiplier) / multiplier - 0.001M,-12}Eur |";
                            i++;
                        }
                    }
                }
                else
                {
                    Pasiv = $" {item[i].Nazev,-16} | {item[i].Typ,-16} | {Math.Ceiling(Prevod(item[i].Hodnota, item[i].Typ) * multiplier) / multiplier - 0.001M,-12}Eur |";
                    if (i + 1 != 4)
                    {
                        if (item[i + 1].Hodnota > 0)
                        {
                            Aktiv = $"| {item[i + 1].Nazev,-16}| {item[i + 1].Typ,-16} | {Math.Ceiling(Prevod(item[i + 1].Hodnota, item[i].Typ) * multiplier) / multiplier,-12}Eur |";
                            i++;
                        }
                    }
                }
                centerText(Aktiv + Pasiv);
            }
            vysAe = Math.Ceiling(vysAe * multiplier) / multiplier;
            vysPe = Math.Ceiling(vysPe * multiplier) / multiplier - 0.001M;

            centerText("Total (rounded to 3 places)");
            centerText($"| Eur {vysAe,-12}| Eur {vysPe,-12} |");
            cara();

            if (rozvaha == 0)
            {
                centerText("Bilance is Valid");
            }
            else
            {
                centerText("Bilance is InValid");
            }
            Console.ReadLine();
        }

        public static FinancialItemType type(string typ)
        {
            switch (typ)
            {
                case "Asset":
                    return FinancialItemType.Asset;
                
                case "Equity":
                    return FinancialItemType.Equity;

                case "PL":
                    return FinancialItemType.PL;

                case "Liability":
                    return FinancialItemType.Liability;
            }
            return FinancialItemType.Asset;
        }

        public static decimal Prevod(decimal czk, FinancialItemType type)
        {
            switch (type)
            {
                case FinancialItemType.Asset:
                    //zadej logiku prepoctu
                    czk /= CZKToEURRate;
                    return czk;
                
                case FinancialItemType.Equity:
                    //zadej logiku prepoctu
                    czk /= CZKToEURRate;
                    return czk;
                
                case FinancialItemType.PL:
                    //zadej logiku prepoctu
                    czk /= CZKToEURRate;
                    return czk;
                
                case FinancialItemType.Liability:
                    //zadej logiku prepoctu
                    czk /= CZKToEURRate;
                    return czk;
            }
            return 0;
        }

        public static void cara() 
        {
            int lineLength = Console.WindowWidth;
            for (int i = 0; i < lineLength; i++)
            {
                Console.Write("-");
            }
        }
        private static void centerText(String text)
        {
            Console.Write(new string(' ', (Console.WindowWidth - text.Length) / 2));
            Console.WriteLine(text);
        }
    }
}