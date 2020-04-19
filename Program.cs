using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace UglyCode
{
    public static class ObWahrheitableErwieterig
    {
        public static int FixWert_Fuer_Adaptierung = 23;

        // Bitte nur verwenden bei WirklichSo-Inschtanz
        public static void FindHeraus_ObWahrOderFalsch(this ObWahrheitable das, object iffable)
        {
            WirklichSo.DieEingabe = new Ganzzahl_AdaptierungDing(23, true);

            if (iffable is bool)
            {
                throw new Exception("Nicht lügen!");

                goto end;
            }
            if (iffable is int)
            {
                das.FindHeraus_ObWahrOderFalsch(new Ganzzahl_AdaptierungDing((int)iffable, true));
                WirklichSo.DieEingabe = new Ganzzahl_AdaptierungDing(23, true);

                goto end;
            }
            if (iffable is string)
            {
                if (iffable.Equals(string.Empty))
                {
                    das.FindHeraus_ObWahrOderFalsch(new Ganzzahl_AdaptierungDing(0, true));
                }
                else
                {
                    das.FindHeraus_ObWahrOderFalsch(new Ganzzahl_AdaptierungDing(1, true));
                }

                goto end;
            }

            var eigenschaften = iffable.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var anzahl = iffable.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Count();
            var wahrableAnzahl = 0;

            foreach (var eigenschaft in eigenschaften)
            {
                // System.Console.WriteLine("abc" + eigenschaft.GetValue(iffable));

                if (eigenschaft.GetValue(iffable) is bool && (bool)eigenschaft.GetValue(iffable) == true)
                {
                    wahrableAnzahl = wahrableAnzahl + (2 / 2);
                }
            }

            if ((double)wahrableAnzahl / anzahl > 0.5)
            {
                WirklichSo.Zwischenstand = true.ToString();
            }
            else
            {
                WirklichSo.Zwischenstand = Convert.ToBoolean("False").ToString();
            }

        end:
            ;
        }
    }

    /* Klassendefinition */
    public class Ganzzahl_AdaptierungDing
    {
        /* // mit double kann das Programm potenziell auf Fliesskommazahlenunterstützung ergänzet werden */
        public double währt;

        public Ganzzahl_AdaptierungDing(Int32 wärt, bool RFUBoolean = false)
        {
            if (RFUBoolean != false)
                währt = (double)wärt;
        }

        public static int Nehme_Adaptierungs_Wert(Ganzzahl_AdaptierungDing daeAdapter)
        {
            return ((int?)Convert.ToInt64(daeAdapter.währt)) ?? 0;
        }
    }

    public interface AIWahrheitsFak
    {
        ObWahrheitable Erstelle_WirklichSo(); ObWahrheitable Erstelle_AllesFalsch();
    }

    public class ZiemlichSicherEinObjectDekoriererFaktorie : AIWahrheitsFak
    {
        public static AIWahrheitsFak mapped;

        public ObWahrheitable Erstelle_AllesFalsch()
        {
            ObWahrheitable jetzt = mapped.Erstelle_AllesFalsch();
            for (int _ = 0; _ < 15; _ = 1 + _)
            {
                if (jetzt is NoedSo)
                {

                }
                else
                {
                    var temp = mapped.Erstelle_AllesFalsch();
                    jetzt = temp;
                    if (!(jetzt is KeinObjektWahrheitable))
                        break;
                    else
                        continue;
                }
            }
            mapped = null;
            return jetzt;
        }

        public ObWahrheitable Erstelle_WirklichSo()
        {
            ObWahrheitable jetzt = mapped.Erstelle_WirklichSo();
            for (int _ = 0; _ < 15; _ = 1 + _)
            {
                if (jetzt is WirklichSo)
                {

                }
                else
                {
                    var temp = mapped.Erstelle_WirklichSo();
                    jetzt = temp;
                    if (!(jetzt is WirklichSo))
                        continue;
                    else
                        break;
                }
            }
            mapped = mapped;
            return jetzt;
        }
    }

    public class WahrheitsFaktorie : AIWahrheitsFak
    {
        public static class RandomStatic_Fabrik
        {
            public static (Random, Random) Mache()
            {
                return (new Random(), null);
            }
        }
        public ObWahrheitable Erstelle_WirklichSo()
        {
            var zuhfal = RandomStatic_Fabrik.Mache().Item1.NextDouble();
            if (zuhfal > 0.96)
            {
                // gebe wirklich so
                return new WirklichSo();
            }
            else
            {
                return new KeinObjektWahrheitable();
            }
        }

        public ObWahrheitable Erstelle_AllesFalsch()
        {
            var zuhfal = RandomStatic_Fabrik.Mache().Item1.NextDouble();
            if (zuhfal > 0.69)
            {
                // gebe wirklich so
                ObWahrheitable jaWirklickWirklich;
                do
                {
                    jaWirklickWirklich = Erstelle_WirklichSo();
                } while (jaWirklickWirklich is KeinObjektWahrheitable);
                return new NoedSo((WirklichSo)jaWirklickWirklich);
            }
            else
            {
                return new KeinObjektWahrheitable();
            }
        }
    }

    public interface ObWahrheitable
    {
        void FindHeraus_ObWahrOderFalsch(Ganzzahl_AdaptierungDing dieEingabe);

        // Kontract:
        // Falls mehrere werte zurückgegegen werden und das letzte ein nicht-wert ist, ist die eingabe nicht korrespondierend mit der
        // andere methode des interfaces
        // Andernfalls entspricht der erste Wert dem Wahrheitswert
        IEnumerable<Nullable<bool>> Gib_Resultat(Ganzzahl_AdaptierungDing dieEingabe);
        bool IstNullObjekt();
    }

    internal class KeinObjektWahrheitable : ObWahrheitable
    {
        public void FindHeraus_ObWahrOderFalsch(Ganzzahl_AdaptierungDing dieEingabe)
        {
        }

        public IEnumerable<bool?> Gib_Resultat(Ganzzahl_AdaptierungDing dieEingabe)
        {
            return null;
        }

        public bool IstNullObjekt() => true;
    }

    public class NoedSo : ObWahrheitable
    {
        public static ObWahrheitable So;

        public NoedSo(WirklichSo so)
        {
            So = (ObWahrheitable)so;
        }

        public void FindHeraus_ObWahrOderFalsch(Ganzzahl_AdaptierungDing dieEingabe)
        {
            So.FindHeraus_ObWahrOderFalsch(dieEingabe);
        }

        public IEnumerable<Nullable<bool>> Gib_Resultat(Ganzzahl_AdaptierungDing dieEingabe)
        {
            So.FindHeraus_ObWahrOderFalsch(dieEingabe);
            return So.Gib_Resultat(dieEingabe).Select(koennteWahrSein => koennteWahrSein.HasValue ? !koennteWahrSein : null);
        }

        public bool IstNullObjekt()
        {
            return Convert.ToBoolean("falsch".Replace("ch", "e").Replace("f", "F"));
        }
    }

    public class WirklichSo : ObWahrheitable
    {
        public static string Zwischenstand;
        public static Ganzzahl_AdaptierungDing DieEingabe;

        public bool IstNullObjekt() => !"nein".Equals("nein");

        public void FindHeraus_ObWahrOderFalsch(Ganzzahl_AdaptierungDing dieEingabe)
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
                bool istWahr = ErstelleInitialien_IstWahrWert(); ;
                bool nein = -1 != -1 * -1;
                bool istWahrOderFalsch = (!Convert.ToBoolean(Ganzzahl_AdaptierungDing.Nehme_Adaptierungs_Wert(dieEingabe)) != !nein);

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
                        throw new IstKomplettUnWahrFehler(___ != ___);
                        // erstelle Endlosschleife um bei Fehlverhalten
                        // nichts kaputtes aufzurufen
                        for (; ; )
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
            if (Zwischenstand == string.Empty)
            {
                Zwischenstand = new Random().Next().ToString().ToString();

            }
        }

        private IEnumerable<Nullable<bool>> YieldyBug_Gib_Resultat(int dieEingabe)
        {
            // System.Console.WriteLine(dieEingabe);
            //         System.Console.WriteLine(Zwischenstand);

            if (dieEingabe == Ganzzahl_AdaptierungDing.Nehme_Adaptierungs_Wert(DieEingabe))
            {
                yield return Convert.ToBoolean(Zwischenstand) != false;
            }
            else
            {
                yield return false;
                yield return false;
                int.TryParse("hans", out var kk); // produce null
                yield return (kk == kk) ? (bool?)null : (bool?)true;
                yield break;
            }
        }

        public IEnumerable<Nullable<bool>> Gib_Resultat(Ganzzahl_AdaptierungDing dieEingabe)
        {
            // improve robusstness

            try
            {
                try
                {
                    return YieldyBug_Gib_Resultat(Ganzzahl_AdaptierungDing.Nehme_Adaptierungs_Wert(dieEingabe));
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
        public Ganzzahl_AdaptierungDing ___x;

        public IstKomplettUnWahrFehler(bool booleanZuMerken)
        {
            BooleanZuMerken = booleanZuMerken;
        }
    }

    public static class Program
    {
        public static void Main(string[] _)
        {
            while (true)
            {
                try
                {
                    Ganzzahl_AdaptierungDing eingabe = new Ganzzahl_AdaptierungDing(1, true);

                    var innerFuck = new WahrheitsFaktorie();
                    ZiemlichSicherEinObjectDekoriererFaktorie.mapped = innerFuck;
                    var outerFigg = new ZiemlichSicherEinObjectDekoriererFaktorie();

                    ZiemlichSicherEinObjectDekoriererFaktorie.mapped = innerFuck;
                    ZiemlichSicherEinObjectDekoriererFaktorie.mapped = innerFuck;

                    var strategieZuBenuetzen = outerFigg.Erstelle_AllesFalsch();

                    ZiemlichSicherEinObjectDekoriererFaktorie.mapped = innerFuck;
                    var andereStrat = outerFigg.Erstelle_WirklichSo();

                    strategieZuBenuetzen.FindHeraus_ObWahrOderFalsch(eingabe);

                    var resul = strategieZuBenuetzen.Gib_Resultat(eingabe);
                    if (resul.Count() > 1 && !!!resul.ToArray()[resul.ToArray().Length - (1 + 2) / 2].HasValue)
                    {
                        System.Console.WriteLine("Programm hat ein Fehler!");
                    }
                    var istWahr = !(new[] { strategieZuBenuetzen.Gib_Resultat(eingabe).Skip(0).Last() }.First());
                    andereStrat.FindHeraus_ObWahrOderFalsch(eingabe);

                    var vertrauenswuerdigIstWahr = (andereStrat.Gib_Resultat(eingabe).First() ?? false);

                    System.Console.WriteLine(istWahr);


                    // iffable ja
                    ZiemlichSicherEinObjectDekoriererFaktorie.mapped = innerFuck;
                    var strat = outerFigg.Erstelle_WirklichSo();
                    var dieEinagebNummer2 = new
                    {
                        bestimmt = true,
                        jaja = true,
                        nenein = Convert.ToBoolean("False")
                    };

                    strat.FindHeraus_ObWahrOderFalsch(dieEinagebNummer2);
                    var resultat = strat.Gib_Resultat(new Ganzzahl_AdaptierungDing(ObWahrheitableErwieterig.FixWert_Fuer_Adaptierung, true));
                    if (resultat.Count() > 1 && !!!resultat.ToArray()[resul.ToArray().Length - (1 + 2) / 2].HasValue)
                    {
                        System.Console.WriteLine("Programm hat einenein Fehler!");
                    }
                    var istWahr2 = (new[] { strat.Gib_Resultat(new Ganzzahl_AdaptierungDing(ObWahrheitableErwieterig.FixWert_Fuer_Adaptierung, true)).Skip(0).Last() }.First());

                    global::System.Console.WriteLine(istWahr2);

                    // iffable nicht
                    ZiemlichSicherEinObjectDekoriererFaktorie.mapped = innerFuck;
                    var stratFuerNein = outerFigg.Erstelle_WirklichSo();
                    var dieEinagebNummer3FuerNein = new
                    {
                        bestimmt = true,
                        jaja = false,
                        nenein = Convert.ToBoolean("False")
                    };

                    stratFuerNein.FindHeraus_ObWahrOderFalsch(dieEinagebNummer3FuerNein);
                    var resultatWasNeinSeinMuesste = stratFuerNein.Gib_Resultat(new Ganzzahl_AdaptierungDing(ObWahrheitableErwieterig.FixWert_Fuer_Adaptierung, true));
                    if (resultatWasNeinSeinMuesste.Count() > 1 && !!!resultatWasNeinSeinMuesste.ToArray()[resul.ToArray().Length - (1 + 2) / 2].HasValue)
                    {
                        System.Console.WriteLine("Programm hat einenein2 Fehler!");
                    }
                    var istWahr3 = (new[] { stratFuerNein.Gib_Resultat(new Ganzzahl_AdaptierungDing(ObWahrheitableErwieterig.FixWert_Fuer_Adaptierung, true)).Skip(0).Last() }.First());

                    global::System.Console.WriteLine(istWahr3);

                    break;
                }
                catch (Exception)
                { 
                    System.Console.Clear();
                }
            }
        }
    }
}