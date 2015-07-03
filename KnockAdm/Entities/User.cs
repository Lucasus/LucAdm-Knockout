namespace KnockAdm
{
    public class User : Entity
    {
        public virtual string UserName { get; set; }
        public virtual string Email { get; set; }
        public virtual string HashedPassword { get; set; }
        public virtual bool Active { get; set; }
    }
}