# **API Kullanım Kılavuzu**

Bu kılavuz, web sitesinden verileri çekme, belirli bir ürünü inceleme ve veritabanına kaydetme işlemlerini adım adım açıklamaktadır.

## **1. Genel Görünüm**

Bu proje, bir web sitesinden belirli ürünleri çekip, bu ürünlerin bilgilerini görüntüleme ve seçilen ürünleri veritabanına kaydetme işlevselliğine sahiptir.
![Screenshot 2024-08-19 202646](https://github.com/user-attachments/assets/959d5acc-dc96-4361-999d-9207e10f1c55)

## **2. Ürün Arama**

**Senaryo:** Hikayeden gittiğimizi varsayarsak, sitede arama kısmına "tshirt" yazdık ve sayfanın linkini kopyaladık.
**İşlem: ** Arama sorgusunu API'ye gönderdiğimizde, belirlediğimiz sayfa sayısı kadar sonuç döner. Bu sonuçlar, tshirtlerin listesi ve linkleri şeklinde sıralanır.
![Screenshot 2024-08-19 202417](https://github.com/user-attachments/assets/4d2b4a13-868f-42ea-a791-c574468666ab)
![Screenshot 2024-08-19 202452](https://github.com/user-attachments/assets/8c7b88f2-4b62-4747-b718-57e88063f191)

## **3. Ürün Bilgilerini İnceleme**

**Senaryo:** Listeden rastgele bir tshirtün linkini seçerek, o ürüne ait detaylı bilgileri görmek istedik.
**İşlem:** Seçtiğiniz ürünün linkini API'ye göndererek, ürünün spesifik detaylarını alabilirsiniz.
![Screenshot 2024-08-19 202521](https://github.com/user-attachments/assets/c5922d4c-4c1f-4365-bd52-e577f1a4cd8d)

## **4. Ürün Veritabanına Kaydetme**

**Senaryo:** Seçtiğimiz ürünü veritabanına kaydetmek istiyoruz.
**İşlem:** Yapmamız gereken tek şey addProduct metodunu çalıştırmaktır. Verileri girmemize gerek yok çünkü session'da "Specific Product" olarak addProduct metoduna veriler taşınmıştır. addProduct metodunu çalıştırdığınızda, ürün veritabanına eklenmiş olacaktır.
![Screenshot 2024-08-19 204152](https://github.com/user-attachments/assets/4d9f39bf-eb34-4245-88e5-389a07cda3ed)

> [!WARNING]
> addProduct metodunda title, price ve link gibi alanlar string olarak gözükebilir. Bu, bir sorun teşkil etmez; execute ettiğiniz sürece scrape ettiğiniz ürün veritabanına kaydedilecektir. Bu durumun neden kaynaklandığına dair net bir bilgim bulunmamakla birlikte, işlevselliği etkilememektedir.


## **5. Verilerin Get Metoduyla Çağrılması**

**Senaryo:** Veritabanındaki ürün verilerini listelemek veya bir ürünü çekmek istiyoruz.
**İşlem:** GET metodu ile veritabanındaki verileri sorgulayabiliriz.
![Screenshot 2024-08-19 202632](https://github.com/user-attachments/assets/6b1ce13a-fadd-4e37-afb0-add3f91ee49d)




# **Gelecekteki Geliştirme Olanakları**

Bu projede, şu an için yalnızca bir web sitesi üzerinden veri çekimi yapılabiliyor. Ancak, projenin kapsamını genişletmek ve daha fazla işlevsellik eklemek için aşağıdaki geliştirme adımları değerlendirilebilir:

**1. Birden Fazla Web Sitesinin Entegrasyonu**

**Amaç:** Farklı web sitelerinden ürün verilerini çekmek ve bu verileri merkezi bir veritabanında toplamak.
**Yöntem:** Mevcut sistemde, yalnızca bir web sitesinden veri çekimi yapılabiliyor. Bu nedenle, farklı web siteleri için ayrı modüller geliştirilebilir. Her bir web sitesi için ayrı sınıflar ve fonksiyonlar tanımlanarak, sistemin genişletilebilir olması sağlanabilir.

**2. CronJob ile Zamanlayıcı Kurulumu**

**Amaç:** Belirli aralıklarla otomatik olarak ürün fiyatlarını kontrol etmek ve kullanıcıları indirimler hakkında bilgilendirmek.
**Yöntem:** Bir cron job kurulumu yapılarak, belirli aralıklarla ürün fiyatları güncellenebilir. Kullanıcıların belirlediği kriterlere (örneğin, %10 indirim) göre, fiyat düştüğünde kullanıcılara otomatik olarak bildirim gönderilebilir. Bu sayede, veritabanında kayıtlı ürünlerin daha uygun fiyatlarla satın alınması sağlanabilir.

**3. İndirim Bildirim Sistemi**

**Amaç:** Kullanıcıların ilgilendiği ürünler indirimdeyken onları bilgilendirmek.
**Yöntem:** Kullanıcıların ilgilendiği ürünleri belirli bir yüzde indirim gördüğünde onlara e-posta veya SMS ile bildirim gönderecek bir sistem entegre edilebilir. Bu sistem, kullanıcıların ürünleri daha ucuza satın almalarını sağlar.

Bu geliştirmelerle, projenin kullanıcı deneyimini artırabilir ve daha geniş bir kitleye hitap edebilir hale getirebilirsiniz.
