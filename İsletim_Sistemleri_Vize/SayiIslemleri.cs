using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace İsletim_Sistemleri_Vize
{
    internal class SayiIslemleri
    {
        private List<int> sayilar = new List<int>();
        private List<int> ciftSayilar = new List<int>();
        private List<int> tekSayilar = new List<int>();
        private List<int> asalSayilar = new List<int>();
        private object lockObj = new object();

        public void SayilariOlustur(int baslangic, int bitis)
        {
            for (int i = baslangic; i <= bitis; i++)
            {
                sayilar.Add(i);
            }
        }

        public List<List<int>> BolunmusListeleriOlustur(int parcaSayisi)
        {
            int parcaUzunlugu = sayilar.Count / parcaSayisi;
            List<List<int>> bolunmusListeler = new List<List<int>>();

            for (int i = 0; i < parcaSayisi; i++)
            {
                List<int> parca = sayilar.Skip(i * parcaUzunlugu).Take(parcaUzunlugu).ToList();
                bolunmusListeler.Add(parca);
            }

            return bolunmusListeler;
        }

        public void CiftSayilariBul(List<List<int>> parcaliListeler)
        {
            lock (lockObj)
            {
                foreach (var parca in parcaliListeler)
                {
                    foreach (int sayi in parca)
                    {
                        if (sayi % 2 == 0)
                        {
                            ciftSayilar.Add(sayi);
                        }
                    }
                }
            }
        }

        public void TekSayilariBul(List<List<int>> parcaliListeler)
        {
            lock (lockObj)
            {
                foreach (var parca in parcaliListeler)
                {
                    foreach (int sayi in parca)
                    {
                        if (sayi % 2 != 0)
                        {
                            tekSayilar.Add(sayi);
                        }
                    }
                }
            }
        }

        public void AsalSayilariBul(List<List<int>> parcaliListeler)
        {
            foreach (var parca in parcaliListeler)
            {
                foreach (int sayi in parca)
                {
                    int sayac = 0;
                    for (int i = 2; i <= Math.Sqrt(sayi); i++)
                    {
                        if (sayi % i == 0)
                        {
                            sayac++;
                            break;
                        }
                    }

                    if (sayac == 0 && sayi > 1)
                    {
                        lock (lockObj)
                        {
                            if (!asalSayilar.Contains(sayi))
                            {
                                asalSayilar.Add(sayi);
                            }
                        }
                    }
                }
            }
        }

        public void SonuclariYazdir()
        {
            Console.WriteLine("Tek Sayilar: " + string.Join(", ", tekSayilar.Distinct()));
            Console.WriteLine();
            Console.WriteLine("Cift Sayilar: " + string.Join(", ", ciftSayilar.Distinct()));
            Console.WriteLine();
            Console.WriteLine("Asal Sayilar: " + string.Join(", ", asalSayilar.Distinct()));
        }
    }
}
