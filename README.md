# FileLoaderService

**FileLoaderService** is a C# API that allows reading files from multiple sources such as Local, HTTP/HTTPS, FTP/SFTP, and Email (IMAP/POP3). Designed for integrations and automated file ingestion workflows.

- 📂 **Local** (File system)
- 🌐 **HTTP/HTTPS** (download from URLs)
- 📤 **FTP/SFTP** (with credentials)
- 📩 **Email** (IMAP/POP3, attachment reading)

Its purpose is to unify access to distributed files from different sources, making it easier to integrate into systems and automated workflows.

---

## 🚀 Features

- Extensible architecture with **interfaces and concrete classes** for each source type.
- JSON responses with **Base64** file content or binary download.
- Standard HTTP error handling and responses.
- Easy to extend for additional sources (Google Drive, Azure Blob, Amazon S3, etc.).

---

## 📦 Requirements

- [.NET 6 or later](https://dotnet.microsoft.com/download)
- NuGet package [`MailKit`](https://www.nuget.org/packages/MailKit) (for IMAP/POP3 support)

📌 Usage Examples

Local
GET http://localhost:5000/api/files/localfile?path=C:\Temp\test.txt

HTTP
GET http://localhost:5000/api/files/httpfile?path=https://example.com/file.txt

FTP
GET http://localhost:5000/api/files/ftpfile?user=username&password=secret&path=ftp://example.com/file.txt

Email (IMAP)
GET http://localhost:5000/api/files/emailfile?host=imap.example.com&port=993&username=user@example.com&pass


Install MailKit:
```bash
dotnet add package MailKit
