namespace basic_banking_app_server.Dtos.CardDtos
{
    public class CardReadDto
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string Type { get; set; }
        public string Network { get; set; }
        public decimal? Balance { get; set; }
        public int? UserId { get; set; }
    }
}
