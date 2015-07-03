using System.Threading.Tasks;

namespace KnockAdm
{
    public interface IRule
    {
        string Name { get; }
        string ErrorMessage { get; }
        Task<bool> CheckAsync();
    }
}