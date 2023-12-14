﻿using Microsoft.EntityFrameworkCore;
using Music_Portal.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Music_Portal.DAL.EF
{
    public class MusicPortalContext: DbContext
    {
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<User> Users { get; set; }
        public MusicPortalContext(DbContextOptions<MusicPortalContext> options)
                   : base(options)
        {
            if (Database.EnsureCreated()) {
                Roles.Add(new Role { Description = "Заблокований користувач" });
                Roles.Add(new Role {  Description = "Неавторизований користувач" });
                Roles.Add(new Role {  Description = "Авторизований користувач" });
                Roles.Add(new Role {  Description = "Адміністратор" });

                Genres.Add(new Genre { Name = "Класична", Description = "Музика минулого, що витримала випробування часом та має аудиторію в сучасному суспільстві. Уже сьогодні як класичні сприймаються не тільки перлини «високого» музичного мистецтва, але й найкращі взірці розважальних жанрів минулого." });
                Genres.Add(new Genre { Name = "Поп-музика", Description = "Даний стиль відноситься до сучасного напряму музики. Цей жанр характеризується простотою, цікавою інструментальною частиною і почуттям ритму, при цьому вокалу приділяється далеко не найосновніша увага. Головною і практично єдиною формою музичних композицій є пісня." });
                Genres.Add(new Genre { Name = "Рок", Description = "Як видно з назви (rock - «качати»), даний жанр музики характеризується ритмічними відчуттями, які пов'язані з певним рухом. Деякі ознаки рок-композицій (електромузичні інструменти, творча самодостатність і ін.) відносяться до вторинних, через що багато стилів музики помилково відносять до року. З цим музичним напрямком пов'язані різні субкультури: панки, хіпі, металісти, емо, готи тощо." });
                Genres.Add(new Genre { Name = "Реп", Description = "Реп є ритмічним речитативом, який зазвичай читається під біт. Виконавцями таких композицій є репери або MC. Реп є однією з основних складових хіп-хопу. Але даний стиль використовується і в інших жанрах (драм-н-бейс, поп-музика, рок, репкор, нью-метал тощо).\r\n\r\nПоходження слова «Реп» засноване від англійського «rap» (удари, стуки) і «to rap» (говорити)." });
                Genres.Add(new Genre { Name = "R & В", Description = "R & B (ритм-н-блюз) відноситься до пісенно-танцювального жанру музики. В основі цього стилю лежать блюзові та джазові напрями першої половини ХХ століття. Відмінною особливістю жанру є танцювальні мотиви, які спонукають слухачів нестримно пуститися в танок. У стилі R & B переважають веселі мелодії, які не несуть в собі особливих філософських або розумових тематик.\r\n\r\nБагато музичних фахівців пов'язують ритм-н-блюз з чорношкірими людьми, оскільки в основі лежать всі «чорні» жанри, за винятком класичних і релігійних мотивів." });
                Genres.Add(new Genre { Name = "Джаз", Description = "Це музичний напрям, що виник в кінці XIX століття у США. Цей стиль музики поєднує в собі африканську і європейську культури.Відмінними рисами цього напрямку є імпровізація, витончений ритм (синкоповані фігури) і унікальні прийоми ритмічних фактур.Джаз також відноситься до танцювальної музики. Композиції є життєрадісними, додають бадьорості і гарний настрій. Але на відміну від R & B джазові мелодії є більш спокійними." });
                Genres.Add(new Genre { Name = "Інструментальна музика", Description = "Композиції цього напрямку музики виконуються за допомогою музичних інструментів, а людський голос в цьому не бере ніякої участі. ІМ буває сольною, ансамблевої та оркестрової. Інструментальна музика є одним з найкращих стилів «для фону». Мелодії, засновані на живих інструментах і сучасних хітах, ідеально підходять для спокійних радіостанцій, а їх прослуховування дарує гармонію під час роботи і відпочинку." });
                Genres.Add(new Genre { Name = "Електро", Description = "Електронна музика є досить широким жанром, мелодії якого створені за допомогою електронних музичних інструментів і комп'ютерних технологій. Такий стиль має різні напрямки, починаючи від експериментальних академічних пісень, закінчуючи популярними електронними танцювальними треками.В електронній музиці поєднуються звуки, утворені електронними технологіями і електромеханічними музичними інструментами (телармоніуму, органом Хаммонда, електрогітарою, терменвоксом і синтезатором)." });
                Genres.Add(new Genre { Name = "Народна музика", Description = "Досить популярним стилем є і народна музика, що відноситься до музичного фольклору. Композиції представляють собою музично-поетичні творчі ідеї народу, які передаються з покоління в покоління. Традиційні мелодії зазвичай створюються сільським населенням. Такий напрям музики є вагомим протиставленням популярному і академічного співу. В основі текстів лежать різні мотиви, починаючи від теплих любовних відносин, закінчуючи страшними і жахливими військовими подіями." });
                Genres.Add(new Genre { Name = "Транс", Description = "Транс є різновидом електронної музики, характерними ознаками якого є штучне звучання, приділення особливої уваги гармонійним партіям і тембрів, а також відносно швидкий темп (від 120 до 150 ударів на хвилину). Зазвичай транс використовується для проведення різних танцювальних заходів." });
                SaveChanges();
                Users.Add(new User { FirstName = "Адміністратор", LastName = "порталу", Login = "admin", Password = "C89DB7CD26A99131F872EA9544148E87", Salt = "F4D3FB2CA189AC1AA3BCFCC642D94D8C", Role = Roles.ElementAt(3) });
                Users.Add(new User { FirstName = "Користувач", LastName = "звичайний", Login = "user", Password = "944560C66F6A8525A921C6529D95B196", Salt = "D08B2D19140768EF03DB1A07E44905B9", Role = Roles.ElementAt(2) });
                SaveChanges();
                Songs.Add(new Song { Name = "Memory Reboot", GenreId = 8, Path = "/Files/MemoryReboot.mp3", Singers = "VOJ,Narvent" });
                SaveChanges();
            };
        }
    }
}
