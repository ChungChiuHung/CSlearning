namespace SimpleServerCommon
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string HashedPassword { get; set; }
    }

}
