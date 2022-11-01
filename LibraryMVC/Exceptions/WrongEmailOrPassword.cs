namespace LibraryMVC.Exceptions
{
    public class WrongEmailOrPassword : Exception
    {
        public WrongEmailOrPassword(string message) : base(message)
        {

        }
    }
}
