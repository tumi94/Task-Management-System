namespace Task_Management_System__Server_.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
        void Rollback();
    }
}
