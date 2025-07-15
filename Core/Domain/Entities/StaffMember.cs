// <copyright file="StaffMember.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>
namespace LibraryManagementCleanArchitecture.Domain.Entities
{
    public class StaffMember : Member
    {
        private string? staffType;

        public StaffMember()
            : base()
        {
        }

        public StaffMember(Guid id, string name, string staffType)
            : base(id, name)
        {
            this.StaffType = staffType;
        }

        public string? StaffType
        {
            get => this.staffType;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Staff type cannot be empty.");
                }

                string lower = value.ToLower();

                if (lower == "minor" || lower == "management")
                {
                    this.staffType = value;
                }
                else
                {
                    throw new ArgumentException("Staff type must be minor or management.");
                }
            }
        }

        public override bool CanBorrow()
        {
            return false;
        }

        public override bool CanViewBooks()
        {
            return this.staffType?.ToLower() == "management";
        }

        public override string ToString()
        {
            return $"\nName: {this.Name}\nType: Staff\nStaff Type: {this.staffType}\n";
        }
    }
}
