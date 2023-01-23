using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DAL
{
    public interface IRepository
    {
        User AuthenticateAdmin(string email, string password);
        IList<Tag> GetTags();
        IList<Apartment> GetApartments();
        IList<User> GetUsers();
        IList<City> GetCities();
        void CreateApartment(Apartment apartment);
        void AddCityToApartment(int apartmentId, string cityName);
        void AddImageToApartment(int apartmentId, Image image);
        void AddTagToApartment(int apartmentId, Tag tag);
        void DeleteApartment(int apartmentId);
        void UpdateApartment(Apartment apartment);
        void CreateTag(string tagName);
        void DeleteTag(int Id);
    }
}
