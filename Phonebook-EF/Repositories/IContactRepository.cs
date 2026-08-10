public interface IContactRepository
{
    Task Add(Contact contact);
    Task<IReadOnlyCollection<Contact>> GetAll();
    Task<Contact> GetOne(Contact contact);
    Task Update(int currentId, Contact newDetails);
    Task Delete(int contactId);
}