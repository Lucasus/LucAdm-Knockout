using System.Collections.Generic;

namespace KnockAdm
{
    public class UsersDto
    {
        public IList<UserItemDto> List { get; set; }
        public int Total { get; set; }
    }
}