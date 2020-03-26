using System.Collections.Generic;
using Windows.UI.Notifications;

namespace MyDotNetCoreWpfApp.Notifications
{
    public sealed class DesktopNotificationHistoryCompat
    {
        private string _aumid;
        private ToastNotificationHistory _history;

        internal DesktopNotificationHistoryCompat(string aumid)
        {
            _aumid = aumid;
            _history = ToastNotificationManager.History;
        }

        public void Clear()
        {
            if (_aumid != null)
            {
                _history.Clear(_aumid);
            }
            else
            {
                _history.Clear();
            }
        }

        public IReadOnlyList<ToastNotification> GetHistory()
        {
            return _aumid != null ? _history.GetHistory(_aumid) : _history.GetHistory();
        }

        public void Remove(string tag)
        {
            if (_aumid != null)
            {
                _history.Remove(tag, string.Empty, _aumid);
            }
            else
            {
                _history.Remove(tag);
            }
        }

        public void Remove(string tag, string group)
        {
            if (_aumid != null)
            {
                _history.Remove(tag, group, _aumid);
            }
            else
            {
                _history.Remove(tag, group);
            }
        }

        public void RemoveGroup(string group)
        {
            if (_aumid != null)
            {
                _history.RemoveGroup(group, _aumid);
            }
            else
            {
                _history.RemoveGroup(group);
            }
        }
    }
}
