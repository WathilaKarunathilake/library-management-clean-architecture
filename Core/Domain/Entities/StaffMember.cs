using System;

namespace LibraryManagementCleanArchitecture.Domain.Entities
{
    public class StaffMember: Member
    {
        private string? staffType;

        public StaffMember(): base()
        {

        }

        public StaffMember(string name, string staffType)
            : base(name)
        {
            StaffType = staffType;
        }

        public string? StaffType
        {
            get => staffType;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Staff type cannot be empty.");

                string lower = value.ToLower();

                if (lower == "minor" || lower == "management")
                    staffType = value;
                else
                    throw new ArgumentException("Staff type must be minor or management.");

            }
        }

        public override bool CanBorrow()
        {
            return false;
        }

        public override bool CanViewBooks()
        {
            return staffType?.ToLower() == "management";
        }

        public override string ToString()
        {
            return $"\nName: {Name}\nType: Staff\nStaff Type: {staffType}\n";
        }
    }
}
