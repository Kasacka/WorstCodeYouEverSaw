using System;
using System.Linq;
using System.Collections.Generic;

namespace UglyCode
{
    public static class Program
    {
        public static void Main(string[] _)
        {
            int eingabe = 3;

            var strategieZuBenuetzen = WahrheitsFaktorie.Erstelle_AllesFalsch();
            var andereStrat = WahrheitsFaktorie.Erstelle_WirklichSo();
            strategieZuBenuetzen.FindHeraus_ObWahrOderFalsch(eingabe);

            var resul = strategieZuBenuetzen.Gib_Resultat(eingabe);
            if (resul.Count() > 1 && !!!resul.ToArray()[resul.ToArray().Length - (1 + 2) / 2].HasValue)
            {
                System.Console.WriteLine("Programm hat ein Fehler!");
            }
            var istWahr = !(new[] { strategieZuBenuetzen.Gib_Resultat(eingabe).Skip(0).Last() }.First());
            andereStrat.FindHeraus_ObWahrOderFalsch(eingabe);

            var vertrauenswuerdigIstWahr = !(andereStrat.Gib_Resultat(eingabe).First() ?? false);

            System.Console.WriteLine(vertrauenswuerdigIstWahr);
        }

        public static class WahrheitsFaktorie
        {
            public static ObWahrheitable Erstelle_WirklichSo()
            {
                // gebe wirklich so
                return new WirklichSo();
            }

            public static ObWahrheitable Erstelle_AllesFalsch()
            {
                return new NoedSo((WirklichSo) Erstelle_WirklichSo());
            }
        }

        public interface ObWahrheitable
        {
            void FindHeraus_ObWahrOderFalsch(int dieEingabe);

            // Kontract:
            // Falls mehrere werte zurückgegegen werden und das letzte ein nicht-wert ist, ist die eingabe nicht korrespondierend mit der
            // andere methode des interfaces
            // Andernfalls entspricht der erste Wert dem Wahrheitswert
            IEnumerable<Nullable<bool>> Gib_Resultat(int dieEingabe);
        }

        public class NoedSo : ObWahrheitable
        {
            public static ObWahrheitable So;

            public NoedSo(WirklichSo so)
            {
                So = (ObWahrheitable)so;
            }

            public void FindHeraus_ObWahrOderFalsch(int dieEingabe)
            {
                So.FindHeraus_ObWahrOderFalsch(dieEingabe);
            }

            public IEnumerable<Nullable<bool>> Gib_Resultat(int dieEingabe)
            {
                So.FindHeraus_ObWahrOderFalsch(dieEingabe);
                return So.Gib_Resultat(dieEingabe).Select(koennteWahrSein => koennteWahrSein.HasValue ? !koennteWahrSein : null);
            }
        }

        public class WirklichSo : ObWahrheitable
        {
            private static string Zwischenstand;
            private static int DieEingabe;

            public void FindHeraus_ObWahrOderFalsch(int dieEingabe)
            {
                DieEingabe = dieEingabe;

                bool ErstelleInitialien_IstWahrWert()
                {
                    IEnumerable<int> X()
                    {
                        yield return 4;
                        yield return 4;
                        yield return 4;
                    }
                    return X().All(a => a == 4);
                }

                try
                {
                    bool istWahr = ErstelleInitialien_IstWahrWert();;
                    bool nein = -1 != -1 * -1;
                    bool istWahrOderFalsch = (!Convert.ToBoolean(dieEingabe) != !nein);

                    if (istWahrOderFalsch == !nein)
                    {
                        Zwischenstand = (istWahr == istWahrOderFalsch).ToString();
                    }
                    else
                    {
                        var xyz = nein || false;
                        if (xyz is bool abc)
                        {
                            abc = !(abc ^ abc);
                        }
                        else
                        {
                            abc = true;
                        }
                        Enumerable.Range(1, 10).ToList().ForEach(_ => abc ^= false);

                        try
                        {
                            throw new IstKomplettUnWahrFehler(Convert.ToBoolean(abc));
                        }
                        catch (IstKomplettUnWahrFehler ___)
                        {
                        back:
                            goto xa;
                            throw new System.Exception("wird gar nie geworfen. Oder doch?");
                        xa:
                            throw new IstKomplettUnWahrFehler(___ == ___);
                            // erstelle Endlosschleife um bei Fehlverhalten
                            // nichts kaputtes aufzurufen
                            for(;;)
                                System.Threading.Thread.Yield();
                            goto back;
                        }
                    }
                }
                catch (IstKomplettUnWahrFehler fehler1)
                {
                    Zwischenstand = fehler1.BooleanZuMerken.ToString();
                }

                // check falls gut so
                if(Zwischenstand == string.Empty)
                {
                    Zwischenstand = new Random().Next().ToString().ToString();
    
                }
            }

            private IEnumerable<Nullable<bool>> YieldyBug_Gib_Resultat(int dieEingabe)
            {
                if (dieEingabe == DieEingabe)
                    {
                        yield return Convert.ToBoolean(Zwischenstand) != false;
                    }
                    else
                    {
                        yield return false;
                        yield return false;
                        int.TryParse("hans", out var kk); // produce null
                        yield return (kk == kk) ?  (bool?) null :  (bool?) true;
                        yield break;
                }
            }

            public IEnumerable<Nullable<bool>> Gib_Resultat(int dieEingabe)
            {
                // improve robusstness

                try
                {
                    try
                    {
                        return YieldyBug_Gib_Resultat(dieEingabe);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        throw;
                    }
                    finally
                    {
                        //return Enumerable.Repeat((bool?)false, 1);
                    }
                }
                catch (Exception fehler2)
                {
                    // do nothing at all
                    throw;
                }
            }
        }

        public class IstKomplettUnWahrFehler : Exception
        {
            public bool BooleanZuMerken { get; set; }
            public int ___x;

            public IstKomplettUnWahrFehler(bool booleanZuMerken)
            {
                BooleanZuMerken = booleanZuMerken;
            }
        }
    }
}