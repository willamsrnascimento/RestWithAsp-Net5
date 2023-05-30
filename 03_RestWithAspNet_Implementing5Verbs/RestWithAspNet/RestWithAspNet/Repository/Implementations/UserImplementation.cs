using RestWithAspNet.Data.VO;
using RestWithAspNet.Model;
using RestWithAspNet.Model.Context;
using RestWithAspNet.Repository.BaseRepository;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace RestWithAspNet.Repository.Implementations
{
    public class UserImplementation : IUserRepository
    {
        private readonly MySqlContext _mySqlContext;

        public UserImplementation(MySqlContext mySqlContext)
        {
            _mySqlContext = mySqlContext;
        }

        public User ValidateCredentials(UserVO userVO)
        {
            var pass = ComputeHash(userVO.Password, new SHA256CryptoServiceProvider());
            return _mySqlContext.Users.FirstOrDefault(u => (u.UserName == userVO.UserName) && (u.Password == pass));
        }

        public User ValidateCredentials(string userName)
        {
            return _mySqlContext.Users.SingleOrDefault(u => (u.UserName == userName));
        }

        public bool RevokeToken(string userName)
        {
            var user = _mySqlContext.Users.SingleOrDefault(u => (u.UserName == userName));

            if (user == null)
            {
                return false;
            }

            user.RefreshToken = null;
            _mySqlContext.SaveChanges();
            return true;
        }

        public User RefreshUserInfo(User user)
        {
            if(!_mySqlContext.Users.Any(u => u.Id.Equals(user.Id)))
            {
                return null;
            }

            var result = _mySqlContext.Users.SingleOrDefault(u => u.Id.Equals(user.Id));

            if(result != null)
            {
                try
                {
                    _mySqlContext.Entry(result).CurrentValues.SetValues(user);
                    _mySqlContext.SaveChanges();
                }
                catch(Exception e)
                {
                    throw;
                }
            }

            return result;
        }

        private string ComputeHash(string password, SHA256CryptoServiceProvider sHA256CryptoServiceProvider)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(password);
            Byte[] hashedBytes = sHA256CryptoServiceProvider.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes);
        }

    }
}
