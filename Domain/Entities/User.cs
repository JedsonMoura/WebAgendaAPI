using Domain.Validation;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class User : Entity
    {
        public string? Username { get; private set; }
        public string? Password { get; private set; }



        public User(string username, string password)
        {
            ValidateDomain(username, password);
        }

        [JsonConstructor]
        public User(int id, string username, string password)
        {
            DomainValidation.When(id < 0, "Invalid Value!");
            Id = id;
            ValidateDomain(username, password);
        }

        private void ValidateDomain(string username, string password)
        {

            DomainValidation.When(string.IsNullOrWhiteSpace(username), "The name cannot be empty or null.");
            DomainValidation.When(password.Length < 3, "The name must be at least 3 characters long.");


            Username = username;
            Password = password;
        }

        public void Update(string username, string password)
        {
            if (!string.Equals(Username, username, StringComparison.OrdinalIgnoreCase)) Username = username;
            if (!string.Equals(Password, password, StringComparison.OrdinalIgnoreCase)) Password = password;
            
        }

        public void SetPasswordHash(string hashedPassword)
        {
            Password = hashedPassword;
        }
    }
}
