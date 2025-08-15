# FileLoaderService

**FileLoaderService** is a C# API that allows reading files from multiple sources such as **Local**, **HTTP/HTTPS**, **FTP/SFTP**, and **Email** (IMAP/POP3).  
It is designed for integrations and automated file ingestion workflows, and it is **fully Dockerized** for quick deployment.

---

## 📂 Supported Sources
- **Local** — Direct access to the file system.
- **HTTP/HTTPS** — Download from URLs.
- **FTP/SFTP** — With authentication.
- **Email** — IMAP/POP3 for reading attachments.

Its main purpose is to **unify access** to files distributed across different sources, simplifying integration into systems and automated processes.

---

## 🚀 Features
- Modular and extensible architecture.
- JSON responses with **Base64** content or binary download.
- Standardized HTTP error handling.
- Easily extendable to more sources (Google Drive, Azure Blob Storage, Amazon S3, etc.).
- **Docker-ready** — Runs instantly in containerized environments.

---

## 🏛 Architectures and Design Patterns Used
This project follows professional development principles and patterns to ensure **maintainability, scalability, and ease of extension**:

### Architecture
- **N-Layer Architecture** (Presentation → Application → Domain → Infrastructure).
- Clear separation of responsibilities.
- Ready to evolve into **Clean Architecture** if needed.

### Design Patterns
- **Strategy** — To handle different file sources using specific classes that implement the same interface (`IFileLoader`).
- **Factory Method** — To create the correct file loader instance based on the source type.
- **Dependency Injection** — Configured with `Microsoft.Extensions.DependencyInjection` for low coupling.
- **Adapter** — To unify external libraries like `MailKit` under a common interface.
- **Template Method** — To define reusable base flows while allowing customization for each loader type.

### Principles and Best Practices
- **SOLID**
- **DRY** (Don't Repeat Yourself)
- **KISS** (Keep It Simple, Stupid)
- **YAGNI** (You Aren’t Gonna Need It)
- Use of **DTOs** for data transfer.
- Code documented and following **.NET Coding Standards** conventions.

---

## 🐳 Running with Docker

**Build the image**

docker build -t fileloaderservice .

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
