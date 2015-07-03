using System;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Threading.Tasks;
using System.Data.Entity;

namespace KnockAdm
{
    public class UserQueryService
    {
        private readonly PersistenceContext _context;

        public UserQueryService(PersistenceContext context)
        {
            _context = context;
        }

        public async Task<UserDto> GetByIdAsync(Validated<int> id)
        {
            var user = await _context.Users.FindAsync(id.Value);
            return user.ToDto<UserDto>();
        }

        public Task<bool> ExistsByUserNameAsync(string userName)
        {
            return _context.Users.AnyAsync(x => x.UserName == userName);
        }

        public Task<bool> ExistsByEmailAsync(string email)
        {
            return _context.Users.AnyAsync(x => x.Email == email);
        }

        public async Task<UsersDto> GetAsync(GetUsersQuery query)
        {
            var sortColumn = string.IsNullOrEmpty(query.SortColumn) 
                ? PropertyName.Get((User x) => x.Id) 
                : query.SortColumn.FirstLetterToUpper();

            var sortType = query.SortType == "desc" ? " descending" : "";

            var users = await _context.Users.Where(SearchCriteria(query.SearchTerm))
                .OrderBy(sortColumn + sortType)
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .Project().To<UserItemDto>()
                .ToListAsync();                

            return new UsersDto()
            {
                List = users,
                Total = await _context.Users.CountAsync(SearchCriteria(query.SearchTerm))
            };
        }

        private Expression<Func<User, bool>> SearchCriteria(string searchTerm)
        {
            return x => x.UserName.Contains(searchTerm) 
                || x.Email.Contains(searchTerm)
                || string.IsNullOrEmpty(searchTerm);
        }
    }
}