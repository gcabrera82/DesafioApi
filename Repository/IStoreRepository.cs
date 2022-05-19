using DesafioApi.Models.Dto;

namespace DesafioApi.Repository
{
    public interface IStoreRepository
    {

        Task<List<StoreDto>> GetStore();
        Task<StoreDto> GetStoreById(int id);

        Task<StoreDto> CreateUpdate(StoreDto storeDto);

        Task<bool> DeleteStore(int id);

        //Stores Avaiable
        //Task<List<StoreDto>> GetStoreAvaiable();
    }
}
