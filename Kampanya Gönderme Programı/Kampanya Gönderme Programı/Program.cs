using Kampanya_Gönderme_Programı;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace TavukFirmaKampanya
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Market listesi
            List<Market> marketler = new List<Market>
            {
                new Market { MarketId = 1, MarketAd = "Çetinkaya", IletisimBilgisi = "sla.karahan3401@gmail.com" },
                new Market { MarketId = 2, MarketAd = "Örnek", IletisimBilgisi = "ornek@mail.com" },
                new Market { MarketId = 3, MarketAd = "Çağrı", IletisimBilgisi = "cagri@mail.com" },
                new Market { MarketId = 4, MarketAd = "A101", IletisimBilgisi = "a101@mail.com" },
                new Market { MarketId = 5, MarketAd = "BİM", IletisimBilgisi = "bim@mail.com" }
            };

            // Her market için kaç kampanya gönderileceğini kullanıcıdan alalım
            foreach (var market in marketler)
            {
                Console.WriteLine($"{market.MarketAd} marketine kaç kampanya göndermek istiyorsunuz?");
                int kampanyaSayisi;
                while (!int.TryParse(Console.ReadLine(), out kampanyaSayisi) || kampanyaSayisi < 1)
                {
                    Console.WriteLine("Geçerli bir sayı giriniz.");
                }

                // Kampanya içeriği depolama listesi (aynı kampanyayı tekrar girmemek için)
                List<string> gonderilenKampanyalar = new List<string>();

                // Kampanyaları gönderiyoruz
                for (int i = 0; i < kampanyaSayisi; i++)
                {
                    string kampanyaIcerik;
                    bool ayniKampanyaMi = true;

                    // Kullanıcıdan kampanya içeriğini al ve aynı olup olmadığını kontrol et
                    do
                    {
                        Console.WriteLine($"{i + 1}. kampanya için içerik giriniz:");
                        kampanyaIcerik = Console.ReadLine();

                        // Aynı kampanya içerik kontrolü
                        if (gonderilenKampanyalar.Contains(kampanyaIcerik))
                        {
                            Console.WriteLine("Bu kampanya daha önce girildi. Lütfen farklı bir kampanya giriniz.");
                        }
                        else
                        {
                            ayniKampanyaMi = false;
                        }
                    } while (ayniKampanyaMi);

                    // Yeni kampanyayı ekliyoruz
                    Kampanya yeniKampanya = new Kampanya
                    {
                        KampanyaId = i + 1, // Kampanya Id her kampanya için farklı olmalı
                        KampanyaIcerik = kampanyaIcerik,
                        BaslangicTarihi = DateTime.Now,
                        BitisTarihi = DateTime.Now.AddDays(1)
                    };

                    gonderilenKampanyalar.Add(kampanyaIcerik); // Girilen kampanyayı listeye ekliyoruz

                    GonderKampanya(market, yeniKampanya);
                }
            }

            Console.WriteLine("Tüm kampanyalar başarıyla gönderildi!");
        }

        static void GonderKampanya(Market market, Kampanya kampanya)
        {
            try
            {
                MailMessage mail = new MailMessage("sirketmail@firma.com", market.IletisimBilgisi);
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");

                mail.Subject = "Yeni Kampanya";
                mail.Body = kampanya.KampanyaIcerik;

                smtpClient.Port = 587;
                smtpClient.Credentials = new System.Net.NetworkCredential("emailkullaniciadi", "emailşifre");
                smtpClient.EnableSsl = true;

                smtpClient.Send(mail);
                Console.WriteLine($"{market.MarketAd} marketine e-posta gönderildi.");
            }
            catch (SmtpException smtpEx)
            {
                Console.WriteLine($"SMTP hatası oluştu: {smtpEx.Message}");
            }
            catch (FormatException formatEx)
            {
                Console.WriteLine($"E-posta formatı hatası oluştu: {formatEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bir hata oluştu: {ex.Message}");
            }
        }
    }
   
}