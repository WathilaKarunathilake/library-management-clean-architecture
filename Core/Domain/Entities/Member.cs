using System;

namespace LibraryManagementCleanArchitecture.Domain.Entities
{
    public abstract class Member
    {
        private string? name;
        private Guid memberID;

        public Guid MemberID {  get { return memberID; } }

        public string? Name
        {
            get { return name; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    name = value;
                else
                    throw new ArgumentException("Name cannot be empty.");
            }
        }

        public Member()
        {

        }

        public Member(Guid id, string name)
        {
            Name = name;
            this.memberID = id;
        }

        abstract public bool CanBorrow();
        abstract public bool CanViewBooks();

    }
}
