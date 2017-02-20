using BusinessLogicTier.DataAccesLayer.Entities;
using System.Collections.Generic;
using BusinessLogicTier.DataTranferObjects;

namespace BusinessLogicTier.EntityConverters
{
    public class UserConverter : IEntityConverter<User, UserInfoDTO>
    {
        public User ToBusinessEntity(UserInfoDTO model)
        {
            var business = new User();
            business.Id = model.Id;
            business.FirstName = model.FirstName;
            business.MidleName = model.MidleName;
            business.LastName = model.LastName;
            business.About = model.About;
            business.Age = model.Age;
            business.UserName = model.UserName;
            return business;
        }

        public IList<User> ToBusinessEntityList(IList<UserInfoDTO> models)
        {
            var list = new List<User>();
            foreach (var model in models)
                list.Add(ToBusinessEntity(model));
            return list;
        }

        public UserInfoDTO ToDataTransferEntity(User model)
        {
            var user = new UserInfoDTO();
            user.Id = model.Id;
            user.FirstName = model.FirstName;
            user.MidleName = model.MidleName;
            user.LastName = model.LastName;
            user.About = model.About;
            user.Age = model.Age;
            user.UserName = model.UserName;
            return user;
        }

        public IList<UserInfoDTO> ToDataTransferEntityList(IList<User> models)
        {
            var list = new List<UserInfoDTO>();
            foreach (var model in models)
                list.Add(ToDataTransferEntity(model));
            return list;
        }
    }
}