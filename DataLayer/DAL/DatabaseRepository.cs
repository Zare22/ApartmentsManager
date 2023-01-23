using DataLayer.Enums;
using DataLayer.Models;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataLayer.DAL
{
    public class DatabaseRepository : IRepository
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

        public User AuthenticateAdmin(string email, string password)
        {
            var dataTable = SqlHelper.ExecuteDataset(connectionString, nameof(AuthenticateAdmin), email, password).Tables[0];
            if (dataTable.Rows.Count == 0) return null;

            DataRow row = dataTable.Rows[0];
            return new User
            {
                Email = row[nameof(User.Email)].ToString(),
                PasswordHash = row[nameof(User.PasswordHash)].ToString()
            };
        }

        public IList<Apartment> GetApartments()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(nameof(GetApartments), connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = command.ExecuteReader();
                    List<Apartment> apartments = new List<Apartment>();
                    while (reader.Read())
                    {
                        Apartment apartment = new Apartment()
                        {
                            Id = (int)reader["Id"],
                            Guid = (Guid)reader["Guid"],
                            CreatedAt = (DateTime)reader["CreatedAt"],
                            DeletedAt = reader.IsDBNull(reader.GetOrdinal("DeletedAt")) ? (DateTime?)null : (DateTime)reader["DeletedAt"],
                            Status = (Status)reader["Status"],
                            CityName = reader.IsDBNull(reader.GetOrdinal("City")) ? null : (string)reader["City"],
                            Address = (string)reader["Address"],
                            Name = (string)reader["Name"],
                            Price = (decimal)reader["Price"],
                            MaxAdults = (int)reader["MaxAdults"],
                            MaxChildren = (int)reader["MaxChildren"],
                            TotalRooms = (int)reader["TotalRooms"],
                            BeachDistance = (int)reader["BeachDistance"],
                            Tags = GetApartmentTags((int)reader["Id"]),
                            Images = GetApartmentImages((int)reader["Id"])
                        };
                        apartments.Add(apartment);
                    }
                    return apartments;
                }
            }
        }

        private IList<Tag> GetApartmentTags(int apartmentId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(nameof(GetApartmentTags), connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ApartmentId", apartmentId);
                    SqlDataReader reader = command.ExecuteReader();
                    List<Tag> tags = new List<Tag>();
                    while (reader.Read())
                    {
                        Tag tag = new Tag()
                        {
                            Id = (int)reader["Id"],
                            Guid = (Guid)reader["Guid"],
                            CreatedAt = (DateTime)reader["CreatedAt"],
                            Name = (string)reader["Name"]
                        };
                        tags.Add(tag);
                    }
                    return tags;
                }
            }
        }

        private IList<Image> GetApartmentImages(int apartmentId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(nameof(GetApartmentImages), connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ApartmentId", apartmentId);
                    SqlDataReader reader = command.ExecuteReader();
                    List<Image> images = new List<Image>();
                    while (reader.Read())
                    {
                        Image image = new Image()
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"],
                            Base64Content = (string)reader["Base64Content"],
                            IsRepresentative = (bool)reader["IsRepresentative"]
                        };
                        images.Add(image);
                    }
                    return images;
                }
            }
        }

        private bool TagInUse(int tagId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("GetTagInUse", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@tagId", tagId);

                    bool isInUse = (bool)command.ExecuteScalar();
                    return isInUse;
                }
            }

        }

        public IList<Tag> GetTags()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(nameof(GetTags), connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = command.ExecuteReader();
                    List<Tag> tags = new List<Tag>();
                    while (reader.Read())
                    {
                        Tag tag = new Tag
                        {
                            Id = (int)reader["Id"],
                            Guid = (Guid)reader["Guid"],
                            CreatedAt = (DateTime)reader["CreatedAt"],
                            Name = (string)reader["Name"]
                        };
                        tags.Add(tag);
                    }
                    foreach (var tag in tags)
                    {
                        tag.IsInUse = TagInUse(tag.Id);
                    }
                    return tags;
                }
            }
        }

        public IList<User> GetUsers()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(nameof(GetUsers), connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = command.ExecuteReader();
                    List<User> users = new List<User>();
                    while (reader.Read())
                    {
                        User user = new User()
                        {
                            Id = (int)reader["Id"],
                            Guid = (Guid)reader["Guid"],
                            CreatedAt = (DateTime)reader["CreatedAt"],
                            DeletedAt = reader.IsDBNull(reader.GetOrdinal("DeletedAt")) ? (DateTime?)null : (DateTime)reader["DeletedAt"],
                            Email = (string)reader["Email"],
                            EmailConfirmed = (bool)reader["EmailConfirmed"],
                            PasswordHash = reader.IsDBNull(reader.GetOrdinal("PasswordHash")) ? null : (string)reader["PasswordHash"],
                            SecurityStamp = reader.IsDBNull(reader.GetOrdinal("SecurityStamp")) ? null : (string)reader["SecurityStamp"],
                            PhoneNumber = reader.IsDBNull(reader.GetOrdinal("PhoneNumber")) ? null : (string)reader["PhoneNumber"],
                            PhoneNumberConfirmed = (bool)reader["PhoneNumberConfirmed"],
                            LockoutEndDateUtc = reader.IsDBNull(reader.GetOrdinal("LockoutEndDateUtc")) ? (DateTime?)null : (DateTime)reader["LockoutEndDateUtc"],
                            LockoutEnabled = (bool)reader["LockoutEnabled"],
                            AccessFailedCount = (int)reader["AccessFailedCount"],
                            UserName = (string)reader["UserName"],
                            Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? null : (string)reader["Address"]
                        };
                        users.Add(user);
                    }
                    return users;
                }
            }
        }

        public IList<City> GetCities()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(nameof(GetCities), connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = command.ExecuteReader();
                    List<City> cities = new List<City>();
                    while (reader.Read())
                    {
                        City city = new City
                        {
                            Id = (int)reader[nameof(City.Id)],
                            Guid = (Guid)reader[nameof(City.Guid)],
                            Name = (string)reader[nameof(City.Name)]
                        };
                        cities.Add(city);
                    }
                    return cities;
                }
            }
        }

        public void CreateApartment(Apartment apartment)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(nameof(CreateApartment), connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(nameof(Apartment.Id), SqlDbType.Int).Direction = ParameterDirection.Output;
                    command.Parameters.AddWithValue(nameof(Apartment.Guid), Guid.NewGuid());
                    command.Parameters.AddWithValue(nameof(Apartment.Name), apartment.Name);
                    command.Parameters.AddWithValue(nameof(Apartment.CreatedAt), DateTime.Now);
                    command.Parameters.AddWithValue(nameof(Apartment.Address), apartment.Address);
                    command.Parameters.AddWithValue(nameof(Apartment.Price), apartment.Price);
                    command.Parameters.AddWithValue(nameof(Apartment.MaxAdults), apartment.MaxAdults);
                    command.Parameters.AddWithValue(nameof(Apartment.MaxChildren), apartment.MaxChildren);
                    command.Parameters.AddWithValue(nameof(Apartment.BeachDistance), apartment.BeachDistance);
                    command.Parameters.AddWithValue(nameof(Apartment.Status), apartment.Status);
                    command.Parameters.AddWithValue(nameof(Apartment.TotalRooms), apartment.TotalRooms);

                    command.ExecuteNonQuery();
                    apartment.Id = (int)command.Parameters[nameof(Apartment.Id)].Value;


                    AddCityToApartment(apartment.Id, apartment.CityName);
                    if (apartment.Tags != null)
                    {
                        apartment.Tags.ToList().ForEach(t => AddTagToApartment(apartment.Id, t));
                    }
                    if (apartment.Images != null)
                    {
                        apartment.Images.ToList().ForEach(i => AddImageToApartment(apartment.Id, i));
                    }
                }

            }
        }

        public void AddCityToApartment(int apartmentId, string cityName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(nameof(AddCityToApartment), connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@ApartmentId", SqlDbType.Int).Value = apartmentId;
                    command.Parameters.Add(nameof(Apartment.CityName), SqlDbType.NVarChar, 50).Value = cityName;


                    command.ExecuteNonQuery();
                }
            }
        }

        public void AddImageToApartment(int apartmentId, Image image)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(nameof(AddImageToApartment), connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ApartmentId", apartmentId);
                    command.Parameters.AddWithValue(nameof(Image.Base64Content), image.Base64Content);
                    command.Parameters.AddWithValue(nameof(Image.Name), image.Name);
                    command.Parameters.AddWithValue(nameof(Image.CreatedAt), DateTime.Now);
                    command.Parameters.AddWithValue(nameof(Image.IsRepresentative), image.IsRepresentative);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void AddTagToApartment(int apartmentId, Tag tag)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("AddTagToApartment", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ApartmentId", apartmentId);
                    command.Parameters.AddWithValue("@TagId", tag.Id);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void CreateTag(string tagName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(nameof(CreateTag), connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@tagName", tagName);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteTag(int Id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(nameof(DeleteTag), connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(nameof(Tag.Id), Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteApartment(int apartmentId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(nameof(DeleteApartment), connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(nameof(Apartment.Id), apartmentId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateApartment(Apartment apartment)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(nameof(UpdateApartment), connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(nameof(Apartment.Id), apartment.Id);
                    command.Parameters.AddWithValue(nameof(Apartment.Name), apartment.Name);
                    command.Parameters.AddWithValue(nameof(Apartment.Address), apartment.Address);
                    command.Parameters.AddWithValue(nameof(Apartment.Price), apartment.Price);
                    command.Parameters.AddWithValue(nameof(Apartment.MaxAdults), apartment.MaxAdults);
                    command.Parameters.AddWithValue(nameof(Apartment.MaxChildren), apartment.MaxChildren);
                    command.Parameters.AddWithValue(nameof(Apartment.BeachDistance), apartment.BeachDistance);
                    command.Parameters.AddWithValue(nameof(Apartment.Status), apartment.Status);
                    command.Parameters.AddWithValue(nameof(Apartment.TotalRooms), apartment.TotalRooms);

                    command.ExecuteNonQuery();


                    AddCityToApartment(apartment.Id, apartment.CityName);
                    if (apartment.Tags != null)
                    {
                        apartment.Tags.ToList().ForEach(t => AddTagToApartment(apartment.Id, t));
                    }
                    if (apartment.Images != null)
                    {
                        DeleteImagesFromApartment(apartment.Images, apartment.Id);
                        apartment.Images.ToList().ForEach(i => AddImageToApartment(apartment.Id, i));
                    }
                }

            }
        }

        private void DeleteImagesFromApartment(IList<Image> images, int apartmentId)
        {
            DataTable imageIds = new DataTable();
            imageIds.Columns.Add("Id", typeof(int));

            foreach (Image image in images)
            {
                imageIds.Rows.Add(image.Id);
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("DeleteImagesFromApartment", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ApartmentId", apartmentId);
                    command.Parameters.AddWithValue("@ImageIds", imageIds).SqlDbType = SqlDbType.Structured;

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
