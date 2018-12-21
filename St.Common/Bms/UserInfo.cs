using Prism.Mvvm;
using Serilog;

namespace St.Common
{
    public class UserInfo : BindableBase
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string SchoolId { get; set; }
        public string SchoolName { get; set; }

        public bool IsLogouted { get; set; }

        private bool _isOnline;

        public bool IsOnline
        {
            get { return _isOnline; }
            set { SetProperty(ref _isOnline, value); }
        }

        public string Pwd { get; set; }

    }
}