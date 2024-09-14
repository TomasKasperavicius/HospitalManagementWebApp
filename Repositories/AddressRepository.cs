using HospitalManagementWebApp.Models;
using HospitalManagementWebApp.Repositories.Interfaces;

namespace HospitalManagementWebApp.Repositories
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        public AddressRepository(HospitalManagerDbContext context) : base(context) { }
    }
}
