using ContactSystem.Application.Entities;

namespace ContactSystem.Infrastructure;

public static class TestDataSeeder
{
    public static void SeedWithTestData(GraniteDataContext dbContext)
    {
        var offices = new List<OfficeEntity>
        {
            new() { Id = Guid.NewGuid(), Name = "San Francisco HQ" },
            new() { Id = Guid.NewGuid(), Name = "Berlin" },
            new() { Id = Guid.NewGuid(), Name = "Bangalore" },
            new() { Id = Guid.NewGuid(), Name = "Toronto" },
            new() { Id = Guid.NewGuid(), Name = "Tel Aviv" },
            new() { Id = Guid.NewGuid(), Name = "London" },
            new() { Id = Guid.NewGuid(), Name = "Shanghai" },
            new() { Id = Guid.NewGuid(), Name = "Sydney" },
            new() { Id = Guid.NewGuid(), Name = "Amsterdam" },
            new() { Id = Guid.NewGuid(), Name = "Paris" },
            new() { Id = Guid.NewGuid(), Name = "Seoul" },
            new() { Id = Guid.NewGuid(), Name = "Stockholm" },
            new() { Id = Guid.NewGuid(), Name = "São Paulo" },
            new() { Id = Guid.NewGuid(), Name = "Dubai" },
            new() { Id = Guid.NewGuid(), Name = "Mexico City" }
        };

        offices.ForEach(o => dbContext.Offices.Add(o));

        int EmployeeCount(string city) => city switch
        {
            "San Francisco HQ" => 33,
            "Berlin" => 7,
            "Bangalore" => 15,
            "Toronto" => 6,
            "Tel Aviv" => 5,
            "London" => 6,
            "Shanghai" => 4,
            "Sydney" => 4,
            "Amsterdam" => 5,
            "Paris" => 5,
            "Seoul" => 4,
            "Stockholm" => 3,
            "São Paulo" => 3,
            "Dubai" => 3,
            "Mexico City" => 2,
            _ => 0
        };

        var namesByRegion = new Dictionary<string, (string[] FirstNames, string[] LastNames)>
        {
            ["San Francisco HQ"] = (new[] { "Liam", "Emma", "Noah", "Olivia", "Ava", "Sophia", "Mason", "Isabella", "Ethan", "Mia" }, new[] { "Smith", "Johnson", "Williams", "Brown", "Jones" }),
            ["Berlin"] = (new[] { "Leon", "Marie", "Ben", "Sophie", "Paul", "Anna", "Lukas" }, new[] { "Müller", "Schmidt", "Schneider", "Fischer", "Weber" }),
            ["Bangalore"] = (new[] { "Arjun", "Ananya", "Ravi", "Priya", "Karan", "Sneha", "Vikram", "Deepa" }, new[] { "Sharma", "Patel", "Reddy", "Gupta", "Kumar" }),
            ["Toronto"] = (new[] { "Jack", "Charlotte", "Liam", "Amelia", "Benjamin", "Ella" }, new[] { "Brown", "Wilson", "Taylor", "Anderson", "Martin" }),
            ["Tel Aviv"] = (new[] { "David", "Yael", "Noam", "Maya", "Eitan" }, new[] { "Levi", "Cohen", "Mizrahi", "Peretz", "Barak" }),
            ["London"] = (new[] { "Oliver", "Amelia", "George", "Isla", "Harry", "Emily" }, new[] { "Smith", "Jones", "Taylor", "Brown", "Williams" }),
            ["Shanghai"] = (new[] { "Wei", "Li", "Hua", "Fang" }, new[] { "Wang", "Li", "Zhang", "Liu", "Chen" }),
            ["Sydney"] = (new[] { "Jack", "Charlotte", "Noah", "Amelia" }, new[] { "Smith", "Jones", "Williams", "Brown" }),
            ["Amsterdam"] = (new[] { "Daan", "Emma", "Lucas", "Sophie" }, new[] { "de Jong", "Jansen", "de Vries", "van den Berg" }),
            ["Paris"] = (new[] { "Lucas", "Emma", "Gabriel", "Louise" }, new[] { "Martin", "Bernard", "Dubois", "Thomas" }),
            ["Seoul"] = (new[] { "Minjun", "Seo-yeon", "Ji-hoon", "Ha-eun" }, new[] { "Kim", "Lee", "Park", "Choi" }),
            ["Stockholm"] = (new[] { "Lucas", "Maja", "Erik" }, new[] { "Johansson", "Andersson", "Karlsson" }),
            ["São Paulo"] = (new[] { "Miguel", "Ana", "Lucas" }, new[] { "Silva", "Santos", "Oliveira" }),
            ["Dubai"] = (new[] { "Ahmed", "Fatima", "Mohammed" }, new[] { "Al Farsi", "Al Mazrouei", "Al Nahyan" }),
            ["Mexico City"] = (new[] { "Sofia", "Mateo" }, new[] { "Hernandez", "Lopez" })
        };

        var random = new Random();
        int idCounter = 1;
        var multiOfficeEmployeeIds = new List<Guid>();

        IEnumerable<OfficeEntity> GetRandomOtherOffices(OfficeEntity current)
        {
            var others = offices.Where(o => o.Id != current.Id).OrderBy(_ => random.Next()).Take(2);
            return others;
        }

        foreach (var office in offices)
        {
            var count = EmployeeCount(office.Name);
            var (firstNames, lastNames) = namesByRegion[office.Name];

            for (int j = 0; j < count; j++, idCounter++)
            {
                var contactId = Guid.NewGuid();

                if (multiOfficeEmployeeIds.Count < 5)
                    multiOfficeEmployeeIds.Add(contactId);

                var contactOffices = new List<ContactOfficeRelation>
                {
                    new() { ContactId = contactId, OfficeId = office.Id }
                };

                if (multiOfficeEmployeeIds.Contains(contactId))
                    contactOffices.AddRange(GetRandomOtherOffices(office).Select(o => new ContactOfficeRelation { ContactId = contactId, OfficeId = o.Id }));

                var firstName = firstNames[idCounter % firstNames.Length];
                var lastName = lastNames[idCounter % lastNames.Length];

                dbContext.Contacts.Add(new ContactEntity
                {
                    Id = contactId,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = $"{firstName.ToLower()}.{lastName.ToLower().Replace(" ", "")}{idCounter}@example.com",
                    ContactOffices = contactOffices
                });
            }
        }
    }
}
