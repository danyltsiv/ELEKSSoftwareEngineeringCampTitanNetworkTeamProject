using System;
using System.Collections.Generic;
using WCFService.DataTranferObjects;
using BusinessLogicTier.Models;
using WCFService.EntityConverters;

namespace WCFService.EntityConverters
{
    /// <summary>
    /// Class AccountDataConverter.
    /// </summary>
    /// <seealso cref="WCFService.EntityConverters.IEntityConverter{BusinessLogicTier.Models.AccountData, WCFService.DataTranferObjects.AccountDTO}" />
    public class AccountDataConverter : IEntityConverter<AccountData, AccountDTO>
    {
        /// <summary>
        /// To the business entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>TBusinessEntity.</returns>
        public AccountData ToBusinessEntity(AccountDTO model)
        {
            var business = new AccountData
            {
                UserName = model.UserName,
                Password = model.Password,
                ConfirmPassword = model.ConfirmPassword
            };

            return business;
        }

        /// <summary>
        /// To the business entity list.
        /// </summary>
        /// <param name="models">The models.</param>
        /// <returns>IList&lt;TBusinessEntity&gt;.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IList<AccountData> ToBusinessEntityList(IList<AccountDTO> models)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// To the data transfer entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>TDataTransferEntity.</returns>
        public AccountDTO ToDataTransferEntity(AccountData model)
        {
            var dto = new AccountDTO
            {
                UserName = model.UserName,
                Password = model.Password,
                ConfirmPassword = model.ConfirmPassword
            };

            return dto;
        }

        /// <summary>
        /// To the data transfer entity list.
        /// </summary>
        /// <param name="models">The models.</param>
        /// <returns>IList&lt;TDataTransferEntity&gt;.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IList<AccountDTO> ToDataTransferEntityList(IList<AccountData> models)
        {
            throw new NotImplementedException();
        }
    }
}
