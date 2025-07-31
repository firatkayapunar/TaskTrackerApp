<h1 align="center">📝 TaskTrackerApp - Yapılacaklar Listesi Uygulaması</h1>


**TaskTrackerApp**, çalışanların görevlerini dijital olarak takip edebileceği, modern yazılım mimarileri ve .NET teknolojileri ile inşa edilmiş, modüler, güvenli ve genişletilebilir To-Do uygulamasıdır. Bu proje, .NET geliştirici yetkinliğini değerlendirmek amacıyla oluşturulmuş bir Case Study projesidir.

---

## 🚀 **Proje Özeti**

**TaskTrackerApp**, görevlerin kullanıcı bazlı oluşturulmasını, listelenmesini, düzenlenmesini, silinmesini ve tamamlandı olarak işaretlenmesini sağlayan, uçtan uca güvenli ve kullanıcı dostu bir görev yönetim sistemidir.

Uygulama iki ana çalışma bileşeninden oluşur:

- **Backend (API)**: ASP.NET Core Web API kullanılarak geliştirildi. JWT tabanlı kimlik doğrulama ve rol bazlı yetkilendirme içerir.
- **Frontend (UI)**: ASP.NET Core Razor Pages ile inşa edilmiştir. Session üzerinden login yönetimi yapılır.

Kod yapısı, **Clean Architecture**, **CQRS**, **SOLID**, **Dependency Injection**, **MediatR**, **FluentValidation** ve  **AutoMapper** gibi modern yazılım mühendisliği tekniklerine göre yapılandırıldı.

---

## 🧪 **Örnek Kullanım Senaryoları**

| Senaryo                | Açıklama                                                                  |
|------------------------|---------------------------------------------------------------------------|
| Görev ekleme           | Giriş yapmış kullanıcı, başlık ve açıklama girerek görev oluşturabilir.   |
| Görev düzenleme        | Kullanıcı, mevcut görevin başlığını ve açıklamasını güncelleyebilir.      |
| Görev tamamlama        | "Complete" butonuyla görev tamamlandı olarak işaretlenir.                 |
| Görev silme            | Kullanıcı, kendi görevini silebilir.                                      |
| Kullanıcı kaydı (Admin)| Sadece Admin rolündeki kullanıcılar yeni kullanıcı ekleyebilir.           |

---

## 🔧 **Kurulum & Veritabanı Ayarları**

<ol>
  <li>
    <strong>Veritabanı migrasyonlarını çalıştırın:</strong>
    <pre><code>Update-Database</code></pre>
    <p>Bu komut, <strong>Package Manager Console</strong> üzerinden çalıştırılmalıdır.</p>
  </li>
  <li>
    <strong>Örnek verileri yüklemek için SQL scriptlerini çalıştırın:</strong>
    <ul>
      <li><code>UsersInsertScript.sql</code></li>
      <li><code>TaskItemsInsertScript.sql</code></li>
    </ul>
  </li>
</ol>

<br>
 ✅ <strong>Bu adımları takip ederek veritabanınızı hazır hale getirebilirsiniz.</strong>

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
├── 📁 TaskTrackerApp.Tests           --> Unit test projeleri
</pre>

---

<h2>🛠️ Kullanılan Teknolojiler ve Mimariler</h2>

<ul> 
  <li><strong>.NET 8</strong> (ASP.NET Core Web API & ASP.NET Core Razor Pages)</li> 
  <li><strong>Entity Framework Core</strong> (ORM – Nesne-ilişkisel haritalama)</li> 
  <li><strong>MediatR</strong> (CQRS için Handler yapısı)</li> 
  <li><strong>FluentValidation</strong> (Gelişmiş model doğrulama)</li> 
  <li><strong>AutoMapper</strong> (Veri dönüşümleri)</li> 
  <li><strong>JWT</strong> (Token tabanlı API authentication)</li> 
  <li><strong>Clean Architecture</strong> (Katmanlı ve ayrık sorumluluklara sahip mimari yapı)</li> 
  <li><strong>MS SQL Server</strong> (Veritabanı yönetim sistemi)</li> 
</ul>

---

## 📌 **Proje Ekran Görüntüleri**

> Aşağıda hem kullanıcı arayüzü hem de API tarafına ait ekran görüntülerini bulabilirsiniz.

### Anasayfa
<img width="1721" height="685" alt="1" src="https://github.com/user-attachments/assets/641cba1b-7d87-4110-ab85-f8c90f9ac42c" />

### Login
<img width="1617" height="780" alt="2" src="https://github.com/user-attachments/assets/3e54e1f6-ccf0-45a5-94cf-2d6a3a71f04d" />

### Task Listesi (User)
<img width="1436" height="580" alt="9" src="https://github.com/user-attachments/assets/b6d11599-6956-494b-80d6-88d383052cda" />

### Task Listesi (Admin)
<img width="1908" height="667" alt="3" src="https://github.com/user-attachments/assets/66af526a-d1c7-41a5-8382-28ac205af149" />

### Register (Admin)
<img width="1748" height="885" alt="4" src="https://github.com/user-attachments/assets/5045679e-e05f-4219-84b9-f5beb4452f27" />

### Task Oluşturma
<img width="1528" height="503" alt="5" src="https://github.com/user-attachments/assets/4f3cca45-ed78-479c-8526-2d35eda95eb1" />

### Task Güncelleme
 <img width="1464" height="538" alt="6" src="https://github.com/user-attachments/assets/f0d3fe24-a62c-46f6-81da-880305f933e0" />

### Task Silme
<img width="1478" height="606" alt="7" src="https://github.com/user-attachments/assets/1e2cb0fa-d730-4d4f-bef2-401eb23670df" />

### Task Durumu Güncelleme
<img width="1493" height="735" alt="8" src="https://github.com/user-attachments/assets/9ff4c0bb-4bdf-4001-9e91-4a296326ebda" />

### API Koleksiyonu
 <img width="1178" height="844" alt="10" src="https://github.com/user-attachments/assets/9f341f9f-b27f-404f-8a31-1a4a63fa7f5a" />
