namespace ShopProject.Domain
{
    public class UseCaseLog
    {
        public int Id { get; set; }
        public int ActorId { get; set; }
        public int UseCaseId { get; set; }
        public string UseCaseName { get; set; }
    }
}
