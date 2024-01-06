using System.Collections;

namespace İsletim_Sistemleri_Vize
{
    class Program
    {
        static void Main()
        {
            SayiIslemleri sayiIslemleri = new SayiIslemleri();
            sayiIslemleri.SayilariOlustur(1, 1000000);

            int parcaSayisi = 4;
            List<List<int>> bolunmusListeler = sayiIslemleri.BolunmusListeleriOlustur(parcaSayisi);

            Thread t1 = new Thread(() => sayiIslemleri.CiftSayilariBul(bolunmusListeler));
            Thread t2 = new Thread(() => sayiIslemleri.TekSayilariBul(bolunmusListeler));
            Thread t3 = new Thread(() => sayiIslemleri.AsalSayilariBul(bolunmusListeler));
            Thread t4 = new Thread(() => sayiIslemleri.AsalSayilariBul(bolunmusListeler));

            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();

            t1.Join();
            t2.Join();
            t3.Join();
            t4.Join();

            sayiIslemleri.SonuclariYazdir();
        }

    }
}