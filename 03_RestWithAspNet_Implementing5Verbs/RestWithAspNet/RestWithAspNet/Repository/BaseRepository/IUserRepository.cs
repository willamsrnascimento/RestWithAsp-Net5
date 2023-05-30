using RestWithAspNet.Data.VO;
using RestWithAspNet.Model;

namespace RestWithAspNet.Repository.BaseRepository
{
    public interface IUserRepository
    {
        User ValidateCredentials(UserVO userVO);
        User ValidateCredentials(string userName);
        bool RevokeToken(string userName);
        public User RefreshUserInfo(User user);
    }
}
