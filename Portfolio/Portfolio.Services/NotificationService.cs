using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Services
{
    public class NotificationService
    {
        public List<Notification> Messages { get; }
        public NotificationService()
        {
            Messages = new List<Notification>();
        }

        public bool HasMessages() { return Messages.Count > 0; }
        public void LogMessage(Notification message) { Messages.Add(message); }
    }

    public class Notification
    {
        public string Message { get; }
        public Notification(string message)
        {
            Message = message;
        }
    }
}
