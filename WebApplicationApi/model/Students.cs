namespace WebApplication1.model
{

    public partial class Students
    {

        // This contains the Student's Personal information.
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public int Grad { get; set; }


        // This contains the Student's credential information.
        public string? Email { get; set; }
        public string? HashPassword { get; set; }
        public Roles.Role Role { get; set; }


        // Contains the student's authentication information.
        public string? RefreshTokenHash { get; set; }
        public DateTime? RefreshTokenExpiresAt { get; set; }
        public DateTime? RefreshTokenRevokedAt { get; set; }

    }

}
