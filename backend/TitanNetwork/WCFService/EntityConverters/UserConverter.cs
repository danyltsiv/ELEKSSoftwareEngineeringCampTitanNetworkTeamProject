using BusinessLogicTier.DataAccesLayer.Entities;
using System.Collections.Generic;
using WCFService.DataTranferObjects;

namespace WCFService.EntityConverters
{
    /// <summary>
    /// Class UserConverter.
    /// </summary>
    /// <seealso cref="WCFService.EntityConverters.IEntityConverter{BusinessLogicTier.DataAccesLayer.Entities.User, WCFService.DataTranferObjects.UserInfoDTO}" />
    public class UserConverter : IEntityConverter<User, UserInfoDTO>
    {
        /// <summary>
        /// To the business entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>TBusinessEntity.</returns>
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

        /// <summary>
        /// To the business entity list.
        /// </summary>
        /// <param name="models">The models.</param>
        /// <returns>IList&lt;TBusinessEntity&gt;.</returns>
        public IList<User> ToBusinessEntityList(IList<UserInfoDTO> models)
        {
            var list = new List<User>();
            foreach (var model in models)
                list.Add(ToBusinessEntity(model));
            return list;
        }

        /// <summary>
        /// To the data transfer entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>TDataTransferEntity.</returns>
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

        /// <summary>
        /// To the data transfer entity list.
        /// </summary>
        /// <param name="models">The models.</param>
        /// <returns>IList&lt;TDataTransferEntity&gt;.</returns>
        public IList<UserInfoDTO> ToDataTransferEntityList(IList<User> models)
        {
            var list = new List<UserInfoDTO>();
            foreach (var model in models)
                list.Add(ToDataTransferEntity(model));
            return list;
        }
    }
}