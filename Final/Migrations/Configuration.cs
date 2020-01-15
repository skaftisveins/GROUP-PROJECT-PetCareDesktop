namespace Final.Migrations
{
    using MyClasses.Entities;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Final.GroomingDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            //muna að breyta
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Final.GroomingDbContext context)
        {
            #region User Seed

            User u1 = new User
            {
                UserName = "Admin",
                Password = "Password",
                IsAdmin = true
               
            };

            User u2 = new User
            {
                UserName = "Employee",
                Password = "Password",
                IsAdmin = false

            };

            context.Users.AddOrUpdate(
                o => o.Id,
                u1,
                u2);

            context.SaveChanges();

            #endregion

            #region Person Seed

            Person p1 = new Person
            {

                FirstName = "Kári",
                LastName = "Stefánsson",
                Kennitala = "010141-2129",
                Email = "KariStef@Decode.is",
                PhoneNumber = "557-1337",
                Street = "Fagraþing 5",
                City = "Kópavogur",
                Zip = 203,
                Preferences = "Staðgreitt, Senda email ásamt sms",
                Notification = false
            };

            Person p2 = new Person
            {

                FirstName = "Nils",
                LastName = "Kristjánsson",
                Kennitala = "160585-3319",
                Email = "nkk1.h18@nemandi.ntv.is",
                PhoneNumber = "691-6057",
                Street = "Vesturberg 181",
                City = "Reykjavík",
                Zip = 800,
                Preferences = "Staðgreitt, vill að það sé hringt ekki sent sms",
                Notification = true
            };

            Person p3 = new Person
            {

                FirstName = "Snævar",
                LastName = "Karlsson",
                Kennitala = "150474-2229",
                Email = "Snjokall@isnet.is",
                PhoneNumber = "480-2729",
                Street = "Hringbraut 52",
                City = "Reykjavík",
                Zip = 104,
                Preferences = "Í reikning, hefur verið no show tvisvar",
                Notification = true
            };

            Person p4 = new Person
            {

                FirstName = "Ylfa",
                LastName = "Björgvinsdóttir",
                Kennitala = "130987-3739",
                Email = "ylfa@ylfa.is",
                PhoneNumber = "595-3030",
                Street = "Rauðarárstíg 45",
                City = "Reykjavík",
                Zip = 101,
                Preferences = "Á tvö stór fiskabúr, pantar þrif.",
                Notification = true
            };


            Person p5 = new Person
            {

                FirstName = "Hannes",
                LastName = "Sveinsson",
                Kennitala = "130645-3879",
                Email = "hannimanni@sveinsson.is",
                PhoneNumber = "567-5958",
                Street = "Þórsgata 4",
                City = "Reykjavík",
                Zip = 101,
                Preferences = "BirdMan, Pantar þrif á fuglabúrum.",
                Notification = true
            };

            Person p6 = new Person
            {

                FirstName = "Margrét",
                LastName = "Egilsdóttir",
                Kennitala = "050989-6469",
                Email = "magga14@isdn.is",
                PhoneNumber = "899-3589",
                Street = "Austurberg 78",
                City = "Reykjavík",
                Zip = 111,
                Preferences = "",
                Notification = true
            };

            Person p7 = new Person
            {

                FirstName = "Joanna",
                LastName = "Jedrzejczyk",
                Kennitala = "180987-2529",
                Email = "Joannachampion@gmail.com",
                PhoneNumber = "555-3345",
                Street = "Ægisíða 62",
                City = "Reykjavík",
                Zip = 107,
                Preferences = "Talar ekki Íslensku",
                Notification = true
            };

            Person p8 = new Person
            {

                FirstName = "Smári",
                LastName = "Gústafsson",
                Kennitala = "271158-8549",
                Email = "smaowgoo@slimey.is",
                PhoneNumber = "656-4649",
                Street = "Hjarðarhagi 12",
                City = "Reykjavík",
                Zip = 107,
                Preferences = "Talar bara í rími",
                Notification = true
            };

            Person p9 = new Person
            {

                FirstName = "Hallgerður",
                LastName = "Hermansdóttir",
                Kennitala = "240341-7369",
                Email = "",
                PhoneNumber = "585-3239",
                Street = "Fálkagata 42",
                City = "Reykjavík",
                Zip = 107,
                Preferences = "Öldruð kona, hefur áður gleymt að hún hefur pantað þjónustu",
                Notification = false
            };

            Person p10 = new Person
            {

                FirstName = "Þór",
                LastName = "Óðinsson",
                Kennitala = "061001-5549",
                Email = "thor@marvel.com",
                PhoneNumber = "555-3245",
                Street = "Valhúsabraut 18",
                City = "Seltjarnarnesi",
                Zip = 420,
                Preferences = "Borgar í gulli",
                Notification = false
            };

            Person p11 = new Person
            {

                FirstName = "Magnea",
                LastName = "Einarsdóttir",
                Kennitala = "010886-5279",
                Email = "Magnei@isnet.is",
                PhoneNumber = "541-2119",
                Street = "Hofteigur 10",
                City = "Reykjavík",
                Zip = 105,
                Preferences = "Vill fá minnis Email ásamt SMS tilkynningu.",
                Notification = true
            };


            Person p12 = new Person
            {

                FirstName = "Þórhallur",
                LastName = "Unnsteinsson",
                Kennitala = "090759-2329",
                Email = "Thorhallur@petco.is",
                PhoneNumber = "528-3138",
                Street = "Asparfell 9",
                City = "Reykjavík",
                Zip = 111,
                Preferences = "Starfsmaður",
                Notification = true
            };

            Person p13 = new Person
            {

                FirstName = "Emma",
                LastName = "Halldórsdóttir",
                Kennitala = "090778-6749",
                Email = "Emma@petco.is",
                PhoneNumber = "697-8789",
                Street = "Unufell 27",
                City = "Reykjavík",
                Zip = 111,
                Preferences = "Starfsmaður",
                Notification = true,
                IsListed = false
            };

            Person p14 = new Person
            {

                FirstName = "Skafti",
                LastName = "Sveinsson",
                Kennitala = "301284-2339",
                Email = "skaftisveins@gmail.com",
                PhoneNumber = "848-5184",
                Street = "Bjarkavellir 1b",
                City = "Hafnarfjörður",
                Zip = 221,
                Preferences = "Staðgreitt, vill að það sé hringt ekki sent sms",
                Notification = true,
            };

            Person p15 = new Person
            {

                FirstName = "Rip",
                LastName = "",
                Kennitala = "010101-0101",
                Email = "Rip@rip.gov",
                PhoneNumber = "555-5555",
                Street = "Fossvogs Kirkjugarður",
                City = "Reykjavik",
                Zip = 105,
                Preferences = "Dummy",
                Notification = false,
            };





            context.People.AddOrUpdate(
                o => o.Id,
                p1,
                p2,
                p3,
                p4,
                p5,
                p6,
                p7,
                p8,
                p9,
                p10,
                p11,
                p12,
                p13,
                p14,
                p15);


            context.SaveChanges();

            #endregion

            #region Species Seed

            Species sp1 = new Species
            {
                SpeciesName = "Dog"
            };

            Species sp2 = new Species
            {
                SpeciesName = "Cat"
            };

            Species sp3 = new Species
            {
                SpeciesName = "Fish"
            };

            Species sp4 = new Species
            {
                SpeciesName = "Bird"
            };

            context.Species.AddOrUpdate(
                o => o.Id,
                sp1,
                sp2,
                sp3,
                sp4);

            context.SaveChanges();

            #endregion

            #region Breeds Seed


            Breed b1 = new Breed
            {
                Species = sp1,
                BreedName = "Scheffer"
            };

            Breed b2 = new Breed
            {
                Species = sp1,
                BreedName = "Boxer"
            };

            Breed b3 = new Breed
            {
                Species = sp1,
                BreedName = "Doberman"
            };

            Breed b4 = new Breed
            {
                Species = sp2,
                BreedName = "Maine Coon"
            };

            Breed b5 = new Breed
            {
                Species = sp2,
                BreedName = "Russian Blue"
            };

            Breed b6 = new Breed
            {
                Species = sp2,
                BreedName = "Siamese Cat"
            };

            Breed b7 = new Breed
            {
                Species = sp2,
                BreedName = "Bengal"
            };

            Breed b8 = new Breed
            {
                Species = sp2,
                BreedName = "Chartreux"
            };

            Breed b9 = new Breed
            {
                Species = sp3,
                BreedName = "Oranda"
            };

            Breed b10 = new Breed
            {
                Species = sp3,
                BreedName = "Fantail"
            };

            Breed b11 = new Breed
            {
                Species = sp3,
                BreedName = "Shubunkin"
            };

            Breed b12 = new Breed
            {
                Species = sp3,
                BreedName = "Lionhead"
            };

            Breed b13 = new Breed
            {
                Species = sp3,
                BreedName = "Tamasaba"
            };

            Breed b14 = new Breed
            {
                Species = sp4,
                BreedName = "Cockatoo"
            };

            Breed b15 = new Breed
            {
                Species = sp4,
                BreedName = "Lovebird"
            };

            Breed b16 = new Breed
            {
                Species = sp4,
                BreedName = "Nestor"
            };

            Breed b17 = new Breed
            {
                Species = sp4,
                BreedName = "Red-Fan Parrot"
            };

            Breed b18 = new Breed
            {
                Species = sp4,
                BreedName = "Macaw"
            };

            Breed b19 = new Breed
            {
                Species = sp1,
                BreedName = "Bordercollie"
            };

            Breed b20 = new Breed
            {
                Species = sp1,
                BreedName = "Chihuahua"
            };

            context.Breeds.AddOrUpdate(
                o => o.Id,
                b1,
                b2,
                b3,
                b4,
                b5,
                b6,
                b7,
                b8,
                b9,
                b10,
                b11,
                b12,
                b13,
                b14,
                b15,
                b16,
                b17,
                b18,
                b19,
                b20);

            context.SaveChanges();

            #endregion

            #region Pet Seed


            Pet pet1 = new Pet
            {
                Owner = p1,
                Species = sp1,
                Breed = b1,
                Name = "Jón Hreggs",
                Age = 7,
                Gender = "Male",
                Allergy = "Súkkulaði",
                Health = "Gigt í mjöðm.",
                Preferences = "Létt snyrta hundinn, muna að klippa neglur.",
                EmployeeNotes = "Hundurinn bítur."

            };

            Pet pet2 = new Pet
            {
                Owner = p1,
                Species = sp1,
                Breed = b3,
                Name = "Bolt",
                Age = 3,
                Gender = "Male",
                Allergy = "Sjampó",
                Health = "",
                Preferences = "Klippa neglur, þrífa eyru vel.",
                EmployeeNotes = "Viðkvæm eyru þau eru stífð."

            };

            Pet pet3 = new Pet
            {
                Owner = p2,
                Species = sp1,
                Breed = b2,
                Name = "Baron",
                Age = 13,
                Gender = "Male",
                Allergy = "",
                Health = "Lamaður í hægri afturfæti.",
                Preferences = "Baða hundinn með sápu, verður að skolast mjög vel.",
                EmployeeNotes = "Reynir allt til að stinga af úr þvottinum."

            };

            Pet pet4 = new Pet
            {
                Owner = p3,
                Species = sp2,
                Breed = b4,
                Name = "Lotta",
                Age = 3,
                Gender = "Female",
                Allergy = "",
                Health = "Blind á hægra auga.",
                Preferences = "Snyrta vel, þrífa og klippa klær.",
                EmployeeNotes = "Klórar mjög ef að eigandinn er ekki viðstattur þegar klær eru klipptar."

            };

            Pet pet5 = new Pet
            {
                Owner = p3,
                Species = sp2,
                Breed = b5,
                Name = "Kisi",
                Age = 12,
                Gender = "Male",
                Allergy = "Sjampó",
                Health = "",
                Preferences = "Nota sjampó með engum ylmefnum",
                EmployeeNotes = ""

            };

            Pet pet6 = new Pet
            {
                Owner = p3,
                Species = sp2,
                Breed = b8,
                Name = "Sir Mjálmi",
                Age = 13,
                Gender = "Male",
                Allergy = "",
                Health = "",
                Preferences = "Klippa á hann ljónsmakka",
                EmployeeNotes = ""

            };

            Pet pet7 = new Pet
            {
                Owner = p4,
                Species = sp3,
                Breed = b9,
                Name = "Elías",
                Age = 1,
                Gender = "Male",
                Allergy = "",
                Health = "",
                Preferences = "Þrífa búrið, gæta þess að sýrustig sé 7.2 - 7.6",
                EmployeeNotes = "Gæta vel að sýjunni hún stíflast auðveldlega."

            };

            Pet pet8 = new Pet
            {
                Owner = p4,
                Species = sp3,
                Breed = b9,
                Name = "Jóhannes",
                Age = 1,
                Gender = "Male",
                Allergy = "",
                Health = "",
                Preferences = "Þrífa búrið, gæta þess að sýrustig sé 7.2 - 7.6",
                EmployeeNotes = "Gæta vel að sýjunni hún stíflast auðveldlega."

            };

            Pet pet9 = new Pet
            {
                Owner = p4,
                Species = sp3,
                Breed = b10,
                Name = "Sigmundur",
                Age = 3,
                Gender = "Male ",
                Allergy = "",
                Health = "",
                Preferences = "Þrífa búrið, gæta þess að sýrustig sé 7.2 - 7.6",
                EmployeeNotes = "Í stærra búrinu sem má finna inni í stofu."

            };

            Pet pet10 = new Pet
            {
                Owner = p4,
                Species = sp3,
                Breed = b11,
                Name = "Kafari",
                Age = 1,
                Gender = "Male",
                Allergy = "",
                Health = "",
                Preferences = "Þrífa búrið, gæta þess að sýrustig sé 7.2 - 7.6",
                EmployeeNotes = "Í stærra búrinu sem má finna inni í stofu, Syndir óreglulega og á glerið"

            };

            Pet pet11 = new Pet
            {
                Owner = p4,
                Species = sp3,
                Breed = b12,
                Name = "SwimJamJimJam",
                Age = 1,
                Gender = "Male",
                Allergy = "",
                Health = "",
                Preferences = "Þrífa búrið, gæta þess að sýrustig sé 7.2 - 7.6",
                EmployeeNotes = "Í stærra búrinu sem má finna inni í stofu."

            };

            Pet pet12 = new Pet
            {
                Owner = p4,
                Species = sp3,
                Breed = b13,
                Name = "Bubbles",
                Age = 3,
                Gender = "Male",
                Allergy = "",
                Health = "",
                Preferences = "Þrífa búrið, gæta þess að sýrustig sé 7.2 - 7.6",
                EmployeeNotes = "Í stærra búrinu sem má finna inni í stofu."

            };

            Pet pet13 = new Pet
            {
                Owner = p5,
                Species = sp4,
                Breed = b14,
                Name = "Icarus",
                Age = 5,
                Gender = "Male",
                Allergy = "",
                Health = "",
                Preferences = "Þrífa búrið, fara vel yfir að allt dót sé vel fest við rimla.",
                EmployeeNotes = "Fuglarnir fá að fljúga um íbúðina það er best að hleypa þeim bara út og taka búrið alveg í sundur."

            };

            Pet pet14 = new Pet
            {
                Owner = p5,
                Species = sp4,
                Breed = b16,
                Name = "Skittles",
                Age = 8,
                Gender = "Male",
                Allergy = "Skittles",
                Health = "Offeitur",
                Preferences = "Þrífa búrið, fara vel yfir að allt dót sé vel fest við rimla.",
                EmployeeNotes = "Fuglarnir fá að fljúga um íbúðina það er best að hleypa þeim bara út og taka búrið alveg í sundur."

            };

            Pet pet15 = new Pet
            {
                Owner = p6,
                Species = sp1,
                Breed = b2,
                Name = "Cesar",
                Age = 5,
                Gender = "Male",
                Allergy = "Pensilín",
                Health = "Fær auðveldlega eyrna sýkingu",
                Preferences = "Þrífa eyrun vel",
                EmployeeNotes = ""

            };


            Pet pet16 = new Pet
            {
                Owner = p7,
                Species = sp2,
                Breed = b5,
                Name = "Rasputin",
                Age = 9,
                Gender = "Male",
                Allergy = "",
                Health = "Hefur fengið orma",
                Preferences = "Snyrta köttinn en láta klærnar eiga sig",
                EmployeeNotes = "Klórar og bítur"

            };


            Pet pet17 = new Pet
            {
                Owner = p8,
                Species = sp1,
                Breed = b1,
                Name = "Rex",
                Age = 1,
                Gender = "Male",
                Allergy = "",
                Health = "",
                Preferences = "Klippa hann mjög stutt, vex hratt aftur",
                EmployeeNotes = "Erfitt að fá hann til að vera kyrran, mjög leiksamur"

            };


            Pet pet18 = new Pet
            {
                Owner = p9,
                Species = sp4,
                Breed = b18,
                Name = "Rio",
                Age = 4,
                Gender = "Male",
                Allergy = "",
                Health = "",
                Preferences = "Þrífa búrið, fara vel yfir að allt dót sé vel fest við rimla.",
                EmployeeNotes = "Taka með búr til að setja fuglinn í á meðan unnið er á búrinu."

            };


            Pet pet19 = new Pet
            {
                Owner = p10,
                Species = sp1,
                Breed = b1,
                Name = "Tanngrisnir",
                Age = 10,
                Gender = "Male",
                Allergy = "",
                Health = "",
                Preferences = "Snyrta vel",
                EmployeeNotes = "Ég er ekki viss um að hundar eigi að hafa horn."

            };

            Pet pet20 = new Pet
            {
                Owner = p10,
                Species = sp1,
                Breed = b1,
                Name = "Tanngnjóstr",
                Age = 10,
                Gender = "Male",
                Allergy = "",
                Health = "",
                Preferences = "Snyrta vel",
                EmployeeNotes = "Ég er ekki viss um að hundar eigi að hafa horn."

            };

            Pet pet21 = new Pet
            {
                Owner = p11,
                Species = sp2,
                Breed = b6,
                Name = "Reykur",
                Age = 5,
                Gender = "Male",
                Allergy = "Sjampó, öll ylmefni",
                Health = "",
                Preferences = "Snyrta vel, sá sem sér um þennan kött getur ekki verið með rakspíra eða ylmvatn á sér",
                EmployeeNotes = "Mjög viðkvæmur fyrir lyktum."

            };

            Pet pet22 = new Pet
            {
                Owner = p14,
                Species = sp1,
                Breed = b19,
                Name = "Hekla",
                Age = 9,
                Gender = "Female",
                Allergy = "Fiskur",
                Health = "",
                Preferences = "Mjög þæg",
                EmployeeNotes = "Getur verið hrædd í rakstri",
                IsListed = false
            };

            Pet pet23 = new Pet
            {
                Owner = p14,
                Species = sp1,
                Breed = b20,
                Name = "Mía",
                Age = 8,
                Gender = "Female",
                Allergy = "",
                Health = "",
                Preferences = "Getur verið erfitt að klippa neglur",
                EmployeeNotes = "Getur verið geltin",
                IsListed = false
            };

            context.Pets.AddOrUpdate(
                o => o.Id,
                pet1,
                pet2,
                pet3,
                pet4,
                pet5,
                pet6,
                pet7,
                pet8,
                pet9,
                pet10,
                pet11,
                pet12,
                pet13,
                pet14,
                pet15,
                pet16,
                pet17,
                pet18,
                pet19,
                pet20,
                pet21,
                pet22,
                pet23);


            context.SaveChanges();

            #endregion

            #region Service Seed

            Service s1 = new Service
            {
                ServiceType = "Full Snyrting",
                Species = sp1,
                HomeCare = false
            };

            Service s2 = new Service
            {
                ServiceType = "Þrif",
                Species = sp1,
                HomeCare = false
            };

            Service s3 = new Service
            {
                ServiceType = "Full Snyrting",
                Species = sp2,
                HomeCare = false
            };

            Service s4 = new Service
            {
                ServiceType = "Þrif",
                Species = sp2,
                HomeCare = false
            };

            Service s5 = new Service
            {
                ServiceType = "Þrif á búri",
                Species = sp3,
                HomeCare = true
            };

            Service s6 = new Service
            {
                ServiceType = "Full yfirferð á slöngum og dælu",
                Species = sp3,
                HomeCare = true
            };

            Service s7 = new Service
            {
                ServiceType = "Þrif á búri",
                Species = sp4,
                HomeCare = true
            };

            Service s8 = new Service
            {
                ServiceType = "Full Snyrting",
                Species = sp4,
                HomeCare = true
            };



            context.Services.AddOrUpdate(
                o => o.Id,
                s1,
                s2,
                s3,
                s4,
                s5,
                s6,
                s7,
                s8);


            context.SaveChanges();

            #endregion

            #region Appointment Seed

            Appointment a1 = new Appointment
            {
                Person = p1,
                Pet = pet1,
                Service = s1,
                BookedDate = DateTime.UtcNow.AddDays(15).Date,

            };

            Appointment a2 = new Appointment
            {
                Person = p1,
                Pet = pet2,
                Service = s1,
                BookedDate = DateTime.UtcNow.AddDays(15).Date,

            };


            Appointment a3 = new Appointment
            {
                Person = p2,
                Pet = pet3,
                Service = s1,
                BookedDate = DateTime.UtcNow.AddDays(11).Date,

            };

            Appointment a4 = new Appointment
            {
                Person = p3,
                Pet = pet4,
                Service = s3,
                BookedDate = DateTime.UtcNow.AddDays(1).Date,

            };


            Appointment a5 = new Appointment
            {
                Person = p3,
                Pet = pet5,
                Service = s3,
                BookedDate = DateTime.UtcNow.AddDays(1).Date,

            };

            Appointment a6 = new Appointment
            {
                Person = p3,
                Pet = pet6,
                Service = s3,
                BookedDate = DateTime.UtcNow.AddDays(1).Date,

            };

            Appointment a7 = new Appointment
            {
                Person = p4,
                Pet = pet7,
                Service = s5,
                BookedDate = DateTime.UtcNow.AddDays(4).Date,

            };

            Appointment a8 = new Appointment
            {
                Person = p4,
                Pet = pet9,
                Service = s6,
                BookedDate = DateTime.UtcNow.AddDays(4).Date,

            };

            Appointment a9 = new Appointment
            {
                Person = p5,
                Pet = pet13,
                Service = s5,
                BookedDate = DateTime.UtcNow.AddDays(4).Date,

            };

            Appointment a10 = new Appointment
            {
                Person = p5,
                Pet = pet14,
                Service = s6,
                BookedDate = DateTime.UtcNow.AddDays(4).Date,

            };

            Appointment a11 = new Appointment
            {
                Person = p6,
                Pet = pet15,
                Service = s1,
                BookedDate = DateTime.UtcNow.AddDays(24).Date,

            };

            Appointment a12 = new Appointment
            {
                Person = p7,
                Pet = pet16,
                Service = s3,
                BookedDate = DateTime.UtcNow.AddDays(15).Date,

            };

            Appointment a13 = new Appointment
            {
                Person = p8,
                Pet = pet17,
                Service = s1,
                BookedDate = DateTime.UtcNow.AddDays(0).Date,

            };

            Appointment a14 = new Appointment
            {
                Person = p9,
                Pet = pet18,
                Service = s7,
                BookedDate = DateTime.UtcNow.AddDays(4).Date,

            };

            Appointment a15 = new Appointment
            {
                Person = p10,
                Pet = pet19,
                Service = s1,
                BookedDate = DateTime.UtcNow.AddDays(0).Date,
                Completed = true

            };

            Appointment a16 = new Appointment
            {
                Person = p11,
                Pet = pet21,
                Service = s3,
                BookedDate = DateTime.UtcNow.AddDays(0).Date,
                Completed = true

            };

            Appointment a17 = new Appointment
            {
                Person = p14,
                Pet = pet22,
                Service = s1,
                BookedDate = DateTime.UtcNow.AddDays(0).Date,
                Islisted = false
            };

            Appointment a18 = new Appointment
            {
                Person = p14,
                Pet = pet23,
                Service = s1,
                BookedDate = DateTime.UtcNow.AddDays(0).Date,
                Islisted = false
            };




            context.Appointments.AddOrUpdate(
                o => o.Id,
                a1,
                a2,
                a3,
                a4,
                a5,
                a6,
                a7,
                a8,
                a9,
                a10,
                a11,
                a12,
                a13,
                a14,
                a15,
                a16,
                a17,
                a18);


            context.SaveChanges();

            #endregion

        }
    }
}
