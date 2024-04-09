using PasswordManager.Models.Dtos.InfoDtos;
using PasswordManager.Models.Enums;

namespace PasswordManager.Repositories.Contracts
{
    public interface IInfoRepository
    {
        // Ivana
        Task<List<InfoGetDto>> GetMyInfos(string email);

        // Alex
        Task<List<InfoGetDto>> GetSharedInfos(string email);

        // Ivana
        Task<List<InfoGetDto>> SortMyInfos(string email, int typeAsInt);

        // Ivana
        Task AddInfo(string email, InfoAddDto infoDto);

        // Ivana
        Task ShareInfo(string email, int friendId, int infoId);
    }
}
