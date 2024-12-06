namespace VacatureApis.Interfaces
{
    public interface IController<IEntity>
    {
        Task<IEntity?> AddAsync(IEntity item);
        Task<IResult> DeleteAsync(int id);
        Task<List<IEntity>> FetchAllAsync();
        Task<IEntity?> GetByIdAsync(int id);
        Task<IEntity?> UpdateAsync(IEntity item);
    }
}