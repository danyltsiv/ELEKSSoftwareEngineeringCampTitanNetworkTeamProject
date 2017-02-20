using System;
using System.Collections.Generic;
using WCFService.DataTranferObjects;
using BusinessLogicTier.Models;
using BusinessLogicTier.EntityConverters;

namespace WCFService.EntityConverters
{
    public class AccountDataConverter : IEntityConverter<AccountData, AccountDTO>
    {
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

        public IList<AccountData> ToBusinessEntityList(IList<AccountDTO> models)
        {
            throw new NotImplementedException();
        }

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

        public IList<AccountDTO> ToDataTransferEntityList(IList<AccountData> models)
        {
            throw new NotImplementedException();
        }
    }
}
