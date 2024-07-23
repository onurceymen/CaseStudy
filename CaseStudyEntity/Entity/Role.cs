﻿namespace CaseStudyEntity.Entity
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
