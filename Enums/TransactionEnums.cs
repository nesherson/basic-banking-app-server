namespace basic_banking_app_server.Enums
{
    public class TransactionEnums
    {
        public enum Method { deposit, withdraw, payment }
        public enum Status { pending, failed, captured, refunded }
    }
}
