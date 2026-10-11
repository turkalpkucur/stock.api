namespace Stock.Entities.Dtos.User
{
    public class UserForLoginDto
    {
        private string _email;
        private string _password;
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _email = value;
                }
                else
                {
                    _email = value.Trim();
                }
            }
        }
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _password = value;
                }
                else
                {
                    _password = value.Trim();
                }
            }
        }
    }
}
