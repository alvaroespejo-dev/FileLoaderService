using AEspejo.FileLoaderService.Enums;
using AEspejo.FileLoaderService.Interfaces;
using AEspejo.FileLoaderService.Models;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;
using System.Net.Mail;
using System.Runtime;

namespace AEspejo.FileLoaderService.Loaders
{
    public class EmailFileLoader : IFileLoader
    {
        private readonly string _host;
        private readonly int _port;
        private readonly string _username;
        private readonly string _password;
        private readonly string _fromEmail;
        private readonly string _subject;

        public EmailFileLoader(string host, int port, string username, string password, string? fromEmail, string? subject)
        {
            _host = host;
            _port = port;
            _username = username;
            _password = password;
            _fromEmail = fromEmail ?? string.Empty;
            _subject = subject ?? string.Empty;
        }

        public async Task<FileResult> LoadAsync(string sourcePath)
        {
            using var imapClient = new ImapClient();
            await imapClient.ConnectAsync(_host, _port, true);
            await imapClient.AuthenticateAsync(_username, _password);

            if (imapClient.IsConnected)
            {
                IMailFolder inbox = imapClient.Inbox;
                await inbox.OpenAsync(FolderAccess.ReadOnly);

                var uids = await inbox.SearchAsync(SearchQueryBySettings());
                foreach (var uid in uids)
                {
                    var message = await inbox.GetMessageAsync(uid);
                    foreach (var attachment in message.Attachments)
                    {
                        if (attachment is MimePart part)
                        {
                            using var ms = new MemoryStream();
                            await part.Content.DecodeToAsync(ms);
                            return new FileResult
                            {
                                FileName = part.FileName,
                                Content = ms.ToArray(),
                                Source = FileTypeEnum.Email
                            };
                        }
                    }
                }
            }
            return null;
        }

        private SearchQuery SearchQueryBySettings()
        {
            SearchQuery searchQuery = SearchQuery.NotSeen;

            if (!string.IsNullOrWhiteSpace(_fromEmail) && !string.IsNullOrWhiteSpace(_subject))
                return SearchQuery.And(
                    SearchQuery.And(
                    SearchQuery.FromContains(_fromEmail),
                    SearchQuery.SubjectContains(_subject)),
                    SearchQuery.NotSeen);

            if (!string.IsNullOrWhiteSpace(_fromEmail))
                return SearchQuery.And(SearchQuery.FromContains(_fromEmail), SearchQuery.NotSeen);

            if (!string.IsNullOrWhiteSpace(_subject))
                return SearchQuery.And(SearchQuery.SubjectContains(_subject), SearchQuery.NotSeen);

            return searchQuery;
        }
    }
}
