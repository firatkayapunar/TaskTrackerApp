<h1 align="center">📝 TaskTrackerApp - Yapılacaklar Listesi Uygulaması</h1>


**TaskTrackerApp**, çalışanların günlük görevlerini düzenli ve etkili şekilde takip edebileceği, sade ama güçlü bir yapılacaklar listesi uygulamasıdır. Proje, .NET geliştirici yetkinliğini değerlendirmek amacıyla bir Case Study kapsamında geliştirilmiştir.
Uygulama; sağlam bir Web API, sade bir kullanıcı arayüzü ve modern mimari prensipleri ile geliştirilmiş, günlük görev takibini kolaylaştırmayı hedefleyen bir çözümdür.

---

## 🚀 **Proje Özeti**
Bu uygulama; görev oluşturma, listeleme, güncelleme, silme ve tamamlanma durumlarını yönetmeyi sağlayan temel bir görev takip sistemi sunar. API tabanlı mimarisi sayesinde frontend ve backend net şekilde ayrılmıştır. UI kısmı ise ASP.NET Core Razor Pages kullanılarak geliştirilmiştir. Bu da modern bir kullanıcı deneyimi sağlar.

✅ **Öne Çıkan Özellikler:**
- Görev oluşturma, listeleme, güncelleme ve silme
- Tamamlandı olarak işaretleme
- Oluşturulma ve bitiş tarihi bilgileri
- RESTful Web API endpoint’leri (CRUD)
- Hatalı istekler için doğru HTTP yanıtları
- Razor Pages ile minimal ve işlevsel kullanıcı arayüzü

---

<h2>📂 Proje Yapısı</h2>

<pre>
📦 TaskTrackerApp
│
├── 📂 TaskTrackerApp.API             --> Web API (Controllers, DTOs)
├── 📂 TaskTrackerApp.Domain          --> Entity modelleri
├── 📂 TaskTrackerApp.Application     --> Uygulama servisleri ve iş mantığı
├── 📂 TaskTrackerApp.Infrastructure  --> EF Core DbContext, Migrations
├── 📂 TaskTrackerApp.UI              --> Razor Pages frontend
├── 📁 TaskTrackerApp.Tests (ops.)    --> Unit test projeleri (isteğe bağlı)
</pre>

---

<h2>🛠️ Kullanılan Teknolojiler ve Mimariler</h2>

<ul> 
  <li><strong>.NET 8</strong> (ASP.NET Core Web API & ASP.NET Core Razor Pages)</li> 
  <li><strong>Entity Framework Core</strong> (ORM – Nesne-ilişkisel haritalama)</li> 
  <li><strong>Clean Architecture</strong> (Katmanlı ve ayrık sorumluluklara sahip mimari yapı)</li> 
  <li><strong>ASP.NET Core Razor Pages</strong> (Frontend geliştirme – dosya tabanlı, modern ve sade arayüz mimarisi)</li> 
  <li><strong>MS SQL Server</strong> (Veritabanı yönetim sistemi)</li> 
</ul>
