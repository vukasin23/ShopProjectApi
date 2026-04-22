using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Domain
{
    public class UseCaseLog
    {
        public int Id { get; set; }
        public int ActorId { get; set; }
        public int UseCaseId { get; set; }
        public string UseCaseName { get; set; }
        public string Data { get; set; }
        public DateTime Date { get; set; }

        public UseCase usecase{ get; set; }
        public User user { get; set; }

    }
}
