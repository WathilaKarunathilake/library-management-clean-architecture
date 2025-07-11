// <copyright file="Member.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.Domain.Entities
{
    public abstract class Member
    {
        private string? name;
        private Guid memberID;

        public Guid MemberID
        {
            get { return this.memberID; }
        }

        public string? Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.name = value;
                }
                else
                {
                    throw new ArgumentException("Name cannot be empty.");
                }
            }
        }

        public Member()
        {
        }

        public Member(Guid id, string name)
        {
            this.Name = name;
            this.memberID = id;
        }

        public abstract bool CanBorrow();

        public abstract bool CanViewBooks();

    }
}
