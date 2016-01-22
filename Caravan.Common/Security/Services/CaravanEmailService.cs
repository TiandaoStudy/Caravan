// Copyright 2015-2025 Finsa S.p.A. <finsa@finsa.it>
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except
// in compliance with the License. You may obtain a copy of the License at:
// 
// "http://www.apache.org/licenses/LICENSE-2.0"
// 
// Unless required by applicable law or agreed to in writing, software distributed under the License
// is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express
// or implied. See the License for the specific language governing permissions and limitations under
// the License.

using Finsa.CodeServices.MailSender;
using Microsoft.AspNet.Identity;
using MimeKit;
using PommaLabs.Thrower;
using System.Threading.Tasks;

namespace Finsa.Caravan.Common.Security.Services
{
    /// <summary>
    ///   Gestisce l'invio delle mail utilizzando l'interfaccia <see cref="IMailSender"/>.
    /// </summary>
    internal sealed class CaravanEmailService : IIdentityMessageService
    {
        private readonly IMailSender _mailSender;

        public CaravanEmailService(IMailSender mailSender)
        {
            RaiseArgumentNullException.IfIsNull(mailSender, nameof(mailSender));
            _mailSender = mailSender;
        }

        public async Task SendAsync(IdentityMessage message)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress("Richard Benson", "richard.benson@finsa.it"));
            mimeMessage.To.Add(new MailboxAddress(message.Destination, message.Destination));
            mimeMessage.Subject = message.Subject;
            mimeMessage.Body = new TextPart("plain") { Text = message.Body };
            await _mailSender.SendMailAsync(mimeMessage);
        }
    }
}
