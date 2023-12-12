using Microsoft.EntityFrameworkCore;
using Music_Portal.BLL.DTO;
using Music_Portal.BLL.Infrastructure;
using Music_Portal.BLL.Interfaces;
using Music_Portal.DAL.Entities;
using Music_Portal.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Music_Portal.BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database;
        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task TryToRegister(RegisterUserDTO regDto)
        {
            //Перш ніж зареєструватись, треба перевірити, чи є такий логін в БД
            var BusyLoginUsersCollection = await Database.Users.GetByLogin(regDto.Login);
            if (BusyLoginUsersCollection.ToList().Any(x => x.Login == regDto.Login))
            {
                throw new ValidationException("Такий логін вже зайнято", "");
            }
            if (regDto.Password != regDto.PasswordConfirm)
            {
                throw new ValidationException("Паролі не співпадають", "");
            }
            var newUser = new User
            {
                Id = regDto.Id,
                FirstName = regDto.FirstName,
                LastName = regDto.LastName,
                Login = regDto.Login, 
                RoleId = 2
            };

            byte[] saltbuf = new byte[16];

            RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(saltbuf);

            StringBuilder sb = new StringBuilder(16);
            for (int i = 0; i < 16; i++)
                sb.Append(string.Format("{0:X2}", saltbuf[i]));
            string salt = sb.ToString();

            //переводим пароль в байт-массив  
            byte[] password = Encoding.Unicode.GetBytes(salt + regDto.Password);

            //создаем объект для получения средств шифрования  
            var md5 = MD5.Create();

            //вычисляем хеш-представление в байтах  
            byte[] byteHash = md5.ComputeHash(password);

            StringBuilder hash = new StringBuilder(byteHash.Length);
            for (int i = 0; i < byteHash.Length; i++)
                hash.Append(string.Format("{0:X2}", byteHash[i]));

            newUser.Password = hash.ToString();
            newUser.Salt = salt;
            await Database.Users.Create(newUser);
            await Database.Save();
        }
        public async Task<UserDTO> TryToLogin(LoginUserDTO loginDto)
        {
            // Якщо в БД немає користувачів, то показати неоднозначну помилку
            var UsersCollection = await Database.Users.GetAll();
            if (UsersCollection.ToList().Count == 0)
            {
                throw new ValidationException("Неправильний логін або пароль", "");
            }
            // Якщо в БД немає користувачів з подібним логіном, то показати неоднозначну помилку
            var SimilarLogins = await Database.Users.GetByLogin(loginDto.Login);
            if (SimilarLogins.ToList().Count == 0)
            {
                throw new ValidationException("Неправильний логін або пароль", "");
            }
            else
            {
                var MeaningUser = SimilarLogins.FirstOrDefault(x => x.Login == loginDto.Login);
                // Якщо в БД немає користувача з таким конкретним логіном, то показати неоднозначну помилку
                if (MeaningUser == null)
                {
                    throw new ValidationException("Неправильний логін або пароль", "");
                }
                else
                {
                    string? salt = MeaningUser.Salt;
                    //переводим пароль в байт-массив  
                    byte[] password = Encoding.Unicode.GetBytes(salt + loginDto.Password);
                    //создаем объект для получения средств шифрования  
                    var md5 = MD5.Create();
                    //вычисляем хеш-представление в байтах  
                    byte[] byteHash = md5.ComputeHash(password);

                    StringBuilder hash = new StringBuilder(byteHash.Length);
                    for (int i = 0; i < byteHash.Length; i++)
                        hash.Append(string.Format("{0:X2}", byteHash[i]));
                    // Якщо паролі не співпадають, то показати неоднозначну помилку
                    if (MeaningUser.Password != hash.ToString())
                    {
                        throw new ValidationException("Неправильний логін або пароль", "");
                    }
                    else 
                    {
                        // Якщо користувач правильно вказав пароль, але наразі заблокований, показати повідомлення про блокування
                        var BlockedUsersCollection = await Database.Users.GetBlockedUsers();
                        if (BlockedUsersCollection.Any(x => x.Login == MeaningUser.Login))
                        {
                            throw new ValidationException("Ваш акаунт був заблокований. Спробуйте створити новий акаунт", "");
                        }
                        // Якщо користувач правильно вказав пароль, але наразі неавторизований, показати повідомлення про розгляд заявки
                        var NonAuthorizedUsersCollection = await Database.Users.GetNotAuthorizedUsers();
                        if (NonAuthorizedUsersCollection.Any(x => x.Login == MeaningUser.Login))
                        {
                            throw new ValidationException("Ваша заявка на реєстрацію розглядається. Очікуйте схвалення або відмову", "");
                        }
                        // Якщо ми дійшли сюди - всі перевірки успішно пройдено, і користувач авторизований, його можна пропустити
                        // Для цього потрібно створити DTO, на основі даних про користувача, що були отримані з БД, і повернути як результат
                        return new UserDTO
                        {
                            Id = MeaningUser.Id,
                            FirstName = MeaningUser.FirstName,
                            LastName = MeaningUser.LastName,
                            Login = MeaningUser.Login,
                            RoleId = MeaningUser.RoleId
                        };
                    }                 

                }
                
            }
           
        }
        public async Task ActivateUser(UserDTO userDto)
        {
            var User = await Database.Users.Get(userDto.Id);
            if (User == null)
            {
                throw new ValidationException("Такого користувача не існує!", "");
            }
            else
            {
                User.RoleId = 3;
                await Database.Save();
            }
        }
        public async Task BlockUser(UserDTO userDto)
        {
            var User = await Database.Users.Get(userDto.Id);
            if (User == null)
            {
                throw new ValidationException("Такого користувача не існує!", "");
            }
            else
            {
                User.RoleId = 1;
                await Database.Save();
            }
        }
        public async Task UpdateUser(UserDTO userDto)
        {
            var User = await Database.Users.Get(userDto.Id);
            if (User == null)
            {
                throw new ValidationException("Такого користувача не існує!", "");
            }
            else
            {
                User.FirstName = userDto.FirstName; 
                User.LastName = userDto.LastName; 
                User.Login = userDto.Login;
                Database.Users.Update(User);
                await Database.Save();
            }
        }
        public async Task DeleteUser(int id)
        {
            var User = await Database.Users.Get(id);
            if (User == null)
            {
                throw new ValidationException("Такого користувача не існує!", "");
            }
            else
            {
                await Database.Users.Delete(id);
                await Database.Save();
            }
        }
        public async Task<UserDTO> GetUser(int id)
        {
            var User = await Database.Users.Get(id);
            if (User == null)
            {
                throw new ValidationException("Такого користувача не існує!", "");
            }
            else
            {
                return new UserDTO
                {
                    FirstName = User.FirstName,
                    LastName = User.LastName,
                    Login = User.Login,
                    Id = User.Id,
                    RoleId = User.RoleId
                };
            }
        }
        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()
            //.ForMember("RoleId", opt => opt.MapFrom(c => c.Role.Id))
            //.ForMember("Id",opt => opt.MapFrom(c=>c.Id))
            //.ForMember("FirstName",opt => opt.MapFrom(c=>c.FirstName))
            //.ForMember("LastName",opt => opt.MapFrom(c=>c.LastName))
            //.ForMember("Login",opt=>opt.MapFrom(c=>c.Login))
            );
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(await Database.Users.GetAll());
        }
        public async Task<IEnumerable<UserDTO>> GetUsersByLogin(string search)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()
            //.ForMember("RoleId", opt => opt.MapFrom(c => c.Role.Id))
            //.ForMember("Id", opt => opt.MapFrom(c => c.Id))
            //.ForMember("FirstName", opt => opt.MapFrom(c => c.FirstName))
            //.ForMember("LastName", opt => opt.MapFrom(c => c.LastName))
            //.ForMember("Login", opt => opt.MapFrom(c => c.Login))
            );
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(await Database.Users.GetByLogin(search));
        }
        public async Task<IEnumerable<UserDTO>> GetAuthorizedUsers()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()
                //.ForMember("RoleId", opt => opt.MapFrom(c => c.Role.Id))
                //.ForMember("Id", opt => opt.MapFrom(c => c.Id))
                //.ForMember("FirstName", opt => opt.MapFrom(c => c.FirstName))
                //.ForMember("LastName", opt => opt.MapFrom(c => c.LastName))
                //.ForMember("Login", opt => opt.MapFrom(c => c.Login))
                );
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(await Database.Users.GetAuthorizedUsers());
        }
        public async Task<IEnumerable<UserDTO>> GetNonAuthorizedUsers()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()
                //.ForMember("RoleId", opt => opt.MapFrom(c => c.Role.Id))
                //.ForMember("Id", opt => opt.MapFrom(c => c.Id))
                //.ForMember("FirstName", opt => opt.MapFrom(c => c.FirstName))
                //.ForMember("LastName", opt => opt.MapFrom(c => c.LastName))
                //.ForMember("Login", opt => opt.MapFrom(c => c.Login))
                );
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(await Database.Users.GetNotAuthorizedUsers());
        }
        public async Task<IEnumerable<UserDTO>> GetBlockedUsers()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()
                //.ForMember("RoleId", opt => opt.MapFrom(c => c.Role.Id))
                //.ForMember("Id", opt => opt.MapFrom(c => c.Id))
                //.ForMember("FirstName", opt => opt.MapFrom(c => c.FirstName))
                //.ForMember("LastName", opt => opt.MapFrom(c => c.LastName))
                //.ForMember("Login", opt => opt.MapFrom(c => c.Login))
                );
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(await Database.Users.GetBlockedUsers());
        }
    }
}
