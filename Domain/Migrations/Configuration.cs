﻿namespace Domain.Migrations
{
    using Domain.Models;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<AcademyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AcademyContext context)
        {
            return;
            var teacher1 = new Teacher { Name = "Лазарев А.С." };
            var teacher2 = new Teacher { Name = "Степанович О.Н." };
            var teacher3 = new Teacher { Name = "Рыжова Н.С." };
            var teacher4 = new Teacher { Name = "Самойлова И.А." };
            var teacher5 = new Teacher { Name = "Воронов Е.В." };
            var teacher6 = new Teacher { Name = "Круглова У.А." };
            var teacher7 = new Teacher { Name = "Смирнов П.С." };
            var teacher8 = new Teacher { Name = "Мельник В.Н." };
            var teacher9 = new Teacher { Name = "Щеглова А.А." };
            var teacher10 = new Teacher { Name = "Иванов Д.Н." };
            var teacher11 = new Teacher { Name = "Демидова Е.А." };
            var teacher12 = new Teacher { Name = "Сечин В.С." };

            context.Subjects.Add(new Subject { Name = "Экономика", Teacher = teacher1 });
            context.Subjects.Add(new Subject { Name = "Английский язык", Teacher = teacher2 });
            context.Subjects.Add(new Subject { Name = "Русский язык", Teacher = teacher3 });
            context.Subjects.Add(new Subject { Name = "География", Teacher = teacher4 });
            context.Subjects.Add(new Subject { Name = "История", Teacher = teacher4 });
            context.Subjects.Add(new Subject { Name = "Математика", Teacher = teacher5 });
            context.Subjects.Add(new Subject { Name = "Информатика", Teacher = teacher5 });
            context.Subjects.Add(new Subject { Name = "Физика", Teacher = teacher5 });
            context.Subjects.Add(new Subject { Name = "Курсы первой помощи", Teacher = teacher6 });
            context.Subjects.Add(new Subject { Name = "Биология", Teacher = teacher6 });
            context.Subjects.Add(new Subject { Name = "Экология", Teacher = teacher6 });
            context.Subjects.Add(new Subject { Name = "Химия", Teacher = teacher7 });
            context.Subjects.Add(new Subject { Name = "Этика", Teacher = teacher8 });
            context.Subjects.Add(new Subject { Name = "Право", Teacher = teacher8 });
            context.Subjects.Add(new Subject { Name = "Половое воспитание", Teacher = teacher8 });
            context.Subjects.Add(new Subject { Name = "Литература", Teacher = teacher9 });
            context.Subjects.Add(new Subject { Name = "Искусство", Teacher = teacher9 });
            context.Subjects.Add(new Subject { Name = "Культура", Teacher = teacher9 });
            context.Subjects.Add(new Subject { Name = "Философия", Teacher = teacher10 });
            context.Subjects.Add(new Subject { Name = "Религия", Teacher = teacher10 });
            context.Subjects.Add(new Subject { Name = "Психология", Teacher = teacher10 });
            context.Subjects.Add(new Subject { Name = "Ораторство", Teacher = teacher11 });
            context.Subjects.Add(new Subject { Name = "Политология", Teacher = teacher11 });
            context.Subjects.Add(new Subject { Name = "Труды (домоведение)", Teacher = teacher12 });
            context.Subjects.Add(new Subject { Name = "Довоенная подготовка", Teacher = teacher12 });

            List<string> days = new List<string> { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница" };
            List<string> hours = new List<string> { "08:00", "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00" };
            foreach (var day in days)
            {
                foreach (var hour in hours)
                {
                    context.Lessons.Add(new Lesson { Day = day, Start = hour });
                }
            }

            context.Groups.Add(new Group { Name = "1 класс" });
            context.Groups.Add(new Group { Name = "2 класс" });
            context.Groups.Add(new Group { Name = "3 класс" });
            context.Groups.Add(new Group { Name = "4 класс" });
            context.Groups.Add(new Group { Name = "5 класс" });
            context.Groups.Add(new Group { Name = "6 класс" });
            context.Groups.Add(new Group { Name = "7 класс" });
            context.Groups.Add(new Group { Name = "8 класс" });
            context.Groups.Add(new Group { Name = "9 класс" });
            context.Groups.Add(new Group { Name = "10 класс" });
            context.Groups.Add(new Group { Name = "11 класс" });
            context.Groups.Add(new Group { Name = "12 класс" });

        }
    }
}
