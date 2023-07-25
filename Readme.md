
<!-- ABOUT THE PROJECT -->
## About The Project

[![Projeyi basit bir kişi rehberi olarak tanımlayabiliriz, burada önemli olan clean architecture, SOLID, generic repository, Queue gibi sistemlerin micros servis mimarisinde nasıl çalıştığını göstermektir]](https://github.com/edipgenc/guide)


Projemiz iki adet micro servisten oluşmaktadır.
Database olarak PostgreSQL ve Mongo Db kullanılmıştır.
Queue için RabbitMQ kullanılmıştır.
Rabbit MQ Docker üzerindne çalışacaktır.
Diğer kullanılan teknolojiler ise Fluent Validation ve Auto Mapperdır.
Tasarım deseni olarak ta Reporsitory Pattern ve Generic Repository Pattern kullanılmıştır.
İlk micro servisimiz olan Guide.Book, Kişi kaydet, update et ve delete gibi crud işlemlerinin yanı sıra kişiye iletişim bilgisi ekle, sil ve güncelle gibi işlemleri yapmaktadır.
İki micro servisimiz ise Guide.Report'tur bu micro servis gelen rapor taleplerini alır ve Queue ya yazar, aynı zamanda tüm istenen rapor listesi, tek rapor detayı ve rapor sonucu gibi metodlarda içerir.

Servise bir rapor talebi geliği zaman micro servis Mongo ya data kaydı oluşturur, aynı zamanda Rabbit MQ ya bir event atar, Book micro servisimizde çalışan comsumer gelen rapor isteklerine göre bir http get oluşturur ve yine book microservisinde yer alan apiye bir istek oluşturur, ilgili rapor tamamlanınca rapor sonucu report micro servisinde yer alan SaveReportResult a bir istek atar, burada data içinde hem rapor sonucu hemde gelen eventin rapor Id si yer alır bu sayede isteği olmayan raporlar Mongoya kaydedilmez.

### Kullanımı

* Book Micro Servisi içerisinde 3 adet Api bulunmaktadır 
* 1.Person - Burada Person add, update, delete getById ve GetAll endpointleri yer almaktadır
* 2.Contact - Burada Contact add, update, delete, GetPersonConact ve GetPersonConactList servisleri yer almaktadır.
Bir kullanıcı önce sisteme Person Add servisinden kişi ekler daha sonra aynı kişiye iletişim bilgisi eklemek için Contact Servisini kullanır, Contact servisinde Add metodu ile iletişim bilgisi ekler, iletişim bilgisinde InfoType alanı enum dur ve (PhoneNumber=1,EMail=2,Location=3) seçeneklerniden birini seçer daha sonra ilgili veri için Content alanını doldurur
Bu bilgiler Postgre SQL de tutulur.
* Ayrıca burada bir servis daha yer almaktadır ancak bu servis sadece consumer tarafından kullanılacağı için swaggerda yer almamaktadır. Bu servisin kullanım amacı ile ilgili bilgi Queue yapısı bölümünde yer alacaktır.


Ayrıca bu micro serviste bir Consumer yer almaktadır, consumer servisi uygulama ağaya kalkarken singelton olarak çalışır ve queue içindeki eventleri consume eder. Consume işleminde gelen eventin içinde yer alan LocationId bilgisi ile Report API'ı üzerinden bir HTTP Client isteği oluşturur, bu istek aynı servisteki Report Servisine ilgili rapor talebini iletir, gelen rapor sonucu ile beraber raporun oluştuğu bilgisi mongo veritabanına kaydedilir ve consume işlemi bu event için tamamlanır.  

* Report Micro Servisi kullanıcıların rapor istemesi için oluşturulmuştur.
* Bu micro servisin içerisinde bir Report API yer almaktadır, bu API  GetRepostList, GetRepostList, GetReportDetail, GetPersonReportByLocation metodlarını barındırır, 
* Bir rapor isteği geldiği zaman bu rapor talebi için önce bir data oluşturulur ve data Mongo Db ye kaydediir, data içeriğinde gelen LocationId bilgisi ve rapor istek tarihi aynı zamanda rapor durum bilgisi yer alır.
Rapor Mongoya kaydedildikten sonra aynı micro serviste yer alan RabbitMQ producer a bir event atılır ve bu istek queue ye iletilmiş olur.
* Bu micro servis sadece mongo db ye bağlanır ve bunlar dışında başka bir görevi yoktur.
* Gelen bütün rapor istekleri mongo db üzerindeki datadan sorgulanır.


### Built With

Below is a list of the technologies I have used to make this project possible.

* [C#](https://docs.microsoft.com/en-us/dotnet/csharp/)
* [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-6.0)
* [EF Core](https://docs.microsoft.com/en-us/ef/core/)
* [AutoMapper](https://github.com/AutoMapper/AutoMapper)
* [FluentValidation](https://docs.fluentvalidation.net/en/latest/)
* [RabbitMQ](https://www.rabbitmq.com/)
* [PostgreSQL](https://www.postgresql.org/)
* [Docker](https://docker.com/)


