namespace BusinessLogicTier.Migrations
{
    using DataAccesLayer.Context;
    using DataAccesLayer.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Diagnostics;
    internal sealed class Configuration : DbMigrationsConfiguration<TitanNetworkContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "BusinessLogicTier.DataAccesLayer.Context.TitanNetworkContext";
        }

        protected override void Seed(TitanNetworkContext context)
        {
            /*try
            {
                var users = new List<User>();
                var messages = new List<Message>();
                var chats = new List<Chat>();
                var rooms = new List<Room>();

                users.Add(new User()
                {
                    FirstName = "John",
                    MidleName = "Dick",
                    LastName = "Doe",
                    UserName = "darkstalker97",
                    Age = 18,
                    About = "Play dota all day"
                });

                users.Add(new User()
                {
                    FirstName = "Xavier",
                    MidleName = "Gradle",
                    LastName = "Jinjer",
                    UserName = "lolipop",
                    Age = 22,
                    About = "Basketball, guitar"
                });

                users.Add(new User()
                {
                    FirstName = "Pro100",
                    MidleName = "Vasya",
                    LastName = "Pupkin",
                    UserName = "vovo4ka23",
                    Age = 11,
                    About = "Go to wkola"
                });
                users.Add(new User()
                {
                    FirstName = "Petro",
                    MidleName = "Andriyovich",
                    LastName = "Poroshenko",
                    UserName = "petka123",
                    Age = 22,
                    About = "Ya klasniy"
                });
                users.Add(new User()
                {
                    FirstName = "Vasiok",
                    MidleName = "Monster",
                    LastName = "Baraban",
                    UserName = "Badumtss",
                    Age = 44,
                    About = "Hochu piwa"
                });
                users.ForEach(user => context.Users.Add(user));
                context.SaveChanges();

                chats.Add(new Chat()
                {
                    Title = "Chatik"
                });
                chats.Add(new Chat()
                {
                    Title = "Chatik dlia pacanov"
                });
                chats.Add(new Chat()
                {
                    Title = "Chat"
                });
                chats.ForEach(chat => context.Chats.Add(chat));
                context.SaveChanges();

                rooms.Add(new Room()
                {
                    UserId = 1,
                    ChatId = 1
                });
                rooms.Add(new Room()
                {
                    UserId = 2,
                    ChatId = 1
                });
                rooms.Add(new Room()
                {
                    UserId = 3,
                    ChatId = 1
                });
                rooms.Add(new Room()
                {
                    UserId = 4,
                    ChatId = 2
                });
                rooms.Add(new Room()
                {
                    UserId = 5,
                    ChatId = 2
                });
                rooms.Add(new Room()
                {
                    UserId = 3,
                    ChatId = 2
                });
                rooms.ForEach(room => context.Rooms.Add(room));
                context.SaveChanges();

                messages.Add(new Message()
                {
                    OldContent = "privet ya 1",
                    NewContent = "privet ya 1",
                    SendDate = DateTime.Now,
                    UserId = 1,
                    ChatId = 1
                });

                messages.Add(new Message()
                {
                    OldContent = "privet ya 2",
                    NewContent = "privet ya 2",
                    SendDate = DateTime.Now,
                    UserId = 2,
                    ChatId = 1
                });
                messages.Add(new Message()
                {
                    OldContent = "privet ya 3",
                    NewContent = "privet ya 3",
                    SendDate = DateTime.Now,
                    UserId = 3,
                    ChatId = 1
                });
                messages.Add(new Message()
                {
                    OldContent = "privet ya 4",
                    NewContent = "privet ya 4",
                    SendDate = DateTime.Now,
                    UserId = 4,
                    ChatId = 2
                });
                messages.Add(new Message()
                {
                    OldContent = "privet ya 5",
                    NewContent = "privet ya 5",
                    SendDate = DateTime.Now,
                    UserId = 5,
                    ChatId = 2
                });
                messages.Add(new Message()
                {
                    OldContent = "privet ya 3",
                    NewContent = "privet ya 3",
                    SendDate = DateTime.Now,
                    UserId = 3,
                    ChatId = 2
                });
                messages.ForEach(message => context.Messages.Add(message));
                context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }
            */
            base.Seed(context);
        }
    }
}
