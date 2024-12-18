using Domain.Validation;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public sealed class Contact : Entity
    {
        public string? Name { get; private set; }
        public string? Email { get; private set; }
        public string? Phone { get; private set; }
        public bool? IsActive { get; private set; }


        public Contact(string name, string email, string phone, bool? isActive)
        {
            ValidateDomain(name, email, phone, isActive);
        }

        [JsonConstructor]
        public Contact(int id, string name, string email, string phone, bool? isActive)
        {
            DomainValidation.When(id < 0, "Invalid Value!");
            Id = id;
            ValidateDomain(name, email, phone, isActive);
        }

        private void ValidateDomain(string name, string email, string phone, bool? isActive)
        {

            DomainValidation.When(string.IsNullOrWhiteSpace(name), "The name cannot be empty or null.");
            DomainValidation.When(name.Length < 3, "The name must be at least 3 characters long.");
            DomainValidation.When(name.Length > 100, "The name cannot be longer than 100 characters.");

            DomainValidation.When(string.IsNullOrWhiteSpace(email), "The email cannot be empty or null.");
            DomainValidation.When(!IsValidEmail(email), "The provided email is invalid.");

            DomainValidation.When(string.IsNullOrWhiteSpace(phone), "The phone number cannot be empty or null.");
            DomainValidation.When(phone.Length < 10 || phone.Length > 15, "The phone number must be between 10 and 15 characters long.");
            DomainValidation.When(!phone.All(char.IsDigit), "The phone number must contain only numbers.");

            DomainValidation.When(isActive == null, "The active state (IsActive) must be specified.");


            Name = name;
            Email = email;
            Phone = phone;
            IsActive = isActive;
        }


        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public void Update(string name, string email, string phone, bool? isActive)
        {
            if (!string.Equals(Name, name, StringComparison.OrdinalIgnoreCase)) Name = name;
            if (!string.Equals(Email, email, StringComparison.OrdinalIgnoreCase)) Email = email;
            if (!string.Equals(Phone, phone, StringComparison.OrdinalIgnoreCase)) Phone = phone;
            if (IsActive != isActive) IsActive = isActive;
        }


    }
}
